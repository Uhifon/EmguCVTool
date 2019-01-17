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
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;


namespace EmgucvDemo
{
    public partial class FormImageSegment : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> srcImage;

        //成员变量
        private string sourceImageFileName = "wky_tms_2272x1704.jpg"; //源图像文件名
        private Image<Bgr, Byte> imageSource = null; //源图像
        private Image<Bgr, Byte> imageSourceClone = null; //源图像的克隆
        private Image<Gray, Int32> imageMarkers = null; //标记图像
        private double xScale = 1d; //原始图像与PictureBox 在x 轴方向上的缩放
        private double yScale = 1d; //原始图像与PictureBox 在y 轴方向上的缩放
        private Point previousMouseLocation = new Point(-1, -1); //上次绘制线条时，鼠标所处的位置 private const int LineWidth = 5; //绘制线条的宽度
        private int drawCount = 1; //用户绘制的线条数目，用于指定线条的颜色

        public FormImageSegment()
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
                srcImage = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image<Gray, Int32> imageMarker = new Image<Gray, Int32>(srcImage.Size);
            CvInvoke.Watershed(srcImage, imageMarker);
            imageBox2.Image = imageMarker;


        }

        private void FormImageSegment_Load(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
 

        }
    }
}
