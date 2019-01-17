using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace EmgucvDemo
{
    public partial class FormContour : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Gray, byte> src;

        double threshold_value;        
        ThresholdType[] threshs = new ThresholdType[7] { ThresholdType.Binary, ThresholdType.BinaryInv, ThresholdType.Trunc, ThresholdType.ToZero, ThresholdType.ToZeroInv, ThresholdType.Mask, ThresholdType.Otsu};
        RetrType[] perttypes = new RetrType[4] { RetrType.External, RetrType.List, RetrType.Ccomp , RetrType.Tree};
        ChainApproxMethod [] chain_appox_methods = new ChainApproxMethod[6] { ChainApproxMethod.ChainCode, ChainApproxMethod.ChainApproxNone, ChainApproxMethod.ChainApproxSimple, ChainApproxMethod.ChainApproxTc89L1, ChainApproxMethod.ChainApproxTc89Kcos, ChainApproxMethod.LinkRuns };
        bool isCouterFind = false;
        int minArea;
        int maxArea;


        public FormContour()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
                src = new Image<Gray, byte>(open.FileName);
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            minArea = Convert.ToInt32(numericUpDown1.Value);
            maxArea = Convert.ToInt32(numericUpDown2.Value);
            threshold_value = Convert.ToDouble(numericUpDown3.Value);
            test(src);    
        }

        private void test(Image<Gray, byte> img)
        {
            int n =Convert.ToInt32(comboBox1.SelectedIndex );

              isCouterFind = false;           
            //Image<Gray, byte> Dyncontour = new Image<Gray, byte>(src.Size);        
            //VectorOfVectorOfPoint use_Dyncontour = new VectorOfVectorOfPoint();
            //VectorOfVectorOfPoint Dynstorage = new VectorOfVectorOfPoint();

            Mat dst1 = new Mat();
            CvInvoke.Threshold(img, dst1, threshold_value, 255,threshs[n]);
           // imageBox2.Image = dst1.Bitmap;
            VectorOfVectorOfPoint vvp = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint use_vvp = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(dst1, vvp, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            int number = vvp.ToArrayOfArray().Length;//取得轮廓的数量.
           
            for (int i = 0; i < number; i++)
            {
                VectorOfPoint  vp = vvp[i];
                double area = CvInvoke.ContourArea(vp);
                if (area > minArea  && area < maxArea)//可按实际图片修改
                {
                    use_vvp.Push(vp);                   
                }
            }
            if (use_vvp.Size > 0)
            {          
                Mat result = new Mat(img.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
                result.SetTo(new MCvScalar(0, 0, 0));
                CvInvoke.DrawContours(result, use_vvp, -1, new MCvScalar(0, 255, 0));

                imageBox2.Image = result;
             
            }         
        } 
    }
}
