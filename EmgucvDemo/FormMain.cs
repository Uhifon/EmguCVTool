using System;
using System.Windows.Forms;

namespace EmgucvDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void 图像轮廓ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormContour form_contour = new FormContour();
            form_contour.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormContour frm_contour = new FormContour();
            frm_contour.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAbsDiff frm_absdiff = new FormAbsDiff();
            frm_absdiff.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormEdge frm_edge = new FormEdge();
            frm_edge.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormFeature frm_feature = new FormFeature();
            frm_feature.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormEqualizeHist frm_equalizHist = new FormEqualizeHist();
            frm_equalizHist.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormCalibration frm_cali = new FormCalibration();
            frm_cali.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormHough frm_hough = new FormHough();
            frm_hough.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormCircle frm_circle = new FormCircle();
            frm_circle.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormFilter frm_filter = new FormFilter();
            frm_filter.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FormMorphology frm_morphology = new FormMorphology();
            frm_morphology.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormCorner frm_corner = new FormCorner();
            frm_corner.Show();
         }

        private void button12_Click(object sender, EventArgs e)
        {
            FormBlob frm_blob = new FormBlob();
            frm_blob.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FormLicensePlate frm_licensePlate = new FormLicensePlate();
            frm_licensePlate.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FormOCR frm_ocr = new FormOCR();
            frm_ocr.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FormExtract frm_extract = new FormExtract();
            frm_extract.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FormTemplateMatch frm_templatematch = new FormTemplateMatch();
            frm_templatematch.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FormQRCode frm_QRcode = new FormQRCode();
            frm_QRcode.Show();   
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FormCameraCali frm_camera_cali = new FormCameraCali();
            frm_camera_cali.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            FormBinary frm_binary = new FormBinary();
            frm_binary.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            FormPyramid frm_pyramid=new FormPyramid();
            frm_pyramid.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FormWarpAffine frm_warp_affine = new FormWarpAffine();
            frm_warp_affine.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            FormWarpPerspective frm_warp_perspective = new FormWarpPerspective();
            frm_warp_perspective.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            FormHistogram frm_histogram = new FormHistogram();
            frm_histogram.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            FormCapture frm_capture = new FormCapture();
            frm_capture.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            FormStitching frm_stitching = new FormStitching();
            frm_stitching.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            FormFloodFill frm_flood_fill = new FormFloodFill();
            frm_flood_fill.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            FormDFT frm_dft = new FormDFT();
            frm_dft.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            FormRemap frm_remap = new FormRemap();
            frm_remap.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            FormBackProjection frm_back_proj = new FormBackProjection();
            frm_back_proj.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            FormHull frmhull = new FormHull();
            frmhull.Show();


        }

        private void button32_Click(object sender, EventArgs e)
        {
            FormImageSegment frm_segment = new FormImageSegment();
            frm_segment.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            FormROI frm_roi = new FormROI();
            frm_roi.Show();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            FormColor frm_color = new FormColor();
            frm_color.Show();
        }
    }
}
