using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
                
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
        //    double sigma = double.Parse(txtGaussSigma.Text);
        //    int maskSize = (int)nudMaskSize.Value;
        //    ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
        //    ImageOperations.DisplayImage(ImageMatrix, pictureBox2);

            int K;
            
            List<RGBPixel> color = new List<RGBPixel>();
            color = Graph.get_color(ImageMatrix);
            //MessageBox.Show(color.Count.ToString());
            textBox2.Text = color.Count.ToString();
            List< Graph.edge> Mst = new List<Graph.edge>();
            Mst = Graph.MST(color);
            textBox1.Text = Graph.sum.ToString();
            int d = Graph.Num_cluster(Mst);
            textBox3.Text = d.ToString();
            if (txt_k.Text == "")
            {
                
                K = d;
                //MessageBox.Show(d.ToString());
                
            }
            else
            { 
            K= int.Parse(txt_k.Text);
            }
            List<List<RGBPixel>> C = new List<List<RGBPixel>>();
            C = Graph.Cluster(K, color, Mst);
            RGBPixel[, ,] final = new RGBPixel[256, 256, 256];
            final = Graph.finalcolor(C);
            Graph.painting(ImageMatrix,final);
            ImageOperations.DisplayImage(ImageMatrix,pictureBox2);

            
        }

        private void txtGaussSigma_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void nudMaskSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
       
    }
}