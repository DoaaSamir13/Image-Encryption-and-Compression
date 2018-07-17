using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ImageEncryptCompress;

namespace ImageQuantization
{
    public partial class MainForm : Form 
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        Encryption obj = new Encryption();
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

        private void button2_Click(object sender, EventArgs e)
        {

            string initial_seed;
            int tap_position;
            initial_seed = textBox1.Text;
            tap_position = int.Parse(textBox2.Text);
            // hna m7taga 2handl lw md5l4 initial seed w tap positon


            int width = ImageOperations.GetWidth(ImageMatrix);
            int height = ImageOperations.GetHeight(ImageMatrix);

            obj = new Encryption(initial_seed, tap_position);
            obj.convert_str_to_arr();
            obj.enc(ImageMatrix, width, height);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int width = ImageOperations.GetWidth(ImageMatrix);
            int height = ImageOperations.GetHeight(ImageMatrix);

            compression compress = new compression();
            compress.count_color(ImageMatrix, width, height);
            compress.final_tree();
        }
    }
}