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
            this.chk_minimizeAtStartup = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_windowsStartup
            // 
            this.btn_windowsStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_windowsStartup.Location = new System.Drawing.Point(47, 12);
            this.btn_windowsStartup.Name = "btn_windowsStartup";
            this.btn_windowsStartup.Size = new System.Drawing.Size(270, 40);
            this.btn_windowsStartup.TabIndex = 0;
            this.btn_windowsStartup.Text = "(Windows startup)";
            this.btn_windowsStartup.UseVisualStyleBackColor = true;
            this.btn_windowsStartup.Click += new System.EventHandler(this.btn_windowsStartup_Click);
            // 
            // btn_state
            // 
            this.btn_state.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_state.Location = new System.Drawing.Point(47, 69);
            this.btn_state.Name = "btn_state";
            this.btn_state.Size = new System.Drawing.Size(270, 40);
            this.btn_state.TabIndex = 1;
            this.btn_state.Text = "(State)";
            this.btn_state.UseVisualStyleBackColor = true;
            this.btn_state.Click += new System.EventHandler(this.btn_state_Click);
            // 
            // chk_backgroundRun
            // 
            this.chk_backgroundRun.AutoSize = true;
            this.chk_backgroundRun.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_backgroundRun.Location = new System.Drawing.Point(62, 120);
            this.chk_backgroundRun.Name = "chk_backgroundRun";
            this.chk_backgroundRun.Size = new System.Drawing.Size(239, 24);
            this.chk_backgroundRun.TabIndex = 2;
            this.chk_backgroundRun.Text = "Run the program in background";
            this.chk_backgroundRun.UseVisualStyleBackColor = true;
            this.chk_backgroundRun.CheckedChanged += new System.EventHandler(this.chk_backgroundRun_CheckedChanged);
            // 
            // chk_runAtStartup
            // 
            this.chk_runAtStartup.AutoSize = true;
            this.chk_runAtStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_runAtStartup.Location = new System.Drawing.Point(62, 147);
            this.chk_runAtStartup.Name = "chk_runAtStartup";
            this.chk_runAtStartup.Size = new System.Drawing.Size(185, 24);
            this.chk_runAtStartup.TabIndex = 3;
            this.chk_runAtStartup.Text = "Run at Windows startup";
            this.chk_runAtStartup.UseVisualStyleBackColor = true;
            this.chk_runAtStartup.CheckedChanged += new System.EventHandler(this.chk_runAtStartup_CheckedChanged);
            // 
            // systemTray
            // 
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "SOS";
            this.systemTray.Visible = true;
            this.systemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemTray_MouseDoubleClick);
            // 
            // chk_minimizeAtStartup
            // 
            this.chk_minimizeAtStartup.AutoSize = true;
            this.chk_minimizeAtStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_minimizeAtStartup.Location = new System.Drawing.Point(62, 174);
            this.chk_minimizeAtStartup.Name = "chk_minimizeAtStartup";
            this.chk_minimizeAtStartup.Size = new System.Drawing.Size(156, 24);
            this.chk_minimizeAtStartup.TabIndex = 4;
            this.chk_minimizeAtStartup.Text = "Minimize at startup";
            this.chk_minimizeAtStartup.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(143, 204);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 235);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.chk_minimizeAtStartup);
            this.Controls.Add(this.chk_runAtStartup);
            this.Controls.Add(this.chk_backgroundRun);
            this.Controls.Add(this.btn_state);
            this.Controls.Add(this.btn_windowsStartup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private CheckBox chk_minimizeAtStartup;
        private Button btn_save;
    }
}