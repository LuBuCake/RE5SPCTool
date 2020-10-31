using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace RE5SPCTool
{
    public partial class Main : Form
    {
        // Globals

        SPAC WorkingSPAC;

        private string[] ToWAVFiles;
        private string[] ToWAVSafeFiles;
        private string[] ToFWSEFiles;
        private string[] ToFWSESafeFiles;

        private string SPACFilePath;

        // Main
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SPACFilePath = "";
            ToWAVFiles = new string[0];
            ToWAVSafeFiles = new string[0];
            ToFWSEFiles = new string[0];
            ToFWSESafeFiles = new string[0];

            SPACNameTextBox.Text = "No SPC file opened";
        }

        // Converter

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
                    ToWAVSafeFiles = new string[OpenFiles.FileNames.Length];

                    for (int i = 0; i < OpenFiles.FileNames.Length; i++)
                    {
                        ToWAVFiles[i] = OpenFiles.FileNames[i];
                        ToWAVSafeFiles[i] = OpenFiles.SafeFileNames[i];
                        ToWAVFilePathBox.Text += ToWAVSafeFiles[i] + " ";
                    }
                }
            }
        }

        private void ToWAVButton_Click(object sender, EventArgs e)
        {
            FWSEReader FWSEFile;
            WAVEWriter WAVFile;
            string newfilename;

            if (ToWAVFiles.Length <= 0)
            {
                MessageBox.Show("No files to convert.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            using (FolderBrowserDialog Directory = new FolderBrowserDialog())
            {
                if (Directory.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fwsefile in ToWAVFiles)
                    {
                        if (!File.Exists(fwsefile))
                        {
                            MessageBox.Show("File(s) is/are no longer present.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ToWAVFiles = new string[0];
                            ToWAVFilePathBox.Text = "";
                            return;
                        }

                        newfilename = ToWAVSafeFiles[Array.IndexOf(ToWAVFiles, fwsefile)];

                        if (newfilename.Contains(".FWSE"))
                            newfilename = newfilename.Replace(".FWSE", ".wav");

                        FWSEFile = new FWSEReader(fwsefile);
                        WAVFile = new WAVEWriter(Directory.SelectedPath + "/" + newfilename, (ushort)FWSEFile.Channels, (uint)FWSEFile.SampleRate, (ushort)FWSEFile.BitsPerSample, FWSEFile.SampleCount, FWSEFile.ConvertedSoundData);
                    }

                    MessageBox.Show("WAVE files generated!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ToWAVFiles = new string[0];
                    ToWAVFilePathBox.Text = "";
                }
            }
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
                    ToFWSESafeFiles = new string[OpenFiles.FileNames.Length];

                    for (int i = 0; i < OpenFiles.FileNames.Length; i++)
                    {
                        ToFWSEFiles[i] = OpenFiles.FileNames[i];
                        ToFWSESafeFiles[i] = OpenFiles.SafeFileNames[i];
                        ToFWSEFilePathBox.Text += ToFWSESafeFiles[i] + " ";
                    }
                }
            }
        }

        private void ToFWSEButton_Click(object sender, EventArgs e)
        {
            WAVEReader WAVEFile;
            FWSEWriter FWSEFile;
            string newfilename;

            if (ToFWSEFiles.Length <= 0)
            {
                MessageBox.Show("No files to convert.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            using (FolderBrowserDialog Directory = new FolderBrowserDialog())
            {
                if (Directory.ShowDialog() == DialogResult.OK)
                {
                    foreach (string wavfile in ToFWSEFiles)
                    {
                        if (!File.Exists(wavfile))
                        {
                            MessageBox.Show("File(s) is/are no longer present.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ToFWSEFiles = new string[0];
                            ToFWSEFilePathBox.Text = "";
                            return;
                        }

                        newfilename = ToFWSESafeFiles[Array.IndexOf(ToFWSEFiles, wavfile)];

                        if (newfilename.Contains(".wav"))
                            newfilename = newfilename.Replace(".wav", ".FWSE");

                        WAVEFile = new WAVEReader(wavfile);

                        if (WAVEFile.WAVECheck())
                            FWSEFile = new FWSEWriter(Directory.SelectedPath + "/" + newfilename, WAVEFile.Subchunk2Data[0]);
                        else
                        {
                            MessageBox.Show(WAVEFile.filepathdir + " is not a valid WAVE file.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    MessageBox.Show("FWSE files generated!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ToFWSEFiles = new string[0];
                    ToFWSEFilePathBox.Text = "";
                }
            }
        }

        // SPAC Tools

        private bool CheckSPAC()
        {
            if (WorkingSPAC == null)
            {
                MessageBox.Show("No SPC container loaded.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        private void OpenSPACButton_Click(object sender, EventArgs e)
        {
            string SPACNameSafe;

            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "RE5 SPC files (*.spc)|*.spc";
                OpenFile.Title = "Select a valid SPC Container file";

                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    SPACFilePath = OpenFile.FileName;
                    WorkingSPAC = new SPAC(SPACFilePath);

                    if (!WorkingSPAC.CheckSPAC())
                    {
                        MessageBox.Show("This is not a valid SPC file.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SPACFilePath = "";
                        WorkingSPAC = null;
                    }

                    SPACNameSafe = OpenFile.SafeFileName;

                    if (SPACNameSafe.Contains(".spc"))
                        SPACNameSafe = SPACNameSafe.Replace(".spc", "");

                    SPACNameTextBox.Text = "SPAC: " + SPACNameSafe;

                    SPACSoundsComboBox.Items.Clear();

                    for (int i = 0; i < WorkingSPAC.NumSounds; i++)
                    {
                        SPACSoundsComboBox.Items.Add("SOUND #: " + i.ToString());
                    }

                    SPACSoundsComboBox.Items.Add("MULTIPLE");
                    SPACSoundsComboBox.SelectedIndex = 0;
                }
            }
        }

        private void SaveSPACButton_Click(object sender, EventArgs e)
        {
            if (!CheckSPAC())
                return;

            WorkingSPAC.SaveSPAC(SPACFilePath);
            MessageBox.Show("SPC file saved!.", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void SPACExtractButton_Click(object sender, EventArgs e)
        {
            if (!CheckSPAC())
                return;

            using (FolderBrowserDialog Directory = new FolderBrowserDialog())
            {
                if (Directory.ShowDialog() == DialogResult.OK)
                {
                    WorkingSPAC.ExtractFWSE(Directory.SelectedPath, ConvertToWavBox.Checked);
                    MessageBox.Show((ConvertToWavBox.Checked ? "WAV" : "FWSE") + " files generated!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void ReplaceFWSEButton_Click(object sender, EventArgs e)
        {
            if (!CheckSPAC())
                return;

            FWSEReader FWSEFile;
            string filename;

            if (SPACSoundsComboBox.SelectedIndex == WorkingSPAC.NumSounds)
            {
                using (OpenFileDialog OpenFile = new OpenFileDialog())
                {
                    OpenFile.Filter = "FWSE files (*.FWSE)|*.FWSE";
                    OpenFile.Title = "Select one or more FWSE file with the index you want to replace on its name";
                    OpenFile.Multiselect = true;

                    if (OpenFile.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < OpenFile.FileNames.Length; i++)
                        {
                            filename = OpenFile.SafeFileNames[i];

                            if (filename.Contains(".FWSE"))
                                filename = filename.Replace(".FWSE", "");

                            try
                            {
                                if (int.Parse(filename) >= 0 && int.Parse(filename) <= (WorkingSPAC.NumSounds - 1))
                                {
                                    FWSEFile = new FWSEReader(OpenFile.FileNames[i]);
                                    WorkingSPAC.ReplaceFWSE(int.Parse(filename), FWSEFile.WholeBuffer, FWSEFile.SoundData);
                                }
                            }
                            catch(Exception)
                            {
                                MessageBox.Show("Something wrong during index conversion, check if your file has the correc index on its name.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        MessageBox.Show("FWSE data replaced!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }

                return;
            }

            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "FWSE files (*.FWSE)|*.FWSE";
                OpenFile.Title = "Select a valid FWSE file";

                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    FWSEFile = new FWSEReader(OpenFile.FileName);

                    if (!FWSEFile.FWSECheck())
                    {
                        MessageBox.Show(OpenFile.FileName + " is not a valid FWSE file.", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WorkingSPAC.ReplaceFWSE(SPACSoundsComboBox.SelectedIndex, FWSEFile.WholeBuffer, FWSEFile.SoundData);
                    MessageBox.Show("FWSE data replaced!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void TopicButton_Click(object sender, EventArgs e)
        {
            DialogResult ShouldOpenURL = MessageBox.Show("This will open a page in your browser, confirm?", "Original Topic Guides", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ShouldOpenURL == DialogResult.Yes)
                Process.Start("https://residentevilmodding.boards.net/thread/13992/resident-evil-fwse-spc-tool");
        }
    }
}
