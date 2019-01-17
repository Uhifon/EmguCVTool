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
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormColor : Form
    {
        Image<Bgr, byte> src;
       
        static string filename;
        OpenFileDialog open;
        public FormColor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
                src = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Hsv, byte> hsvImage= src.Convert <Hsv, byte>();
            imageBox1.Image = hsvImage;

            VectorOfMat channels = new VectorOfMat(); //创建vectorOfmat类型存储分离后的图像
            CvInvoke.Split(hsvImage, channels); //通道分离
            InputOutputArray mix_channel = channels.GetInputOutputArray(); //获得数组

            Mat H_channel = mix_channel.GetMat(0); //获得第一通道
            Mat S_channel = mix_channel.GetMat(1); //获得第二通道
            Mat V_channel = mix_channel.GetMat(2);//获得第一通道

            imageBox2.Image = H_channel; //显示第一通道
            imageBox3.Image = S_channel; //显示第二通道
            imageBox4.Image = V_channel; //显示第三通道

        }

        private void button3_Click(object sender, EventArgs e)
        {

            VectorOfMat channels = new VectorOfMat(); //创建vectorOfmat类型存储分离后的图像
            CvInvoke.Split(src, channels); //通道分离
            InputOutputArray mix_channel = channels.GetInputOutputArray(); //获得数组

            Mat B_channel = mix_channel.GetMat(0); //获得第一通道
            Mat G_channel = mix_channel.GetMat(1); //获得第二通道
            Mat R_channel = mix_channel.GetMat(2);//获得第一通道

            imageBox2.Image = B_channel; //显示第一通道
            imageBox3.Image = G_channel; //显示第二通道
            imageBox4.Image = R_channel; //显示第三通道
        }
    }
}
