using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWSEConverter
{
    public class FWSECodec
    {
        public int[] ADPCMTable = {
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

        public int[] CIMAADPCM_INDEX_TABLE = { 8, 6, 4, 2, -1, -1, -1, -1, -1, -1, -1, -1, 2, 4, 6, 8 };

        private int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }

        public void MTF_IMA_ExpandNible(ref byte[] FWSEData, int Offset, int nibble_shift, ref int sample_decoded_last, ref int step_index)
        {
            int sample_nibble, sample_decoded, step, delta;

            sample_nibble = (FWSEData[Offset] >> nibble_shift) & 0xF;
            sample_decoded = sample_decoded_last;
            step = ADPCMTable[step_index];

            delta = step * (2 * sample_nibble - 15);
            sample_decoded += delta;

            sample_decoded_last = sample_decoded;
            step_index += CIMAADPCM_INDEX_TABLE[sample_nibble];
            if (step_index < 0) step_index = 0;
            if (step_index > 88) step_index = 88;
        }

        public int[] DecodeMTF_IMA(byte[] FWSEData)
        {
            int sample_count = FWSEData.Length * 2;
            int sample_decoded_last = 0;
            int step_index = 0;

            int nibble_shift = 0;

            if (step_index < 0) step_index = 0;
            if (step_index > 88) step_index = 88;

            int[] Result = new int[sample_count];

            for (int i = 0; i < FWSEData.Length; i++)
            {
                nibble_shift = 0;

                MTF_IMA_ExpandNible(ref FWSEData, i, nibble_shift, ref sample_decoded_last, ref step_index);
                Result[i] = Clamp(sample_decoded_last >> 4, -32767, 32767);

                nibble_shift = 4;

                MTF_IMA_ExpandNible(ref FWSEData, i, nibble_shift, ref sample_decoded_last, ref step_index);
                Result[i+1] = Clamp(sample_decoded_last >> 4, -32767, 32767);
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

        // FWSE Specific

        public string Format;
        public int Version;
        public int DataSize;
        public int Buffer;
        public int Channels;

        public int SampleCount;
        public int SampleRate;
        public int BitsPerSample;

        public byte[] Garbage;

        public byte[] SoundData;

        public FWSEReader(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Open);
            BR = new BinaryReader(FS);

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

            Garbage = BR.ReadBytes(992); // Buffer (1024) - Header (32)

            SoundData = BR.ReadBytes((int)FS.Length - Buffer);

            BR.Dispose();
        }

        public string HeaderString()
        {
            string result = "Format: " + Format + Environment.NewLine +
            "Version: " + Version.ToString() + Environment.NewLine +
            "Data Size: " + DataSize + Environment.NewLine +
            "Buffer: " + Buffer + Environment.NewLine +
            "Channels: " + Channels + Environment.NewLine +
            "Samples: " + SampleCount + Environment.NewLine +
            "Rate: " + SampleRate + Environment.NewLine +
            "Bit: " + BitsPerSample + Environment.NewLine +
            "Sound Bytes: " + SoundData.Length.ToString();

            return result;
        }
    }

    public class FWSEWriter
    {
        FileStream FS;
        BinaryWriter BW;

        public FWSEWriter(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Create);
            BW = new BinaryWriter(FS);


        }
    }
}
