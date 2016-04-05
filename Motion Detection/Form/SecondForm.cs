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
using SVM;
using HMM;
using AviFile;

namespace Motion_Detection_v2
{
    public partial class SecondForm : Form
    {
        private bool altPressed = false;
        private Capture cap;
        private Image<Gray, Byte> cache;
        private Image<Bgr, Byte> image;
        private Filtering.Filtering filter;
        private ClassifierSvm svm;
        private ClassifierHmm hmm;
        private Point cursor = new Point();
        private Point preCursor = new Point();
        private Point centerCursor = new Point();
        private Point currentCursor = new Point();
        private int scrollPos;
        private int currentScrollPos = 0;
        private String mouseEvent;
        private List<int> observation=new List<int>();
        private int imageName = 0;
        private double sMin, sMax;
        private double hMin, hMax;
        private int handStatus;
        private int prevHandStatus;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private const int SB_VERT = 0x1;
        private const int VK_MENU = 0x12;
        private const uint KEYEVENTF_KEYUP = 0x2;
        private AviManager aviManager;
        private VideoStream aviStream;
        private int videoIncrement = 0;
        private int aviNumber = 0;
        private int videoNumber = 1;
        private int indexCam = 0;

        private Preview prev = new Preview();

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, Int32 dwData, IntPtr dwExtraInfo);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public SecondForm(int indexcam)
        {
            indexCam = indexcam;
            InitializeComponent();
            InitializeCamera(indexcam);
        }

