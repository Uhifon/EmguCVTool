/*
灰度直方图
灰度直方图是关于灰度级分布的函数， 是对图像中灰度级分布的统计。
灰度直方图是将数字图像中的所有像素， 按照灰度值的大小， 统计其出现的
频率。 灰度直方图是灰度级的函数， 它表示图像中具有某种灰度级的像素的
个数， 反映了图像中某种灰度出现的频率。 在图像处理中能够客观反映照片
的曝光程度。 肉眼主观的反映图像的暗度， 中间调及高光等色调是非常困难
的， 所以直方图的使用是不可避免的。
直方图分布特点
1 . 曝光不足的照片的直方图： 此类型的直方图曲线偏重于左侧， 多数的像
素集中在左侧，而右侧的像素很少，并且 200 左右到 255 之间很少甚至一片空白。
这种图片看过去过于暗淡， 暗的部位过多， 亮度不足。
2.曝光过度的直方图： 与曝光不足形成对比， 曲线偏重于右侧， 多数像素集
中右侧， 而左侧的像素很少， 0 到 50 左右的曲线值较低。 这样的图片亮度过高。
3.灰蒙蒙的照片： 直方图的像素主要集中在中间， 凸起状态。 两边的像素值
都偏低， 这样的照片看上去模糊， 灰蒙蒙。
4.高反差直方图： 中间凹下去， 两边凸起， 这种类型的图片看上去明亮反差
比较大。
EmguCv 直方图实现
EmguCv 实现直方图相对来说是比较简单的， 实现方法种类也比较多。 在第一133
章中。 相信大家还记得如何添加 Emgu.CV.UI 中的控件。 HistogramBox 控件专门
用于直方图的显示。 HistogramViewer 类也可以显示直方图， 这个类在命名空间
Emgu.CV.UI 中， 同时也可以采用函数 DenseHistogram()计算直方图， 再进行绘
画出直方图。
*/


using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace EmgucvDemo
{
    public partial class FormHistogram : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        Image<Bgr, byte> src1;
        Image<Bgr, byte> src2;

        Emgu.CV.CvEnum.HistogramCompMethod[] his = new Emgu.CV.CvEnum.HistogramCompMethod[6]
        {
            Emgu.CV.CvEnum.HistogramCompMethod.Correl,
            Emgu.CV.CvEnum.HistogramCompMethod.Chisqr,
            Emgu.CV.CvEnum.HistogramCompMethod.Intersect,
            Emgu.CV.CvEnum.HistogramCompMethod.Bhattacharyya,
            Emgu.CV.CvEnum.HistogramCompMethod.Hellinger,
            Emgu.CV.CvEnum.HistogramCompMethod.ChisqrAlt
        };

        public FormHistogram()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
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
            //删除所有控制
            histogramBox1.ClearHistogram();
            //获取直方图
            histogramBox1.GenerateHistograms(src,256);
            histogramBox1.Refresh();
            histogramBox1.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {       
            Image<Bgr, byte> image = new Image<Bgr, byte>(300, 300, new Bgr(0, 0, 0));
            Image<Gray, byte> gray_image = src.Convert<Gray, byte>();
            //直方图计算1
            DenseHistogram dense = new DenseHistogram(256, new RangeF(0, 255));
            dense.Calculate(new Image<Gray, Byte>[] { gray_image },//需要计算的图像的数组。
                true,//如果这是 true,直方图不是一开始确定的。 这个特性允许用户来计算一个直方图从几张图片, 或者更新直方图。 
                null//蒙版
                );
            float[] data = dense.GetBinValues(); //获得数据。 返回 float[] 数组。

            //// 直方图计算方法2
            //Mat srcHist = new Mat(gray_image.Size,Emgu.CV.CvEnum.DepthType.Cv8U,1);
            //int[] channels = new int[1] {0};
            //int[] histSize = new int[1] {256};
            //float[] Ranges = new float[2] { 0, 255 };
            //CvInvoke.CalcHist(gray_image, channels, //图像的通道，它是一个数组，如果是灰度图像则channels[1]={0};如果是彩色图像则channels[3]={0,1,2}；如果是只是求彩色图像第2个通道的直方图，则channels[1]={1};
            //    null, srcHist,//计算得到的直方图
            //    histSize, //直方图横坐标的区间数。如果是10，则它会横坐标分为10份，然后统计每个区间的像素点总和。
            //    Ranges, //这是一个二维数组，用来指出每个区间的范围。
            //    true);
            //Image<Gray, float> img = new Image<Gray, float>(srcHist.Bitmap);
            //float[] data=new float[img.Data.Length];
            //for (int i = 0; i < img.Data.Length; i++)
            //{
            //      data[i] = img.Data[i,0,0];
            //}

            float max = data[0];
            //获取最大值
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] * 256 / max;
                image.Draw(new LineSegment2DF(new PointF(i + 20, 255), new PointF(i + 21, 255 - data[i])), new Bgr(255, 255, 255), 2);
            }
            imageBox2.Image = image;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox3.Load(filename);
                src1 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox4.Load(filename);
                src2 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            //在比较直方图时，最佳操作是在HSV空间中操作，所以需要将BGR空间转换为HSV空间  
            Mat srcHsvImage = new Mat(CvInvoke.cvGetSize(src1), DepthType.Cv8U, 3);
            Mat compareHsvImage = new Mat(CvInvoke.cvGetSize(src2), DepthType.Cv8U, 3);
            CvInvoke.CvtColor(src1, srcHsvImage, ColorConversion.Bgr2Hsv);
            CvInvoke.CvtColor(src2, compareHsvImage, ColorConversion.Bgr2Hsv);
           
            //采用H-S直方图进行处理  
            //首先得配置直方图的参数  
            Mat srcHist = new Mat(CvInvoke.cvGetSize(src1), DepthType.Cv8U, 3);
            Mat compHist = new Mat(CvInvoke.cvGetSize(src2), DepthType.Cv8U, 3);
            //H、S通道  
            int[] channels = new int[2] { 0, 1 };
            int[] histSize = new int[2] { 30, 32 };
            float[] Ranges = new float[2] { 0, 180 };
            
            //进行原图直方图的计算  
            CvInvoke.CalcHist(srcHsvImage, channels, null, srcHist, histSize, Ranges, true);
            //对需要比较的图进行直方图的计算  
            CvInvoke.CalcHist(compareHsvImage, channels, null, compHist, histSize, Ranges, true);

            //注意：这里需要对两个直方图进行归一化操作  
            CvInvoke.Normalize(srcHist, srcHist, 1, 0, NormType.MinMax);
            CvInvoke.Normalize(compHist, compHist, 1, 0, NormType.MinMax);

            //对得到的直方图对比  
            //相关：CV_COMP_CORREL      
            //卡方：CV_COMP_CHISQR  
            //直方图相交：CV_COMP_INTERSECT  
            //Bhattacharyya距离：CV_COMP_BHATTACHARYYA  
            double g_dCompareRecult = CvInvoke.CompareHist(srcHist, compHist, his[comboBox1.SelectedIndex]);
            richTextBox1.Text = "方法 " + comboBox1.SelectedIndex + "：两幅图像比较的结果为：" + g_dCompareRecult+"\r\n";
        }

        private void FormHistogram_Load(object sender, EventArgs e)
        {

        }
    }
}
