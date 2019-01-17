using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormTemplateMatch : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> scr;
        Image<Bgr, byte> temp;

        public FormTemplateMatch()
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
                scr = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox2.Load(filename);
                temp = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {    
            Mat result = new Mat(new Size(scr.Width - temp.Width + 1, scr.Height - temp.Height + 1), Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            //创建mat 存储输出匹配结果。
            CvInvoke.MatchTemplate(scr, temp, result, Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed); //采用系数匹配法，匹配值越大越接近准确图像。
            CvInvoke.Normalize(result, result, 255, 0, Emgu.CV.CvEnum.NormType.MinMax); //把数据进行以最大值255 最小值0 进行归一化。
            result = result.ToImage<Gray, byte>().Mat;//result 类型转成Byte 类型。
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint(); //创建VectorOfVectorOfPoint 类型保存轮廓。
            int threshold = 180; //设置阈值。
            Mat data = new Mat();//创建data 存储阈值后的图像。
            while (true)
            {
                CvInvoke.Threshold(result, data, threshold, 255, Emgu.CV.CvEnum.ThresholdType.BinaryInv);//阈值操作。
                CvInvoke.FindContours(data, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);// 存储轮廓。

                if (contours.Size <= 1)//判断匹配个数是否小于等于10
                {                   
                    break;
                }
                threshold -= 2;//阈值降低

            }
            for (int i = 0; i < contours.Size; i++)//遍历每个连通域。
            {
                VectorOfPoint contour = contours[i];
                MCvMoments moment = CvInvoke.Moments(contour);//获得连通域的矩
                Point p = new Point((int)(moment.M10 / moment.M00), (int)(moment.M01 / moment.M00));// 获得连通域的中心
                CvInvoke.Rectangle(scr, new Rectangle(p, temp.Size), new MCvScalar(0, 0, 255), 4);//绘制匹配区域。
            }
            imageBox1.Image = scr ;
           
        }
    }
}
