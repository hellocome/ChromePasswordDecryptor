namespace ChromePasswordDecryptor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GPDMainPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LVPasswords = new System.Windows.Forms.ListView();
            this.CHActionUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CNUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHEncryptedPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctbFilter = new CommonLib.UserInterface.WinForms.CustomTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnImport = new CommonLib.UserInterface.WinForms.NoBorderActionButton();
            this.btnExport = new CommonLib.UserInterface.WinForms.NoBorderActionButton();
            this.btnClearLog = new CommonLib.UserInterface.WinForms.NoBorderActionButton();
            this.btnReload = new CommonLib.UserInterface.WinForms.NoBorderActionButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            this.PanelWS.SuspendLayout();
            this.FormPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.GPDMainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelTitle
            // 
            this.LabelTitle.Size = new System.Drawing.Size(194, 20);
            this.LabelTitle.Text = "Google Password Decryptor";
            // 
            // FormPanel
            // 
            this.FormPanel.Size = new System.Drawing.Size(798, 530);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.GPDMainPanel);
            this.MainPanel.Size = new System.Drawing.Size(798, 498);
            // 
            // GPDMainPanel
            // 
            this.GPDMainPanel.Controls.Add(this.panel2);
            this.GPDMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GPDMainPanel.Location = new System.Drawing.Point(0, 0);
            this.GPDMainPanel.Name = "GPDMainPanel";
            this.GPDMainPanel.Padding = new System.Windows.Forms.Padding(3);
            this.GPDMainPanel.Size = new System.Drawing.Size(798, 498);
            this.GPDMainPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LVPasswords);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 492);
            this.panel2.TabIndex = 3;
            // 
            // LVPasswords
            // 
            this.LVPasswords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVPasswords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHActionUrl,
            this.CNUsername,
            this.CHEncryptedPassword,
            this.CHPassword});
            this.LVPasswords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVPasswords.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LVPasswords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LVPasswords.FullRowSelect = true;
            this.LVPasswords.GridLines = true;
            this.LVPasswords.Location = new System.Drawing.Point(0, 72);
            this.LVPasswords.MultiSelect = false;
            this.LVPasswords.Name = "LVPasswords";
            this.LVPasswords.Size = new System.Drawing.Size(792, 282);
            this.LVPasswords.TabIndex = 4;
            this.LVPasswords.UseCompatibleStateImageBehavior = false;
            this.LVPasswords.View = System.Windows.Forms.View.Details;
            // 
            // CHActionUrl
            // 
            this.CHActionUrl.Text = "ActionUrl";
            this.CHActionUrl.Width = 178;
            // 
            // CNUsername
            // 
            this.CNUsername.Text = "Username";
            this.CNUsername.Width = 160;
            // 
            // CHEncryptedPassword
            // 
            this.CHEncryptedPassword.Text = "Encrypted Password";
            this.CHEncryptedPassword.Width = 239;
            // 
            // CHPassword
            // 
            this.CHPassword.Text = "Text Password";
            this.CHPassword.Width = 166;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 354);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(792, 138);
            this.panel1.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(786, 132);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ctbFilter);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(792, 72);
            this.panel3.TabIndex = 0;
            // 
            // ctbFilter
            // 
            this.ctbFilter.BackColor = System.Drawing.Color.White;
            this.ctbFilter.BorderColor = System.Drawing.Color.Gray;
            this.ctbFilter.ControlBackColor = System.Drawing.Color.White;
            this.ctbFilter.DisplayFont = new System.Drawing.Font("Segoe UI", 16F);
            this.ctbFilter.DisplayForeColor = System.Drawing.Color.Gray;
            this.ctbFilter.DisplayText = "";
            this.ctbFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctbFilter.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctbFilter.ForeColor = System.Drawing.Color.Gray;
            this.ctbFilter.Location = new System.Drawing.Point(0, 11);
            this.ctbFilter.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ctbFilter.MaxLength = 500;
            this.ctbFilter.Name = "ctbFilter";
            this.ctbFilter.Padding = new System.Windows.Forms.Padding(1);
            this.ctbFilter.PasswordChar = '\0';
            this.ctbFilter.Size = new System.Drawing.Size(282, 42);
            this.ctbFilter.TabIndex = 0;
            this.ctbFilter.UserIcon = global::ChromePasswordDecryptor.Properties.Resources.Search;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 53);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(282, 19);
            this.panel5.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnImport);
            this.panel4.Controls.Add(this.btnExport);
            this.panel4.Controls.Add(this.btnClearLog);
            this.panel4.Controls.Add(this.btnReload);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(282, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(510, 72);
            this.panel4.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(391, 11);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(113, 42);
            this.btnImport.TabIndex = 5;
            this.btnImport.TabStop = false;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(268, 11);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(113, 42);
            this.btnExport.TabIndex = 3;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            this.btnClearLog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClearLog.FlatAppearance.BorderSize = 0;
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.Location = new System.Drawing.Point(145, 11);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(113, 42);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.TabStop = false;
            this.btnClearLog.Text = "ClearLog";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(187)))), ((int)(((byte)(89)))));
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(19, 11);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(113, 42);
            this.btnReload.TabIndex = 0;
            this.btnReload.TabStop = false;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "password.data";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "password.data";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.DisplayIcon = global::ChromePasswordDecryptor.Properties.Resources.Chrome;
            this.DisplayText = "Google Password Decryptor";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Google Password Decryptor";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.PanelWS.ResumeLayout(false);
            this.FormPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.GPDMainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GPDMainPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView LVPasswords;
        private System.Windows.Forms.ColumnHeader CHActionUrl;
        private System.Windows.Forms.ColumnHeader CNUsername;
        private System.Windows.Forms.ColumnHeader CHEncryptedPassword;
        private System.Windows.Forms.ColumnHeader CHPassword;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel panel4;
        private CommonLib.UserInterface.WinForms.NoBorderActionButton btnReload;
        private CommonLib.UserInterface.WinForms.NoBorderActionButton btnClearLog;
        private CommonLib.UserInterface.WinForms.CustomTextBox ctbFilter;
        private System.Windows.Forms.Panel panel5;
        private CommonLib.UserInterface.WinForms.NoBorderActionButton btnExport;
        private CommonLib.UserInterface.WinForms.NoBorderActionButton btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

