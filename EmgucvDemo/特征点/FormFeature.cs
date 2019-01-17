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
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;

namespace EmgucvDemo
{
    public partial class FormFeature : Form
    {
        public FormFeature()
        {
            InitializeComponent();
        }

        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;
        Image<Bgr, byte> dst;

        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                pictureBoxSrc.Load(filename);
                src = new Image<Bgr, byte>(open.FileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                pictureBoxDst.Load(filename);
                dst = new Image<Bgr, byte>(open.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (src != null && dst != null)
            {
                Image<Bgr, Byte> srcImg = src.Copy();
                Image<Bgr, Byte> dstImg = dst.Copy();
                long score;
                long matchTime;
                Point[] observedPoints = new Point[4];
                using (Mat modelImage = dstImg.Mat)
                using (Mat observedImage = srcImg.Mat)
                {
                    var result = DrawMatches.Draw(modelImage, observedImage, out matchTime, out score, out observedPoints);
                    imageBox1.Image = result;

                    if (score > Convert.ToInt32(numericUpDown1.Value))
                    {
                        using (VectorOfPoint vp = new VectorOfPoint(observedPoints))
                        {
                            CvInvoke.Polylines(src, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                        }
                        pictureBoxSrc.Image = src.Bitmap;
                    }
                }
                textBox1.Text = "score" + score + "," + "matchtime:" + matchTime;
            }
        }


     
    }    
      
}
