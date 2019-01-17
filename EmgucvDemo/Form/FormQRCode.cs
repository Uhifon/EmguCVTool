using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Threading;
using Emgu.CV.Util;
using ZXing;
using ZXing.QrCode;
using System.Drawing.Imaging;

namespace EmgucvDemo
{
    public partial class FormQRCode : Form
    {
        static string filename;
        OpenFileDialog open;
        Mat image;
        public FormQRCode()
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
                image = new Mat(open.FileName);
            
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (image == null) return;
             Mat image_gray=new Mat();
            CvInvoke.CvtColor(image,image_gray,ColorConversion.Bgr2Gray);
            CvInvoke.GaussianBlur(image_gray, image_gray, new Size(3, 3), 0);  //滤波  
            imageBox1.Image= image_gray;
            Mat dst = new Mat();
            CvInvoke.Threshold(image_gray, dst, 130, 255, ThresholdType.Otsu);  //二值化  
            dst.Save("1.jpg");
            //【】膨胀
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 1), new Point(-1, -1));  //膨胀腐蚀核
            //morphologyEx(image,image,MORPH_OPEN,element);  
            for (int i = 0; i < 2; i++)
            {
                CvInvoke.Dilate(dst, dst, element, new Point(-1, -1), 1, BorderType.Reflect101, new MCvScalar(0, 0, 255));
                i++;
            }
            dst.Save("2.jpg");
            //寻找轮廓定位
            Mat result = new Mat(image.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
            result.SetTo(new MCvScalar(0, 0, 0));
            VectorOfVectorOfPoint vvp = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint use_vvp = new VectorOfVectorOfPoint();
           
            //通过轮廓树找出二维码
            int [,] hierarchy = CvInvoke.FindContourTree(dst,vvp,ChainApproxMethod.ChainApproxNone);
            int c = 0, ic = 0;
            int parentIdx = -1;
            for (int i = 0; i < vvp.Size; i++)
            {
                //hierarchy[i][2] != -1 表示不是最外面的轮廓
                if (hierarchy[i,2] != -1 && ic == 0)
                {
                    parentIdx = i;
                    ic++;
                }
                else if (hierarchy[i,2] != -1)
                {
                    ic++;
                }
                //最外面的清0
                else if (hierarchy[i,2] == -1)
                {
                    ic = 0;
                    parentIdx = -1;
                }
                //找到定位点信息
                if (ic >= 2)
                {
                    use_vvp.Push(vvp[i]);
                    ic = 0;
                    parentIdx = -1;
                    CvInvoke.DrawContours(result, use_vvp, -1, new MCvScalar(0, 255, 0));
                    CvInvoke.DrawContours(image, use_vvp, -1, new MCvScalar(255, 0, 0), 1, LineType.EightConnected);
                }
            }

            //CvInvoke.FindContours(dst, vvp, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //int number = vvp.ToArrayOfArray().Length;//取得轮廓的数量.
            //for (int i = 0; i < number; i++)
            //{
            //    VectorOfPoint vp = vvp[i];
            //    double area = CvInvoke.ContourArea(vp);
            //    if (area < 850 || area > 950)
            //        continue;
            //    // 根据矩形特征进行几何分析
            //    RotatedRect box = CvInvoke.MinAreaRect(vp);
            //    float w = box.Size.Width;
            //    float h = box.Size.Height;
            //    float rate = Math.Min(w, h) / Math.Max(w, h);
            //    if (rate > 0.85 && w < 35 && h < 35)
            //    {
            //        use_vvp.Push(vp);
            //        CvInvoke.DrawContours(result, use_vvp, -1, new MCvScalar(0, 255, 0));
            //        CvInvoke.DrawContours(image, use_vvp, -1, new MCvScalar(255, 0, 0), 1, LineType.EightConnected);
            //    }
            //}

            //【】连接定位点
            Point[] points = new Point[3];
            for (int i = 0; i < use_vvp.Size; i++)
            {
                points[i] = Center_cal(use_vvp, i);
            }
            CvInvoke.Line(image, points[0], points[1], new MCvScalar(0, 255, 0), 1);
            CvInvoke.Line(image, points[1], points[2], new MCvScalar(0, 255, 0), 1);
            CvInvoke.Line(image, points[0], points[2], new MCvScalar(0, 255, 0), 1);

        
            imageBox1.Image = image;

            //【】透视变换矫正二维码  需要4个点
           
            PointF[] point_src = new PointF[4];
            PointF[] point_dst = new PointF[4];

            for(int i=0;i<3;i++)
            {
                point_dst[i] = points[i];
            }
  
            Mat warp_mat = CvInvoke.GetPerspectiveTransform(point_src, point_dst);//获取透视矩阵
     //       byte[,,] data = warp_mat.ToImage<Gray, byte>().Data;
            //【】对图像进行透视变换
            Mat warp_dst=new Mat();
            CvInvoke.WarpPerspective(image, warp_dst, warp_mat, image.Size);//进行透视操作。
            imageBox2.Image = warp_dst;
        }

        //找到所提取轮廓的中心点
        //在提取的中心小平行四边形的边界上每隔周长个像素提取一个点的坐标，求所提取四个点的平均坐标（即为小正方形的大致中心）
        private Point Center_cal(VectorOfVectorOfPoint contours, int i)
        {
                int centerx = 0, centery = 0, n = contours[i].Size;
                centerx = (contours[i][n / 4].X + contours[i][n * 2 / 4].X + contours[i][3 * n / 4].X + contours[i][n - 1].X) / 4;
                centery = (contours[i][n / 4].Y + contours[i][n * 2 / 4].Y + contours[i][3 * n / 4].Y + contours[i][n - 1].Y) / 4;
                Point point1 = new Point(centerx, centery);
                return point1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap map = image.Bitmap;
             richTextBox1.Text =  Read1(map);
        }

        /// <summary>
        /// 读取二维码
        /// 读取失败，返回空字符串
        /// </summary>
        /// <param name="filename">指定二维码图片位置</param>
        static string Read1(Bitmap bitmap)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(filename);
            Result result = reader.Decode(map);
            return result == null ? "" : result.Text;
        }

        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        static void Generate1(string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(text);
            string filename = @"H:\桌面\截图\generate1.png";
            map.Save(filename, ImageFormat.Png);
            map.Dispose();
        }


    }
}
