using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmgucvDemo
{

    public partial class FormMorphology : Form
    {
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> src;

        Emgu.CV.CvEnum.MorphOp morphop;

        public FormMorphology()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
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

        private void button7_Click(object sender, EventArgs e)
        {      
            switch (comboBox1.SelectedIndex)
            {
                case 0: morphop = Emgu.CV.CvEnum.MorphOp.Erode;break;
                case 1: morphop = Emgu.CV.CvEnum.MorphOp.Dilate; break;
                case 2: morphop = Emgu.CV.CvEnum.MorphOp.Open; break;
                case 3: morphop = Emgu.CV.CvEnum.MorphOp.Close; break;
                case 4: morphop = Emgu.CV.CvEnum.MorphOp.Gradient; break;
                case 5: morphop = Emgu.CV.CvEnum.MorphOp.Tophat; break;
                case 6: morphop = Emgu.CV.CvEnum.MorphOp.Blackhat; break;
                default:break;
            }            
            //开运算(Opening Operation)：先腐蚀后膨胀.
            //开运算可以用来消除小物体、在纤细点处分离物体、平滑较大物体的边界的同时并不明显改变其面积
            Image<Bgr, byte> dst = src.CopyBlank();
            //定义内核的大小和形状，一般情况Size里的值为Point里对应的是的两倍加1
            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
            CvInvoke.MorphologyEx(src, dst, morphop, element, new Point(1, 1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(255, 0, 0, 255));
            //0:Erode腐蚀就是原图中的高亮部分被腐蚀，“领域被蚕食”，效果图拥有比原图更小的高亮区域。
            //1:Dilate膨胀就是图像中的高亮部分进行膨胀，“领域扩张”，效果图拥有比原图更大的高亮区域。
            //2:Open //开运算(Opening Operation)：先腐蚀后膨胀.
            //开运算可以用来消除小物体、在纤细点处分离物体、平滑较大物体的边界的同时并不明显改变其面积
            //3:Close 闭运算(Closing Operation)：先膨胀后腐蚀.
            //闭运算能够排除小型黑洞(黑色区域)
            //4:Gradient//形态学梯度（Morphological Gradient）:为膨胀图与腐蚀图之差.
            //对二值图像进行这一操作可以将团块（blob）的边缘突出出来。我们可以用形态学梯度来保留物体的边缘轮廓
            //5:Tophat  //顶帽运算（Top Hat）:又常常被译为”礼帽“运算。为原图像与“开运算“的结果图之差
            //为开运算带来的结果是放大了裂缝或者局部低亮度的区域，因此，从原图中减去开运算后的图，得到的效果图突出了比原图轮廓周围的区域更明亮的区域，且这一操作和选择的核的大小相关。
            //顶帽运算往往用来分离比邻近点亮一些的斑块。当一幅图像具有大幅的背景的时候，而微小物品比较有规律的情况下，可以使用顶帽运算进行背景提取。
            //6:Blackhat //黑帽（Black Hat）运算: 为”闭运算“的结果图与原图像之差。
            //黑帽运算后的效果图突出了比原图轮廓周围的区域更暗的区域，且这一操作和选择的核的大小相关。
            //所以，黑帽运算用来分离比邻近点暗一些的斑块
            imageBox2.Image = dst;
         

        }

    }
}
