using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FWSEConverter
{
    public partial class Main : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        // Main
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //AllocConsole();
        }

        // Methods

        private void TestButton_Click(object sender, EventArgs e)
        {
            FWSEReader file;
            FWSECodec Codec = new FWSECodec();
            WAVEWriter newfile;

            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "FWSE files (*.FWSE)|*.FWSE";
                OpenFile.RestoreDirectory = true;

                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    file = new FWSEReader(OpenFile.FileName);
                    int[] Converted = Codec.DecodeMTF_IMA(file.SoundData);

                    ResultLabel.Text = file.HeaderString();

                    using (SaveFileDialog SaveFile = new SaveFileDialog())
                    {
                        SaveFile.Filter = "WAVE files (*.wav)|*.wav";
                        SaveFile.RestoreDirectory = true;

                        if (SaveFile.ShowDialog() == DialogResult.OK)
                        {
                            int[][] newbytes = new int[1][];
                            newbytes[0] = Converted;

                            newfile = new WAVEWriter(SaveFile.FileName, 1, 48000, 16, Converted.Length, newbytes);
                        }
                    }
                }
            }
        }
    }
}
