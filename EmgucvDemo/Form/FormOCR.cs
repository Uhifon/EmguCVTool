using System;

using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.OCR;


namespace EmgucvDemo
{
    public partial class FormOCR : Form
    {
        private Tesseract _ocr;//创建Tesseract 类
        static string filename;
        OpenFileDialog open;
        Image<Bgr, byte> image;//创建Image 输入图片
        Image<Gray, byte> gray;//创建Image 灰度输入图片。

        public FormOCR()
        {
            InitializeComponent();

            _ocr = new Tesseract();//实例化Tesseract 类。
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName;
                imageBox1.Load(filename);
                image = new Image<Bgr, byte>(open.FileName);
                gray = image.Convert<Gray, Byte>();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath+"//";//申明数据源的路径，在运行目录的tessdata 文件夹下。
            string language = "";//申明选择语言。
                                 //*判断选择的语言*//
            if (checkBox1.Checked && checkBox2.Checked)//checkBox1 为识别英文。
            {
                language = "chi_sim+eng";
            }
            else
            {
                if (checkBox2.Checked)
                {
                    language = "chi_sim";
                }
                else
                {
                    language = "eng";
                    checkBox1.Checked = true;
                }
            }
            try
            {
                _ocr = new Tesseract("", language, OcrEngineMode.Default);//指定参数实例化tessdata 类。地址为空时，需将tessdata文件夹放在debug根目录                        
                _ocr.PageSegMode = PageSegMode.SingleBlock;
                _ocr.SetImage(gray);                
                int result = _ocr.Recognize();
                if (result != 0)
                {
                    MessageBox.Show("识别失败！");
                    return;
                }
                Tesseract.Character[] characters = _ocr.GetCharacters();//获取识别数据
                //Bgr drawColor = new Bgr(Color.Blue);//创建Bgr 为蓝色。
                //foreach (Tesseract.Character c in characters)//遍历每个识别数据。
                //{
                //    image.Draw(c.Region, drawColor, 1);//绘制检测到的区域。
                //}
                //imageBox1.Image = image;//显示绘制矩形区域的图像            
                String text = _ocr.GetUTF8Text();//得到识别字符串。
                richTextBox1.Text = text;//显示获取的字符串。
            }
            catch
            {
                MessageBox.Show("检查运行目录是否有语言包");
            }
        }
    }
}
  
 
 