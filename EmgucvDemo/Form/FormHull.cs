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
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace EmgucvDemo
{
    public partial class FormHull : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> image1;
        public FormHull()
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
            Image<Gray, byte> image2 = new Image<Gray, byte>(image1.Width, image1.Height);
            Image<Gray, byte> image3 = new Image<Gray, byte>(image1.Width, image1.Height);
            Image<Bgr, byte> image4 = new Image<Bgr, byte>(image1.Width, image1.Height);
            CvInvoke.Canny(image1, image2, 100, 60);
            VectorOfVectorOfPoint con = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(image2, con, image3, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);

            Point[][] con1 = con.ToArrayOfArray();
            PointF[][] con2 = Array.ConvertAll<Point[], PointF[]>(con1, new Converter<Point[], PointF[]>(PointToPointF));
            for (int i = 0; i < con.Size; i++)
            {
                PointF[] hull = CvInvoke.ConvexHull(con2[i], true);
                for (int j = 0; j < hull.Length; j++)
                {
                    Point p1 = new Point((int)(hull[j].X + 0.5), (int)(hull[j].Y + 0.5));
                    Point p2;
                    if (j == hull.Length - 1)
                        p2 = new Point((int)(hull[0].X + 0.5), (int)(hull[0].Y + 0.5));
                    else
                        p2 = new Point((int)(hull[j + 1].X + 0.5), (int)(hull[j + 1].Y + 0.5));
                    CvInvoke.Circle(image4, p1, 3, new MCvScalar(0, 255, 255, 255), 6);
                    CvInvoke.Line(image4, p1, p2, new MCvScalar(255, 255, 0, 255), 3);
                }
            }
            for (int i = 0; i < con.Size; i++)
                CvInvoke.DrawContours(image4, con, i, new MCvScalar(255, 0, 255, 255), 2);

            imageBox2.Image = image1.ConcateVertical(image4);
        }

        public static PointF[] PointToPointF(Point[] pf)
        {
            PointF[] aaa = new PointF[pf.Length];
            int num = 0;
            foreach (var point in pf)
            {
                aaa[num].X = (int)point.X;
                aaa[num++].Y = (int)point.Y;
            }
            return aaa;
        }
    }
}
