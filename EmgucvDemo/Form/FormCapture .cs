using System;
using System.Windows.Forms;
using Emgu.CV;


namespace EmgucvDemo
{
    public partial class FormCapture : Form
    {
        VideoCapture _capture;
        Mat frame = new Mat();

        public FormCapture()
        {
            InitializeComponent();
            _capture = new VideoCapture();
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 320);//设置捕捉到帧的高度为320
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 240);//设置捕捉到帧的宽度为240
            _capture.FlipHorizontal = true;//捕捉到帧数据进行水平翻转
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Write(frame);
            _capture.ImageGrabbed += new EventHandler(video);//捕捉到帧执行线程
            _capture.Start();//开始捕捉帧
         
            //或者以下代码实现
            //   Application.Idle +=new EventHandler(frame);
        }

        private void video(object sender,EventArgs e)
        {        
            _capture.Retrieve(frame,0);//进行帧捕捉
            imageBox1.Image = frame;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _capture.Stop();//暂停捕捉帧
       
        }

        public void Write(Mat frame)
        {
            string path = "C:\\Users\\Public\\Desktop\\1.avi";    
            VideoWriter _Wvideo = new VideoWriter(path, VideoWriter.Fourcc('M', 'J', 'P', 'G'), 25, frame.Size, true);
           _Wvideo.Write(frame);
        }
    }
}
