using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormFilter : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;

        int g_nBoxFilterValue = 5;         //方框滤波内核值  
        int g_nBlurValue = 5;             //均值滤波内核值  
        int g_nGaussianBlurValue = 5;      //高斯滤波内核值  
        int g_nMedianBlurValue = 5;       //中值滤波参数值  
        int g_nBilateralFilterValue = 10;  //双边滤波参数值 

        public FormFilter()
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
            Image<Bgr, byte> dst = src.CopyBlank();
            CvInvoke.BoxFilter(src,dst,Emgu.CV.CvEnum.DepthType.Default,new Size(g_nBoxFilterValue, g_nBoxFilterValue),new Point(-1,-1));
            //第一个参数，InputArray类型的src，输入图像，即源图像。
            //第二个参数，OutputArray类型的dst，即目标图像，需要和源图片有一样的尺寸和类型。
            //第三个参数，int类型的ddepth，输出图像的深度，-1代表使用原图深度，即src.depth()。
            //第四个参数，Size类型的ksize，内核的大小。一般这样写Size(w, h)来表示内核的大小(其中，w 为像素宽度， h为像素高度)。Size（3,3）就表示3x3的核大小，Size（5,5）就表示5x5的核大小，也就是滤波器模板的大小。
            //第五个参数，Point类型的anchor，表示锚点（即被平滑的那个点），默认值为Point(-1, -1)。如果这个点坐标是负值的话，就表示取核的中心为锚点，所以默认值Point(-1, -1)表示这个锚点在核的中心。
            //第六个参数，bool类型的normalize，默认值为true，一个标识符，表示内核是否被其区域归一化（normalized）了。
            //第七个参数，int类型的borderType，用于推断图像外部像素的某种边界模式。有默认值BORDER_DEFAULT，我们一般不去管它。
            imageBox2.Image = dst;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> dst = src.CopyBlank();
            CvInvoke.Blur(src, dst, new Size(g_nBlurValue, g_nBlurValue), new Point(-1, -1));
            //第一个参数，InputArray类型的src，输入图像，即源图像，填Mat类的对象即可。该函数对通道是独立处理的，且可以处理任意通道数的图片，但需要注意，待处理的图片深度应该为CV_8U, CV_16U, CV_16S, CV_32F 以及 CV_64F之一。
            //第二个参数，OutputArray类型的dst，即目标图像，需要和源图片有一样的尺寸和类型。比如可以用Mat::Clone，以源图片为模板，来初始化得到如假包换的目标图。
            //第三个参数，Size类型的ksize内核的大小。一般这样写Size(w, h)来表示内核的大小(其中，w 为像素宽度， h为像素高度)。Size（3,3）就表示3x3的核大小，Size（5,5）就表示5x5的核大小
            //第四个参数，Point类型的anchor，表示锚点（即被平滑的那个点），注意他有默认值Point(-1, -1)。如果这个点坐标是负值的话，就表示取核的中心为锚点，所以默认值Point(-1, -1)表示这个锚点在核的中心。
            //第五个参数，int类型的borderType，用于推断图像外部像素的某种边界模式。有默认值BORDER_DEFAULT，我们一般不去管它。
            imageBox2.Image = dst;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> dst = src.CopyBlank();
            CvInvoke.GaussianBlur(src, dst, new Size(g_nGaussianBlurValue, g_nGaussianBlurValue), 0, 0);
            //第一个参数，InputArray类型的src，输入图像，即源图像，填Mat类的对象即可。它可以是单独的任意通道数的图片，但需要注意，图片深度应该为CV_8U,CV_16U, CV_16S, CV_32F 以及 CV_64F之一。
            //第二个参数，OutputArray类型的dst，即目标图像，需要和源图片有一样的尺寸和类型。比如可以用Mat::Clone，以源图片为模板，来初始化得到如假包换的目标图。
            //第三个参数，Size类型的ksize高斯内核的大小。其中ksize.width和ksize.height可以不同，但他们都必须为正数和奇数。或者，它们可以是零的，它们都是由sigma计算而来。
            //第四个参数，double类型的sigmaX，表示高斯核函数在X方向的的标准偏差。
            //第五个参数，double类型的sigmaY，表示高斯核函数在Y方向的的标准偏差。若sigmaY为零，就将它设为sigmaX，如果sigmaX和sigmaY都是0，那么就由ksize.width和ksize.height计算出来。
            //为了结果的正确性着想，最好是把第三个参数Size，第四个参数sigmaX和第五个参数sigmaY全部指定到。
            //第六个参数，int类型的borderType，用于推断图像外部像素的某种边界模式。有默认值BORDER_DEFAULT，我们一般不去管它。     
            imageBox2.Image = dst;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> dst = src.CopyBlank();
            CvInvoke.MedianBlur(src, dst, g_nMedianBlurValue);
            //第一个参数，InputArray类型的src，函数的输入参数，填1、3或者4通道的Mat类型的图像；当ksize为3或者5的时候，图像深度需为CV_8U，CV_16U，或CV_32F其中之一，而对于较大孔径尺寸的图片，它只能是CV_8U。
            //第二个参数，OutputArray类型的dst，即目标图像，函数的输出参数，需要和源图片有一样的尺寸和类型。我们可以用Mat::Clone，以源图片为模板，来初始化得到如假包换的目标图。
            //第三个参数，int类型的ksize，孔径的线性尺寸（aperture linear size），注意这个参数必须是大于1的奇数，比如：3，5，7，9...
            imageBox2.Image = dst;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> dst = src.CopyBlank();
            CvInvoke.BilateralFilter(src, dst, g_nBilateralFilterValue, g_nBilateralFilterValue * 2, g_nBilateralFilterValue / 2);
            //第一个参数，InputArray类型的src，输入图像，即源图像，需要为8位或者浮点型单通道、三通道的图像。
            //第二个参数，OutputArray类型的dst，即目标图像，需要和源图片有一样的尺寸和类型。
            //第三个参数，int类型的d，表示在过滤过程中每个像素邻域的直径。如果这个值我们设其为非正数，那么OpenCV会从第五个参数sigmaSpace来计算出它来。
            //第四个参数，double类型的sigmaColor，颜色空间滤波器的sigma值。这个参数的值越大，就表明该像素邻域内有更宽广的颜色会被混合到一起，产生较大的半相等颜色区域。
            //第五个参数，double类型的sigmaSpace坐标空间中滤波器的sigma值，坐标空间的标注方差。他的数值越大，意味着越远的像素会相互影响，从而使更大的区域足够相似的颜色获取相同的颜色。当d > 0，d指定了邻域大小且与sigmaSpace无关。否则，d正比于sigmaSpace。
            //第六个参数，int类型的borderType，用于推断图像外部像素的某种边界模式。注意它有默认值BORDER_DEFAULT。
            imageBox2.Image = dst;
          
        }
    }
}
