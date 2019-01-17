using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace EmgucvDemo
{
    public partial class FormPyramid : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        BorderType[] border_type = new BorderType[9] { BorderType.NegativeOne,BorderType.Constant, BorderType.Replicate, BorderType.Reflect, BorderType.Wrap, BorderType.Default, BorderType.Reflect101, BorderType.Transparent, BorderType.Isolated };

        public FormPyramid()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
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
            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
 
            int i = comboBox1.SelectedIndex;
            try
            {
                CvInvoke.PyrDown(src, dst1, border_type[comboBox1.SelectedIndex]);//进行高斯向下采样（缩小）
                CvInvoke.PyrUp(src, dst2, border_type[comboBox1.SelectedIndex]);//进行拉普拉斯向上采样（放大）
                label1.Text = "原图size:" + src.Size.ToString();//获取原图大小
                label2.Text = "高斯向下采样输出图像size:" + dst1.Size.ToString();//获取输出图像大小
                label3.Text = "拉普拉斯向上采样输出图像size:" + dst2.Size.ToString();

                imageBox2.Image = dst1;
                imageBox3.Image = dst2;
            }
            catch
            {
                throw;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            Size size = new Size();
            size.Height = int.Parse(textBox1.Text);
            size.Width = int.Parse(textBox2.Text);
            CvInvoke.Resize(src, dst, size);
            imageBox4.Image = dst;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
