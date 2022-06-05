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
            this.btn_windowsStartup = new System.Windows.Forms.Button();
            this.btn_state = new System.Windows.Forms.Button();
            this.chk_backgroundRun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_windowsStartup
            // 
            this.btn_windowsStartup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_windowsStartup.Location = new System.Drawing.Point(57, 12);
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
            this.btn_state.Location = new System.Drawing.Point(57, 69);
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
            this.chk_backgroundRun.Location = new System.Drawing.Point(61, 125);
            this.chk_backgroundRun.Name = "chk_backgroundRun";
            this.chk_backgroundRun.Size = new System.Drawing.Size(246, 24);
            this.chk_backgroundRun.TabIndex = 2;
            this.chk_backgroundRun.Text = "Run the program in background?";
            this.chk_backgroundRun.UseVisualStyleBackColor = true;
            this.chk_backgroundRun.CheckedChanged += new System.EventHandler(this.chk_backgroundRun_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 170);
            this.Controls.Add(this.chk_backgroundRun);
            this.Controls.Add(this.btn_state);
            this.Controls.Add(this.btn_windowsStartup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopping Oracle Services - SOS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_windowsStartup;
        private Button btn_state;
        private CheckBox chk_backgroundRun;
    }
}