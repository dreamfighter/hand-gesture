using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.BLOB;
using Emgu.CV.VideoSurveillance;
using Emgu.Util.TypeEnum;
using Emgu.CV;
using SVM;
using HMM;

//class yang di gunakan ada A,B,C,D,E,F,G,H,I,J

namespace Motion_Detection_v2
{
    public partial class MainForm : Form
    {

        private Emgu.CV.Capture cap;
        //private int _width, _height, _hMin = 1, _hMax = 15, _sMin = 62, _sMax = 100, _crMin = 117, _crMax = 138, _cbMin = 78, _cbMax = 115;
        private int _width, _height, _hMin = 0, _hMax = 90, _sMin = 15, _sMax = 150, _crMin = 109, _crMax = 123, _cbMin = 135, _cbMax = 168;
        private Filtering.SkinFiltering skin;
        private Filtering.Filtering filtering;

        private int counter = -1;
        private Point[] p;
        private int paused = 0;
        private int inc = 0;

        private Seq<Point> hull;
        private Seq<Point> filteredHull;
        private Seq<MCvConvexityDefect> defects;
        private MCvConvexityDefect[] defectArray;
        private Rectangle handRect;
        private MCvBox2D box;
        private Ellipse ellip;
        private Setting setting;

        //private Ycc YCrCb_min;
        //private Ycc YCrCb_max;
        //private AdaptiveSkinDetector adaptiveSkin = new AdaptiveSkinDetector(1, AdaptiveSkinDetector.MorphingMethod.ERODE_DILATE);


        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        public MainForm()
        {
            InitializeComponent();
            p = new Point[2];
            openFileDialog1.FileName = "thesis/image_training/1/image (50).bmp";
            fileNameTextBox.Text = openFileDialog1.FileName;

            filtering = new Motion_Detection_v2.Filtering.Filtering();
            filtering.loadSetting();
        }

        public void predict(Emgu.CV.Image<Gray, Byte> img)
        {
            int temp = 0;
            List<int> list = new List<int>();
            List<int> index = new List<int>();
            List<double> val = new List<double>();

            for (int i = 0; i < img.Height; i++)
            {
                temp++;
                for (int j = 0; j < img.Width; j++)
                {
                    if (img.Data[i, j, 0] == 255)
                    {
                        list.Add(j);
                    }
                }
                try
                {
                    index.Add(temp);
                    String str = "" + list.Average() / 100;
                    val.Add(double.Parse(str));
                }
                catch (Exception ex) { }
            }
            int n = index.Count();
            Node[] x = new Node[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = new Node(index[i], val[i]);
                //info.Text += index[i] + ":" + val[i] + System.Environment.NewLine;
            }

            Model model = Model.Read("model/data-2410.mdl");
            info.Text += Prediction.Predict(model, x) + System.Environment.NewLine;
            info.SelectionStart = info.Text.Length;
            info.ScrollToCaret();
            info.Refresh();
        }

        public void createDataTesting(Emgu.CV.Image<Gray, Byte> img)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter("testing/data-" + DateTime.Now.Day + DateTime.Now.Month + ".test", true);

            // write a line of text to the file
            int temp = 0;
            tw.Write(classLabel.Text + " ");
            for (int i = 0; i < img.Height; i++)
            {
                temp++;
                List<int> list = new List<int>();
                for (int j = 0; j < img.Width; j++)
                {
                    if (img.Data[i, j, 0] == 255)
                    {
                        list.Add(j);
                    }
                }
                try
                {
                    String str = "" + list.Average() / 100;
                    tw.Write(temp + ":" + str.Substring(0, 7) + " ");
                }
                catch (Exception ex) { }
            }
            tw.WriteLine("");

            // close the stream
            tw.Close();
        }

        public void createDataTraining(Emgu.CV.Image<Gray,Byte> img) 
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter("training/data-" + DateTime.Now.Day + DateTime.Now.Month +".train",true);

