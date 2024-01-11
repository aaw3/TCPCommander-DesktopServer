namespace TCPCommander
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StartButton = new System.Windows.Forms.Button();
            this.TCP_PortTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.TCP_PortLabel = new System.Windows.Forms.Label();
            this.TCP_AvailablePortsLabel = new System.Windows.Forms.Label();
            this.DebugButton = new System.Windows.Forms.Button();
            this.UDP_AvailablePortsLabel = new System.Windows.Forms.Label();
            this.UDP_PortLabel = new System.Windows.Forms.Label();
            this.UDP_PortTextBox = new System.Windows.Forms.TextBox();
            this.TCP_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(385, 40);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(93, 38);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Start Server";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // TCP_PortTextBox
            // 
            this.TCP_PortTextBox.Location = new System.Drawing.Point(59, 92);
            this.TCP_PortTextBox.MaxLength = 65535;
            this.TCP_PortTextBox.Name = "TCP_PortTextBox";
            this.TCP_PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.TCP_PortTextBox.TabIndex = 0;
            this.TCP_PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TCP_PortTextBox_KeyPress);
            this.TCP_PortTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TCP_PortTextBox_KeyUp);
            // 
            // IPLabel
            // 
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel.Location = new System.Drawing.Point(112, 5);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(174, 65);
            this.IPLabel.TabIndex = 2;
            this.IPLabel.Text = "IP Address Here";
            this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TCP_PortLabel
            // 
            this.TCP_PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCP_PortLabel.Location = new System.Drawing.Point(19, 143);
            this.TCP_PortLabel.Name = "TCP_PortLabel";
            this.TCP_PortLabel.Size = new System.Drawing.Size(174, 53);
            this.TCP_PortLabel.TabIndex = 3;
            this.TCP_PortLabel.Text = "Port Here";
            this.TCP_PortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TCP_AvailablePortsLabel
            // 
            this.TCP_AvailablePortsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCP_AvailablePortsLabel.Location = new System.Drawing.Point(59, 115);
            this.TCP_AvailablePortsLabel.Name = "TCP_AvailablePortsLabel";
            this.TCP_AvailablePortsLabel.Size = new System.Drawing.Size(100, 23);
            this.TCP_AvailablePortsLabel.TabIndex = 4;
            this.TCP_AvailablePortsLabel.Text = "(1 - 65535)";
            this.TCP_AvailablePortsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DebugButton
            // 
            this.DebugButton.Location = new System.Drawing.Point(385, 105);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(93, 38);
            this.DebugButton.TabIndex = 3;
            this.DebugButton.Text = "Debug Info";
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
            // 
            // UDP_AvailablePortsLabel
            // 
            this.UDP_AvailablePortsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UDP_AvailablePortsLabel.Location = new System.Drawing.Point(241, 115);
            this.UDP_AvailablePortsLabel.Name = "UDP_AvailablePortsLabel";
            this.UDP_AvailablePortsLabel.Size = new System.Drawing.Size(100, 23);
            this.UDP_AvailablePortsLabel.TabIndex = 8;
            this.UDP_AvailablePortsLabel.Text = "(1 - 65535)";
            this.UDP_AvailablePortsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UDP_PortLabel
            // 
            this.UDP_PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UDP_PortLabel.Location = new System.Drawing.Point(199, 143);
            this.UDP_PortLabel.Name = "UDP_PortLabel";
            this.UDP_PortLabel.Size = new System.Drawing.Size(174, 53);
            this.UDP_PortLabel.TabIndex = 7;
            this.UDP_PortLabel.Text = "Port Here";
            this.UDP_PortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UDP_PortTextBox
            // 
            this.UDP_PortTextBox.Location = new System.Drawing.Point(241, 92);
            this.UDP_PortTextBox.MaxLength = 65535;
            this.UDP_PortTextBox.Name = "UDP_PortTextBox";
            this.UDP_PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.UDP_PortTextBox.TabIndex = 1;
            this.UDP_PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UDP_PortTextBox_KeyPress);
            this.UDP_PortTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UDP_PortTextBox_KeyUp);
            // 
            // TCP_label
            // 
            this.TCP_label.AutoSize = true;
            this.TCP_label.Location = new System.Drawing.Point(95, 76);
            this.TCP_label.Name = "TCP_label";
            this.TCP_label.Size = new System.Drawing.Size(28, 13);
            this.TCP_label.TabIndex = 9;
            this.TCP_label.Text = "TCP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "UDP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 200);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TCP_label);
            this.Controls.Add(this.UDP_AvailablePortsLabel);
            this.Controls.Add(this.UDP_PortLabel);
            this.Controls.Add(this.UDP_PortTextBox);
            this.Controls.Add(this.DebugButton);
            this.Controls.Add(this.TCP_AvailablePortsLabel);
            this.Controls.Add(this.TCP_PortLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.TCP_PortTextBox);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.Text = "TCPCommander Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox TCP_PortTextBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label TCP_PortLabel;
        private System.Windows.Forms.Label TCP_AvailablePortsLabel;
        private System.Windows.Forms.Button DebugButton;
        private System.Windows.Forms.Label UDP_AvailablePortsLabel;
        private System.Windows.Forms.Label UDP_PortLabel;
        private System.Windows.Forms.TextBox UDP_PortTextBox;
        private System.Windows.Forms.Label TCP_label;
        private System.Windows.Forms.Label label4;
    }
}

