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
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormRemap : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> srcImage;
        public FormRemap()
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
                srcImage = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> dstImage = srcImage.CopyBlank();
            Image<Gray, float> map_x = srcImage.CopyBlank().Convert<Gray,float>();
            Image<Gray, float> map_y = srcImage.CopyBlank().Convert<Gray, float>();

          //  Mat map_x = new Mat(srcImage.Size, Emgu.CV.CvEnum.DepthType.Cv32F, 1);
          //  Mat map_y = new Mat(srcImage.Size, Emgu.CV.CvEnum.DepthType.Cv32F, 1);
            for(int j=0;j<srcImage.Rows;j++)
            {
                for(int i=0;i<srcImage.Cols;i++)
                {
                    map_x.Data[j, i, 0] = (float)i;
                    map_y.Data[j, i, 0] = (float)(srcImage.Cols-j);
                }
            }
            CvInvoke.Remap(srcImage,dstImage,map_x,map_y, Emgu.CV.CvEnum.Inter.Linear);
            imageBox2.Image = dstImage;

        }
    }
}
