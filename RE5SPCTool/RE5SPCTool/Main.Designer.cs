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

namespace RE5SPCTool
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ToWAVButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ConverterPage = new System.Windows.Forms.TabPage();
            this.ToFWSEGPBox = new System.Windows.Forms.GroupBox();
            this.ToFWSEOpenFilesButton = new System.Windows.Forms.Button();
            this.ToFWSEFilePathBox = new System.Windows.Forms.TextBox();
            this.ToFWSEButton = new System.Windows.Forms.Button();
            this.ToWAVGPBox = new System.Windows.Forms.GroupBox();
            this.ToWAVOpenFilesButton = new System.Windows.Forms.Button();
            this.ToWAVFilePathBox = new System.Windows.Forms.TextBox();
            this.ContainerPage = new System.Windows.Forms.TabPage();
            this.SPACToolsGPBox = new System.Windows.Forms.GroupBox();
            this.SPACToolsReplaceGPBox = new System.Windows.Forms.GroupBox();
            this.ReplaceFWSEButton = new System.Windows.Forms.Button();
            this.SPACSoundsComboBox = new System.Windows.Forms.ComboBox();
            this.ReplaceLabel = new System.Windows.Forms.Label();
            this.WithLabel = new System.Windows.Forms.Label();
            this.SPACToolsExtractGPBox = new System.Windows.Forms.GroupBox();
            this.SPACExtractButton = new System.Windows.Forms.Button();
            this.ConvertToWavBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SPACNameTextBox = new System.Windows.Forms.TextBox();
            this.OpenSPACButton = new System.Windows.Forms.Button();
            this.SaveSPACButton = new System.Windows.Forms.Button();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.TopicButton = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.ConverterPage.SuspendLayout();
            this.ToFWSEGPBox.SuspendLayout();
            this.ToWAVGPBox.SuspendLayout();
            this.ContainerPage.SuspendLayout();
            this.SPACToolsGPBox.SuspendLayout();
            this.SPACToolsReplaceGPBox.SuspendLayout();
            this.SPACToolsExtractGPBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToWAVButton
            // 
            this.ToWAVButton.Location = new System.Drawing.Point(237, 47);
            this.ToWAVButton.Name = "ToWAVButton";
            this.ToWAVButton.Size = new System.Drawing.Size(87, 23);
            this.ToWAVButton.TabIndex = 0;
            this.ToWAVButton.Text = "Convert";
            this.ToWAVButton.UseVisualStyleBackColor = true;
            this.ToWAVButton.Click += new System.EventHandler(this.ToWAVButton_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.ConverterPage);
            this.MainTabControl.Controls.Add(this.ContainerPage);
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(365, 201);
            this.MainTabControl.TabIndex = 1;
            // 
            // ConverterPage
            // 
            this.ConverterPage.Controls.Add(this.ToFWSEGPBox);
            this.ConverterPage.Controls.Add(this.ToWAVGPBox);
            this.ConverterPage.Location = new System.Drawing.Point(4, 24);
            this.ConverterPage.Name = "ConverterPage";
            this.ConverterPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConverterPage.Size = new System.Drawing.Size(357, 173);
            this.ConverterPage.TabIndex = 0;
            this.ConverterPage.Text = "FWSE Converter";
            this.ConverterPage.UseVisualStyleBackColor = true;
            // 
            // ToFWSEGPBox
            // 
            this.ToFWSEGPBox.Controls.Add(this.TopicButton);
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEOpenFilesButton);
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEFilePathBox);
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEButton);
            this.ToFWSEGPBox.Location = new System.Drawing.Point(6, 90);
            this.ToFWSEGPBox.Name = "ToFWSEGPBox";
            this.ToFWSEGPBox.Size = new System.Drawing.Size(345, 77);
            this.ToFWSEGPBox.TabIndex = 2;
            this.ToFWSEGPBox.TabStop = false;
            this.ToFWSEGPBox.Text = "Convert to FWSE";
            // 
            // ToFWSEOpenFilesButton
            // 
            this.ToFWSEOpenFilesButton.Location = new System.Drawing.Point(299, 18);
            this.ToFWSEOpenFilesButton.Name = "ToFWSEOpenFilesButton";
            this.ToFWSEOpenFilesButton.Size = new System.Drawing.Size(25, 23);
            this.ToFWSEOpenFilesButton.TabIndex = 1;
            this.ToFWSEOpenFilesButton.Text = "...";
            this.ToFWSEOpenFilesButton.UseVisualStyleBackColor = true;
            this.ToFWSEOpenFilesButton.Click += new System.EventHandler(this.ToFWSEOpenFilesButton_Click);
            // 
            // ToFWSEFilePathBox
            // 
            this.ToFWSEFilePathBox.AllowDrop = true;
            this.ToFWSEFilePathBox.Location = new System.Drawing.Point(22, 18);
            this.ToFWSEFilePathBox.Name = "ToFWSEFilePathBox";
            this.ToFWSEFilePathBox.Size = new System.Drawing.Size(271, 23);
            this.ToFWSEFilePathBox.TabIndex = 0;
            // 
            // ToFWSEButton
            // 
            this.ToFWSEButton.Location = new System.Drawing.Point(237, 47);
            this.ToFWSEButton.Name = "ToFWSEButton";
            this.ToFWSEButton.Size = new System.Drawing.Size(87, 23);
            this.ToFWSEButton.TabIndex = 0;
            this.ToFWSEButton.Text = "Convert";
            this.ToFWSEButton.UseVisualStyleBackColor = true;
            this.ToFWSEButton.Click += new System.EventHandler(this.ToFWSEButton_Click);
            // 
            // ToWAVGPBox
            // 
            this.ToWAVGPBox.Controls.Add(this.ToWAVOpenFilesButton);
            this.ToWAVGPBox.Controls.Add(this.ToWAVFilePathBox);
            this.ToWAVGPBox.Controls.Add(this.ToWAVButton);
            this.ToWAVGPBox.Location = new System.Drawing.Point(6, 6);
            this.ToWAVGPBox.Name = "ToWAVGPBox";
            this.ToWAVGPBox.Size = new System.Drawing.Size(345, 78);
            this.ToWAVGPBox.TabIndex = 1;
            this.ToWAVGPBox.TabStop = false;
            this.ToWAVGPBox.Text = "Convert to WAV";
            // 
            // ToWAVOpenFilesButton
            // 
            this.ToWAVOpenFilesButton.Location = new System.Drawing.Point(299, 18);
            this.ToWAVOpenFilesButton.Name = "ToWAVOpenFilesButton";
            this.ToWAVOpenFilesButton.Size = new System.Drawing.Size(25, 23);
            this.ToWAVOpenFilesButton.TabIndex = 1;
            this.ToWAVOpenFilesButton.Text = "...";
            this.ToWAVOpenFilesButton.UseVisualStyleBackColor = true;
            this.ToWAVOpenFilesButton.Click += new System.EventHandler(this.ToWAVOpenFilesButton_Click);
            // 
            // ToWAVFilePathBox
            // 
            this.ToWAVFilePathBox.AllowDrop = true;
            this.ToWAVFilePathBox.Location = new System.Drawing.Point(22, 18);
            this.ToWAVFilePathBox.Name = "ToWAVFilePathBox";
            this.ToWAVFilePathBox.Size = new System.Drawing.Size(271, 23);
            this.ToWAVFilePathBox.TabIndex = 0;
            // 
            // ContainerPage
            // 
            this.ContainerPage.Controls.Add(this.SPACToolsGPBox);
            this.ContainerPage.Controls.Add(this.groupBox1);
            this.ContainerPage.Location = new System.Drawing.Point(4, 24);
            this.ContainerPage.Name = "ContainerPage";
            this.ContainerPage.Padding = new System.Windows.Forms.Padding(3);
            this.ContainerPage.Size = new System.Drawing.Size(357, 173);
            this.ContainerPage.TabIndex = 1;
            this.ContainerPage.Text = "SPC Container";
            this.ContainerPage.UseVisualStyleBackColor = true;
            // 
            // SPACToolsGPBox
            // 
            this.SPACToolsGPBox.Controls.Add(this.SPACToolsReplaceGPBox);
            this.SPACToolsGPBox.Controls.Add(this.SPACToolsExtractGPBox);
            this.SPACToolsGPBox.Location = new System.Drawing.Point(6, 65);
            this.SPACToolsGPBox.Name = "SPACToolsGPBox";
            this.SPACToolsGPBox.Size = new System.Drawing.Size(345, 102);
            this.SPACToolsGPBox.TabIndex = 6;
            this.SPACToolsGPBox.TabStop = false;
            this.SPACToolsGPBox.Text = "Tools";
            // 
            // SPACToolsReplaceGPBox
            // 
            this.SPACToolsReplaceGPBox.Controls.Add(this.ReplaceFWSEButton);
            this.SPACToolsReplaceGPBox.Controls.Add(this.SPACSoundsComboBox);
            this.SPACToolsReplaceGPBox.Controls.Add(this.ReplaceLabel);
            this.SPACToolsReplaceGPBox.Controls.Add(this.WithLabel);
            this.SPACToolsReplaceGPBox.Location = new System.Drawing.Point(156, 22);
            this.SPACToolsReplaceGPBox.Name = "SPACToolsReplaceGPBox";
            this.SPACToolsReplaceGPBox.Size = new System.Drawing.Size(183, 74);
            this.SPACToolsReplaceGPBox.TabIndex = 1;
            this.SPACToolsReplaceGPBox.TabStop = false;
            this.SPACToolsReplaceGPBox.Text = "Replace";
            // 
            // ReplaceFWSEButton
            // 
            this.ReplaceFWSEButton.Location = new System.Drawing.Point(92, 44);
            this.ReplaceFWSEButton.Name = "ReplaceFWSEButton";
            this.ReplaceFWSEButton.Size = new System.Drawing.Size(75, 23);
            this.ReplaceFWSEButton.TabIndex = 5;
            this.ReplaceFWSEButton.Text = "Browse";
            this.ReplaceFWSEButton.UseVisualStyleBackColor = true;
            this.ReplaceFWSEButton.Click += new System.EventHandler(this.ReplaceFWSEButton_Click);
            // 
            // SPACSoundsComboBox
            // 
            this.SPACSoundsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SPACSoundsComboBox.FormattingEnabled = true;
            this.SPACSoundsComboBox.Location = new System.Drawing.Point(67, 17);
            this.SPACSoundsComboBox.Name = "SPACSoundsComboBox";
            this.SPACSoundsComboBox.Size = new System.Drawing.Size(99, 23);
            this.SPACSoundsComboBox.TabIndex = 2;
            // 
            // ReplaceLabel
            // 
            this.ReplaceLabel.AutoSize = true;
            this.ReplaceLabel.Location = new System.Drawing.Point(12, 20);
            this.ReplaceLabel.Name = "ReplaceLabel";
            this.ReplaceLabel.Size = new System.Drawing.Size(51, 15);
            this.ReplaceLabel.TabIndex = 3;
            this.ReplaceLabel.Text = "Replace:";
            // 
            // WithLabel
            // 
            this.WithLabel.AutoSize = true;
            this.WithLabel.Location = new System.Drawing.Point(12, 49);
            this.WithLabel.Name = "WithLabel";
            this.WithLabel.Size = new System.Drawing.Size(36, 15);
            this.WithLabel.TabIndex = 4;
            this.WithLabel.Text = "With:";
            // 
            // SPACToolsExtractGPBox
            // 
            this.SPACToolsExtractGPBox.Controls.Add(this.SPACExtractButton);
            this.SPACToolsExtractGPBox.Controls.Add(this.ConvertToWavBox);
            this.SPACToolsExtractGPBox.Location = new System.Drawing.Point(6, 22);
            this.SPACToolsExtractGPBox.Name = "SPACToolsExtractGPBox";
            this.SPACToolsExtractGPBox.Size = new System.Drawing.Size(144, 74);
            this.SPACToolsExtractGPBox.TabIndex = 0;
            this.SPACToolsExtractGPBox.TabStop = false;
            this.SPACToolsExtractGPBox.Text = "Extract";
            // 
            // SPACExtractButton
            // 
            this.SPACExtractButton.Location = new System.Drawing.Point(35, 17);
            this.SPACExtractButton.Name = "SPACExtractButton";
            this.SPACExtractButton.Size = new System.Drawing.Size(75, 23);
            this.SPACExtractButton.TabIndex = 3;
            this.SPACExtractButton.Text = "Extract";
            this.SPACExtractButton.UseVisualStyleBackColor = true;
            this.SPACExtractButton.Click += new System.EventHandler(this.SPACExtractButton_Click);
            // 
            // ConvertToWavBox
            // 
            this.ConvertToWavBox.AutoSize = true;
            this.ConvertToWavBox.Location = new System.Drawing.Point(15, 47);
            this.ConvertToWavBox.Name = "ConvertToWavBox";
            this.ConvertToWavBox.Size = new System.Drawing.Size(119, 19);
            this.ConvertToWavBox.TabIndex = 1;
            this.ConvertToWavBox.Text = "Convert to: (.wav)";
            this.ConvertToWavBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SPACNameTextBox);
            this.groupBox1.Controls.Add(this.OpenSPACButton);
            this.groupBox1.Controls.Add(this.SaveSPACButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SPC File";
            // 
            // SPACNameTextBox
            // 
            this.SPACNameTextBox.Enabled = false;
            this.SPACNameTextBox.Location = new System.Drawing.Point(183, 19);
            this.SPACNameTextBox.Name = "SPACNameTextBox";
            this.SPACNameTextBox.ReadOnly = true;
            this.SPACNameTextBox.Size = new System.Drawing.Size(141, 23);
            this.SPACNameTextBox.TabIndex = 2;
            // 
            // OpenSPACButton
            // 
            this.OpenSPACButton.Location = new System.Drawing.Point(21, 19);
            this.OpenSPACButton.Name = "OpenSPACButton";
            this.OpenSPACButton.Size = new System.Drawing.Size(75, 23);
            this.OpenSPACButton.TabIndex = 0;
            this.OpenSPACButton.Text = "Open";
            this.OpenSPACButton.UseVisualStyleBackColor = true;
            this.OpenSPACButton.Click += new System.EventHandler(this.OpenSPACButton_Click);
            // 
            // SaveSPACButton
            // 
            this.SaveSPACButton.Location = new System.Drawing.Point(102, 19);
            this.SaveSPACButton.Name = "SaveSPACButton";
            this.SaveSPACButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSPACButton.TabIndex = 1;
            this.SaveSPACButton.Text = "Save";
            this.SaveSPACButton.UseVisualStyleBackColor = true;
            this.SaveSPACButton.Click += new System.EventHandler(this.SaveSPACButton_Click);
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.CopyrightLabel.Location = new System.Drawing.Point(202, 216);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(175, 15);
            this.CopyrightLabel.TabIndex = 1;
            this.CopyrightLabel.Text = "Copyright © 2020 by LuBuCake";
            // 
            // TopicButton
            // 
            this.TopicButton.Location = new System.Drawing.Point(21, 47);
            this.TopicButton.Name = "TopicButton";
            this.TopicButton.Size = new System.Drawing.Size(23, 23);
            this.TopicButton.TabIndex = 2;
            this.TopicButton.Text = "?";
            this.TopicButton.UseVisualStyleBackColor = true;
            this.TopicButton.Click += new System.EventHandler(this.TopicButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 237);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.MainTabControl);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resident Evil 5 - FWSE / SPC Tool";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainTabControl.ResumeLayout(false);
            this.ConverterPage.ResumeLayout(false);
            this.ToFWSEGPBox.ResumeLayout(false);
            this.ToFWSEGPBox.PerformLayout();
            this.ToWAVGPBox.ResumeLayout(false);
            this.ToWAVGPBox.PerformLayout();
            this.ContainerPage.ResumeLayout(false);
            this.SPACToolsGPBox.ResumeLayout(false);
            this.SPACToolsReplaceGPBox.ResumeLayout(false);
            this.SPACToolsReplaceGPBox.PerformLayout();
            this.SPACToolsExtractGPBox.ResumeLayout(false);
            this.SPACToolsExtractGPBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ToWAVButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ConverterPage;
        private System.Windows.Forms.TabPage ContainerPage;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.GroupBox ToWAVGPBox;
        private System.Windows.Forms.TextBox ToWAVFilePathBox;
        private System.Windows.Forms.Button ToWAVOpenFilesButton;
        private System.Windows.Forms.GroupBox ToFWSEGPBox;
        private System.Windows.Forms.Button ToFWSEOpenFilesButton;
        private System.Windows.Forms.TextBox ToFWSEFilePathBox;
        private System.Windows.Forms.Button ToFWSEButton;
        private System.Windows.Forms.GroupBox SPACToolsGPBox;
        private System.Windows.Forms.Button ReplaceFWSEButton;
        private System.Windows.Forms.Button SPACExtractButton;
        private System.Windows.Forms.Label WithLabel;
        private System.Windows.Forms.CheckBox ConvertToWavBox;
        private System.Windows.Forms.Label ReplaceLabel;
        private System.Windows.Forms.ComboBox SPACSoundsComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button OpenSPACButton;
        private System.Windows.Forms.Button SaveSPACButton;
        private System.Windows.Forms.TextBox SPACNameTextBox;
        private System.Windows.Forms.GroupBox SPACToolsReplaceGPBox;
        private System.Windows.Forms.GroupBox SPACToolsExtractGPBox;
        private System.Windows.Forms.Button TopicButton;
    }
}

