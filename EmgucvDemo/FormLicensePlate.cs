using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVDemo
{
    public partial class FormLicensePlate : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;

        public FormLicensePlate()
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
            FindLicensePlate();
            ReadLicensePlate();
        }

        private void FindLicensePlate()
        {
            Image<Bgr, Byte> simage = src.Copy();    //new Image<Bgr, byte>("license-plate.jpg");
            //Image<Bgr, Byte> simage = sizeimage.Resize(400, 300, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
            Image<Gray, Byte> GrayImg = new Image<Gray, Byte>(simage.Width, simage.Height);
        //IntPtr GrayImg1 = CvInvoke.cvCreateImage(simage.Size, Emgu.CV.CvEnum.IplDepth.IplDepth_8U, 1);
            
            //灰度化
       //     CvInvoke.CvtColor(simage.Mat, GrayImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            //首先创建一张16深度有符号的图像区域
          //  IntPtr Sobel = CvInvoke.cvCreateImage(simage.Size, Emgu.CV.CvEnum.IplDepth.IplDepth16S, 1);
            //X方向的Sobel算子检测
            CvInvoke.Sobel(GrayImg, GrayImg, Emgu.CV.CvEnum.DepthType.Cv16S,2, 0, 3);
          //  IntPtr temp = CvInvoke.cvCreateImage(simage.Size, Emgu.CV.CvEnum.IplDepth.IplDepth_8U, 1);
            CvInvoke.cvConvertScale(GrayImg, GrayImg, 0.00390625, 0);
            ////int it = ComputeThresholdValue(GrayImg.ToBitmap());
            ////二值化处理
            ////Image<Gray, Byte> dest = GrayImg.ThresholdBinary(new Gray(it), new Gray(255));
            Image<Gray, Byte> dest = new Image<Gray, Byte>(simage.Width, simage.Height);
            //二值化处理
            CvInvoke.Threshold(GrayImg, dest, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            //       IntPtr temp1 = CvInvoke.cvCreateImage(simage.Size, Emgu.CV.CvEnum.IplDepth.IplDepth_8U, 1);
              Image<Gray, Byte> dest1 = new Image<Gray, Byte>(simage.Width, simage.Height);
            //定义内核
            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));

      //      CvInvoke.CreateStructuringElementEx(3, 1, 1, 0, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT, temp1);
            CvInvoke.Dilate(dest, dest1, element, new Point(1,1),1,Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(255, 0, 0, 255));
            CvInvoke.Erode(dest1, dest1, element, new Point(1, 1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(255, 0, 0, 255));
            CvInvoke.Dilate(dest1, dest1, element, new Point(1, 1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(255, 0, 0, 255));
            CvInvoke.CreateStructuringElementEx(1, 3, 0, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT, temp1);
            CvInvoke.cvErode(dest1, dest1, temp1, 2);
            CvInvoke.cvDilate(dest1, dest1, temp1, 2);
            IntPtr dst = CvInvoke.cvCreateImage(simage.Size, Emgu.CV.CvEnum.IplDepth.IplDepth_8U, 3);
            CvInvoke.cvZero(dst);
            //dest.Dilate(10);
            //dest.Erode(5);
            using (MemStorage stor = new MemStorage())
            {
                  CvInvoke.FindContours( dest1,
                    Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                    Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_CCOMP,
                    stor);
                for (; contours != null; contours = contours.HNext)
                {

                    Rectangle box = contours.BoundingRectangle;
                    Image<Bgr, Byte> test = simage.CopyBlank();
                    test.SetValue(255.0);
                    double whRatio = (double)box.Width / box.Height;
                    int area = (int)box.Width * box.Height;
                    if (area > 1000 && area < 10000)
                    {
                        if ((3.0 < whRatio && whRatio < 6.0))
                        {
                            test.Draw(box, new Bgr(Color.Red), 2);
                            simage.Draw(box, new Bgr(Color.Red), 2);//CvInvoke.cvNamedWindow("dst");
                            //CvInvoke.cvShowImage("dst", dst);
                            imageBox1.Image = simage;
                        }
                    }
                }
            }
             
        }

        private void ReadLicensePlate()
        {

        }
    }
}
