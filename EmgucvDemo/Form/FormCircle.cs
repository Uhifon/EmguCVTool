using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormCircle : Form
    {
        Image<Bgr, byte> src;
        Image<Gray, byte> gray_scr;
        static string filename;
        OpenFileDialog open;

        public FormCircle()
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
        private void button2_Click(object sender, EventArgs e)
        {
            gray_scr = src.Convert<Gray, byte>();
            //进行中值滤波去噪
            gray_scr = gray_scr.SmoothMedian(5);    

            //  CvInvoke.EqualizeHist(gray_scr, gray_scr);  //直方图均衡化
            imageBox2.Image = gray_scr;
            CvInvoke.Threshold(gray_scr, gray_scr, Convert.ToInt32(numericUpDown1.Value), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            imageBox3.Image = gray_scr;
            //获取内核，用于形态学操作
            Mat kennel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, new Size(3, 3), new Point(-1, -1));
            ////对图像进行形态闭学开操作
            CvInvoke.MorphologyEx(gray_scr, gray_scr, Emgu.CV.CvEnum.MorphOp.Close, kennel, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            ////腐蚀
            CvInvoke.MorphologyEx(gray_scr, gray_scr, Emgu.CV.CvEnum.MorphOp.Erode, kennel, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            //形态学闭操作
            CvInvoke.MorphologyEx(gray_scr, gray_scr, Emgu.CV.CvEnum.MorphOp.Close, kennel, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            imageBox4.Image = gray_scr;

            //  gray_scr = gray_scr.SmoothMedian(3);
             gray_scr = gray_scr.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 45, new Gray(-1));
            // gray_scr = gray_scr.Dilate(1);

            //canny轮廓检测
            gray_scr = gray_scr.Canny(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown2.Value / 2));
            imageBox5.Image = gray_scr;
            //膨胀(避免轮廓的不连续【重点】
            gray_scr = gray_scr.Dilate(1);
            imageBox5.Image = gray_scr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            CircleF circle=new CircleF();
            VectorOfVectorOfPoint vvp = new VectorOfVectorOfPoint();//存储所有的轮廓
            VectorOfVectorOfPoint cricle_vvp = new VectorOfVectorOfPoint();//存储筛选过后圆的轮廓.

            CvInvoke.FindContours(gray_scr, vvp, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);

            for (int i = 0; i < vvp.Size; i++)//遍历所有轮廓
            {
                double area = CvInvoke.ContourArea(vvp[i]);//假设如果轮廓为圆，即面积为:PI*R^2
                double length = CvInvoke.ArcLength(vvp[i], true);//假设如果轮廓为圆，即周长为:PI*R*2
                double r = (area / length) * 2;//假设如果轮廓为圆: 面积/周长=R/2；即半径为： 面积*2/周长
                double c = (area / (length * length));//假设轮廓为圆： 面积/(周长的平方)=1/(4*PI)常数； 0.0796                 
                if (c > 0.063 && c < 0.08)//满足相应条件
                {
                    if (r < Convert.ToInt32(numericUpDown4.Value) && r > Convert.ToInt32(numericUpDown3.Value))
                    {
                        cricle_vvp.Push(vvp[i]);//提取筛选后的圆轮廓
                         circle = CvInvoke.MinEnclosingCircle(vvp[i]);
                        textBox1.Text += "找到第" + i + "个圆，比例值：" + c + "半径为：" + r + "\r\n"+ "圆心为：" + circle.Center.X+","+ circle.Center.Y+","+ "半径为：" + circle.Radius + "\r\n"; 
                    }
                }
            }

            Mat result = new Mat(gray_scr.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
            result.SetTo(new MCvScalar(0, 0, 0));
            CvInvoke.DrawContours(result, vvp, -1, new MCvScalar(0, 255, 0));
            imageBox6.Image = result;

            if (cricle_vvp.Size == 0)
            {
                textBox1.Text = "没有找到合适的圆！";
            }
            else
            {
                for (int i = 0; i < cricle_vvp.Size; i++)
                {
                    //Rectangle rect = CvInvoke.BoundingRectangle(cricle_vvp[i]);
                    //src.Draw(rect, new Bgr(0, 0, 255), 2);       
                    CvInvoke.Circle(src,new Point((int)circle.Center.X,(int) circle.Center.Y), (int)circle.Radius,new Bgr(0,0,255).MCvScalar,3);

                    CvInvoke.Circle(src,new Point((int)circle.Center.X,(int) circle.Center.Y), 3,new Bgr(0,0,255).MCvScalar,3);
                }
                imageBox1.Image = src;
                imageBox1.Refresh();
            }
        }
    }
}



