namespace EmgucvDemo
{
    partial class FormExtract
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_BlueMin = new System.Windows.Forms.TextBox();
            this.txt_BlueMax = new System.Windows.Forms.TextBox();
            this.txt_GreenMin = new System.Windows.Forms.TextBox();
            this.txt_GreenMax = new System.Windows.Forms.TextBox();
            this.txt_RedMin = new System.Windows.Forms.TextBox();
            this.txt_RedMax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "加载图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(112, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // imageBox2
            // 
            this.imageBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox2.Location = new System.Drawing.Point(369, 132);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(331, 277);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(142, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 45);
            this.button2.TabIndex = 4;
            this.button2.Text = "颜色提取";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(75, 99);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txt_BlueMin
            // 
            this.txt_BlueMin.Location = new System.Drawing.Point(353, 32);
            this.txt_BlueMin.Name = "txt_BlueMin";
            this.txt_BlueMin.Size = new System.Drawing.Size(36, 21);
            this.txt_BlueMin.TabIndex = 6;
            this.txt_BlueMin.Text = "250";
            // 
            // txt_BlueMax
            // 
            this.txt_BlueMax.Location = new System.Drawing.Point(353, 59);
            this.txt_BlueMax.Name = "txt_BlueMax";
            this.txt_BlueMax.Size = new System.Drawing.Size(36, 21);
            this.txt_BlueMax.TabIndex = 7;
            this.txt_BlueMax.Text = "255";
            // 
            // txt_GreenMin
            // 
            this.txt_GreenMin.Location = new System.Drawing.Point(395, 32);
            this.txt_GreenMin.Name = "txt_GreenMin";
            this.txt_GreenMin.Size = new System.Drawing.Size(36, 21);
            this.txt_GreenMin.TabIndex = 8;
            this.txt_GreenMin.Text = "100";
            // 
            // txt_GreenMax
            // 
            this.txt_GreenMax.Location = new System.Drawing.Point(395, 59);
            this.txt_GreenMax.Name = "txt_GreenMax";
            this.txt_GreenMax.Size = new System.Drawing.Size(36, 21);
            this.txt_GreenMax.TabIndex = 9;
            this.txt_GreenMax.Text = "150";
            // 
            // txt_RedMin
            // 
            this.txt_RedMin.Location = new System.Drawing.Point(437, 32);
            this.txt_RedMin.Name = "txt_RedMin";
            this.txt_RedMin.Size = new System.Drawing.Size(36, 21);
            this.txt_RedMin.TabIndex = 10;
            this.txt_RedMin.Text = "50";
            // 
            // txt_RedMax
            // 
            this.txt_RedMax.Location = new System.Drawing.Point(437, 59);
            this.txt_RedMax.Name = "txt_RedMax";
            this.txt_RedMax.Size = new System.Drawing.Size(36, 21);
            this.txt_RedMax.TabIndex = 11;
            this.txt_RedMax.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(350, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "blue";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(392, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "green";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(441, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "red";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(315, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "min";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(315, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "max";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(27, 132);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 277);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // FormExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 458);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_RedMax);
            this.Controls.Add(this.txt_RedMin);
            this.Controls.Add(this.txt_GreenMax);
            this.Controls.Add(this.txt_GreenMin);
            this.Controls.Add(this.txt_BlueMax);
            this.Controls.Add(this.txt_BlueMin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "FormExtract";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormExtract";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_BlueMin;
        private System.Windows.Forms.TextBox txt_BlueMax;
        private System.Windows.Forms.TextBox txt_GreenMin;
        private System.Windows.Forms.TextBox txt_GreenMax;
        private System.Windows.Forms.TextBox txt_RedMin;
        private System.Windows.Forms.TextBox txt_RedMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}