        private void InitializeCamera(int indexcam)
        {
            cap = new Capture(indexcam);
            //cap = new Capture("video/F/video (" + videoNumber + ").avi");
            cap.FlipHorizontal = true;
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 240);
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 320);
            videoNumber++;
        }

        private void SecondForm_Load(object sender, EventArgs e)
        {
            svm = new ClassifierSvm();
            svm.readModel("model/temp.mdl");
            hmm = new ClassifierHmm();
            filter = new Filtering.Filtering();
            try
            {
                String[] temp = filter.loadSetting();
                hMin = int.Parse(temp[0]);
                hMax = int.Parse(temp[1]);
                sMin = int.Parse(temp[2]);
                sMax = int.Parse(temp[3]);
            }
            catch (Exception ex)
            {
                hMin = 29;
                hMax = 90;
                sMin = 59;
                sMax = 255;
            }
            trackHsvMin.Value = (int)hMin;// 100;
            trackHsvMax.Value = (int)hMax;// 123; 
            mouseEvent = "";
        }

        private void timerDiference_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(400, 400);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(400, 400);

            //get image from camera
            try
            {
                using (image = cap.QueryFrame())
                {

                    pictureBox1.Image = image.ToBitmap();
                    if (checkSaturation.Checked)
                    {
                        sMin = trackHsvMin.Value;
                        sMax = trackHsvMax.Value;
                    }
                    else
                    {
                        hMin = trackHsvMin.Value;
                        hMax = trackHsvMax.Value;
                    }

                    //color segmentation
                    cache = filter.HSV(image, hMin, hMax, sMin, sMax);
                    //pictureBox2.Image = cache.ToBitmap();

                    //filter noise
                    //cache._Dilate(1);
                    //cache._Erode(1);
                    //pictureBox8.Image = cache.ToBitmap();
                    //cache = filter.Median(cache, 7);
                    //pictureBox6.Image = cache.ToBitmap();
                    //cache = filter.Mean(cache, 7);
                    //pictureBox7.Image = cache.ToBitmap();

                    //get Blob
                    try
                    {
                        //temp1 = filter.camShift(cache, out temp2, out cursor);
                        temp1 = filter.detectBlob(cache, out temp2, out cursor);
                    }
                    catch (Exception ex) { }

                    pictureBox3.Image = temp2.ToBitmap();
                    MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, 1.0, 1.0);
                    temp2.Draw(mouseEvent, ref font, new Point(100, 100), new Bgr(0, 0, 255));
                    prev.pictureBox1.Image = temp2.ToBitmap();

                    if (checkSave.Checked)
                    {
                        imageName++;
                        temp1.Save(saveDirectory.Text + "/image (" + imageName + ").bmp");
                    }

                    //feature extraction
                    Image<Gray, Byte> result = filter.reduceSize(temp1);

                    //SVM clasification
                    prevHandStatus = handStatus;
                    handStatus = svm.predict(result, new Size(10, 10));
                    if (checkPredict.Checked && result.GetAverage().Intensity != 0 && temp1.Convert<Gray, Byte>().GetSum().Intensity > 10000)
                    {
                        if (prevHandStatus != 1 && handStatus == 1)
                        {
                            GetCursorPos(out currentCursor);
                            centerCursor = cursor;
                        }


                        if ((prevHandStatus == 1 && handStatus != 1) && !timerCache.Enabled)
                            timerCache.Enabled = true;
                        else if (mouseEvent == "C" && (prevHandStatus == 0 && handStatus != 0) && !timerCache.Enabled)
                        {
                            timerCache.Enabled = true;
                        }

                        if (prevHandStatus != 11 && timerCache.Enabled)
                            observation.Add(prevHandStatus);

                        if (handStatus != 11 && timerCache.Enabled)
                            observation.Add(handStatus);

                        infoSvm.Text += handStatus + "-";

                        infoSvm.SelectionStart = infoSvm.Text.Length;
                        infoSvm.ScrollToCaret();
                        infoSvm.Refresh();
                    }
                    //pictureBox4.Image = result.ToBitmap();
                    //pictureBox5.Image = result.ToBitmap();

                    image.Dispose();
                    cache.Dispose();
                    temp1.Dispose();
                    temp2.Dispose();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                if (videoNumber > 1)
                    cap.Dispose();
                InitializeCamera(indexCam);
                if (videoNumber >= 20)
                    timerDiference.Enabled = false;
            }
        }

        private void mouseAction()
        {
            switch (mouseEvent)
            {
                case "A"://click
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "B"://double click
                    {
                        break;
                    }
                case "C"://drag 
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "D"://drop
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        break;
                    }
                case "E"://scrolldown
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, new System.IntPtr());
                        //scrollPos = 60;
                        //scrollDown();
                        break;
                    }
                case "F"://scrollup
                    {
                        scrollPos = 60;
                        scrollUp();
                        break;
                    }
                case "G":// ALT Keydown
                    {
                       
                        if (!altPressed)
                            keybd_event((byte)VK_MENU, 0, 0, 0);
                        else
                            keybd_event((byte)VK_MENU, 0, KEYEVENTF_KEYUP, 0);
                        altPressed = !altPressed;
                        break;
                    }
            }
        }

        public void scrollDown()
        {
            if (scrollPos - currentScrollPos > 0)
            {
                int y = (scrollPos - currentScrollPos) / 10;
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -y, new System.IntPtr());
                currentScrollPos++;
                scrollDown();
            }
            currentScrollPos = 0;
        }

        public void scrollUp()
        {
            if (scrollPos - currentScrollPos > 0)
            {
                int y = (scrollPos - currentScrollPos) / 20;
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, y, new System.IntPtr());
                currentScrollPos++;
                scrollUp();
            }
            currentScrollPos = 0;
        }

        private void timerCache_Tick(object sender, EventArgs e)
        {
            if (observation.Count != 0)
            {
                mouseEvent = hmm.predict(observation.ToArray());
                mouseAction();
                infoHmm.Text += mouseEvent + ":" + observation[0];
                for (int i = 1; i < observation.Count; i++)
                    infoHmm.Text += "-" + observation[i];
                infoHmm.Text += System.Environment.NewLine;
                infoHmm.SelectionStart = infoHmm.Text.Length;
                infoHmm.ScrollToCaret();
                infoHmm.Refresh();
                observation = new List<int>();
            }
            timerCache.Enabled = false;
        }

        private void mouseTimer_Tick(object sender, EventArgs e)
        {
            if (handStatus == 1 || (mouseEvent == "C" && handStatus==0))
            {
                try
                {
                    cursor.X = currentCursor.X + cursor.X - centerCursor.X;
                    cursor.Y = currentCursor.Y + cursor.Y - centerCursor.Y;

                }
                catch (Exception ex)
                {
                    centerCursor = cursor;
                }
                GetCursorPos(out preCursor);
                int x = preCursor.X + (int)(1.0 * (cursor.X - preCursor.X) / 3);
                int y = preCursor.Y + (int)(1.0 * (cursor.Y - preCursor.Y) / 3);
                if (Math.Abs(cursor.X - preCursor.X) > 20)
                    SetCursorPos(x, y);

                if (Math.Abs(cursor.Y - preCursor.Y) > 20) 
                    SetCursorPos(x, y);
            }
        }

        private void butTraining_Click(object sender, EventArgs e)
        {
            svm.learning("data-svm.train");
        }

        private void butPredictTraining_Click(object sender, EventArgs e)
        {
            MessageBox.Show(svm.predict("data-svm.train").ToString() + "%");
        }

        private void butPredictTesting_Click(object sender, EventArgs e)
        {
            MessageBox.Show(svm.predict("data-svm.test").ToString() + "%");
        }

        private void butCreateModel_Click(object sender, EventArgs e)
        {
            svm.learning("data-svm.train");
        }

        private void butCreateTraining_Click(object sender, EventArgs e)
        {
            svm.createDataTraining("data-svm.train", "imagesTraining_right_left", 12, 20);
        }

        private void butcapture_Click(object sender, EventArgs e)
        {
            if (!timerDiference.Enabled)
            {
                butcapture.Text = "Pause";
                timerDiference.Enabled = true;
                timerCache.Enabled = true;
                mouseTimer.Enabled = true;
                timerDiference.Interval = 100;
                timerCache.Interval = 300;
                mouseTimer.Interval = timerDiference.Interval;
            }
            else
            {
                butcapture.Text = "Capture";
                timerDiference.Enabled = false;
                timerCache.Enabled = false;
                mouseTimer.Enabled = false;
            }
        }

        private void butTestingCreate_Click(object sender, EventArgs e)
        {
            svm.createDataTesting("data-svm.test", "imagesTesting_left", 11, 20);
        }

        private void butTrainingHmm_Click(object sender, EventArgs e)
        {
            String[] c = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int[] A = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            int B = 11;
            hmm = new ClassifierHmm(c, A, B);
            MessageBox.Show("Finish");
        }

        private void butTrainHmm_Click(object sender, EventArgs e)
        {
            hmm.readModel("model/temp.hmm");
            hmm.readObservation("training/data-observation.sequence");
            hmm.learning(100, 0.01);
            MessageBox.Show("Finish");
        }

        private void checkPredict_CheckedChanged(object sender, EventArgs e)
        {
            mouseTimer.Enabled = true;
            hmm.readModel("model/left-right2.hmm");
            prev.Show();
        }

        private void checkSaturation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSaturation.Checked)
            {
                trackHsvMin.Value = (int) sMin;
                trackHsvMax.Value = (int) sMax;
            }
            else
            {
                trackHsvMin.Value = (int)hMin;
                trackHsvMax.Value = (int)hMax;
            }
        }

        private void SecondForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            filter.saveSetting((int)hMin, (int)hMax, (int)sMin, (int)sMax);
            try
            {
                aviManager.Close();
            }
            catch (Exception ex) { }
        }

        private void butPredictHmm_Click(object sender, EventArgs e)
        {
            hmm.readModel("model/data-mdl.hmm");
            MessageBox.Show(hmm.predict("training/data-observation.sequence").ToString() + "%");
        }

        private void butChooseDir_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
                saveDirectory.Text = folderBrowserDialog1.SelectedPath;
        }

        private void butRecord_Click(object sender, EventArgs e)
        {
            //create a new AVI file            
            recordTimer.Enabled = true;
        }

        private void recordTimer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> frame = cap.QueryFrame();
            String filename = "A/" + aviNumber;
            if (checkSave.Checked)
            {
                imageName++;
                frame.Save(saveDirectory.Text + "/image (" + imageName + ").bmp");
            }
            if (aviNumber == 0)
            {
                aviManager = new AviManager(@"" + saveDirectory.Text + "/video_testing/" + filename + ".avi", false);
                //add a new video stream and one frame to the new file                
                aviStream = aviManager.AddVideoStream(false, 20, frame.ToBitmap());
                aviNumber++;
            }
            videoIncrement++;
            if (videoIncrement > 15)
            {
                aviManager.Close();
                videoIncrement = 0;
                aviManager = new AviManager(@"" + saveDirectory.Text + "/video_testing/" + filename + ".avi", false);
                //add a new video stream and one frame to the new file
                aviStream = aviManager.AddVideoStream(false, 20, frame.ToBitmap());
                aviNumber++;
            }
            aviStream.AddFrame(frame.ToBitmap());
            pictureBox1.Image = frame.ToBitmap();
        }

        private void checkSave_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
