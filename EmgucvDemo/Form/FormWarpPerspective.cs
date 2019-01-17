/*
透视变换（Perspective Transformation)是指利用透视中心、 像点、 目标点
三点共线的条件， 按透视旋转定律使承影面（透视面） 绕迹线（透视轴） 旋
转某一角度， 破坏原有的投影光线束， 仍能保持承影面上投影几何图形不变
的变换。
透视变换常用于图象的校正， 例如在移动机器人视觉导航研究中， 由于
摄像机与地面之间有一倾斜角， 而不是直接垂直朝下（正投影）， 有时希望
将图象校正成正投影的形式， 就需要利用透视变换。
透视变换采用 3*3 矩阵。
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmgucvDemo
{
    public partial class FormWarpPerspective : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        Mat data = new Mat();

        public FormWarpPerspective()
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
            //创建用于获取透视的四个点坐标。 这四个点坐标与之前的坐标顺序倒序。 { 1， 2， 3， 4}=>{ 4,3,2,1}
            PointF[] point_src = new PointF[] { new PointF(0, 0), new PointF(src.Width, 0),new PointF(0, src.Height),new PointF(src.Width,src.Height)};
            PointF[] point_dst = new PointF[] { new PointF(src.Width,src.Height),new PointF(0, src.Height), new PointF(src.Width, 0),new PointF(0, 0) };
            data = CvInvoke.GetPerspectiveTransform(point_src, point_dst);//获取透视矩阵
       
            byte[,,] da = data.ToImage<Gray, byte>().Data;
            //获取仿射矩阵的像素值
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    richTextBox1.Text += da[i, j, 0] + "\t";
                }
                richTextBox1.Text += "\n";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mat dstImage = new Mat();
            CvInvoke.WarpPerspective(src, dstImage, data, src.Size);//进行透视操作。
            imageBox2.Image = dstImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {


         //   CvInvoke.PerspectiveTransform(src,data);//进行透视操作。
           
        }
    }
}
