//角点检测分类：
//<1>基于灰度图像的角点检测
//<2> 基于二值图像的角点检测
//<3>基于轮廓曲线的角点检测
//而基于灰度图像的角点检测又可分为基于梯度、基于模板和基于模板梯度组合三类方法，
//其中基于模板的方法主要考虑像素领域点的灰度变化，即图像亮度的变化，将与邻点亮度对比足够大的点定义为角点。
//常见的基于模板的角点检测算法有Kitchen-Rosenfeld角点检测算法，Harris角点检测算法、KLT角点检测算法及SUSAN角点检测算法。
//和其他角点检测算法相比，SUSAN角点检测算法具有算法简单、位置准确、抗噪声能力强等特点。

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormCorner : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Gray, byte> src;

        public FormCorner()
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
                src = new Image<Gray, byte>(open.FileName);
            }
        }
        int thresh = 30; //当前阈值  
        int max_thresh = 175; //最大阈值 

        private void button2_Click(object sender, EventArgs e)
        {
            //---------------------------【1】定义一些局部变量-----------------------------  
            Image<Gray,float> dstImage=new Image<Gray, float>(src.Size);//目标图  
            Mat normImage=new Mat();//归一化后的图  
            Image<Gray,byte> scaledImage=new Image<Gray, byte>(src.Size);//线性变换后的八位无符号整型的图  

            //---------------------------【2】初始化---------------------------------------  
            //置零当前需要显示的两幅图，即清除上一次调用此函数时他们的值  
 
            Image<Gray, byte> g_srcImage1 = src.Clone();

            //---------------------------【3】正式检测-------------------------------------  
            //进行角点检测  点检测传出的为Float类型的数据
            CvInvoke.CornerHarris(src, dstImage, 2);
           
            // 归一化与转换  
            CvInvoke.Normalize(dstImage, normImage, 0, 255,  Emgu.CV.CvEnum.NormType.MinMax);
            double min = 0, max = 0;
            Point minp = new Point(0, 0);
            Point maxp = new Point(0, 0);
            CvInvoke.MinMaxLoc(normImage, ref min, ref max, ref minp, ref maxp);
            double scale = 255 / (max - min);
            double shift = min * scale;
            CvInvoke.ConvertScaleAbs(normImage, scaledImage, scale, shift);//将归一化后的图线性变换成8位无符号整型   

            //---------------------------【4】进行绘制-------------------------------------  
            // 将检测到的，且符合阈值条件的角点绘制出来  
            byte[] data = scaledImage.Bytes;
            for (int j = 0; j < normImage.Rows; j++)
            {
                for (int i = 0; i < normImage.Cols; i++)
                {
                    int k = i * src.Width + j;
                    if (k < data.Length)
                    {
                        if (data[k] > thresh)
                        {
                            CvInvoke.Circle(g_srcImage1, new Point(i, j), 5, new MCvScalar(10, 10, 255), 2);
                            CvInvoke.Circle(scaledImage, new Point(i, j), 5, new MCvScalar(0, 10, 255), 2);
                        }
                    }
                }
            }
            imageBox1.Image=g_srcImage1;
            imageBox2.Image = scaledImage;
        }





    }
}
