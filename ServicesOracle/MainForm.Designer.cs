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
            this.btn_windowsStartup = new System.Windows.Forms.Button();
            this.btn_state = new System.Windows.Forms.Button();
            this.chk_backgroundRun = new System.Windows.Forms.CheckBox();
            this.chk_runAtStartup = new System.Windows.Forms.CheckBox();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btn_windowsStartup
            // 
            this.btn_windowsStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_windowsStartup.Location = new System.Drawing.Point(36, 12);
            this.btn_windowsStartup.Name = "btn_windowsStartup";
            this.btn_windowsStartup.Size = new System.Drawing.Size(250, 40);
            this.btn_windowsStartup.TabIndex = 0;
            this.btn_windowsStartup.Text = "(Windows startup)";
            this.btn_windowsStartup.UseVisualStyleBackColor = true;
            this.btn_windowsStartup.Click += new System.EventHandler(this.btn_windowsStartup_Click);
            // 
            // btn_state
            // 
            this.btn_state.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_state.Location = new System.Drawing.Point(36, 69);
            this.btn_state.Name = "btn_state";
            this.btn_state.Size = new System.Drawing.Size(250, 40);
            this.btn_state.TabIndex = 1;
            this.btn_state.Text = "(State)";
            this.btn_state.UseVisualStyleBackColor = true;
            this.btn_state.Click += new System.EventHandler(this.btn_state_Click);
            // 
            // chk_backgroundRun
            // 
            this.chk_backgroundRun.AutoSize = true;
            this.chk_backgroundRun.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_backgroundRun.Location = new System.Drawing.Point(40, 124);
            this.chk_backgroundRun.Name = "chk_backgroundRun";
            this.chk_backgroundRun.Size = new System.Drawing.Size(246, 24);
            this.chk_backgroundRun.TabIndex = 2;
            this.chk_backgroundRun.Text = "Run the program in background?";
            this.chk_backgroundRun.UseVisualStyleBackColor = true;
            this.chk_backgroundRun.CheckedChanged += new System.EventHandler(this.chk_backgroundRun_CheckedChanged);
            // 
            // chk_runAtStartup
            // 
            this.chk_runAtStartup.AutoSize = true;
            this.chk_runAtStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_runAtStartup.Location = new System.Drawing.Point(40, 154);
            this.chk_runAtStartup.Name = "chk_runAtStartup";
            this.chk_runAtStartup.Size = new System.Drawing.Size(185, 24);
            this.chk_runAtStartup.TabIndex = 3;
            this.chk_runAtStartup.Text = "Run at Windows startup";
            this.chk_runAtStartup.UseVisualStyleBackColor = true;
            // 
            // systemTray
            // 
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "System tray icon";
            this.systemTray.Visible = true;
            this.systemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 184);
            this.Controls.Add(this.chk_runAtStartup);
            this.Controls.Add(this.chk_backgroundRun);
            this.Controls.Add(this.btn_state);
            this.Controls.Add(this.btn_windowsStartup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopping Oracle Services - SOS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_windowsStartup;
        private Button btn_state;
        private CheckBox chk_backgroundRun;
        private CheckBox chk_runAtStartup;
        private NotifyIcon systemTray;
    }
}