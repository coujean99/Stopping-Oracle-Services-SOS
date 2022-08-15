namespace OracleServices
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_ServicesOnStartup = new System.Windows.Forms.Button();
            this.btn_ServicesState = new System.Windows.Forms.Button();
            this.chk_RunAtStartup = new System.Windows.Forms.CheckBox();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.chk_MinimizeAtStartup = new System.Windows.Forms.CheckBox();
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.chk_RunInBackground = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_ServicesOnStartup
            // 
            this.btn_ServicesOnStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_ServicesOnStartup.Location = new System.Drawing.Point(47, 12);
            this.btn_ServicesOnStartup.Name = "btn_ServicesOnStartup";
            this.btn_ServicesOnStartup.Size = new System.Drawing.Size(270, 40);
            this.btn_ServicesOnStartup.TabIndex = 0;
            this.btn_ServicesOnStartup.Text = "(Windows startup)";
            this.btn_ServicesOnStartup.UseVisualStyleBackColor = true;
            this.btn_ServicesOnStartup.Click += new System.EventHandler(this.Btn_ServicesOnStartup_Click);
            // 
            // btn_ServicesState
            // 
            this.btn_ServicesState.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_ServicesState.Location = new System.Drawing.Point(47, 69);
            this.btn_ServicesState.Name = "btn_ServicesState";
            this.btn_ServicesState.Size = new System.Drawing.Size(270, 40);
            this.btn_ServicesState.TabIndex = 1;
            this.btn_ServicesState.Text = "(State)";
            this.btn_ServicesState.UseVisualStyleBackColor = true;
            this.btn_ServicesState.Click += new System.EventHandler(this.Btn_ServicesState_Click);
            // 
            // chk_RunAtStartup
            // 
            this.chk_RunAtStartup.AutoSize = true;
            this.chk_RunAtStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_RunAtStartup.Location = new System.Drawing.Point(62, 147);
            this.chk_RunAtStartup.Name = "chk_RunAtStartup";
            this.chk_RunAtStartup.Size = new System.Drawing.Size(185, 24);
            this.chk_RunAtStartup.TabIndex = 3;
            this.chk_RunAtStartup.Text = "Run at Windows startup";
            this.chk_RunAtStartup.UseVisualStyleBackColor = true;
            this.chk_RunAtStartup.CheckedChanged += new System.EventHandler(this.Chk_RunAtStartup_CheckedChanged);
            // 
            // systemTray
            // 
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "SOS";
            this.systemTray.Visible = true;
            this.systemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SystemTray_MouseDoubleClick);
            // 
            // chk_MinimizeAtStartup
            // 
            this.chk_MinimizeAtStartup.AutoSize = true;
            this.chk_MinimizeAtStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_MinimizeAtStartup.Location = new System.Drawing.Point(62, 174);
            this.chk_MinimizeAtStartup.Name = "chk_MinimizeAtStartup";
            this.chk_MinimizeAtStartup.Size = new System.Drawing.Size(156, 24);
            this.chk_MinimizeAtStartup.TabIndex = 4;
            this.chk_MinimizeAtStartup.Text = "Minimize at startup";
            this.chk_MinimizeAtStartup.UseVisualStyleBackColor = true;
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Location = new System.Drawing.Point(143, 204);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveSettings.TabIndex = 5;
            this.btn_SaveSettings.Text = "Save";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.Btn_SaveSettings_Click);
            // 
            // chk_RunInBackground
            // 
            this.chk_RunInBackground.AutoSize = true;
            this.chk_RunInBackground.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_RunInBackground.Location = new System.Drawing.Point(62, 120);
            this.chk_RunInBackground.Name = "chk_RunInBackground";
            this.chk_RunInBackground.Size = new System.Drawing.Size(152, 24);
            this.chk_RunInBackground.TabIndex = 2;
            this.chk_RunInBackground.Text = "Run in background";
            this.chk_RunInBackground.UseVisualStyleBackColor = true;
            this.chk_RunInBackground.Click += new System.EventHandler(this.chk_RunInBackground_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 235);
            this.Controls.Add(this.btn_SaveSettings);
            this.Controls.Add(this.chk_MinimizeAtStartup);
            this.Controls.Add(this.chk_RunAtStartup);
            this.Controls.Add(this.chk_RunInBackground);
            this.Controls.Add(this.btn_ServicesState);
            this.Controls.Add(this.btn_ServicesOnStartup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopping Oracle Services - SOS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_ServicesOnStartup;
        private Button btn_ServicesState;
        private CheckBox chk_RunAtStartup;
        private NotifyIcon systemTray;
        private CheckBox chk_MinimizeAtStartup;
        private Button btn_SaveSettings;
        private CheckBox chk_RunInBackground;
    }
}