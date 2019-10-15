using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace EmgucvDemo
{
    public partial class FormCalibration : Form
    {
        Image<Bgr, byte> imag;
        PointF[] points_camera = new PointF[9];
        PointF[] points_robot = new PointF[9];

        double A;
        double B;
        double C;
        double D;
        double E;
        double F;

        public FormCalibration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            imag = new Image<Bgr, byte>("圆.jpg");
            imageBox1.Image = imag;
        }

        private void btn_FindCycle_Click(object sender, EventArgs e)
        {
            Image<Gray, float> image_gray = imag.Convert<Gray, float>();
            CvInvoke.CvtColor(imag, image_gray, ColorConversion.Bgr2Gray);
            double cannyThreshold = Convert.ToDouble(numericUpDown1.Value);
            double circleAccumulatorThreshold = Convert.ToDouble(numericUpDown2.Value);
            int minRadius = Convert.ToInt32(numericUpDown3.Value);
            int maxRadius = Convert.ToInt32(numericUpDown4.Value);
            CircleF[] circles = CvInvoke.HoughCircles(image_gray, HoughType.Gradient, 1.01, 80.0, cannyThreshold, circleAccumulatorThreshold, minRadius, maxRadius);
            //if (circles.Length != 9)
            //    return;
            Mat circleImage = new Mat(image_gray.Size, DepthType.Cv8U, 3);
            circleImage.SetTo(new MCvScalar(0));

            for (int i = 1; i < circles.Length - 1; i++)
            {
                CircleF temp;
                for (int j = 0; j < circles.Length - i; j++)
                {
                    if (circles[j].Center.X > circles[j + 1].Center.X)
                    {
                        temp = circles[j + 1];
                        circles[j + 1] = circles[j];
                        circles[j] = temp;
                    }
                    if (circles[j].Center.X == circles[j + 1].Center.X)
                    {
                        if (circles[j].Center.Y > circles[j + 1].Center.Y)
                        {
                            temp = circles[j + 1];
                            circles[j + 1] = circles[j];
                            circles[j] = temp;
                        }
                    }
                }
            }

            int num = 1;
            foreach (CircleF circle in circles)
            {
                CvInvoke.Circle(circleImage, Point.Round(circle.Center), (int)circle.Radius, new Bgr(Color.Brown).MCvScalar, 2);
                CvInvoke.Circle(imag, Point.Round(circle.Center), (int)circle.Radius, new Bgr(Color.Red).MCvScalar, 2);
                CvInvoke.Circle(imag, Point.Round(circle.Center), 1, new Bgr(Color.Blue).MCvScalar, 2);
                Graphics g = Graphics.FromImage(imag.Bitmap);
                String str = num.ToString() + "\r\n" + "X:" + circle.Center.X.ToString() + "\r\n" + "Y:" + circle.Center.Y.ToString();
                Font font = new Font("宋体", 25);
                SolidBrush sbrush = new SolidBrush(Color.Blue);
                g.DrawString(str, font, sbrush, new PointF(circle.Center.X + 10, circle.Center.Y - 10));

                string tbName1 = "tb_PicX" + num;
                string tbName2 = "tb_PicY" + num;
                TextBox tb1 = (TextBox)this.Controls.Find(tbName1, false)[0];
                TextBox tb2 = (TextBox)this.Controls.Find(tbName2, false)[0];
                tb1.Text = circle.Center.X.ToString();
                tb2.Text = circle.Center.Y.ToString();
                points_camera[num - 1].X = circle.Center.X;
                points_camera[num - 1].Y = circle.Center.Y;
                num += 1;
            }
            imageBox1.Refresh();
        }


        private void btn_Cal_Click(object sender, EventArgs e)
        {
            for (int num = 1; num <= 9; num++)
            {
                string tbName1 = "tb_RobX" + num;
                string tbName2 = "tb_RobY" + num;
                TextBox tb1 = (TextBox)this.Controls.Find(tbName1, false)[0];
                TextBox tb2 = (TextBox)this.Controls.Find(tbName2, false)[0];
                points_robot[num - 1].X = Convert.ToSingle(tb1.Text);
                points_robot[num - 1].Y = Convert.ToSingle(tb2.Text);

                string tbName3 = "tb_PicX" + num;
                string tbName4 = "tb_PicY" + num;
                TextBox tb3 = (TextBox)this.Controls.Find(tbName3, false)[0];
                TextBox tb4 = (TextBox)this.Controls.Find(tbName4, false)[0];
                points_camera[num - 1].X = Convert.ToSingle(tb3.Text);
                points_camera[num - 1].Y = Convert.ToSingle(tb4.Text);
            }

            CalRobot();
            textBox_a.Text = A.ToString();
            textBox_b.Text = B.ToString();
            textBox_c.Text = C.ToString();
            textBox_d.Text = D.ToString();
            textBox_e.Text = E.ToString();
            textBox_f.Text = F.ToString();

        }

        ////相机矫正
        //private void CalibrateCamera()
        //{
        //    Image<Bgr, byte> ImgOri = imag.Copy();
        //    MCvPoint3D32f[][] corners_object_list = null;
        //    PointF[][] corners_points_list = null;
        //    Matrix<Single> cameraMatrix = new Matrix<Single>(480, 320);
        //    Matrix<Single> distortionCoeffs = new Matrix<Single>(480, 320);
        //    MCvTermCriteria termCriteria = new MCvTermCriteria();
        //    Mat[] rotationVectors;
        //    Mat[] translationVectors;

        //    CvInvoke.CalibrateCamera(corners_object_list, corners_points_list, ImgOri.Size, cameraMatrix, distortionCoeffs, CalibType.FixAspectRatio, termCriteria, out rotationVectors, out translationVectors);

        //    //畸形矫正
        //    float Scale = 0.8f;
        //    Image<Bgr, byte> ImgDst = ImgOri.CopyBlank();
        //    Matrix<Single> newCameraMatrix = new Matrix<Single>(480, 320);
        //    cameraMatrix.CopyTo(newCameraMatrix);
        //    newCameraMatrix[0, 0] = newCameraMatrix[0, 0] * Scale;
        //    newCameraMatrix[1, 1] = newCameraMatrix[1, 1] * Scale;
        //    CvInvoke.Undistort(ImgOri, ImgDst, cameraMatrix, distortionCoeffs, newCameraMatrix);
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            PointF p = new PointF();
            PointF _p = new PointF();
            p.X = Convert.ToSingle(textBox_picx.Text);
            p.Y = Convert.ToSingle(textBox_picy.Text);
            _p = TransformPoint(p);
            textBox_robotx.Text = _p.X.ToString();
            textBox_roboty.Text = _p.Y.ToString();
        }




        private void CalRobot()
        {
            Mat warpMat;
            warpMat = CvInvoke.EstimateAffine2D(points_camera, points_robot);    //4.0
        //    warpMat = CvInvoke.EstimateRigidTransform(points_camera, points_robot, true);  //3.0
            Image<Gray, float> img = warpMat.ToImage<Gray, float>();
            A = img.Data[0, 0, 0];
            B = img.Data[0, 1, 0];
            C = img.Data[0, 2, 0];
            D = img.Data[1, 0, 0];
            E = img.Data[1, 1, 0];
            F = img.Data[1, 2, 0];
        }

        public PointF TransformPoint(PointF pPoint)
        {
            //********************************************            
            // x = x'k*cost-y'k* sint+x0,
            // y = x'k*sint+y'k* cost+y0.
            //A = k*cost,B =-k* sint,C
            //D = k*sint,E = k* cost,F
            //********************************************* 
            PointF tPoint = new PointF();
            tPoint.X = Convert.ToSingle(A * pPoint.X + B * pPoint.Y + C);
            tPoint.Y = Convert.ToSingle(D * pPoint.X + E * pPoint.Y + F);
            return tPoint;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
