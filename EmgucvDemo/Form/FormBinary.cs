using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace EmgucvDemo
{
    public partial class FormBinary : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Gray, byte> src;
        ThresholdType[] threshold_type = new ThresholdType[7] { ThresholdType.Binary, ThresholdType.BinaryInv, ThresholdType.Trunc, ThresholdType.ToZero, ThresholdType.ToZeroInv, ThresholdType.Mask, ThresholdType.Otsu };
        AdaptiveThresholdType[] adaptive_threshold_type = new AdaptiveThresholdType[2] { AdaptiveThresholdType .GaussianC, AdaptiveThresholdType.MeanC};

        public FormBinary()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;

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
            Mat dst = new Mat();
            double threshold = Convert.ToDouble(numericUpDown1.Value);
            double maxValue = Convert.ToDouble(numericUpDown2.Value);
            int i = comboBox1.SelectedIndex;
            CvInvoke.Threshold(src, dst, threshold, maxValue, threshold_type[i]);
            imageBox2.Image = dst;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            double maxValue = Convert.ToDouble(numericUpDown2.Value);
            int i = comboBox1.SelectedIndex;
            int j = comboBox2.SelectedIndex;
            int blockSize = Convert.ToInt32(numericUpDown3.Value); 
            double param1 = Convert.ToDouble(numericUpDown4.Value);
            CvInvoke.AdaptiveThreshold(src, dst, maxValue, 
                adaptive_threshold_type[j], //— ADAPTIVE_THRESH_MEAN_C ：领域内均值   ADAPTIVE_THRESH_GAUSSIAN_C ：领域内像素点加权和，权 重为一个高斯窗口
                threshold_type[i],
                blockSize,    //规定领域大小（一个正方形的领域）
                param1////阈值等于均值或者加权值减去这个常数（为0相当于阈值 就是求得领域内均值或者加权值）这种方法理论上得到的效果更好，相当于在动态自适应的调整属于自己像素点的阈值，而不是整幅图像都用一个阈值。
                );
            imageBox2.Image = dst;
        }
    }
}
