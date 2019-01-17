using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;

namespace EmgucvDemo
{
    public partial class FormBlob : Form
    {
        public FormBlob()
        {
            InitializeComponent();
        }
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> img;

        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img = new Image<Bgr, byte>(open.FileName);
            Image<Gray, byte> imgGray = img.Convert<Gray, byte>();
            /* blob detector */
            //Gray Gavg = imgGray.GetAverage();
            double minValue = 255;
            double maxValue = 0;
            Point minLoc = new Point();
            Point maxLoc = new Point();
            CvInvoke.MinMaxLoc(imgGray, ref minValue, ref maxValue, ref minLoc, ref maxLoc);

            SimpleBlobDetectorParams blobparams = new SimpleBlobDetectorParams();
            blobparams.FilterByArea = true; //斑点面积的限制变量  
            blobparams.MinArea = 2000;// 斑点的最小面积
            blobparams.MaxArea = 300000;// 斑点的最大面积
            blobparams.MinThreshold = (float)minValue + 1;  //二值化的起始阈值，即公式1的T1 
            blobparams.MaxThreshold = (float)maxValue;   //二值化的终止阈值，即公式1的T2  
            blobparams.FilterByCircularity = true;  ////斑点圆度的限制变量，默认是不限制  
            blobparams.MinCircularity = (float)0.5;//斑点的最小圆度  
            blobparams.MaxCircularity = 1;//斑点的最大圆度  
            blobparams.FilterByConvexity = true;    //斑点凸度的限制变量 
            blobparams.MinConvexity = (float)0.8;//斑点的最小凸度  
            blobparams.MaxConvexity = 10;//斑点的最大凸度  
            blobparams.FilterByInertia = true;  // //斑点惯性率的限制变量  
            blobparams.MinInertiaRatio = (float)0.4;//斑点的最小惯性率  
            blobparams.MaxInertiaRatio = 1;//斑点的最大惯性率  
            blobparams.FilterByColor = false; //斑点颜色的限制变量 
            blobparams.blobColor = 255; //斑点颜色的限制变量 
            blobparams.ThresholdStep = 135;//二值化的阈值步长，即公式1的t  
            blobparams.MinRepeatability = new IntPtr(2); //重复的最小次数，只有属于灰度图像斑点的那些二值图像斑点数量大于该值时，该灰度图像斑点才被认为是特征点  
            SimpleBlobDetector detector = new SimpleBlobDetector(blobparams);

            MKeyPoint[] keypoints = detector.Detect(imgGray);
            Image<Bgr, byte> imgBgr = img.Copy();

            foreach (MKeyPoint keypoint in keypoints)
            {
                imgBgr.Draw(new Rectangle((int)(keypoint.Point.X - keypoint.Size / 2), (int)(keypoint.Point.Y - keypoint.Size / 2), (int)keypoint.Size, (int)keypoint.Size), new Bgr(255, 0, 0), 1);
                imageBox2.Image = imgBgr;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
