namespace FWSEConverter
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
            this.ToWAVGPBox = new System.Windows.Forms.GroupBox();
            this.ToWAVOpenFilesButton = new System.Windows.Forms.Button();
            this.ToWAVFilePathBox = new System.Windows.Forms.TextBox();
            this.ContainerPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.ToFWSEGPBox = new System.Windows.Forms.GroupBox();
            this.ToFWSEOpenFilesButton = new System.Windows.Forms.Button();
            this.ToFWSEFilePathBox = new System.Windows.Forms.TextBox();
            this.ToFWSEButton = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.ConverterPage.SuspendLayout();
            this.ToWAVGPBox.SuspendLayout();
            this.ToFWSEGPBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToWAVButton
            // 
            this.ToWAVButton.Location = new System.Drawing.Point(240, 60);
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
            this.MainTabControl.Size = new System.Drawing.Size(365, 338);
            this.MainTabControl.TabIndex = 1;
            // 
            // ConverterPage
            // 
            this.ConverterPage.Controls.Add(this.ToFWSEGPBox);
            this.ConverterPage.Controls.Add(this.ToWAVGPBox);
            this.ConverterPage.Location = new System.Drawing.Point(4, 24);
            this.ConverterPage.Name = "ConverterPage";
            this.ConverterPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConverterPage.Size = new System.Drawing.Size(357, 310);
            this.ConverterPage.TabIndex = 0;
            this.ConverterPage.Text = "FWSE Converter";
            this.ConverterPage.UseVisualStyleBackColor = true;
            // 
            // ToWAVGPBox
            // 
            this.ToWAVGPBox.Controls.Add(this.ToWAVOpenFilesButton);
            this.ToWAVGPBox.Controls.Add(this.ToWAVFilePathBox);
            this.ToWAVGPBox.Controls.Add(this.ToWAVButton);
            this.ToWAVGPBox.Location = new System.Drawing.Point(6, 6);
            this.ToWAVGPBox.Name = "ToWAVGPBox";
            this.ToWAVGPBox.Size = new System.Drawing.Size(345, 103);
            this.ToWAVGPBox.TabIndex = 1;
            this.ToWAVGPBox.TabStop = false;
            this.ToWAVGPBox.Text = "Convert to WAV";
            // 
            // ToWAVOpenFilesButton
            // 
            this.ToWAVOpenFilesButton.Location = new System.Drawing.Point(302, 31);
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
            this.ToWAVFilePathBox.Location = new System.Drawing.Point(25, 31);
            this.ToWAVFilePathBox.Name = "ToWAVFilePathBox";
            this.ToWAVFilePathBox.Size = new System.Drawing.Size(271, 23);
            this.ToWAVFilePathBox.TabIndex = 0;
            // 
            // ContainerPage
            // 
            this.ContainerPage.Location = new System.Drawing.Point(4, 24);
            this.ContainerPage.Name = "ContainerPage";
            this.ContainerPage.Padding = new System.Windows.Forms.Padding(3);
            this.ContainerPage.Size = new System.Drawing.Size(357, 310);
            this.ContainerPage.TabIndex = 1;
            this.ContainerPage.Text = "SPC Container";
            this.ContainerPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(202, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Copyright © 2020 by LuBuCake";
            // 
            // ToFWSEGPBox
            // 
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEOpenFilesButton);
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEFilePathBox);
            this.ToFWSEGPBox.Controls.Add(this.ToFWSEButton);
            this.ToFWSEGPBox.Location = new System.Drawing.Point(6, 115);
            this.ToFWSEGPBox.Name = "ToFWSEGPBox";
            this.ToFWSEGPBox.Size = new System.Drawing.Size(345, 103);
            this.ToFWSEGPBox.TabIndex = 2;
            this.ToFWSEGPBox.TabStop = false;
            this.ToFWSEGPBox.Text = "Convert to FWSE";
            // 
            // ToFWSEOpenFilesButton
            // 
            this.ToFWSEOpenFilesButton.Location = new System.Drawing.Point(302, 31);
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
            this.ToFWSEFilePathBox.Location = new System.Drawing.Point(25, 31);
            this.ToFWSEFilePathBox.Name = "ToFWSEFilePathBox";
            this.ToFWSEFilePathBox.Size = new System.Drawing.Size(271, 23);
            this.ToFWSEFilePathBox.TabIndex = 0;
            // 
            // ToFWSEButton
            // 
            this.ToFWSEButton.Location = new System.Drawing.Point(240, 60);
            this.ToFWSEButton.Name = "ToFWSEButton";
            this.ToFWSEButton.Size = new System.Drawing.Size(87, 23);
            this.ToFWSEButton.TabIndex = 0;
            this.ToFWSEButton.Text = "Convert";
            this.ToFWSEButton.UseVisualStyleBackColor = true;
            this.ToFWSEButton.Click += new System.EventHandler(this.ToFWSEButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 376);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainTabControl);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resident Evil 5 - FWSE Converter";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainTabControl.ResumeLayout(false);
            this.ConverterPage.ResumeLayout(false);
            this.ToWAVGPBox.ResumeLayout(false);
            this.ToWAVGPBox.PerformLayout();
            this.ToFWSEGPBox.ResumeLayout(false);
            this.ToFWSEGPBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ToWAVButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ConverterPage;
        private System.Windows.Forms.TabPage ContainerPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox ToWAVGPBox;
        private System.Windows.Forms.TextBox ToWAVFilePathBox;
        private System.Windows.Forms.Button ToWAVOpenFilesButton;
        private System.Windows.Forms.GroupBox ToFWSEGPBox;
        private System.Windows.Forms.Button ToFWSEOpenFilesButton;
        private System.Windows.Forms.TextBox ToFWSEFilePathBox;
        private System.Windows.Forms.Button ToFWSEButton;
    }
}

