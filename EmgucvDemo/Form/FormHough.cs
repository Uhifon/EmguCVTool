using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormHough : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        Image<Bgr, byte> src2;

        public FormHough()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                pictureBox1.Load(filename);
                src = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void btn_FindCycle_Click(object sender, EventArgs e)
        {
            if (src == null)
                return;
            textBox1.Text = null;
            double dp = Convert.ToDouble(numericUpDown1.Value); ;
            double minDist = Convert.ToDouble(numericUpDown2.Value);
            double cannyThreshold = Convert.ToDouble(numericUpDown3.Value);
            double circleAccumulatorThreshold = Convert.ToDouble(numericUpDown4.Value);
            int minRadius = Convert.ToInt32(numericUpDown5.Value);
            int maxRadius = Convert.ToInt32(numericUpDown6.Value);

            Image<Gray, byte> image_gray = src.Convert<Gray, byte>();
            CircleF[] circles = CvInvoke.HoughCircles(image_gray, HoughType.Gradient, dp, minDist, cannyThreshold, circleAccumulatorThreshold, minRadius, maxRadius);

            foreach (CircleF circle in circles)
            {
                CvInvoke.Circle(image_gray, Point.Round(circle.Center), (int)circle.Radius, new Bgr(Color.Red).MCvScalar, 3);
                pictureBox1.Image = image_gray.Bitmap;      
                pictureBox1.Refresh();
            }
            textBox1.Text = "共检测到" + circles.Length + "个圆！" + "\r\n";
            for (int i = 0; i < circles.Length; i++)
            {
                textBox1.Text += "第"+i+"个圆的圆心坐标：("+circles[i].Center.X +","+ circles[i].Center.Y+")"+"半径："+ circles[i].Radius+"\r\n";
            }
        }

 

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            double Thershold1 = Convert.ToDouble(numericUpDown8.Value);
            double Thershold2 = Convert.ToDouble(numericUpDown9.Value);
            Image<Gray, byte> imagedst=new Image<Gray, byte>(src.Size);
          
            //CvInvoke.Threshold(imagesrc, imagedst, Thershold1, 255, ThresholdType.Binary);
            CvInvoke.Canny(src, imagedst, Thershold1, Thershold2);
            //CvInvoke.CvtColor(imagedst, imagedst, ColorConversion.BayerBg2Gray);//灰度化
            pictureBox2.Image = imagedst.Bitmap;
            VectorOfInt vp = new VectorOfInt();
            double rho = Convert.ToDouble(numericUpDown7.Value);
            int HoughLinesThershold = Convert.ToInt32(numericUpDown12.Value);//大于此阈值的交点，才会被认为是一条直线 
            double minLineLenth = Convert.ToDouble(numericUpDown11.Value);
            double maxGrap = Convert.ToDouble(numericUpDown10.Value);
            try
            {
                // CvInvoke.HoughLines(imagedst, vp, rho, theta, HoughLinesThershold, minLineLenth, maxGrap);
                // LineSegment2D[][] _lines = imagedst.HoughLines(100,100, rho, theta,HoughLinesThershold,minLineLenth,maxGrap);
                //for(int i=0;i<_lines[0].Length;i++)
                //{                  
                //    //for(int j = 0; j < _lines[0].Length; j++)
                //    CvInvoke.Line(src, _lines[0][i].P1, _lines[0][i].P2, new Bgr(0, 0, 255).MCvScalar, 2);//在原图像中画线
                //}

                LineSegment2D[] lines = CvInvoke.HoughLinesP(imagedst,//8位单通道图像
                                                            rho, //距离分辨率
                                                            Math.PI / 180, //角度分辨率
                                                            HoughLinesThershold,//交点个数阈值
                                                            minLineLenth,//最小直线长度
                                                            maxGrap//最大直线间隙,间隙大于该值，则被认为是两条线段，否则是一条
                                                            );
                textBox1.Text = "总共找到" + lines.Length + "根直线。" + "\r\n";
                src2 = src.Copy();
                for (int i = 0; i < lines.Length; ++i)
                {
                    CvInvoke.Line(src2, lines[i].P1, lines[i].P2, new Bgr(0, 0, 255).MCvScalar, 3);//在原图像中画线
                    double angle = Math.Atan2(lines[i].P1.Y - lines[i].P2.Y, lines[i].P1.X - lines[i].P2.X);
                    textBox1.Text += ("第" + i + "根直线的角度：" + angle + "\r\n").ToString();
                }

            }
            catch
            {
                throw;
            }
            pictureBox1.Image = src2.Bitmap;
        }

        private void FormHough_Load(object sender, EventArgs e)
        {

        }
    }
    
}