            // write a line of text to the file
            int temp = 0;
            tw.Write(classLabel.Text + " ");
            for (int i = 0; i < img.Height; i++)
            {
                temp++;
                List<int> list = new List<int>();
                for (int j = 0; j < img.Width; j++)
                {
                    if (img.Data[i, j, 0] == 255)
                    {
                        list.Add(j);
                    }
                }
                try
                {
                    String str = "" + list.Average() / 100;
                    tw.Write(temp + ":" + str.Substring(0, 7) + " ");
                }
                catch (Exception ex) { }
            }
            info.Text += inc++;
            tw.WriteLine(""); 
            info.SelectionStart = info.Text.Length;
            info.ScrollToCaret();
            info.Refresh();
            tw.Close();
        }

        public void initYCrCb() {

            trackCbMin.Value = filtering.yCrCb.cb_min;
            trackCbMax.Value = filtering.yCrCb.cb_max;
            trackCrMin.Value = filtering.yCrCb.cr_min;
            trackCrMax.Value = filtering.yCrCb.cr_max;

            //YCrCb_min = new Ycc(0, 131, 80);
            //YCrCb_max = new Ycc(255, 185, 135);
        }

        public void initHSV()
        {
            int[] val = filtering.settingFilter();
            _hMin = val[0];
            _hMax = val[1];
            _sMin = val[2];
            _sMax = val[3];

            if (radioButton3.Checked)
            {
                trackSkinMin.Value = _hMin;
                trackBarMinVal.Text = "" + _hMin;
                trackSkinMax.Value = _hMax;
                trackBarMaxVal.Text = "" + _hMax;
            }
            else {
                trackSkinMin.Value = _sMin;
                trackBarMinVal.Text = "" + _sMin;
                trackSkinMax.Value = _sMax;
                trackBarMaxVal.Text = "" + _sMax;
            }
        }

        public Blob[] GetBlobs(Emgu.CV.Image<Gray, Byte> Ptr, int threshold, IntPtr maskImage, bool borderColor, bool findMoments)
        {
            int count;
            IntPtr vector = Emgu.CV.BlobsInvoke.CvGetBlobs(Ptr, threshold, maskImage, borderColor, findMoments, out count);
            IntPtr[] blobsPtrs = new IntPtr[count];
            GCHandle handle = GCHandle.Alloc(blobsPtrs, GCHandleType.Pinned);
            Emgu.Util.Toolbox.memcpy(handle.AddrOfPinnedObject(), vector, count * Marshal.SizeOf(typeof(IntPtr)));
            handle.Free();
            Blob[] Blobs = new Blob[count];
            for (int i = 0; i < blobsPtrs.Length; i++)
                Blobs[i] = new Blob(blobsPtrs[i]);
            return Blobs;
        }

        public Blob GetSingleBlobs(Emgu.CV.Image<Gray, Byte> Ptr, int threshold, IntPtr maskImage, bool borderColor, bool findMoments)
        {
            int count;
            IntPtr vector = Emgu.CV.BlobsInvoke.CvGetBlobs(Ptr, threshold, maskImage, borderColor, findMoments, out count);
            IntPtr[] blobsPtrs = new IntPtr[count];
            GCHandle handle = GCHandle.Alloc(blobsPtrs, GCHandleType.Pinned);
            Emgu.Util.Toolbox.memcpy(handle.AddrOfPinnedObject(), vector, count * Marshal.SizeOf(typeof(IntPtr)));
            handle.Free();
            //Emgu.CV.Blob[] Blobs = new Emgu.CV.Blob[count];
            //for (int i = 0; i < blobsPtrs.Length; i++)
            //Blobs[0] = new Emgu.CV.Blob(blobsPtrs[0]);
            return new Blob(blobsPtrs[0]);
        }

        public void DrawBlobs(Emgu.CV.Image<Bgr, Byte> img, Blob blobs, bool fill, bool drawBoundingBox, Emgu.CV.Structure.Bgr BoundingBoxColor, bool drawConvexHull, Emgu.CV.Structure.Bgr ConvexHullColor, bool drawEllipse, Emgu.CV.Structure.Bgr EllipseColor, bool drawCentroid, Emgu.CV.Structure.Bgr CentroidColor, bool drawAngle, Emgu.CV.Structure.Bgr AngleColor)
        {
            Random r = new Random(0);
            Blob b = blobs;
           // foreach (var b in blobs)
            //{
                try
                {
                    p[counter] = new Point((int)b.CentroidX, (int)b.CentroidY);
                }
                catch (Exception ex) { }
                if (counter == 1)
                {
                    Emgu.CV.Image<Gray, Byte> skeleton = new Emgu.CV.Image<Gray, Byte>(30, 30);
                    //skeleton = img.Copy(b.BoundingBox).Resize(100, 100, INTER.CV_INTER_AREA).Convert<Gray, Byte>();
                    try
                    {
                        skeleton = img.Copy(b.BoundingBox).Resize(30, 30, INTER.CV_INTER_AREA).Convert<Gray, Byte>().Canny(new Gray(100), new Gray(255));
                        //Emgu.CV.CvInvoke.cvDistTransform(skeleton, skeleton, Emgu.CV.CvEnum.DIST_TYPE.CV_DIST_L1, 5, null, IntPtr.Zero);
                        if (checkTrainData.Checked)
                        {
                            createDataTraining(skeleton);
                        }
                        if (checkTestingData.Checked)
                        {
                            createDataTesting(skeleton);
                        }
                        if (checkPredict.Checked)
                        {
                            predict(skeleton);
                        }
                        pictureBox2.Image = skeleton.ToBitmap();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (fill)
                    b.FillBlob(img, new MCvScalar(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));
                if (drawBoundingBox)
                    img.Draw(b.BoundingBox, BoundingBoxColor, 1);
                if (drawConvexHull)
                    img.DrawPolyline(b.ConvexHull, true, ConvexHullColor, 1);
                if (drawEllipse)
                    img.Draw(b.BestFitEllipse, EllipseColor, 1);



                if (drawCentroid)
                {
                    img.Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
                                           new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
                         CentroidColor, 1);
                    img.Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
                                           new Point((int)b.CentroidX, (int)b.CentroidY + 4)),
                         CentroidColor, 1);
                }
                if (drawAngle)
                {
                    double x1, x2, y1, y2;
                    x1 = b.CentroidX - 0.005 * b.Area * Math.Cos(b.Angle);
                    y1 = b.CentroidY - 0.005 * b.Area * Math.Sin(b.Angle);
                    x2 = b.CentroidX + 0.005 * b.Area * Math.Cos(b.Angle);
                    y2 = b.CentroidY + 0.005 * b.Area * Math.Sin(b.Angle);
                    img.Draw(new LineSegment2D(new Point((int)x1, (int)y1), new Point((int)x2, (int)y2)), AngleColor, 1);
                }
                b.Clear();
                //break;
            //}
        }

        public void DrawBlobs(Emgu.CV.Image<Bgr, Byte> img,Blob[] blobs,bool fill,bool drawBoundingBox, Emgu.CV.Structure.Bgr BoundingBoxColor,bool drawConvexHull, Emgu.CV.Structure.Bgr ConvexHullColor,bool drawEllipse, Emgu.CV.Structure.Bgr EllipseColor,bool drawCentroid, Emgu.CV.Structure.Bgr CentroidColor,bool drawAngle, Emgu.CV.Structure.Bgr AngleColor)
        {
            Random r = new Random(0);
            foreach (var b in blobs)
            {
                try
                {
                    p[counter] = new Point((int)b.CentroidX, (int)b.CentroidY);
                }
                catch (Exception ex) { }
                if (counter == 1)
                {
                    Emgu.CV.Image<Gray, Byte> skeleton = new Emgu.CV.Image<Gray, Byte>(30, 30);
                    //skeleton = img.Copy(b.BoundingBox).Resize(100, 100, INTER.CV_INTER_AREA).Convert<Gray, Byte>();
                    try{
                        skeleton = img.Copy(b.BoundingBox).Resize(30, 30, INTER.CV_INTER_AREA).Convert<Gray, Byte>().Canny(new Gray(100), new Gray(255));
                        //Emgu.CV.CvInvoke.cvDistTransform(skeleton, skeleton, Emgu.CV.CvEnum.DIST_TYPE.CV_DIST_L1, 5, null, IntPtr.Zero);
                        if (checkTrainData.Checked)
                        {
                            createDataTraining(skeleton);
                        }
                        if (checkTestingData.Checked)
                        {
                            createDataTesting(skeleton);
                        }
                        if (checkPredict.Checked)
                        {
                            predict(skeleton);
                        }
                        pictureBox2.Image = skeleton.ToBitmap();
                    }catch(Exception ex){
                    }
                }
                if (fill)
                    b.FillBlob(img, new MCvScalar(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));
                if (drawBoundingBox)
                    img.Draw(b.BoundingBox, BoundingBoxColor, 1);
                if (drawConvexHull)
                    img.DrawPolyline(b.ConvexHull, true, ConvexHullColor, 1);
                if (drawEllipse)
                    img.Draw(b.BestFitEllipse, EllipseColor, 1);

               

                if (drawCentroid)
                {
                    img.Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
                                           new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
                         CentroidColor, 1);
                    img.Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
                                           new Point((int)b.CentroidX, (int)b.CentroidY + 4)),
                         CentroidColor, 1);
                }
                if (drawAngle)
                {
                    double x1, x2, y1, y2;
                    x1 = b.CentroidX - 0.005 * b.Area * Math.Cos(b.Angle);
                    y1 = b.CentroidY - 0.005 * b.Area * Math.Sin(b.Angle);
                    x2 = b.CentroidX + 0.005 * b.Area * Math.Cos(b.Angle);
                    y2 = b.CentroidY + 0.005 * b.Area * Math.Sin(b.Angle);
                    img.Draw(new LineSegment2D(new Point((int)x1, (int)y1),new Point((int)x2, (int)y2)),AngleColor, 1);
                }
                b.Clear();
                break;
            }
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (paused==1)
            {
                paused = 2;
                timer2.Enabled = false;
                timer1.Enabled = false;
                captureButton.Text = "Capture";
            }else if(paused==2)
            {
                paused = 1;
                timer2.Enabled = true;
                timer1.Enabled = true;
                captureButton.Text = "Pause";
            }
            else
            {
                captureButton.Text = "Pause";
                paused = 1;
                _width = 640;//320;
                _height = 480;//240;
                if (checkBox2.Checked)
                {
                    cap = new Emgu.CV.Capture(1);
                    //cap = new Emgu.CV.Capture("video/C/video (1).avi");
                }
                else
                {
                    cap = new Emgu.CV.Capture(openFileDialog1.FileName);
                }
                
                cap.FlipHorizontal = true; //flipHorizontal camera
                timer2.Enabled = true;
                timer1.Enabled = true;
                skin = new Motion_Detection_v2.Filtering.SkinFiltering();
                
            }
        }

        public void mouseTracking(int x,int y)
        {
            //Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            //int maxMouseSpeed = rect.Width / 20;
            //System.Drawing.Point pp;
            //GetCursorPos(out pp);
            //pp.X = Math.Min(Math.Max(0, pp.X + (int)((maxMouseSpeed / 2) * b.CentroidX)), rect.Width);
            //pp.Y = Math.Min(Math.Max(0, pp.Y + (int)((maxMouseSpeed / 2) * b.CentroidY)), rect.Height);
            //SetCursorPos(pp.X, pp.Y);

            SetCursorPos(x, y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Emgu.CV.Image<Gray, Byte> result = new Emgu.CV.Image<Gray, Byte>(_width, _height);
            Emgu.CV.Image<Gray, Byte> result1 = new Emgu.CV.Image<Gray, Byte>(_width, _height);
            Emgu.CV.Image<Bgr, Byte> skinResult = new Emgu.CV.Image<Bgr, Byte>(_width, _height);

            Emgu.CV.Image<Bgr, Byte> image = cap.QueryFrame();
            //Emgu.CV.Image<Bgr, Byte> image = new Image<Bgr,byte>("thesis/A/image (50).bmp");
            if (image == null)
            {
                //cap = new Emgu.CV.Capture("video/C/video (1).avi");
                if (checkBox2.Checked)
                {
                    cap = new Emgu.CV.Capture(1);
                }
                else
                {
                    cap = new Emgu.CV.Capture(openFileDialog1.FileName);
                }
                cap.FlipHorizontal = true;
                return;
            }
            //Console.WriteLine(cap.)
            //try
            //{
            if (!hsvFilter.Checked && !ycrcbFilter.Checked)
            {
                result = image.Convert<Gray, Byte>();
                //CvInvoke.cvAdaptiveThreshold(result, result, 20, Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_GAUSSIAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY_INV, 3, 5);
            }
            else if (hsvFilter.Checked && ycrcbFilter.Checked)
            {
                result = skin.filterHSV(image, _hMin, _hMax, _sMin, _sMax);
                result1 = skin.filterYCrCb(image, _crMin, _crMax, _cbMin, _cbMax);
                Emgu.CV.CvInvoke.cvOr(result, result1, result, IntPtr.Zero);
            }
            else if (ycrcbFilter.Checked)
            {

                //adaptiveSkin.Process(image, result);
                //result = skinResult.Convert<Gray, Byte>();
                //result = skin.filterYCrCb(image, _crMin, _crMax, _cbMin, _cbMax);
                result = filtering.YCrCb(image, _crMin, _crMax, _cbMin, _cbMax);
                //result = filtering.filterYCrCb(image.Copy());
            }
            else if (hsvFilter.Checked)
            {
                result = filtering.HSV(image, _hMin, _hMax, _sMin, _sMax);
                //result = skin.filterHSV(image, _hMin, _hMax, _sMin, _sMax);
            }


            if (checkBox1.Checked)
            {
                result._Erode(trackBar1.Value);
            }
            if (dilateCheck.Checked)
            {
                result._Dilate(trackDilate.Value);
            }

            if (CannyCheck.Checked)
            {
                result = result.Canny(new Gray(100), new Gray(255));
                //result = result.Laplace(trackBarLapace.Value).Convert<Gray,Byte>();
            }

            if (blurCheck.Checked)
            {
                //result = result.SmoothGaussian(3);
                
                result = result.SmoothMedian(trackBlur.Value);
                //result = result.SmoothBlur(7,trackBlur.Value);
            }


            Emgu.CV.Image<Bgr, Byte> img = result.Convert<Bgr, Byte>();
            if (blurCheck.Checked)
            {
                result.Draw(new Rectangle(new Point(0, 0), new Size(cap.Width, cap.Height)), new Gray(0), 5);
                filtering.extractContourAndHull(result,out img);
                //ExtractContourAndHull(img, result);

                //DrawAndComputeFingersNum(img);
                /*
                Blob[] blobs = GetBlobs(result, 100, IntPtr.Zero, true, true);                    
                   
                // Skip the one with the biggest area
                IEnumerable<Blob> filtered = blobs.OrderByDescending(b => b.Area);
                // And the one with the worst perimeter-area ratio
                filtered = filtered.OrderBy(b => b.PerimeterLength / b.Area).Skip(1);
                DrawBlobs(img, filtered.ToArray(),
                            false,                           // Fill
                            true, new Bgr(255, 0, 0),        // Draw Bounding box
                            false, new Bgr(0, 255, 0),       // Draw Convex hull 
                            false, new Bgr(0, 0, 255),       // Draw Ellipse
                            false, new Bgr(255, 0, 0),       // Draw centroid
                            false, new Bgr(100, 100, 100)    // Draw Angle
                            );
             */
            }
            
            originalPicture.Image = image.ToBitmap();
            pictureBox1.Image = img.ToBitmap();
            
        }

        private void ExtractContourAndHull(Emgu.CV.Image<Bgr, Byte> currentFrame, Emgu.CV.Image<Gray, byte> skin)
        {
            using (MemStorage storage = new MemStorage())
            {

                Contour<Point> contours = skin.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST, storage);
                Contour<Point> biggestContour = null;

                Double Result1 = 0;
                Double Result2 = 0;
                while (contours != null)
                {
                    Result1 = contours.Area;
                    if (Result1 > Result2)
                    {
                        Result2 = Result1;
                        biggestContour = contours;
                    }
                    contours = contours.HNext;
                }

                if (biggestContour != null)
                {
                    //currentFrame.Draw(biggestContour, new Bgr(Color.DarkViolet), 2);

                    //Console.WriteLine("biggestContour.Count length " + biggestContour.Count());
                    Contour<Point> currentContour = biggestContour.ApproxPoly(biggestContour.Perimeter * 0.0025, storage);
                    //currentContour.RemoveAt(currentContour.Count() - 1);
                    //currentContour.RemoveAt(currentContour.Count() - 1);
                    currentFrame.Draw(currentContour, new Emgu.CV.Structure.Bgr(Color.LimeGreen), 2);
                    biggestContour = currentContour;


                    hull = biggestContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    box = biggestContour.GetMinAreaRect();
                    PointF[] points = box.GetVertices();

                    //draw rect
                    handRect = box.MinAreaRect();
                    currentFrame.Draw(handRect, new Bgr(200, 0, 0), 1);

                    Point[] ps = new Point[points.Length];
                    for (int i = 0; i < points.Length; i++)
                        ps[i] = new Point((int)points[i].X, (int)points[i].Y);

                    //Console.WriteLine("hull length " + hull.Count());
                    //currentFrame.DrawPolyline(hull.ToArray(), true, new Bgr(200, 125, 75), 2);
                    currentFrame.Draw(new CircleF(new PointF(box.center.X, box.center.Y), 3), new Bgr(200, 125, 75), 2);

                    //ellip.MCvBox2D= CvInvoke.cvFitEllipse2(biggestContour.Ptr);
                    //currentFrame.Draw(new Ellipse(ellip.MCvBox2D), new Bgr(Color.LavenderBlush), 3);

                    PointF center;
                    float radius;
                    //CvInvoke.cvMinEnclosingCircle(biggestContour.Ptr, out  center, out  radius);
                    //currentFrame.Draw(new CircleF(center, radius), new Bgr(Color.Gold), 2);

                    //currentFrame.Draw(new CircleF(new PointF(ellip.MCvBox2D.center.X, ellip.MCvBox2D.center.Y), 3), new Bgr(100, 25, 55), 2);
                    //currentFrame.Draw(ellip, new Bgr(Color.DeepPink), 2);

                    //CvInvoke.cvEllipse(currentFrame, new Point((int)ellip.MCvBox2D.center.X, (int)ellip.MCvBox2D.center.Y), new System.Drawing.Size((int)ellip.MCvBox2D.size.Width, (int)ellip.MCvBox2D.size.Height), ellip.MCvBox2D.angle, 0, 360, new MCvScalar(120, 233, 88), 1, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, 0);
                    //currentFrame.Draw(new Ellipse(new PointF(box.center.X, box.center.Y), new SizeF(box.size.Height, box.size.Width), box.angle), new Bgr(0, 0, 0), 2);


                    filteredHull = new Seq<Point>(storage);
                    for (int i = 0; i < hull.Total; i++)
                    {
                        if (Math.Sqrt(Math.Pow(hull[i].X - hull[i + 1].X, 2) + Math.Pow(hull[i].Y - hull[i + 1].Y, 2)) > box.size.Width / 10)
                        {
                            filteredHull.Push(hull[i]);
                        }
                    }

                    defects = biggestContour.GetConvexityDefacts(storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);

                    defectArray = defects.ToArray();
                    //Console.WriteLine("defectArray length " + defectArray.Length);
                }
            }
        }

        private void DrawAndComputeFingersNum(Image<Bgr, Byte> currentFrame)
        {
            int fingerNum = 0;

            #region hull drawing
            /*
            for (int i = 0; i < filteredHull.Total; i++)
            {
                PointF hullPoint = new PointF((float)filteredHull[i].X,
                                              (float)filteredHull[i].Y);
                CircleF hullCircle = new CircleF(hullPoint, 4);
                currentFrame.Draw(hullCircle, new Bgr(Color.Aquamarine), 2);
            }
             */
            #endregion

            #region defects drawing
            if (defects == null)
            {
                return;
            }


            //drawing finger filter
            float radius = 0f;
            for (int i = 0; i < defects.Total; i++)
            {
                PointF startPoint = new PointF((float)defectArray[i].StartPoint.X,
                                                (float)defectArray[i].StartPoint.Y);

                PointF depthPoint = new PointF((float)defectArray[i].DepthPoint.X,
                                                (float)defectArray[i].DepthPoint.Y);

                PointF endPoint = new PointF((float)defectArray[i].EndPoint.X,
                                                (float)defectArray[i].EndPoint.Y);

                LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                CircleF startCircle = new CircleF(startPoint, 5f);

                CircleF depthCircle = new CircleF(depthPoint, 5f);

                CircleF endCircle = new CircleF(endPoint, 5f);

                //Custom heuristic based on some experiment, double check it before use
                if ((startCircle.Center.Y < box.center.Y || depthCircle.Center.Y < box.center.Y) 
                    && (startCircle.Center.Y < depthCircle.Center.Y) 
                    && (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > box.size.Height / 6.5))
                {
                    fingerNum++;
                    currentFrame.Draw(startDepthLine, new Bgr(Color.Green), 2);
                    //currentFrame.Draw(depthEndLine, new Bgr(Color.Magenta), 2);
                }

                float x = Math.Abs(box.center.X - startPoint.X);
                float y = Math.Abs(box.center.Y - startPoint.Y);
                float r = (float)Math.Sqrt(x * x + y * y);
                if (radius < r)
                {
                    radius = r;
                }
                currentFrame.Draw(startCircle, new Bgr(Color.Red), 2);
                currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 5);
                //currentFrame.Draw(endCircle, new Bgr(Color.DarkBlue), 4);
            }
            #endregion

            // to draw count finger 
            //MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 5d, 5d);
            //currentFrame.Draw(fingerNum.ToString(), ref font, new Point(50, 150), new Bgr(Color.White));

            CircleF handCircle = new CircleF(box.center, radius);
            if (radius > 2)
            {
                //currentFrame.Draw(handCircle, new Bgr(Color.Green), 2);
            }
        }
                                      
    

        public Emgu.CV.Image<Gray, Byte> OptimizeBlobs(Emgu.CV.Image<Gray, Byte> img)
        {
            // can improve image quality, but expensive if real-time capture
            img._EqualizeHist(); 

            // convert img to temporary HSV object
            Emgu.CV.Image<Hsv, Byte> imgHSV = img.Convert<Hsv, Byte>();

            // break down HSV
            Emgu.CV.Image<Gray, Byte>[] channels = imgHSV.Split();
            Emgu.CV.Image<Gray, Byte> imgHSV_hue        = channels[0];   // hue channel
            Emgu.CV.Image<Gray, Byte> imgHSV_saturation = channels[1];   // saturation channel
            Emgu.CV.Image<Gray, Byte> imgHSV_value      = channels[2];   // value channel

            //use the saturation and value channel to filter noise. [you will need to tweak these values]
            Emgu.CV.Image<Gray, Byte> saturationFilter = imgHSV_saturation.InRange(new Gray(0), new Gray(80));
            Emgu.CV.Image<Gray, Byte> valueFilter = imgHSV_value.InRange(new Gray(200), new Gray(255));

            // combine the filters to get the final image to process.
            Emgu.CV.Image<Gray, byte> imgTarget = valueFilter.And(saturationFilter);

            return imgTarget;
        }

        private void detectBlob()
        {
            Emgu.CV.Image<Bgr, Byte> img = cap.QueryFrame();
            Emgu.CV.Image<Gray, Byte> temp = img.Convert<Gray, Byte>();
            OptimizeBlobs(img.Convert<Gray,Byte>());
            BGStatModel<Gray> bsm = new BGStatModel<Gray>(img.Convert<Gray, Byte>(), Emgu.CV.CvEnum.BG_STAT_TYPE.FGD_STAT_MODEL);
            bsm.Update(img.Convert<Gray, Byte>());

            BlobSeq oldBlobs = new BlobSeq();
            BlobSeq newBlobs = new BlobSeq();

            Emgu.CV.VideoSurveillance.FGDetector<Gray> fd = new FGDetector<Gray>(Emgu.CV.CvEnum.FORGROUND_DETECTOR_TYPE.FGD);
            BlobDetector bd = new BlobDetector(Emgu.CV.CvEnum.BLOB_DETECTOR_TYPE.CC);
            BlobTracker bt = new BlobTracker(Emgu.CV.CvEnum.BLOBTRACKER_TYPE.CC);

            BlobTrackerAutoParam<Gray> btap = new BlobTrackerAutoParam<Gray>();
            btap.BlobDetector = bd;
            btap.FGDetector = fd;
            btap.BlobTracker = bt;
            btap.FGTrainFrames = 5;
            BlobTrackerAuto<Gray> bta = new BlobTrackerAuto<Gray>(btap);
            Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
            {
                img = cap.QueryFrame();

                //OptimizeBlobs(img.Convert<Gray, Byte>());
                bd.DetectNewBlob(img, bsm.ForgroundMask, newBlobs, oldBlobs);

                List<MCvBlob> blobs = new List<MCvBlob>(bta);

                MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, 1.0, 1.0);
                foreach (MCvBlob blob in blobs)
                {
                    temp.Draw(Rectangle.Round(blob), new Gray(255.0), 2);
                    temp.Draw(blob.ID.ToString(), ref font, Point.Round(blob.Center), new Gray(255.0));
                }
                Emgu.CV.Image<Gray, Byte> fg = bta.ForgroundMask;
            });
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 2)
            {
                counter = 0;
            }
        }

        public Point getCentroid(Emgu.CV.Image<Gray, Byte> img)
        {
            List<int> pxc = new List<int>();
            List<int> pyc = new List<int>();

            int i = 0, j = 0;
            for (i = 0; i < img.Height; i++)
                for (j = 0; j < img.Width; j++)
                {
                    if (img.Data[i, j, 0] == 255)
                    {
                        pxc.Add(i);
                        pyc.Add(j);
                    }
                }

            return new Point(pyc.Min(), pxc.Min());
        }

        public Rectangle getRectangle(Emgu.CV.Image<Gray, Byte> img)
        {
            List<int> pxc = new List<int>();
            List<int> pyc = new List<int>();

            int i=0, j=0;
            for (i = 0; i < img.Height; i++)
                for (j = 0; j < img.Width; j++)
                {
                    if (img.Data[i,j,0] == 255)
                    {
                        pxc.Add(i);
                        pyc.Add(j);
                    }
                }

            return new Rectangle(new Point(pyc.Min(), pxc.Min()),new Size(100,100));
        }

        public Point getCorner(Emgu.CV.Image<Gray, Byte> img)
        {
            MCvMoments cog = img.GetMoments(true);
            
            int xc = (int)(cog.m10 / cog.m00);
            int yc = (int)(cog.m01 / cog.m00);
            return new Point(xc, yc);
        }

        public void getCorner(Emgu.CV.Image<Gray, Byte> img,out Double xc,out Double yc)
        {
            List<int> pxc = new List<int>();
            List<int> pyc = new List<int>();

            int i=0, j=0;
            for (i = 0; i < img.Height; i++)
                for (j = 0; j < img.Width; j++)
                {
                    if (img.Data[i,j,0] == 255)
                    {
                        pxc.Add(i);
                        pyc.Add(j);
                    }
                }
            yc = pxc.Average();
            xc = pyc.Average();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Bgr, Byte> bmp = new Emgu.CV.Image<Bgr, byte>("hand_small.bmp");
            Emgu.CV.Image<Hsv, Byte> hsv = bmp.Convert<Hsv, Byte>();
            Emgu.CV.Image<Gray, Byte>[] channels = hsv.Split();
            MessageBox.Show(channels[0].GetAverage().ToString());
        }

        private void ycrcbFilter_CheckedChanged(object sender, EventArgs e)
        {
            initYCrCb();
        }

        private void hsvFilter_CheckedChanged(object sender, EventArgs e)
        {
            initHSV();
        }

        private void trackSkinMin_Scroll(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                _hMin = trackSkinMin.Value;
            }
            else if (radioButton4.Checked)
            {
                _sMin = trackSkinMin.Value;
            }
            trackBarMinVal.Text = trackSkinMin.Value.ToString();
        }

        private void trackSkinMax_Scroll(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                _hMax = trackSkinMax.Value;
            }
            else if (radioButton4.Checked)
            {
                _sMax = trackSkinMax.Value;
            }
            trackBarMaxVal.Text = trackSkinMax.Value.ToString();
        }

        private void trackCrMin_Scroll(object sender, EventArgs e)
        {
            _crMin = trackCrMin.Value;
            trackCrMinVal.Text = trackCrMin.Value.ToString();
        }

        private void trackCrMax_Scroll(object sender, EventArgs e)
        {
            _crMax = trackCrMax.Value;
            trackCrMaxVal.Text = trackCrMax.Value.ToString();
        }

        private void trackCbMin_Scroll(object sender, EventArgs e)
        {
            _cbMin = trackCbMin.Value;
            trackCbMinVal.Text = trackCbMin.Value.ToString();
        }

        private void trackCbMax_Scroll(object sender, EventArgs e)
        {
            _cbMax = trackCbMax.Value;
            trackCbMaxVal.Text = trackCbMax.Value.ToString();
        }

        private void blurCheck_CheckedChanged(object sender, EventArgs e)
        {
            trackBlur.Value = 15;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                trackSkinMin.Value = _hMin;
                trackBarMinVal.Text = ""+_hMin;
                trackSkinMax.Value = _hMax;
                trackBarMaxVal.Text = ""+_hMax;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                trackSkinMin.Value = _sMin;
                trackBarMinVal.Text = ""+_sMin;
                trackSkinMax.Value = _sMax;
                trackBarMaxVal.Text = ""+_sMax;
            }

        }

        private void normalizationCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pauseBut_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Problem train = Problem.Read("training/data-2410.train");
            Parameter parameters = new Parameter();

            double C;
            double Gamma;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.
            ParameterSelection.Grid(train,parameters,"params/data-2410.params",out C,out Gamma);
            parameters.C = C; 
            parameters.Gamma = Gamma;
            //RangeTransform rt = RangeTransform.Compute(train);
            //RangeTransform.Write("scaling/data-2410.train.scale", rt);
            //Model model = Training.Train(Scaling.Scale(rt,train), parameters);
            //Training.PerformCrossValidation(train, parameters, 5);
            Model model = Training.Train(train, parameters);
            Model.Write("model/data-2410.mdl", model);
        }

        private void predictBut_Click(object sender, EventArgs e)
        {
            Problem test = Problem.Read("training/data-2410.train");
            Model model = Model.Read("model/data-2410.mdl");
            //RangeTransform rt = RangeTransform.Compute(test);
            //RangeTransform.Write("scaling/data-2410.test.scale", rt);
            //Problem.Write("training/data-2410.train.scale", Scaling.Scale(rt, test));
            //MessageBox.Show(Prediction.Predict(Scaling.Scale(rt, test), "predict/data-2410.train.predict", model, true) * 100 + "%");
            MessageBox.Show(Prediction.Predict(test, "predict/data-2410.train.predict", model, true) * 100 + "%");
        }

        private void TestingBut_Click(object sender, EventArgs e)
        {
            Problem test = Problem.Read("testing/data-2410.test");
            Model model = Model.Read("model/data-2410.mdl");
            //RangeTransform rt = RangeTransform.Compute(test);
            //RangeTransform.Write("scaling/data-2410.test.scale", rt);
            //MessageBox.Show(Prediction.Predict(Scaling.Scale(rt, test), "predict/data-2410.test.predict", model, true) * 100 + "%");
            MessageBox.Show(Prediction.Predict(test, "predict/data-2410.test.predict", model, true) * 100 + "%");
        }

        private void checkPredictHmm_CheckedChanged(object sender, EventArgs e)
        {
            HiddenMarkovModel hmm = new HiddenMarkovModel("A", 11, 5);
            int[] x = new int[3];
            x[0] = 1;
            x[1] = 2;
            x[2] = 3;
            //hmm.decoding(x);
        }

        private void butHmmLearning_Click(object sender, EventArgs e)
        {
            String[] c = { "A", "B" };
            int[] A = { 2, 2 };
            int B = 5;
            int[] O = { 0, 1, 2, 3, 4, 2, 1, 2 };
            int[] O1 = { 0, 2, 1, 3, 1, 2, 1, 2 };
            ClassifierHmm classifier = new ClassifierHmm(c, A, B);
            classifier.readObservation("training/data-observation.sequence");
            classifier.learning();
            MessageBox.Show(classifier.predict(O1));
        }

        private void butHmmTesting_Click(object sender, EventArgs e)
        {
            int[] O = { 0, 1, 2, 3, 4, 2, 1, 2 };
            int[] O1 = { 0, 2, 1, 3, 1, 2, 1, 2 };
            ClassifierHmm HmmClass = new ClassifierHmm();
            HmmClass.readModel("model/data-mdl.hmm");
            MessageBox.Show(HmmClass.predict(O));
        }

        private void checkPredict_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileNameTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            Entity.YCrCb yCrCb = new Entity.YCrCb();
            yCrCb.cb_min = _cbMin;
            yCrCb.cb_max = _cbMax;
            yCrCb.cr_min = _crMin;
            yCrCb.cr_max = _crMax;

            filtering.saveSetting(_hMin, _hMax, _sMin, _sMax, yCrCb);
        }
    }
}
