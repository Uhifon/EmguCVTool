using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmgucvDemo
{
    public partial class FormAbsDiff : Form
    {    
        static string curFileNameSrc;
        static string curFileNameDst;
        Bitmap curBitmapSrc;
        Bitmap curBitmapDst;

        public FormAbsDiff()
        {
            InitializeComponent();
        }

        //载入图像img1
        private void button1_Click(object sender, EventArgs e)
        {
            //Create open dialog;
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "All Image files|*.bmp;*.gif;*.jpg;*.ico;*png";
            //Seting the title of dialog;
            opnDlg.Title = "Open Src image files";
            opnDlg.ShowHelp = true;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileNameSrc = opnDlg.FileName;
                try
                {
                    curBitmapSrc = new Bitmap(curFileNameSrc);
                    imageBox1.Image = curBitmapSrc;
                }
                catch
                {
                    MessageBox.Show("programe error");
                }
            }
        }
        //载入图像img2
        private void button2_Click(object sender, EventArgs e)
        {
            //Create open dialog;
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "All Image files|*.bmp;*.gif;*.jpg;*.ico;*png";
            //Seting the title of dialog;
            opnDlg.Title = "Open Dst image files";
            opnDlg.ShowHelp = true;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileNameDst = opnDlg.FileName;
                try
                {
                    curBitmapDst = new Bitmap(curFileNameDst);
                    imageBox2.Image = curBitmapDst;
                }
                catch
                {
                    MessageBox.Show("programe error");
                }
            }
        }
        //相减计算
        private void button3_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> curBitmapSrc_1 = new Image<Gray, Byte>(curBitmapSrc);
            Image<Gray, Byte> curBitmapDst_1= new Image<Gray, Byte>(curBitmapDst);
            Image<Gray, Byte> result = new Image<Gray, Byte>(curBitmapSrc);
            //CvInvoke.cvCopy(img1, img_1, IntPtr.Zero);
            //CvInvoke.cvCopy(img2, img_2, IntPtr.Zero);

           

            result = curBitmapSrc_1.AbsDiff(curBitmapDst_1);
            pictureBox3.Image = result.Bitmap;

        }
    }

}

