using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWSEConverter
{
    public static class FWSECodec
    {
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
            32767,

            0
        };

        public static int[] ADPCM_IndexTable = { -1, -1, -1, -1, 2, 4, 6, 8, -1, -1, -1, -1, 2, 4, 6, 8 };

        public static int[] CAPCOM_IndexTable = { 8, 6, 4, 2, -1, -1, -1, -1, -1, -1, -1, -1, 2, 4, 6, 8 };

        private static int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }

        public static void MTF_IMA_ExpandNible(byte[] FWSEData, int Offset, int nibble_shift, ref int sample_decoded_last, ref int step_index)
        {
            int sample_nibble, sample_decoded, step, delta;

            sample_nibble = (FWSEData[Offset] >> nibble_shift) & 0xF;
            sample_decoded = sample_decoded_last;
            step = ADPCMTable[step_index];

            delta = step * (2 * sample_nibble - 15);
            sample_decoded += delta;

            sample_decoded_last = sample_decoded;
            step_index += CAPCOM_IndexTable[sample_nibble];

            if (step_index < 0) step_index = 0;
            if (step_index > 88) step_index = 88;
        }

        public static int[] DecodeMTF_IMA(byte[] FWSEData)
        {
            int sample_count = FWSEData.Length * 2;
            int sample_decoded_last = 0;
            int step_index = 0;

            int[] ResultA = new int[FWSEData.Length];
            int[] ResultB = new int[FWSEData.Length];
            int[] Result = new int[sample_count];

            for (int i = 0; i < FWSEData.Length; i++)
            {
                MTF_IMA_ExpandNible(FWSEData, i, 4, ref sample_decoded_last, ref step_index);
                ResultA[i] = Clamp(sample_decoded_last >> 4, -32768, 32767);

                MTF_IMA_ExpandNible(FWSEData, i, 0, ref sample_decoded_last, ref step_index);
                ResultB[i] = Clamp(sample_decoded_last >> 4, -32768, 32767);
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

        public static void MTF_IMA_SimplifyNible(int[] WAVEData, int offset, ref int sample_encoded, ref int sample_predicted, ref int step_index)
        {
            int sample_original, step, diff;
            int bit0, bit1, bit2, bit3;

            sample_original = WAVEData[offset];
            diff = sample_original - sample_predicted;

            step = ADPCMTable[step_index];

            if (diff >= 0)
                bit3 = 0;
            else
            {
                bit3 = 1;
                diff = -diff;
            }

            if (diff >= step)
            {
                bit2 = 1;
                diff = diff - step;
            }
            else
            {
                bit2 = 0;
            }

            if (diff >= (step / 2))
            {
                bit1 = 1;
                diff = diff - (step / 2);
            }
            else
            {
                bit1 = 0;
            }

            if (diff >= (step / 4))
            {
                bit0 = 1;
            }
            else
            {
                bit0 = 0;
            }

            sample_encoded = Convert.ToInt32(bit3.ToString() + bit2.ToString() + bit1.ToString() + bit0.ToString(), 2);
            sample_predicted = sample_original;

            step_index += ADPCM_IndexTable[sample_encoded];

            if (step_index < 0) step_index = 0;
            else if (step_index > 88) step_index = 88;
        }

        public static int[] EncodeMTF_IMA(int[] WAVEData)
        {
            int[] Result = new int[WAVEData.Length / 2];
            string[] StorageA = new string[WAVEData.Length];
            string[] StorageB = new string[WAVEData.Length];

            int sample_encoded = 0;
            int sample_predicted = 0;
            int step_index = 0;

            for (int i = 0; i < WAVEData.Length; i++)
            {
                MTF_IMA_SimplifyNible(WAVEData, i, ref sample_encoded, ref sample_predicted, ref step_index);

                if (i % 2 == 0)
                    StorageA[i] = (sample_encoded).ToString("X");
                else
                    StorageB[i] = (sample_encoded).ToString("X");
            }

            string bytetemp = "";

            for (int i = 0; i < WAVEData.Length; i++)
            {
                if (i % 2 == 0)
                {
                    bytetemp += StorageA[i];
                }
                else
                {
                    bytetemp += StorageB[i] + ",";
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

            /*
            SoundData = FWSECodec.EncodeMTF_IMA(WAVAData);

            for (int i = 0; i < SoundData.Length; i++)
            {
                BW.Write((byte)SoundData[i]);
            }
            */

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
