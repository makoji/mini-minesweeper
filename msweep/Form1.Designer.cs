namespace msweep
{
    partial class Form1
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
            this.diffQuestion = new System.Windows.Forms.Label();
            this.bttnDiffEasy = new System.Windows.Forms.Button();
            this.bttnDiffMedium = new System.Windows.Forms.Button();
            this.bttnDiffHard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // diffQuestion
            // 
            this.diffQuestion.AutoSize = true;
            this.diffQuestion.Enabled = false;
            this.diffQuestion.Location = new System.Drawing.Point(66, 75);
            this.diffQuestion.Name = "diffQuestion";
            this.diffQuestion.Size = new System.Drawing.Size(371, 25);
            this.diffQuestion.TabIndex = 0;
            this.diffQuestion.Text = "What difficulty would you like to play?";
            // 
            // bttnDiffEasy
            // 
            this.bttnDiffEasy.Location = new System.Drawing.Point(53, 157);
            this.bttnDiffEasy.Name = "bttnDiffEasy";
            this.bttnDiffEasy.Size = new System.Drawing.Size(395, 63);
            this.bttnDiffEasy.TabIndex = 1;
            this.bttnDiffEasy.Text = "Easy";
            this.bttnDiffEasy.UseVisualStyleBackColor = true;
            this.bttnDiffEasy.Click += new System.EventHandler(this.bttnDiffEasy_Click);
            // 
            // bttnDiffMedium
            // 
            this.bttnDiffMedium.Location = new System.Drawing.Point(53, 245);
            this.bttnDiffMedium.Name = "bttnDiffMedium";
            this.bttnDiffMedium.Size = new System.Drawing.Size(395, 63);
            this.bttnDiffMedium.TabIndex = 2;
            this.bttnDiffMedium.Text = "Medium";
            this.bttnDiffMedium.UseVisualStyleBackColor = true;
            this.bttnDiffMedium.Click += new System.EventHandler(this.bttnDiffMedium_Click);
            // 
            // bttnDiffHard
            // 
            this.bttnDiffHard.Location = new System.Drawing.Point(53, 334);
            this.bttnDiffHard.Name = "bttnDiffHard";
            this.bttnDiffHard.Size = new System.Drawing.Size(395, 63);
            this.bttnDiffHard.TabIndex = 3;
            this.bttnDiffHard.Text = "Hard";
            this.bttnDiffHard.UseVisualStyleBackColor = true;
            this.bttnDiffHard.Click += new System.EventHandler(this.bttnDiffHard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(504, 474);
            this.Controls.Add(this.bttnDiffHard);
            this.Controls.Add(this.bttnDiffMedium);
            this.Controls.Add(this.bttnDiffEasy);
            this.Controls.Add(this.diffQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "choose the difficulty level";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label diffQuestion;
        private System.Windows.Forms.Button bttnDiffEasy;
        private System.Windows.Forms.Button bttnDiffMedium;
        private System.Windows.Forms.Button bttnDiffHard;
    }
}

