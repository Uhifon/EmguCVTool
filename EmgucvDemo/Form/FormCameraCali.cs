using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;

namespace EmgucvDemo
{
    public partial class FormCameraCali : Form
    {
        
        public FormCameraCali()
        {
            InitializeComponent();
        }
        Mat[] imgs = new Mat[3];
        private void button1_Click(object sender, EventArgs e)
        {         
            Image[] images=new Image[3];
            imgs[0] = CvInvoke.Imread(@"E:\工作\项目\3.云台自动化线\胶水检测\点胶照片\1.bmp");
            imgs[1] = CvInvoke.Imread(@"E:\工作\项目\3.云台自动化线\胶水检测\点胶照片\2.bmp");
            imgs[2] = CvInvoke.Imread(@"E:\工作\项目\3.云台自动化线\胶水检测\点胶照片\3.bmp");
            imageList1.ColorDepth = ColorDepth.Depth24Bit;
            imageList1.Images.Add(imgs[0].Bitmap) ;
            imageList1.Images.Add(imgs[1].Bitmap);
            imageList1.Images.Add(imgs[2].Bitmap);

            pictureBox1.Image = imageList1.Images[0];
            pictureBox2.Image = imageList1.Images[1];
            pictureBox3.Image = imageList1.Images[2];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[1];
            pictureBox2.Image = imageList1.Images[2];
            pictureBox3.Image = imageList1.Images[0];
        }
    }
}
