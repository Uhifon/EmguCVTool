using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace EmgucvDemo
{
    public partial class FormEdge : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
 

        public FormEdge()
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
 
            double Thershold1 = Convert.ToDouble(numericUpDown1.Value);
            double Thershold2 = Convert.ToDouble(numericUpDown2.Value);
            Image<Gray, byte> dst_gray = src.Convert<Gray, byte>() ;         
            // 先使用 3x3内核来降噪  
            CvInvoke.Blur(dst_gray, dst_gray, new Size(3, 3),new Point(1,1));
            CvInvoke.Canny(dst_gray, dst_gray, Thershold1, Thershold2);
            //第一个参数，InputArray类型的image，输入图像，即源图像，填Mat类的对象即可，且需为单通道8位图像。
            //第二个参数，OutputArray类型的edges，输出的边缘图，需要和源图片有一样的尺寸和类型。
            //第三个参数，double类型的threshold1，第一个滞后性阈值。
            //第四个参数，double类型的threshold2，第二个滞后性阈值。
            //第五个参数，int类型的apertureSize，表示应用Sobel算子的孔径大小，其有默认值3。
            //第六个参数，bool类型的L2gradient，一个计算图像梯度幅值的标识，有默认值false。

            imageBox2.Image = dst_gray;
        }

        /// <summary>
        /// Sobel边缘检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> srcimage = src.Copy();
            //【1】创建 grad_x 和 grad_y 矩阵  
            Mat grad_x = new Mat();
            Mat grad_y=new Mat();
            Mat abs_grad_x = new Mat();
            Mat abs_grad_y = new Mat();
            Mat dst = new Mat();
            //【2】求 X方向梯度  
            CvInvoke.Sobel(srcimage, grad_x,DepthType.Default, 1, 0);
            CvInvoke.ConvertScaleAbs(grad_x, abs_grad_x,1,0);
            imageBox2.Image = abs_grad_x;

            //【3】求Y方向梯度  
            CvInvoke.Sobel(srcimage, grad_y, DepthType.Default, 0, 1);
           CvInvoke.ConvertScaleAbs(grad_y, abs_grad_y, 1, 0);
            imageBox3.Image = abs_grad_y;

            //【4】合并梯度(近似)  
            CvInvoke.AddWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, dst);
            imageBox4.Image = dst;
        }

        //拉普拉斯锐化
        private void button4_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> srcimage = src.Copy();
            //【0】变量的定义  
            Mat src_gray = new Mat();
            Mat dst = new Mat();
            Mat abs_dst = new Mat() ;

            //【1】使用高斯滤波消除噪声  
            CvInvoke.GaussianBlur(srcimage, srcimage, new Size(3, 3), 0, 0);
            imageBox2.Image = srcimage;
            //【2】转换为灰度图  
            CvInvoke.CvtColor(srcimage, src_gray,ColorConversion.Bgr2Gray);
            imageBox3.Image = src_gray;
            //【3】使用Laplace函数  
            CvInvoke.Laplacian(src_gray, dst,DepthType.Cv16S, 3, 1, 0);
            //第一个参数，InputArray类型的image，输入图像，即源图像，填Mat类的对象即可，且需为单通道8位图像。
            //第二个参数，OutputArray类型的edges，输出的边缘图，需要和源图片有一样的尺寸和通道数。
            //第三个参数，int类型的ddept，目标图像的深度。
            //第四个参数，int类型的ksize，用于计算二阶导数的滤波器的孔径尺寸，大小必须为正奇数，且有默认值1。
            //第五个参数，double类型的scale，计算拉普拉斯值的时候可选的比例因子，有默认值1。
            //第六个参数，double类型的delta，表示在结果存入目标图（第二个参数dst）之前可选的delta值，有默认值0。
            //第七个参数， int类型的borderType，边界模式，默认值为BORDER_DEFAULT。这个参数可以在官方文档中borderInterpolate()处得到更详细的信息。
            //【4】计算绝对值，并将结果转换成8位  
            CvInvoke.ConvertScaleAbs(dst, abs_dst,1,0);
            imageBox4.Image = abs_dst;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //EMGUCV 中未找到Scharr
            ////使用Scharr滤波器运算符计算x或y方向的图像差分。其实它的参数变量和Sobel基本上是一样的，除了没有ksize核的大小
            //Image<Bgr, byte> srcimage = src.Copy();
            ////【1】创建 grad_x 和 grad_y 矩阵  
            //Mat grad_x = new Mat();
            //Mat grad_y = new Mat();
            //Mat abs_grad_x = new Mat();
            //Mat abs_grad_y = new Mat();
            //Mat dst = new Mat();
            ////【2】求 X方向梯度  
            ////Scharr(srcimage, grad_x, DepthType.Default, 1, 0);
            //CvInvoke.ConvertScaleAbs(grad_x, abs_grad_x, 1, 1);
            //imageBox2.Image = abs_grad_x;

            ////【3】求Y方向梯度  
            //CvInvoke.Sobel(srcimage, grad_y, DepthType.Default, 0, 1);
            //CvInvoke.ConvertScaleAbs(grad_y, abs_grad_y, 1, 1);
            //imageBox3.Image = abs_grad_y;

            ////【4】合并梯度(近似)  
            //CvInvoke.AddWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, dst);
            //imageBox4.Image = dst;


        }
    }
}
