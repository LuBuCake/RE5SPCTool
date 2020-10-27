using System;
using System.IO;

namespace FWSEConverter
{
    // Format lookup: http://soundfile.sapp.org/doc/WaveFormat/

    public class WAVEReader
    {
        FileStream FS;
        BinaryReader BR;

        // RIFF Chunk
        public string ChunkID;          // 4 Bytes raw string 'RIFF'
        public uint ChunkSize;          // unasigned int, should equal to total filelength - 8
        public string Format;           // 4 Bytes raw string 'WAVE'

        // FMT sub-chunk
        public string Subchunck1ID;     // 4 Bytes raw string 'fmt '
        public uint Subchunk1Size;      // 4 Bytes 16 for PCM, This is the size of the rest of the Subchunk which follows this number.
        public ushort AudioFormat;      // 2 Bytes 1 for PCM, other values means other type of compression
        public ushort NumChannels;      // 2 Bytes Mono = 1, Stereo = 2, etc...
        public uint SampleRate;         // 4 Bytes 8000, 44100, etc...
        public uint ByteRate;           // 4 Bytes SampleRate * NumChannels * BitsPerSample / 8
        public ushort BlockAlign;       // 2 Bytes NumChannels * BitsPerSample / 8
        public ushort BitsPerSample;    // 2 Bytes 8 bits = 8, 16 bits = 16, etc...

        // DATA sub-chunk
        public string Subchunck2ID;     // 4 Bytes raw string 'data'
        public uint Subchunk2Size;      // 4 Bytes NumSamples * NumChannels * BitsPerSample / 8
        public int[][] Subchunk2Data;   // Var array containing the raw sample data

        public long Samples;

        public WAVEReader(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Open);
            BR = new BinaryReader(FS);

            // RIFF Reading

            for (int i = 0; i < 4; i++)
            {
                ChunkID = ChunkID + (char)BR.ReadByte();
            }

            ChunkSize = BR.ReadUInt32();

            for (int i = 0; i < 4; i++)
            {
                Format = Format + (char)BR.ReadByte();
            }

            // FMT Reading

            for (int i = 0; i < 4; i++)
            {
                Subchunck1ID = Subchunck1ID + (char)BR.ReadByte();
            }

            Subchunk1Size = BR.ReadUInt32();
            AudioFormat = BR.ReadUInt16();
            NumChannels = BR.ReadUInt16();
            SampleRate = BR.ReadUInt32();
            ByteRate = BR.ReadUInt32();
            BlockAlign = BR.ReadUInt16();
            BitsPerSample = BR.ReadUInt16();

            // DATA Reading

            for (int i = 0; i < 4; i++)
            {
                Subchunck2ID = Subchunck2ID + (char)BR.ReadByte();
            }

            Subchunk2Size = BR.ReadUInt32();

            // Getting Samples

            Samples = (Subchunk2Size / NumChannels) / (BitsPerSample / 8);

            Subchunk2Data = new int[NumChannels][];

            for (int i = 0; i < NumChannels; i++)
            {
                Subchunk2Data[i] = new int[Samples];
            }

            for (int SampleIndex = 0; SampleIndex < Samples; SampleIndex++)
            {
                for (int Channels = 0; Channels < NumChannels; Channels++)
                {
                    if (BitsPerSample == 8)
                    {
                        Subchunk2Data[Channels][SampleIndex] = BR.ReadByte();
                    }
                    else if (BitsPerSample == 16)
                    {
                        Subchunk2Data[Channels][SampleIndex] = BR.ReadInt16();
                    }
                    else if (BitsPerSample == 32)
                    {
                        Subchunk2Data[Channels][SampleIndex] = BR.ReadInt32();
                    }
                }
            }

            FS.Dispose();
            BR.Dispose();
        }
    }

    public class WAVEWriter
    {
        FileStream FS;
        BinaryWriter BW;

        // RIFF Chunk
        public string ChunkID = "RIFF";
        public uint ChunkSize;
        public string Format = "WAVE";

        // FMT sub-chunk
        public string Subchunck1ID = "fmt ";
        public uint Subchunk1Size = 16;
        public ushort AudioFormat = 1;
        public ushort NumChannels;
        public uint SampleRate;
        public uint ByteRate;
        public ushort BlockAlign;
        public ushort BitsPerSample;

        // DATA sub-chunk
        public string Subchunck2ID = "data";
        public uint Subchunk2Size;
        public int[][] Subchunk2Data;

        public long Samples;


        public WAVEWriter(string FilePath, ushort ChannelQuantity, uint SampleFrequency, ushort BitSample, long SampleQuantity, int[][] SoundData)
        {
            FS = new FileStream(FilePath, FileMode.Create);
            BW = new BinaryWriter(FS);

            NumChannels = ChannelQuantity;
            SampleRate = SampleFrequency;
            BitsPerSample = BitSample;
            Samples = SampleQuantity;

            ByteRate = SampleRate * NumChannels * BitsPerSample / 8;
            BlockAlign = (ushort)(NumChannels * BitsPerSample / 8);

            Subchunk2Data = SoundData;
            Subchunk2Size = (uint)Samples * NumChannels * BitsPerSample / 8;

            ChunkSize = 4 + (8 + Subchunk1Size) + (8 + Subchunk2Size);

            // RIFF Chunk
            BW.Write(ChunkID.ToCharArray());
            BW.Write(BitConverter.GetBytes(ChunkSize));
            BW.Write(Format.ToCharArray());

            // FMT sub-chunk
            BW.Write(Subchunck1ID.ToCharArray());
            BW.Write(BitConverter.GetBytes(Subchunk1Size));
            BW.Write(BitConverter.GetBytes(AudioFormat));
            BW.Write(BitConverter.GetBytes(NumChannels));
            BW.Write(BitConverter.GetBytes(SampleRate));
            BW.Write(BitConverter.GetBytes(ByteRate));
            BW.Write(BitConverter.GetBytes(BlockAlign));
            BW.Write(BitConverter.GetBytes(BitsPerSample));

            // DATA sub-chunk
            BW.Write(Subchunck2ID.ToCharArray());
            BW.Write(BitConverter.GetBytes(Subchunk2Size));

            for (int SampleIndex = 0; SampleIndex < Samples; SampleIndex++)
            {
                for (int Channels = 0; Channels < NumChannels; Channels++)
                {
                    if (BitsPerSample == 8)
                    {
                        BW.Write(BitConverter.GetBytes((byte)SoundData[Channels][SampleIndex]));
                    }
                    else if (BitsPerSample == 16)
                    {
                        BW.Write(BitConverter.GetBytes((short)SoundData[Channels][SampleIndex]));
                    }
                    else if (BitsPerSample == 32)
                    {
                        BW.Write(BitConverter.GetBytes(SoundData[Channels][SampleIndex]));
                    }
                }
            }

            FS.Dispose();
            BW.Dispose();
        }
    }
}
