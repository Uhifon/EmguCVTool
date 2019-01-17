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
    public partial class FormDFT : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> srcimage;
        Image<Gray, float>  image;
        public FormDFT()
        {
            InitializeComponent();
            // Load image
        }
        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
                srcimage = new Image<Bgr, byte>(open.FileName);
                image = srcimage.Convert<Gray, float>();
            }          

        }
        private void button2_Click(object sender, EventArgs e)
        {
           //获取最佳Size，以便可以使用FFT，通常2*3*5倍数
            int M = CvInvoke.GetOptimalDFTSize(image.Rows);
            int N = CvInvoke.GetOptimalDFTSize(image.Cols);
            //图像扩展
            Mat padded = new Mat();
            CvInvoke.CopyMakeBorder(image, padded, 0, M - image.Rows, 0, N - image.Cols, BorderType.Constant, new MCvScalar(1));

            //创建一个2通道矩阵，0通道为源图数据，1通道为0

            Mat m = new Mat(padded.Size, DepthType.Cv32F, 1);
            m.SetTo(new MCvScalar(255));
            CvInvoke.Divide(padded, m, padded);
            m.SetTo(new MCvScalar(0));
            VectorOfMat matVector = new VectorOfMat();
            matVector.Push(padded);
            matVector.Push(m);
            Mat matComplex = new Mat(padded.Size, DepthType.Cv32F, 2);
            CvInvoke.Merge(matVector, matComplex);
            padded.Dispose();
            m.Dispose();
            matVector.Dispose();
            // This will hold the DFT data，创建2通道矩阵，储存变换后结果
            Matrix<float> forwardDft = new Matrix<float>(image.Rows, image.Cols, 2);
            CvInvoke.Dft(matComplex, forwardDft, DxtType.Forward, 0);

            // We'll display the magnitude，显示谱图像
            Matrix<float> forwardDftMagnitude = GetDftMagnitude(forwardDft);
            SwitchQuadrants(ref forwardDftMagnitude);

            // Now compute the inverse to see if we can get back the original
            //进行反变换
            Matrix<float> reverseDft = new Matrix<float>(forwardDft.Rows, forwardDft.Cols, 2);
            CvInvoke.Dft(forwardDft, reverseDft, DxtType.InvScale, 0);
            Matrix<float> reverseDftMagnitude = GetDftMagnitude(reverseDft);

            imageBox1.Image = image;
            imageBox2.Image = Matrix2Image(forwardDftMagnitude);
            imageBox3.Image = Matrix2Image(reverseDftMagnitude);

        }

        private Image<Gray, float> Matrix2Image(Matrix<float> matrix)
        {
            CvInvoke.Normalize(matrix, matrix, 0.0, 255.0, NormType.MinMax);

            Image<Gray, float> image = new Image<Gray, float>(matrix.Size);
            matrix.CopyTo(image);

            return image;
        }

        // Real part is magnitude, imaginary is phase. 
        // Here we compute log(sqrt(Re^2 + Im^2) + 1) to get the magnitude and 
        // rescale it so everything is visible
        private Matrix<float> GetDftMagnitude(Matrix<float> fftData)
        {
            VectorOfMat outMat = new VectorOfMat();
            //The Real part of the Fourier Transform
            Matrix<float> outReal = new Matrix<float>(fftData.Size);
            //The imaginary part of the Fourier Transform
            Matrix<float> outIm = new Matrix<float>(fftData.Size);
            CvInvoke.Split(fftData, outMat);
            outMat[0].ConvertTo(outReal, DepthType.Cv32F);
            outMat[1].ConvertTo(outIm, DepthType.Cv32F);

            CvInvoke.Pow(outReal, 2.0, outReal);
            CvInvoke.Pow(outIm, 2.0, outIm);

            CvInvoke.Add(outReal, outIm, outReal);
            CvInvoke.Pow(outReal, 0.5, outReal);

            // outReal=outReal.Add(125); // 1 + Mag
            // CvInvoke.Log(outReal, outReal); // log(1 + Mag)
            // outReal=outReal.Mul(0.01);
            // CvInvoke.Normalize(outReal, outReal, 0, 1, NormType.MinMax);  
            double scale = Math.Sqrt(fftData.Width * fftData.Height);
            for (int i = 0; i < fftData.Height; i++)
            {
                for (int j = 0; j < fftData.Width; j++)
                {
                    outReal.Data[i, j] = (float)(Math.Max(0, Math.Min(255, outReal.Data[i, j] / scale * 255)));
                }
            }
            //图像整列
            Matrix<float> outFinal = outReal.GetSubRect(new Rectangle(0, 0, outReal.Cols & -2, outReal.Height & -2));

            return outFinal;
        }

        // We have to switch quadrants so that the origin is at the image center
        private void SwitchQuadrants(ref Matrix<float> matrix)
        {

            int cx = matrix.Cols / 2;
            int cy = matrix.Rows / 2;

            Matrix<float> q0 = matrix.GetSubRect(new Rectangle(0, 0, cx, cy));
            Matrix<float> q1 = matrix.GetSubRect(new Rectangle(cx, 0, cx, cy));
            Matrix<float> q2 = matrix.GetSubRect(new Rectangle(0, cy, cx, cy));
            Matrix<float> q3 = matrix.GetSubRect(new Rectangle(cx, cy, cx, cy));
            Matrix<float> tmp = new Matrix<float>(q0.Size);

            q0.CopyTo(tmp);
            q3.CopyTo(q0);
            tmp.CopyTo(q3);
            q1.CopyTo(tmp);
            q2.CopyTo(q1);
            tmp.CopyTo(q2);

        }



      
    }


}
