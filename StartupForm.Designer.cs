namespace Confetti
{
    partial class StartupForm
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
            this.StartButton = new System.Windows.Forms.Button();
            this.AddNode = new System.Windows.Forms.Button();
            this.IpEntry = new System.Windows.Forms.TextBox();
            this.IpDisplay = new System.Windows.Forms.ListBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.srcPortBox = new System.Windows.Forms.TextBox();
            this.dstPortBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.reqIdBox = new System.Windows.Forms.TextBox();
            this.outgoingIdBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.filenameBox = new System.Windows.Forms.TextBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(13, 372);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(260, 20);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start!";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddNode
            // 
            this.AddNode.Location = new System.Drawing.Point(197, 12);
            this.AddNode.Name = "AddNode";
            this.AddNode.Size = new System.Drawing.Size(75, 23);
            this.AddNode.TabIndex = 1;
            this.AddNode.Text = "Add Name";
            this.AddNode.UseVisualStyleBackColor = true;
            this.AddNode.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // IpEntry
            // 
            this.IpEntry.Location = new System.Drawing.Point(13, 14);
            this.IpEntry.Name = "IpEntry";
            this.IpEntry.Size = new System.Drawing.Size(178, 20);
            this.IpEntry.TabIndex = 2;
            this.IpEntry.Text = "0.0.0.0";
            // 
            // IpDisplay
            // 
            this.IpDisplay.FormattingEnabled = true;
            this.IpDisplay.Location = new System.Drawing.Point(13, 41);
            this.IpDisplay.Name = "IpDisplay";
            this.IpDisplay.Size = new System.Drawing.Size(259, 134);
            this.IpDisplay.TabIndex = 3;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(13, 182);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "Remove Selected";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // srcPortBox
            // 
            this.srcPortBox.Location = new System.Drawing.Point(91, 213);
            this.srcPortBox.Name = "srcPortBox";
            this.srcPortBox.Size = new System.Drawing.Size(100, 20);
            this.srcPortBox.TabIndex = 5;
            this.srcPortBox.Text = "11001";
            // 
            // dstPortBox
            // 
            this.dstPortBox.Location = new System.Drawing.Point(91, 239);
            this.dstPortBox.Name = "dstPortBox";
            this.dstPortBox.Size = new System.Drawing.Size(100, 20);
            this.dstPortBox.TabIndex = 6;
            this.dstPortBox.Text = "11000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Source Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dest. Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Request ID";
            // 
            // reqIdBox
            // 
            this.reqIdBox.Location = new System.Drawing.Point(94, 343);
            this.reqIdBox.Name = "reqIdBox";
            this.reqIdBox.Size = new System.Drawing.Size(27, 20);
            this.reqIdBox.TabIndex = 9;
            this.reqIdBox.Text = "8";
            // 
            // outgoingIdBox
            // 
            this.outgoingIdBox.Location = new System.Drawing.Point(94, 317);
            this.outgoingIdBox.Name = "outgoingIdBox";
            this.outgoingIdBox.Size = new System.Drawing.Size(27, 20);
            this.outgoingIdBox.TabIndex = 11;
            this.outgoingIdBox.Text = "8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "OutgoingID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Filename";
            // 
            // filenameBox
            // 
            this.filenameBox.Location = new System.Drawing.Point(94, 292);
            this.filenameBox.Name = "filenameBox";
            this.filenameBox.Size = new System.Drawing.Size(85, 20);
            this.filenameBox.TabIndex = 13;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(185, 289);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 15;
            this.OpenButton.Text = "Open...";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 404);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.filenameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.outgoingIdBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reqIdBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dstPortBox);
            this.Controls.Add(this.srcPortBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.IpDisplay);
            this.Controls.Add(this.IpEntry);
            this.Controls.Add(this.AddNode);
            this.Controls.Add(this.StartButton);
            this.Name = "StartupForm";
            this.Text = "Startup Dialog";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button AddNode;
        private System.Windows.Forms.TextBox IpEntry;
        private System.Windows.Forms.ListBox IpDisplay;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TextBox srcPortBox;
        private System.Windows.Forms.TextBox dstPortBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox reqIdBox;
        private System.Windows.Forms.TextBox outgoingIdBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox filenameBox;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}