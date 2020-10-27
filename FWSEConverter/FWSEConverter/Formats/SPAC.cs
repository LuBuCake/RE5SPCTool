using System.IO;

namespace FWSEConverter
{
    public class SPAC
    {
        FileStream FS;
        BinaryReader BR;

        // HEADER Buffer = 0x20 (32)

        public string Format;
        public int Version;
        public int NumSounds;
        public int DataUnknow1;
        public int DataUnknow2;
        public int Meta1Start;
        public int Meta2Start;
        public int SoundDataStart;

        public int MetaSize; // SoundDataStart - ((1024 * NumSounds) + 32)
        public byte[] Meta;

        public int FWSEBufferSize = 1024;

        public int[] FWSESoundSize;
        public byte[][] FWSEBufferData;
        public byte[][] FWSESoundData;

        public long SavePosition;

        public SPAC(string FilePath)
        {
            LoadSPAC(FilePath);
        }

        private void LoadSPAC(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Open);
            BR = new BinaryReader(FS);

            // HEADER

            for (int i = 0; i < 4; i++)
            {
                Format = Format + (char)BR.ReadByte();
            }

            Version = BR.ReadInt32();
            NumSounds = BR.ReadInt32();
            DataUnknow1 = BR.ReadInt32();
            DataUnknow2 = BR.ReadInt32();
            Meta1Start = BR.ReadInt32();
            Meta2Start = BR.ReadInt32();
            SoundDataStart = BR.ReadInt32();

            SavePosition = BR.BaseStream.Position;

            // DATA

            FWSESoundSize = new int[NumSounds];
            FWSEBufferData = new byte[NumSounds][];
            FWSESoundData = new byte[NumSounds][];

            BR.BaseStream.Position += 8;

            for (int i = 0; i < NumSounds; i++)
            {
                FWSESoundSize[i] = BR.ReadInt32();
                FWSESoundSize[i] -= 1024;
                BR.BaseStream.Position -= 4;
                BR.BaseStream.Position += 1024;
            }

            BR.BaseStream.Position = SavePosition;

            for (int i = 0; i < NumSounds; i++)
            {
                FWSEBufferData[i] = new byte[FWSEBufferSize];
                FWSEBufferData[i] = BR.ReadBytes(FWSEBufferSize);
            }

            MetaSize = SoundDataStart - ((FWSEBufferSize * NumSounds) + 32);

            Meta = new byte[MetaSize];
            Meta = BR.ReadBytes(MetaSize);

            for (int i = 0; i < NumSounds; i++)
            {
                FWSESoundData[i] = new byte[FWSESoundSize[i]];
                FWSESoundData[i] = BR.ReadBytes(FWSESoundSize[i]);
            }

            FS.Dispose();
            BR.Dispose();
        }

        public void SaveSPAC(string FilePath)
        {
            FS = new FileStream(FilePath, FileMode.Create);
            BinaryWriter BW = new BinaryWriter(FS);

            // HEADER

            BW.Write(Format.ToCharArray());
            BW.Write(Version);
            BW.Write(NumSounds);
            BW.Write(DataUnknow1);
            BW.Write(DataUnknow2);
            BW.Write(Meta1Start);
            BW.Write(Meta2Start);
            BW.Write(SoundDataStart);

            for (int i = 0; i < NumSounds; i++)
            {
                BW.Write(FWSEBufferData[i]);
            }

            BW.Write(Meta);

            for (int i = 0; i < NumSounds; i++)
            {
                BW.Write(FWSESoundData[i]);
            }

            FS.Dispose();
            BW.Dispose();
        }

        public bool CheckSPAC()
        {
            return Format == "SPAC" && Version == 4;
        }

        public void ReplaceFWSE(int Index, byte[] Buffer, byte[] SoundData)
        {
            FWSEBufferData[Index] = Buffer;
            FWSESoundData[Index] = SoundData;
        }

        public void ExtractFWSE(string Directory, bool ConvertToWAV = false)
        {
            FWSEWriter FwseW;
            WAVEWriter WaveW;

            int[][] Converted = new int[1][];

            string filename;

            for (int i = 0; i < NumSounds; i++)
            {
                filename = Directory + "/" + i.ToString();

                if (ConvertToWAV)
                {
                    Converted[0] = new int[FWSESoundData[i].Length * 2];
                    Converted[0] = FWSECodec.DecodeMTF_IMA(FWSESoundData[i]);

                    filename += ".wav";
                    WaveW = new WAVEWriter(filename, 1, 48000, 16, FWSESoundData[i].Length * 2, Converted);
                }
                else
                {
                    filename += ".FWSE";
                    FwseW = new FWSEWriter(filename, FWSEBufferData[i], FWSESoundData[i]);
                }
            }
        }
    }
}
