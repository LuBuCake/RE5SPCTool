using System;
using System.IO;
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
        // Main
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        // Globals

        private string[] ToWAVFiles;
        private string[] ToFWSEFiles;

        // Methods

        private string StringBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private void ToWAVOpenFilesButton_Click(object sender, EventArgs e)
        {          
            using (OpenFileDialog OpenFiles = new OpenFileDialog())
            {
                OpenFiles.Filter = "FWSE files (*.FWSE)|*.FWSE";
                OpenFiles.Title = "Select one or more FWSE files";
                OpenFiles.Multiselect = true;

                if (OpenFiles.ShowDialog() == DialogResult.OK)
                {
                    ToWAVFilePathBox.Text = "";

                    ToWAVFiles = new string[OpenFiles.FileNames.Length];

                    for (int i = 0; i < OpenFiles.FileNames.Length; i++)
                    {
                        ToWAVFiles[i] = OpenFiles.FileNames[i];
                        ToWAVFilePathBox.Text += ToWAVFiles[i] + " ";
                    }
                }
            }
        }

        private void ToWAVButton_Click(object sender, EventArgs e)
        {
            FWSEReader FWSEFile;
            WAVEWriter WAVFile;

            if (ToWAVFiles.Length <= 0)
            {
                MessageBox.Show("No files to convert.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            foreach (string fwsefile in ToWAVFiles)
            {
                if (!File.Exists(fwsefile))
                {
                    MessageBox.Show("File(s) is/are no longer present.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ToWAVFiles = new string[0];
                    ToWAVFilePathBox.Text = "";
                    return;
                }

                FWSEFile = new FWSEReader(fwsefile);
                WAVFile = new WAVEWriter(fwsefile, (ushort)FWSEFile.Channels, (uint)FWSEFile.SampleRate, (ushort)FWSEFile.BitsPerSample, FWSEFile.SampleCount, FWSEFile.ConvertedSoundData);
            }

            MessageBox.Show("Files Converted!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ToWAVFiles = new string[0];
            ToWAVFilePathBox.Text = "";
        }

        private void ToFWSEOpenFilesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenFiles = new OpenFileDialog())
            {
                OpenFiles.Filter = "WAVE files (*.wav)|*.wav";
                OpenFiles.Title = "Select one or more WAVE files";
                OpenFiles.Multiselect = true;

                if (OpenFiles.ShowDialog() == DialogResult.OK)
                {
                    ToFWSEFilePathBox.Text = "";

                    ToFWSEFiles = new string[OpenFiles.FileNames.Length];

                    for (int i = 0; i < OpenFiles.FileNames.Length; i++)
                    {
                        ToFWSEFiles[i] = OpenFiles.FileNames[i];
                        ToFWSEFilePathBox.Text += ToFWSEFiles[i] + " ";
                    }
                }
            }
        }

        private void ToFWSEButton_Click(object sender, EventArgs e)
        {
            WAVEReader WAVEFile;
            WAVEWriter WAVENewFile;

            if (ToFWSEFiles.Length <= 0)
            {
                MessageBox.Show("No files to convert.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            foreach (string wavfile in ToFWSEFiles)
            {
                if (!File.Exists(wavfile))
                {
                    MessageBox.Show("File(s) is/are no longer present.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ToFWSEFiles = new string[0];
                    ToFWSEFilePathBox.Text = "";
                    return;
                }

                WAVEFile = new WAVEReader(wavfile);
                WAVENewFile = new WAVEWriter(wavfile, WAVEFile.NumChannels, WAVEFile.SampleRate, WAVEFile.BitsPerSample, WAVEFile.Samples, WAVEFile.Subchunk2Data);
            }

            MessageBox.Show("Files Converted!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ToFWSEFiles = new string[0];
            ToFWSEFilePathBox.Text = "";
        }
    }
}
