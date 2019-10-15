using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormStitching : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> image1;//创建Image 输入图片
        Image<Bgr, byte> image2;//创建Image 输入图片
        Image<Bgr, byte> image3;//创建Image 输入图片
        Image<Bgr, byte> image4;//创建Image 输入图片

        public FormStitching()
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
                image1 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox2.Load(filename);
                image2 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox3.Load(filename);
                image3 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox4.Load(filename);
                image4 = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stitcher _sticher = new Stitcher(Stitcher.Mode.Scans);//创建一个 Sticher 类。
            Mat result_image = new Mat();//创建 Mat 存储输出全景图
            VectorOfMat sti_image = new VectorOfMat();//创建 VectorOfMat 类型， 输入图像拼接数组
            // 添加图像到 sti_image 中， 不按照循序进行添加， 说明拼接图像与顺序无关*//
            sti_image.Push(image1);
            sti_image.Push(image2);
            sti_image.Push(image3);
            sti_image.Push(image4);
            Stitcher.Status status = _sticher.Stitch(sti_image, result_image);//进行图像拼接， 返回 bool 类型， 是否拼接成功。
            if (status == Stitcher.Status.Ok)
                imageBox5.Image = result_image;//显示图像。
            else
                MessageBox.Show("拼接失败", "提示");
        } 
    }
}
