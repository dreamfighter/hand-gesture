using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Motion_Detection_v2
{
    public partial class DepthForm : Form
    {
        public DepthForm()
        {
            InitializeComponent();
        }

        private void DepthForm_Load(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            String input = "";
            Bitmap bmp = new Bitmap(640, 480);
            int y = 0;
            while((input = sr.ReadLine())!=null){
                String[] gembel = input.Split(',');
                for (int x = 0; x < gembel.Length; x++)
                {
                    //int p = 255 * ((Int32.Parse(gembel[x]) - 800) / (3500 - 800));
                    int p = Int32.Parse(gembel[x]);
                    p = (255 * p) / 8550;
                    if (p > 255)
                    {
                        p = 255;
                    }
                    //Console.WriteLine(p);

                    Color c = Color.FromArgb(255, 0, 0, p);
                    bmp.SetPixel(x, y, c);
                    /*
                    if (p < 1024)
                    {
                        Color c = Color.FromArgb(
                        bmp.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                     * 
                     */
                }
                y++;
            }
            pictureBox1.Image = bmp;
        }
    }
}
