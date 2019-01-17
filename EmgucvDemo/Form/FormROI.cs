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
    public partial class FormROI : Form
    {
        static string filename;
        OpenFileDialog open;
        Mat image;
        public FormROI()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                pictureBox1.Load(filename);
                image = new Mat(open.FileName,ImreadModes.AnyColor);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Size maksSize = new Size(image.Width+2,image.Height+2);
            Mat mask = new Mat (maksSize, DepthType.Cv8U,1);
            Point center = new Point(200,200); ;
            int radius = 100;
            CvInvoke.Circle(mask, center, radius, new MCvScalar(255,255,255),2);
            pictureBox2.Image = mask.Bitmap;   
            Point seed = new Point(center.X+1, center.Y+1);
            Rectangle rect = new Rectangle();
            //漫水填充
            //pi的值表示为 v(pi),if  v(seed)-loDiff<v(pi)<v(seed)+upDiff,将pi的值设置为newVal
            int area=   CvInvoke.FloodFill(image,                 //输入/输出1通道或3通道，8位或浮点图像。
                            mask,                 //掩模,。它应该为单通道、8位、长和宽上都比输入图像 image 大两个像素点的图像
                            seed,                  //漫水填充算法的起始点
                            new MCvScalar(255,255,255),//像素点被染色的值，即在重绘区域像素的新值
                            out rect,               //用于设置floodFill函数将要重绘区域的最小边界矩形区域
                            new MCvScalar(0,0,0),//表示当前观察像素值与其部件邻域像素值或者待加入该部件的种子像素之间的亮度或颜色之负差（lower brightness/color difference）的最大值
                            new MCvScalar(0,0,0),//表示当前观察像素值与其部件邻域像素值或者待加入该部件的种子像素之间的亮度或颜色之正差（lower brightness/color difference）的最大值
                            Connectivity.EightConnected,
                            FloodFillType.FixedRange       //操作标志符
                            );//将种子点所在的连通域内的
                              //所有区域变成newVal
                             //mask.(rect).SetTo(new MCvScalar(255));
         
            pictureBox1.Image = image.Bitmap;
            pictureBox1.Refresh();
            Mat maskImage = new Mat();
            image.CopyTo(maskImage, image);
            pictureBox3.Image = maskImage.Bitmap;
        }
    }
}
