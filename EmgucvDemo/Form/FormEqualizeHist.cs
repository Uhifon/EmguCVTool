using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace EmgucvDemo
{
    public partial class FormEqualizeHist : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Gray, byte> src;
       

        public FormEqualizeHist()
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
            }
            src = new Image<Gray, byte>(open.FileName);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
 
        private void button2_Click(object sender, EventArgs e)
        {         
            Mat Histimg = new Mat(CvInvoke.cvGetSize(src), DepthType.Cv8U, 1);
            CvInvoke.EqualizeHist(src,//单通道8bit图像
                Histimg);   //均衡化         
            imageBox2.Image = Histimg;
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageBox2.Image.Save(@"1.bmp");
        }
    }
}
