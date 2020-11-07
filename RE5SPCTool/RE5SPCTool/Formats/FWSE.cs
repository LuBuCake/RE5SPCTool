/*
This file is part of RESIDENT EVIL 5 FWSE/SPC Tool.

RESIDENT EVIL 5 FWSE/SPC Tool is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License
as published by the Free Software Foundation, either version 3 of
the License, or (at your option) any later version.

RESIDENT EVIL 5 FWSE/SPC Tool is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with RESIDENT EVIL 5 FWSE/SPC Tool. If not, see <https://www.gnu.org/licenses/>6.
*/

using System.IO;

namespace RE5SPCTool
{
    public static class FWSECodec
    {
        // ADPCM Tables

        public static int[] ADPCMTable = {
            7, 8, 9, 10, 11, 12, 13, 14,
            16, 17, 19, 21, 23, 25, 28, 31,
            34, 37, 41, 45, 50, 55, 60, 66,
            73, 80, 88, 97, 107, 118, 130, 143,
            157, 173, 190, 209, 230, 253, 279, 307,
            337, 371, 408, 449, 494, 544, 598, 658,
            724, 796, 876, 963, 1060, 1166, 1282, 1411,
            1552, 1707, 1878, 2066, 2272, 2499, 2749, 3024,
            3327, 3660, 4026, 4428, 4871, 5358, 5894, 6484,
            7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899,
            15289, 16818, 18500, 20350, 22385, 24623, 27086, 29794,
            32767
        };

        public static int[] CAPCOM_IndexTable = { 8, 6, 4, 2, -1, -1, -1, -1, -1, -1, -1, -1, 2, 4, 6, 8 };

        // Utils

        private static int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }

        // Decode

        private static int IMA_MTF_ExpandNibble(int nibble, int shift, ref int sample_decoded_last, ref int step_index)
        {
            int step, delta, sample;

            nibble = nibble >> shift & 0xF;

            step = ADPCMTable[step_index];
            sample = sample_decoded_last;

            delta = step * (2 * nibble - 15);

            sample += delta;
            sample_decoded_last = sample;

            step_index += CAPCOM_IndexTable[nibble];
            step_index = Clamp(step_index, 0, 88);

            return Clamp(sample >> 4, -32768, 32767);
        }

        public static int[] DecodeMTF_IMA(byte[] FWSEData)
        {
            int[] Result = new int[FWSEData.Length * 2];

            int sample_decoded_last = 0;
            int result_index = 0;
            int step_index = 0;
            int sample;

            for (int i = 0; i < FWSEData.Length; i++)
            {
                sample = IMA_MTF_ExpandNibble(FWSEData[i], 4, ref sample_decoded_last, ref step_index);
                Result[result_index] = sample; result_index++;

                sample = IMA_MTF_ExpandNibble(FWSEData[i], 0, ref sample_decoded_last, ref step_index);
                Result[result_index] = sample; result_index++;
            }

            return Result;
        }

        // Encode

        public static int MTF_IMA_SimplifyNible(int sample, ref int sample_predicted, ref int step_index)
        {
            int diff, step, nibble;

            diff = (sample << 4) - sample_predicted;
            step = ADPCMTable[step_index];

            nibble = Clamp((diff / step / 2) + 8, 0, 15);

            sample_predicted += step * (2 * nibble - 15);

            step_index += CAPCOM_IndexTable[nibble];
            step_index = Clamp(step_index, 0, 88);

            return nibble;
        }

