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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormFloodFill : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;

        public FormFloodFill()
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
            FloodFilling();
        }

        private void FloodFilling()
        {
            Image<Gray, byte> gray = src.Convert<Gray,byte>();
      //      CvInvoke.Threshold(gray, gray, 100, 255, Emgu.CV.CvEnum.ThresholdType.Binary);         
            Rectangle rect = new Rectangle();
            Mat mask=new Mat(src.Rows + 2,src.Cols + 2, DepthType.Cv8U,1); //初始化掩模mask
            CvInvoke.FloodFill(gray,                 //输入/输出1通道或3通道，8位或浮点图像。
                                mask,               //掩模,。它应该为单通道、8位、长和宽上都比输入图像 image 大两个像素点的图像
                                new Point(50, 100), //漫水填充算法的起始点
                                new MCvScalar(0, 0, 0),//像素点被染色的值，即在重绘区域像素的新值
                                out rect,               //用于设置floodFill函数将要重绘区域的最小边界矩形区域
                                new MCvScalar(0, 0, 0),//表示当前观察像素值与其部件邻域像素值或者待加入该部件的种子像素之间的亮度或颜色之负差（lower brightness/color difference）的最大值
                                new MCvScalar(100, 100, 100),//表示当前观察像素值与其部件邻域像素值或者待加入该部件的种子像素之间的亮度或颜色之正差（lower brightness/color difference）的最大值
                                Connectivity.EightConnected,
                                FloodFillType.Default       //操作标志符
                                );//将种子点所在的连通域内的
                            //所有区域变成newVal

            imageBox2.Image = gray;
        }

    }
}
