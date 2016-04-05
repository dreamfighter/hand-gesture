using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System.Diagnostics;

namespace Motion_Detection_v2
{
    public partial class FormFaceDetection : Form
    {
        private Emgu.CV.Capture cap;
        private HaarCascade face = new HaarCascade("haarcascade_frontalface_alt2.xml");
        private HaarCascade eye = new HaarCascade("haarcascade_eye.xml");

        public FormFaceDetection()
        {
            InitializeComponent();

            cap = new Emgu.CV.Capture(1);
            cap.FlipHorizontal = true;
            //timer1.Interval = 30;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs eventArgs)
        {
            using (Image<Bgr, Byte> image = cap.QuerySmallFrame())
            {
                Image<Gray, Byte> gray = image.Convert<Gray, Byte>(); //Convert it to Grayscale
                gray._EqualizeHist();
                //Stopwatch watch = Stopwatch.StartNew();
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
              face,
              1.1,
              10,
              Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
              new Size(20, 20));

                if (facesDetected.Length > 0 && facesDetected[0].Length > 0)
                {
                    
                    image.Draw(facesDetected[0][0].rect, new Bgr(Color.Blue), 2);
                    /*
                    foreach (MCvAvgComp f in facesDetected[0])
                    {
                        //draw the face detected in the 0th (gray) channel with blue color
                        image.Draw(f.rect, new Bgr(Color.Blue), 2);

                        //Set the region of interest on the faces
                        gray.ROI = f.rect;
                    
                        MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                           eye,
                           1.1,
                           10,
                           Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                           new Size(20, 20));
                        gray.ROI = Rectangle.Empty;

                    
                        foreach (MCvAvgComp e in eyesDetected[0])
                        {
                            Rectangle eyeRect = e.rect;
                            eyeRect.Offset(f.rect.X, f.rect.Y);
                            image.Draw(eyeRect, new Bgr(Color.Red), 2);
                        }
                    
                    }
                     */
                    //watch.Stop();
                    pictureBox1.Image = image.ToBitmap();
                }
                //ImageViewer.Show(image, String.Format("Perform face and eye detection in {0} milliseconds", watch.ElapsedMilliseconds));
            }
        }
    }
}
