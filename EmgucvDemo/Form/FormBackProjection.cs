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
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace EmgucvDemo
{
    public partial class FormBackProjection : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> bgrImage;
        Image<Hsv, byte> hsvImage;
        Image<Hsv, byte> hueImage;
        int g_bins = 30;//直方图组距

        public FormBackProjection()
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
                bgrImage = new Image<Bgr, byte>(open.FileName);
                hsvImage = bgrImage.Convert<Hsv, byte>();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int[] ch = { 0, 0 };
            CvInvoke.MixChannels(hsvImage,hueImage,ch); 
            //【2】直方图计算，并归一化                  
            float[] hue_range = new float[2] { 0.00f, 180.00f };          
            Mat hist = new Mat(hueImage.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            int[] channels = new int[1] { 0 };
            int[] histSize = new int[1] { Math.Max(g_bins, 2) };
            float[] ranges = new float[2] { 0, 180 };
            CvInvoke.CalcHist(hueImage, channels,null, hist, histSize, ranges, true);
            CvInvoke.Normalize(hist, hist,0,255,NormType.MinMax,DepthType.Default,null);
            //【3】计算反向投影
            Mat backproj=new Mat();
            CvInvoke.CalcBackProject(hueImage,channels,hist,backproj,ranges,1);
            //【4】显示反向投影
            imageBox2.Image = backproj;
            //【5】绘制直方图
            Image<Gray, float> img = new Image<Gray, float>(hist.Bitmap);
            float[] data = new float[img.Data.Length];
            for (int i = 0; i < img.Data.Length; i++)
            {
                data[i] = img.Data[i, 0, 0];
            }
            float max = data[0];
            //获取最大值
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }
            Image<Bgr, byte> image = new Image<Bgr, byte>(300, 300, new Bgr(0, 0, 0));

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] * 256 / max;
                image.Draw(new LineSegment2DF(new PointF(i + 20, 255), new PointF(i + 21, 255 - data[i])), new Bgr(255, 255, 255), 2);
            }
            imageBox3.Image = image;
        }
    }
}
