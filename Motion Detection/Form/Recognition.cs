using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVM;
using HMM;
using HandShape;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Motion_Detection_v2
{
    public partial class Recognition : Form
    {
        private Filtering.Filtering filter = new Filtering.Filtering();

        private MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 0.6, 0.6);
        private Point cursor = new Point();
        private Image<Gray, Byte> captureImage = new Image<Gray, Byte>(0, 0);
        private Image<Bgr, Byte> output = new Image<Bgr, byte>(10, 10);
        private List<int> observation = new List<int>();
        private StreamWriter log;
        private ClassifierSvm svm;
        private ClassifierHmm hmm;
        private Capture cap;
        private Setting setting = new Setting();
        private Preview prev = new Preview();
        private MouseAction mouseAction = new MouseAction();
        private double sMin, sMax;
        private double hMin, hMax;
        private int sleep = 0;
        private int tickNumber = 0;
        private bool start = false;
        private string gesture = "";
        private HandShape.HandShape handShape;
        private int handStatus = -1;
        private static int MAX_STACK = 40;
        private Stack<int> stackObservations = new Stack<int>();
        private Filtering.KalmanFiltering kalman = new Filtering.KalmanFiltering();

        public Recognition()
        {
            InitializeComponent();
            loadSetting();
            settingWebcam();
            settingFiltering();
            settingSvmClassifier();
            settingHmmClassifier();
            settingInterval();
            
        }

        public void loadSetting()
        {
            setting.loadSetting();
            filter.loadSetting();
        }

        public void clearSetting()
        {
            svmParams.Items.Clear();
            hmmParams.Items.Clear();
            ListOfDevices.Items.Clear();
        }

        private void settingCapture()
        {
            cap = new Capture(ListOfDevices.SelectedIndex);
            //cap = new Capture("video_testing/realtime.avi");
            cap.FlipHorizontal = true;
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 240);
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 320);
        }

        private void settingFiltering()
        {
            hMin = setting.filterVal[0];
            hMax = setting.filterVal[1];
            sMin = setting.filterVal[2];
            sMax = setting.filterVal[3];
            trackHsvMin.Value = (int)hMin;// 100;
            trackHsvMax.Value = (int)hMax;// 123;
            
        }

        private void settingSvmClassifier()
        {
            DirectoryInfo di = new DirectoryInfo("model");
            FileInfo[] rgFiles = di.GetFiles("*.mdl");
            foreach (FileInfo fi in rgFiles)
            {
                svmParams.Items.Add(fi.Name);
                if (fi.Name == setting.svmParam)
                    svmParams.SelectedItem = svmParams.Items[svmParams.Items.Count - 1];
            }
        }

        private void settingHmmClassifier()
        {
            DirectoryInfo di = new DirectoryInfo("model");
            FileInfo[] rgFiles = di.GetFiles("*.hmm");
            foreach (FileInfo fi in rgFiles)
            {
                hmmParams.Items.Add(fi.Name);
                if (fi.Name == setting.hmmParam)
                    hmmParams.SelectedItem = hmmParams.Items[hmmParams.Items.Count - 1];
            }
        }

        private void settingInterval()
        {
            intervalNum.Value = setting.interval;
        }

        private void loadClassifier()
        {
            svm = new ClassifierSvm();
            svm.readModel("model/" + svmParams.Items[svmParams.SelectedIndex].ToString());
            Console.WriteLine("model/" + svmParams.Items[svmParams.SelectedIndex].ToString());
            hmm = new ClassifierHmm();
            hmm.readModel("model/" + hmmParams.Items[hmmParams.SelectedIndex].ToString());
        }

        private void startClassifier()
        {
            settingCapture();
            loadClassifier();
        }

        private void loadLog()
        {
            log = new StreamWriter("predict/log.predict");
        }

        private void settingWebcam()
        {
            foreach (Camera cam in CameraService.AvailableCameras)
            {
                ListOfDevices.Items.Add(cam);
                if (cam.ToString() == setting.cameraParam)
                    ListOfDevices.SelectedItem = ListOfDevices.Items[ListOfDevices.Items.Count - 1];
            }
        }
        
        private void checkSaturation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSaturation.Checked)
            {
                trackHsvMin.Value = (int)sMin;
                trackHsvMax.Value = (int)sMax;
            }
            else
            {
                trackHsvMin.Value = (int)hMin;
                trackHsvMax.Value = (int)hMax;
            }
        }

        private void Recognition_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                setting.saveSetting((int)hMin, (int)hMax, (int)sMin, (int)sMax, svmParams.SelectedItem.ToString(), hmmParams.SelectedItem.ToString(), ListOfDevices.SelectedItem.ToString(), (int)intervalNum.Value);
            }
            catch (Exception ex) { Console.WriteLine(""); }
            
            try
            {
                log.Close();
            }
            catch (Exception ex) { Console.Write(ex.Message); }
        }

        public Image<Bgr, Byte> extractFeature1(Image<Bgr, Byte> image,out Rectangle rec)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, byte>(0, 0);
            rec = new Rectangle();
            
            Image<Gray, Byte> cache;
            //color segmentation
            //cache = filter.HSV(image, hMin, hMax, sMin, sMax);
            cache = filter.YCrCb(image, hMin, hMax, sMin, sMax);

            //filter noise
            cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                temp1 = filter.detectBlob(cache, out rec, out cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cache.Dispose();
            return temp1;
        }

        public Image<Bgr, Byte> extractFeature(Image<Bgr, Byte> image, out Rectangle rec)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            Image<Gray, Byte> cache;
            rec = new Rectangle();
            //color segmentation
            cache = filter.HSV(image, hMin, hMax, sMin, sMax);

            //filter noise
            //cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                temp1 = filter.detectBlob(cache, out temp2, out rec, out cursor);
            }
            catch (Exception ex){Console.WriteLine(ex.Message);}

            if (filteringToolStripMenuItem.Checked)
                output = temp2;

            cache.Dispose();
            //temp2.Dispose();
            return temp1;
        }



        public Image<Bgr, Byte> extractFeatureHand(Image<Bgr, Byte> image, out Rectangle rec)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            Image<Gray, Byte> cache;
            rec = new Rectangle();
            //color segmentation
            //cache = filter.HSV(image, hMin, hMax, sMin, sMax);

            //color segmentation
            int[] val = filter.settingFilter();
            // cache = filter.HSV(image, val[0], val[1], val[2], val[3]);
            cache = filter.YCrCb(image, filter.yCrCb.cr_min, filter.yCrCb.cr_max, filter.yCrCb.cb_min, filter.yCrCb.cb_max);

            //filter noise
            cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                //temp1 = filter.detectBlob(cache, out temp2, out rec, out cursor);
                handShape = filter.extractContourAndHull(cache, out temp2);
                //cursor = new Point((int)handShape.getCenter().X, (int)handShape.getCenter().Y);
                //cursor = new Point((int)handShape.fingersStartPoint[1].X, (int)handShape.fingersStartPoint[0].X);
                rec = handShape.handrect;
                temp1 = handShape.getImage().Convert<Bgr, Byte>();
                //Console.WriteLine("kalman prediction point " + handShape.fingersStartPoint[1].X + "," + handShape.fingersStartPoint[1].Y);
                PointF[] pts = kalman.filterPoints(handShape.fingersStartPoint[1]);
                cursor = new Point((int)pts[1].X, (int)pts[1].Y);
                //Console.WriteLine("kalman prediction " + pts[1].X + "," + pts[1].Y);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (filteringToolStripMenuItem.Checked)
            {
                output = temp2;
            }

            cache.Dispose();
            //temp2.Dispose();
            return temp1;
        }

        private String action(String gesture)
        {
            if (actionEnableToolStripMenuItem.Checked)
            {
                mouseAction.action(gesture);
            }
            
            switch (gesture)
            {
                case "A": return "Left Click";
                case "B": return "Right Click";
                case "C": return "Double Click";
                case "D": return "Mouse Down";
                case "E": return "Mouse Up";
                //case "F": return "Shift Tab";
                //case "G": return "Tab";
                //case "H": return "Scroll Down";
                //case "I": return "Scroll Up";
                default :return "Idle";
            }
        }

        private void capture_Tick(object sender, EventArgs e)
        {
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

            //cap = new Emgu.CV.Capture("thesis/A/image (50).bmp");

            //using (Image<Bgr, Byte> image = new Image<Bgr, Byte>("C:\\Users\\zeger\\Pictures\\image_testing\\2\\image (38).bmp"))
            using (Image<Bgr, Byte> image = cap.QueryFrame())
            {
                
                    //if (image != null && result.GetAverage().Intensity != 0 && temp1.Convert<Gray, Byte>().GetSum().Intensity > 10000)
                if (image != null)
                {
                    Rectangle rec;
                    Image<Bgr, Byte> img = extractFeatureHand(image, out rec);
                    /*
                    try
                    {
                        captureImage = filter.reduceSize(img);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    */

                    if (trackingEnableToolStripMenuItem.Checked && handShape.maximumInscribedCircle.Radius>50)
                    {
                        mouseAction.setCursor(cursor);
                    }
                    

                    //SVM clasification
                    image.Draw(rec, new Bgr(0, 0, 255), 1);


                    if (originalToolStripMenuItem.Checked)
                    {
                        output = image;
                    }

                    output.Draw(handStatus+"", ref font, new Point((int)handShape.getCenter().X, (int)handShape.getCenter().Y), new Bgr(255, 0, 255));
                    output.Draw(gesture, ref font, new Point(rec.X, rec.Y - 5), new Bgr(255, 0, 255));
                    //output.Draw(new CircleF(new PointF(kalman.px, kalman.py), 4), new Bgr(Color.Blue), 2);
                    //output.Draw(new CircleF(new PointF(kalman.cx, kalman.cy), 4), new Bgr(Color.Purple), 2);
                    
                    if (!previewCheck.Checked)
                    {
                        pictureBox3.Image = output.ToBitmap();
                    }
                    else
                    {
                        prev.pictureBox1.Image = output.ToBitmap();
                    }

                }
                else
                {
                    timerCapture.Enabled = false;
                    timerObserve.Enabled = false;
                    //timerGesture.Enabled = false;
                }
                output.Dispose();
                image.Dispose();
            }
        }

        private void timerObserve_Tick(object sender, EventArgs e)
        {
            try
            {
                //SVM clasification
                if (trackBar1.Value + tickNumber >= trackBar1.Maximum)
                {
                    if (observation.Count <= 0)
                        gesture = "null";
                    else
                    {
                        double prob = 0;
                        string o = observation[0].ToString();
                        for (int i = 1; i < observation.Count; i++)
                        {
                            o += "-" + observation[i].ToString();
                        }
                        gesture = action(hmm.predict(observation.ToArray(), out prob));
                        log.WriteLine(gesture + ";" + prob + ";" + o);
                        Console.WriteLine(gesture + ";" + prob + ";" + o);
                        observation.Clear();
                        stackObservations.Clear();
                    }
                    trackBar1.Value = 0;
                    sleep = -10;
                }
                else
                {
                    if (sleep < trackBar1.Minimum)
                    {
                        sleep++;
                    }
                    else
                    {
                        trackBar1.Value += tickNumber;
                        prev.trackBar1.Value = trackBar1.Value;
                        //int handStatus = svm.predict(captureImage, new Size(10, 10));
                        //handStatus = svm.predictByAttrb("1:0.217465354691424 2:0.79750758046959 3:0.317899874390046 4:0.176208191174783 5:0 6:0 7:0 8:0 ");
                        handStatus = svm.predict(handShape);
                        Console.WriteLine(handStatus);
                        observation.Add(handStatus);
                        stackObservations.Push(handStatus);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void timerGesture_Tick(object sender, EventArgs e)
        {
            if (observation.Count <= 0)
                gesture = "null";
            else
            {
                double prob = 0;
                gesture = action(hmm.predict(observation.ToArray(), out prob));
                Console.WriteLine();
                log.WriteLine(gesture + ";" + prob);
                observation = new List<int>();
            }
            trackBar1.Value = 0;
            //timerGesture.Enabled = false;
        }

        private void recognitionBut_Click_1(object sender, EventArgs e)
        {
            if (svmParams.SelectedIndex != 0 && hmmParams.SelectedIndex != 0)
                if (!start)
                {
                    start = true;
                    recognitionBut.Text = "Stop Recognition";
                    startClassifier();
                    loadLog();

                    timerCapture.Interval = 41;
                    timerGesture.Interval = 1000;
                    timerObserve.Interval = (int)intervalNum.Value;

                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = (timerGesture.Interval / timerObserve.Interval);
                    prev.trackBar1.Minimum = 0;
                    prev.trackBar1.Maximum = (timerGesture.Interval / timerObserve.Interval);

                    tickNumber = trackBar1.Maximum / (timerGesture.Interval / timerObserve.Interval);
                    if (tickNumber == 0)
                        tickNumber = 1;

                    timerObserve.Enabled = true;
                    timerCapture.Enabled = true;
                    //timerGesture.Enabled = true;

                    if (trackingEnableToolStripMenuItem.Checked)
                    {
                        mouseAction.mouseTimerEnable();
                    }
                }
                else
                {
                    cap.Dispose();
                    start = false;
                    recognitionBut.Text = "Start Recognition";

                    timerObserve.Enabled = false;
                    timerCapture.Enabled = false;
                    //timerGesture.Enabled = false;
                    mouseAction.mouseTimerDisable();
                    log.Close();

                }
            else
                MessageBox.Show("Parameter SVM and HMM cannot null!!");
        }

        private void previewCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!previewCheck.Checked)
                prev.Hide();
            else
                prev.Show();
        }

        private void sVMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrainingSvm svmForm = new FormTrainingSvm();
            svmForm.Show();
        }

        private void hMMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrainingHmm hmmForm = new FormTrainingHmm();
            hmmForm.Show();
        }

        private void captureVideoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormVideo formvideo = new FormVideo(ListOfDevices.SelectedIndex);
            formvideo.Show();
        }

        private void seccondFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecondForm cf = new SecondForm(ListOfDevices.SelectedIndex);
            cf.Show();
        }

        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveConfigDialog.ShowDialog())
                    setting.saveSetting(saveConfigDialog.FileName,(int)hMin, (int)hMax, (int)sMin, (int)sMax, svmParams.SelectedItem.ToString(), hmmParams.SelectedItem.ToString(), ListOfDevices.SelectedItem.ToString(), (int)intervalNum.Value);
            }
            catch (Exception ex) { Console.WriteLine(""); }
        }

        private void openConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openConfigDialog.ShowDialog())
            {
                clearSetting();
                setting.loadSetting(openConfigDialog.FileName);
                settingWebcam();
                settingFiltering();
                settingSvmClassifier();
                settingHmmClassifier();
                settingInterval();
            }
        }

        private void filteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filteringToolStripMenuItem.Checked)
                filteringToolStripMenuItem.Checked = false;
            else
            {
                filteringToolStripMenuItem.Checked = true;
                originalToolStripMenuItem.Checked = false;
            }
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalToolStripMenuItem.Checked)
                originalToolStripMenuItem.Checked = false;
            else
            {
                filteringToolStripMenuItem.Checked = false;
                originalToolStripMenuItem.Checked = true;
            }

        }

        private void trackingEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (trackingEnableToolStripMenuItem.Checked)
            {
                trackingEnableToolStripMenuItem.Checked = false;
                mouseAction.mouseTimerDisable();
            }
            else
            {
                trackingEnableToolStripMenuItem.Checked = true;
                mouseAction.mouseTimerEnable();
            }
        }

        private void actionEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (actionEnableToolStripMenuItem.Checked)
                actionEnableToolStripMenuItem.Checked = false;
            else
                actionEnableToolStripMenuItem.Checked = true;
        }

        private void extractionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void faceDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFaceDetection faceDetection = new FormFaceDetection();
            faceDetection.Show();
        }

        private void depthImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepthForm form = new DepthForm();
            form.Show();
        }

       
    }
}
