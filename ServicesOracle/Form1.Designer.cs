namespace ServicesOracle
{
    partial class Form1
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
            this.btn_auDemarrage = new System.Windows.Forms.Button();
            this.btn_etat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_auDemarrage
            // 
            this.btn_auDemarrage.Location = new System.Drawing.Point(56, 12);
            this.btn_auDemarrage.Name = "btn_auDemarrage";
            this.btn_auDemarrage.Size = new System.Drawing.Size(218, 23);
            this.btn_auDemarrage.TabIndex = 0;
            this.btn_auDemarrage.Text = "(Au démarrage)";
            this.btn_auDemarrage.UseVisualStyleBackColor = true;
            this.btn_auDemarrage.Click += new System.EventHandler(this.btn_auDemarrage_Click);
            // 
            // btn_etat
            // 
            this.btn_etat.Location = new System.Drawing.Point(56, 55);
            this.btn_etat.Name = "btn_etat";
            this.btn_etat.Size = new System.Drawing.Size(218, 23);
            this.btn_etat.TabIndex = 1;
            this.btn_etat.Text = "(État)";
            this.btn_etat.UseVisualStyleBackColor = true;
            this.btn_etat.Click += new System.EventHandler(this.btn_etat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 93);
            this.Controls.Add(this.btn_etat);
            this.Controls.Add(this.btn_auDemarrage);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services Oracle arrière plan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_auDemarrage;
        private Button btn_etat;
    }
}