        public static int[] EncodeMTF_IMA(int[] WAVEData)
        {
            int[] Result = new int[WAVEData.Length / 2];

            int sample_predicted = 0;
            int sample_encoded = 0;
            int step_index = 0;

            int nibble_left = 0;
            int nibble_right = 0;
            int nibble_counter = 0;

            for (int i = 0; i < WAVEData.Length; i++)
            {
                sample_encoded = MTF_IMA_SimplifyNible(WAVEData[i], ref sample_predicted, ref step_index);

                if (i % 2 == 0)
                    nibble_left = sample_encoded;
                else
                {
                    nibble_right = sample_encoded;
                    Result[nibble_counter] = (nibble_left << 4) | nibble_right;
                    nibble_counter++;
                }
            }

            return Result;
        }
    }

    public class FWSEReader
    {
        FileStream FS;
        BinaryReader BR;

        public int FileLength;
        public byte[] AllData;

        // FWSE Header
        public string Format;
        public int Version;
        public int DataSize; // FileStream length
        public int Buffer;
        public int Channels;

        public int SampleCount;
        public int SampleRate;
        public int BitsPerSample;

        public byte[] UnknowData;

        public byte[] WholeBuffer;

        // DATA

        public byte[] SoundData;
        public int[][] ConvertedSoundData;

        public FWSEReader(string FilePath)
        {
            ReadFWSE(FilePath);
        }

        private void ReadFWSE(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Open);
            BR = new BinaryReader(FS);

            WholeBuffer = new byte[1024];
            WholeBuffer = BR.ReadBytes(1024);

            BR.BaseStream.Position = 0;

            for (int i = 0; i < 4; i++)
            {
                Format = Format + (char)BR.ReadByte();
            }

            Version = BR.ReadInt32();
            DataSize = BR.ReadInt32();
            Buffer = BR.ReadInt32();
            Channels = BR.ReadInt32();

            SampleCount = BR.ReadInt32();
            SampleRate = BR.ReadInt32();
            BitsPerSample = BR.ReadInt32();

            UnknowData = BR.ReadBytes(992); // Buffer (1024) - Header (32)

            SoundData = BR.ReadBytes((int)FS.Length - Buffer);

            BR.Dispose();

            ConvertedSoundData = new int[Channels][];

            for (int i = 0; i < Channels; i++)
            {
                ConvertedSoundData[i] = FWSECodec.DecodeMTF_IMA(SoundData);
            }
        }

        public bool FWSECheck()
        {
            return Format == "FWSE" && Version == 2;
        }
    }

    public class FWSEWriter
    {
        FileStream FS;
        BinaryWriter BW;

        // FWSE Header
        public string Format = "FWSE";
        public int Version = 2;
        public int DataSize;
        public int Buffer = 1024;
        public int Channels = 1;

        public int SampleCount;
        public int SampleRate = 48000;
        public int BitsPerSample = 16;

        public int[] SoundData;

        public FWSEWriter(string FilePath, int[] WAVEData)
        {
            WriteNewFWSE(FilePath, WAVEData);
        }

        public FWSEWriter(string FilePath, byte[] FWSEBufferData, byte[] FWSESoundData)
        {
            WriteExistentFWSE(FilePath, FWSEBufferData, FWSESoundData);
        }

        // Write from WAV

        public void WriteNewFWSE(string FilePath, int[] WAVEData)
        {
            FS = new FileStream(FilePath, FileMode.Create);
            BW = new BinaryWriter(FS);

            SampleCount = WAVEData.Length;
            DataSize = 1024 + (SampleCount / 2);

            // FWSE Header

            BW.Write(Format.ToCharArray());
            BW.Write(Version);
            BW.Write(DataSize);
            BW.Write(Buffer);
            BW.Write(Channels);

            BW.Write(SampleCount);
            BW.Write(SampleRate);
            BW.Write(BitsPerSample);

            // UnknowData

            for (int i = 0; i < 8; i++)
            {
                BW.Write((byte)0xFF);
            }

            for (int i = 0; i < 4; i++)
            {
                BW.Write((byte)0x00);
            }

            for (int i = 0; i < 20; i++)
            {
                BW.Write((byte)0xCC);
            }

            for (int i = 0; i < 4; i++)
            {
                BW.Write((byte)0x00);
            }

            for (int i = 0; i < 24; i++)
            {
                BW.Write((byte)0xCC);
            }

            for (int i = 0; i < 124; i++)
            {
                BW.Write((byte)0x00);
            }

            for (int i = 0; i < 640; i++)
            {
                BW.Write((byte)0xCC);
            }

            for (int i = 0; i < 168; i++)
            {
                BW.Write((byte)0x00);
            }

            /*
            for (int i = 0; i < 992; i++)
            {
                BW.Write((byte)0xFF);
            }
            */

            // SoundData
            SoundData = FWSECodec.EncodeMTF_IMA(WAVEData);

            for (int i = 0; i < SoundData.Length; i++)
            {
                BW.Write((byte)SoundData[i]);
            }

            FS.Dispose();
            BW.Dispose();
        }

        public void WriteExistentFWSE(string FilePath, byte[] FWSEBufferData, byte[] FWSESoundData)
        {
            FS = new FileStream(FilePath, FileMode.Create);
            BW = new BinaryWriter(FS);

            BW.Write(FWSEBufferData);
            BW.Write(FWSESoundData);

            FS.Dispose();
            BW.Dispose();
        }
    }
}
