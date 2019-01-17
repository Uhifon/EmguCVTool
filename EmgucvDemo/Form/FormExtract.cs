using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormExtract : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> bgr_img;
        public FormExtract()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                pictureBox2.Load(filename);
                bgr_img = new Image<Bgr, byte>(open.FileName);
            }
        }
  

        private void button2_Click(object sender, EventArgs e)
        {
            double blue_min = double.Parse(txt_BlueMin.Text);
            double blue_max = double.Parse(txt_BlueMax.Text);
            double green_min = double.Parse(txt_GreenMin.Text);
            double green_max = double.Parse(txt_GreenMax.Text);
            double red_min = double.Parse(txt_RedMin.Text);
            double red_max = double.Parse(txt_RedMax.Text);
            Bgr min = new Bgr(blue_min, green_min, red_min);//黄色的最小值，允许一定的误差。
            Bgr max = new Bgr(blue_max, green_max, red_max);//黄色的最大值，允许一定的误差。
            Image<Gray, byte> result = bgr_img.InRange(min, max);//进行颜色提取。
            pictureBox2.Image = bgr_img.Bitmap;//显示输入图像。
            imageBox2.Image = result;//显示提取颜色区域。
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>(bgr_img.Bitmap).Resize(pictureBox2.Width, pictureBox2.Height, Emgu.CV.CvEnum.Inter.Area);
            //获取imagebox1 控件的图片，并重置大小与控件的大小一致。
            Bgr color = img[e.Y, e.X];
            //获得鼠标点击位置的坐标。
            label1.Text = color.ToString();
            //显示颜色的数值。
            Image<Bgr, byte> img1 = new Image<Bgr, byte>(imageBox2.Width, imageBox2.Height, color);
            //以固定颜色创建一张图片，并显示在ImageBox2。用于再次确定颜色是否正确。
            pictureBox1.Image = img1.Bitmap;
        }
    }
}
