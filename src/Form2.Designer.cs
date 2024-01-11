namespace TCPCommander
{
    partial class Form2
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
            this.IPLabel = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.SessionTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IPLabel
            // 
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel.Location = new System.Drawing.Point(314, 45);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(278, 49);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "Status";
            this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(88, 179);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(738, 259);
            this.LogTextBox.TabIndex = 2;
            // 
            // SessionTimeLabel
            // 
            this.SessionTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionTimeLabel.Location = new System.Drawing.Point(314, 94);
            this.SessionTimeLabel.Name = "SessionTimeLabel";
            this.SessionTimeLabel.Size = new System.Drawing.Size(278, 49);
            this.SessionTimeLabel.TabIndex = 3;
            this.SessionTimeLabel.Text = "Session Time";
            this.SessionTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 450);
            this.Controls.Add(this.SessionTimeLabel);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.IPLabel);
            this.Name = "Form2";
            this.Text = "Server Status:";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label IPLabel;
        public System.Windows.Forms.TextBox LogTextBox;
        public System.Windows.Forms.Label SessionTimeLabel;
    }
}