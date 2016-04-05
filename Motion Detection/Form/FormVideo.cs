using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using AviFile;

namespace Motion_Detection_v2
{
    public partial class FormVideo : Form
    {
        private Capture cap;
        private AviManager aviManager;
        private VideoStream aviStream;
        private int videoIncrement = 0;
        private int videoSleep = 0;
        private int aviNumber = 0;

        public FormVideo(int index)
        {
            InitializeComponent();
            cap = new Capture(index);
            cap.FlipHorizontal = true;
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 240);
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 320);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recordTimer.Enabled = true;
            recordTimer.Interval = 24;
            trackBar1.Minimum = 24;
            trackBar1.Maximum = 72;
        }

        private void recordTimer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> frame = cap.QueryFrame();
            if (videoIncrement < trackBar1.Minimum)
                videoIncrement++;
            else
            {
                String filename = "" + aviNumber;
                
                if (videoIncrement > trackBar1.Maximum)
                {
                    try
                    {
                        aviManager.Close();
                    }
                    catch (Exception ex) { }
                    videoIncrement = 0;
                }
                else if (videoIncrement == trackBar1.Minimum)
                {
                    aviManager = new AviManager(@"video_noise/" + filename + ".avi", false);
                    //add a new video stream and one frame to the new file
                    aviStream = aviManager.AddVideoStream(false, 24, frame.ToBitmap());
                    aviNumber++;
                    videoIncrement++;
                }
                else
                {
                    trackBar1.Value = videoIncrement;
                    videoIncrement++;
                    aviStream.AddFrame(frame.ToBitmap());
                }
            }
            pictureBox3.Image = frame.ToBitmap();
        }

        private void FormVideo_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                aviManager.Close();
            }
            catch (Exception ex){}
        }
    }
}
