/*
仿射变换可以通过一系列的原子变换的复合来实现， 
包括： 平移（Translation）、 缩放（Scale）、 翻转（Flip）、 旋转（Rotation）和错切（Shear）。
*/


using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmgucvDemo
{
    public partial class FormWarpAffine : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        Mat data = new Mat();

        public FormWarpAffine()
        {
            InitializeComponent();
        }
  


        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
                src = new Image<Bgr, byte>(open.FileName);
            }
        }

        //方法1获取仿射矩阵
        private void button2_Click(object sender, EventArgs e)
        {
            PointF[] point_src = new PointF[] { new PointF(0, 0), new PointF(90, 0), new PointF(0, 90)};
            PointF[] point_dst = new PointF[] { new PointF(0, 0), new PointF(0, 90), new PointF(90, 0) };         
            data = CvInvoke.GetAffineTransform(point_src,point_dst);
            float[,,] da = data.ToImage<Gray, float>().Data;
            //获取仿射矩阵的像素值
            for(int i = 0;i< 2;i++)
            {
                for(int j=0;j<3;j++)
                {
                    richTextBox1.Text += da[i, j, 0] + "\t";
                }
                richTextBox1.Text += "\n";
            }

        }



        //方法2计算仿射矩阵 
        private void button4_Click(object sender, EventArgs e)
        {
            
            PointF p = new PointF(Single.Parse(textBox1.Text), Single.Parse(textBox2.Text));
            double angle = Double.Parse(textBox3.Text);
            double scale = Double.Parse(textBox4.Text);
            CvInvoke.GetRotationMatrix2D(p, angle, scale, data);
            float[,,] da = data.ToImage<Gray, float>().Data;
            //获取仿射矩阵的像素值
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    richTextBox2.Text += da[i, j, 0] + "\t";
                }
                richTextBox2.Text += "\n";
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Mat dstImage = new Mat();
            CvInvoke.WarpAffine(src, dstImage,data,src.Size);
            imageBox2.Image = dstImage;
        }
    }
}
