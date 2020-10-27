using System;
using System.IO;

namespace FWSEConverter
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

        public static int[] ADPCM_IndexTable = { -1, -1, -1, -1, 2, 4, 6, 8, -1, -1, -1, -1, 2, 4, 6, 8 };

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
            int sample_count = FWSEData.Length * 2;
            int sample_decoded_last = 0;
            int step_index = 0;
            int sample = 0;

            int[] ResultA = new int[FWSEData.Length];
            int[] ResultB = new int[FWSEData.Length];
            int[] Result = new int[sample_count];

            for (int i = 0; i < FWSEData.Length; i++)
            {
                sample = IMA_MTF_ExpandNibble(FWSEData[i], 4, ref sample_decoded_last, ref step_index);
                ResultA[i] = sample;

                sample = IMA_MTF_ExpandNibble(FWSEData[i], 0, ref sample_decoded_last, ref step_index);
                ResultB[i] = sample;
            }

            int A = 0;
            int B = 0;

            for (int i = 0; i < sample_count; i++)
            {
                if (i % 2 == 0)
                {
                    Result[i] = ResultA[A];
                    A++;
                }
                else
                {
                    Result[i] = ResultB[B];
                    B++;
                }
            }

            return Result;
        }

        // Encode

        public static int MTF_IMA_SimplifyNible(int sample, ref int sample_predicted, ref int step_index)
        {
            int diff, step, nibble;

            diff = (sample << 4) - sample_predicted;
            step = ADPCMTable[step_index];

            nibble = Clamp((int)Math.Round(diff / 2.0 / step) + 8, 0, 15);

            sample_predicted += step * (2 * nibble - 15);

            step_index += CAPCOM_IndexTable[nibble];
            step_index = Clamp(step_index, 0, 88);

            return nibble;
        }

        public static int[] EncodeMTF_IMA(int[] WAVEData)
        {
            int[] Result = new int[WAVEData.Length / 2];
            string[] Storage = new string[WAVEData.Length];

            int sample_predicted = 0;
            int sample_encoded = 0;
            int step_index = 0;

            for (int i = 0; i < WAVEData.Length; i++)
            {
                sample_encoded = MTF_IMA_SimplifyNible(WAVEData[i], ref sample_predicted, ref step_index);
                Storage[i] = sample_encoded.ToString("X");
            }

            string bytetemp = "";

            for (int i = 0; i < WAVEData.Length; i++)
            {
                if (i % 2 == 0)
                {
                    bytetemp += Storage[i];
                }
                else
                {
                    bytetemp += Storage[i] + ",";
                }
            }

            string[] organizedbytes = bytetemp.Split(',');

            for (int i = 0; i < organizedbytes.Length - 1; i++)
            {
                Result[i] = Convert.ToInt32(organizedbytes[i], 16);
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
            BW.Write(BitConverter.GetBytes(Version));
            BW.Write(BitConverter.GetBytes(DataSize));
            BW.Write(BitConverter.GetBytes(Buffer));
            BW.Write(BitConverter.GetBytes(Channels));

            BW.Write(BitConverter.GetBytes(SampleCount));
            BW.Write(BitConverter.GetBytes(SampleRate));
            BW.Write(BitConverter.GetBytes(BitsPerSample));

            // UnknowData

            for (int i = 0; i < 992; i++)
            {
                BW.Write((byte)0xFF);
            }

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
