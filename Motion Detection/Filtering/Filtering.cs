using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.BLOB;
using Emgu.Util.TypeEnum;
using Emgu.CV.CvEnum;
using System.Reflection;
using HandShape;
using HandGestureRecognition.SkinDetector;

namespace Motion_Detection_v2.Filtering
{
    class Filtering
    {
        private IColorSkinDetector skinDetector;
        private Image<Bgr, Byte> image;
        private Image<Bgr, Byte>[] imageList;
        private Point cursor;
        public Entity.YCrCb yCrCb;
        private Ycc YCrCb_min;
        private Ycc YCrCb_max;
        private AdaptiveSkinDetector detector;
        private KalmanFiltering kalman = new KalmanFiltering();

        public Filtering()
        {
            detector = new AdaptiveSkinDetector(1, AdaptiveSkinDetector.MorphingMethod.NONE);
            //hsv_min = new Hsv(0, 45, 0);
            //hsv_max = new Hsv(20, 255, 255);
            YCrCb_min = new Ycc(0, 131, 80);
            YCrCb_max = new Ycc(255, 185, 135);
        }
        /// <summary>
        ///   Filtering Color
        /// </summary>
        /// <remarks>
        ///   create image that want to filter 
        /// </remarks>        
        /// <param name="image">image that want to filter.</param>
        public Filtering(Image<Bgr, Byte> image)
        {
            this.image = image;
        }
        /// <summary>
        ///   Filtering Color
        /// </summary>
        /// <remarks>
        ///   create image list that want to filter 
        /// </remarks>        
        /// <param name="imageList">List of image that want to filter.</param>
        public Filtering(Image<Bgr, Byte>[] imageList)
        {
            this.imageList = imageList;
        }

        public Image<Gray, Byte> filterYCrCb(Image<Bgr, Byte> currentFrame)
        {
            //Image<Gray,Byte> skin = new Image<Gray,byte>(currentFrameCopy.Width,currentFrameCopy.Height);                
                //detector.Process(currentFrameCopy, skin);
            skinDetector = new YCrCbSkinDetector();

            return skinDetector.DetectSkin(currentFrame, YCrCb_min, YCrCb_max);
        }
        public Image<Gray, Byte> YCrCb(Image<Bgr, Byte> image, Double crmin, Double crmax, Double cbmin, Double cbmax)
        {
            using (Emgu.CV.Image<Ycc, Byte> ycc = image.Convert<Ycc, Byte>())
            {
                Image<Gray, Byte>[] channels = ycc.Split();

                //channels[0] is the mask for hue less than 20 or larger than 160
                //Emgu.CV.CvInvoke.cvInRangeS(channels[0], new MCvScalar(20), new MCvScalar(160), channels[0]);
                //channels[0]._Not();
                //return channels[2];

                CvInvoke.cvInRangeS(channels[1], new MCvScalar(cbmin), new MCvScalar(cbmax), channels[1]);
                CvInvoke.cvInRangeS(channels[2], new MCvScalar(crmin), new MCvScalar(crmax), channels[2]);

                //channels[1]._Not();
                //channels[2]._Not();
                //channels[1] is the mask for satuation of at least 10, this is mainly used to filter out white pixels
                //channels[1]._ThresholdBinary(new Gray(10), new Gray(255.0));

                CvInvoke.cvAnd(channels[1], channels[2], channels[1], IntPtr.Zero);
                channels[0].Dispose();
                //channels[2].Dispose();
                ycc.Dispose();
                return channels[1];
            }
        }
        public Image<Gray, Byte> RGB(Image<Bgr, Byte> image, Double hmin, Double hmax)
        {
            using (Emgu.CV.Image<Bgr, Byte> bgr = image.Convert<Bgr, Byte>())
            {
                //hsv._EqualizeHist();
                Image<Gray, Byte>[] channels = bgr.Split();

                CvInvoke.cvInRangeS(channels[0], new MCvScalar(hmin), new MCvScalar(hmax), channels[0]);
                CvInvoke.cvInRangeS(channels[1], new MCvScalar(hmin), new MCvScalar(hmax), channels[1]);
                CvInvoke.cvInRangeS(channels[1], new MCvScalar(0.23), new MCvScalar(0.68), channels[1]);
                //channels[1]._Not();
                CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
                CvInvoke.cvAnd(channels[0], channels[2], channels[0], IntPtr.Zero);
                return channels[1];
            }
        }
        public Image<Gray, Byte> HSV(Image<Bgr, Byte> image, Double hmin, Double hmax)
        {
            using (Emgu.CV.Image<Hsv, Byte> hsv = image.Convert<Hsv, Byte>())
            {
                hsv._EqualizeHist();
                Image<Gray, Byte>[] channels = hsv.Split();

                CvInvoke.cvInRangeS(channels[0], new MCvScalar(hmin), new MCvScalar(hmax), channels[0]);
                CvInvoke.cvInRangeS(channels[1], new MCvScalar(59), new MCvScalar(255), channels[1]);
                //channels[1]._Not();
                CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
                channels[2].Dispose();
                channels[1].Dispose();
                hsv.Dispose();
                return channels[0];
            }
        }
        public Image<Gray, Byte> HSV(Image<Bgr, Byte> image, Double hmin, Double hmax,Double smin,Double smax)
        {
            using (Emgu.CV.Image<Hsv, Byte> hsv = image.Convert<Hsv, Byte>())
            {
                hsv._EqualizeHist();
                Image<Gray, Byte>[] channels = hsv.Split();

                CvInvoke.cvInRangeS(channels[0], new MCvScalar(hmin), new MCvScalar(hmax), channels[0]);
                CvInvoke.cvInRangeS(channels[1], new MCvScalar(smin), new MCvScalar(smax), channels[1]);
                //channels[1]._Not();
                CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
                channels[2].Dispose();
                channels[1].Dispose();
                hsv.Dispose();
                return channels[0];
            }
        }
        public Image<Bgr, Byte> camShift(Image<Gray, Byte> img, out Image<Bgr, Byte> rec, out Point point)
        {
            
            MCvConnectedComp trackcomp = new MCvConnectedComp();
            MCvBox2D trackbox = new MCvBox2D();
            Rectangle window = new Rectangle(new Point(0,0),new Size(img.Width, img.Height));
            CvInvoke.cvCamShift(img, window, new MCvTermCriteria(100, 0.1), out trackcomp, out trackbox);
            //img.Draw(trackcomp.rect, new Gray(0), 5);

           // MessageBox.Show(window.ToString());
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);

            rec = img.Convert<Bgr, Byte>();
            result = rec.Clone();
            Rectangle box = trackcomp.rect;
            rec.Draw(box, new Bgr(255, 0, 0), 1);
            int pX;
            int pY;

            pX = (int)((1.0 * trackbox.center.X * Screen.PrimaryScreen.Bounds.Width) / img.Width);
            pY = (int)((1.0 * trackbox.center.Y * Screen.PrimaryScreen.Bounds.Height) / img.Height);

            point = new Point(pX, pY);
                if (box.Height > box.Width)
                {
                    int x = (box.Height - box.Width) / 2;
                    return result.Copy(new Rectangle(new Point(box.X - x, box.Y), new Size(box.Height, box.Height)));
                }
                else
                {
                    int y = (box.Width - box.Height) / 2;
                    return result.Copy(new Rectangle(new Point(box.X, box.Y - y), new Size(box.Width, box.Width)));
                }
            
        }
        public Image<Gray, Byte> Mean(Image<Gray, Byte> image, int val)
        {
            return image.SmoothBlur(val, val);
        }
        public Image<Bgr, Byte> Median(Image<Bgr, Byte> image, int val)
        {
            return image.SmoothMedian(val);
        }
        public Image<Gray, Byte> Median(Image<Gray, Byte> image, int val)
        {
            return image.SmoothMedian(val);
        }
        public Blob[] GetBlobs(Image<Gray, Byte> Ptr, int threshold, IntPtr maskImage, bool borderColor, bool findMoments)
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
        public Image<Bgr, Byte> detectBlob(Blob blobs, out Image<Bgr, Byte> rec, out Point point, Image<Gray, Byte> img)
        {
            img.Draw(new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)), new Gray(0), 5);
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);
            rec = img.Convert<Bgr, Byte>();
            result = rec.Clone();
            Rectangle box = blobs.BoundingBox;
            rec.Draw(blobs.BoundingBox, new Bgr(255, 0, 0), 1);

            int pX = box.Width / 2;
            int pY = box.Height / 2;

            pX = (int)((1.0 * blobs.CentroidX * Screen.PrimaryScreen.Bounds.Width) / (img.Width - 300));
            pY = (int)((1.0 * blobs.CentroidY * Screen.PrimaryScreen.Bounds.Height) / (img.Height - 300));

            img.Dispose();

            point = new Point(pX, pY);
            if (box.Height > box.Width)
            {
                int x = (box.Height - box.Width) / 2;
                return result.Copy(new Rectangle(new Point(box.X - x, box.Y), new Size(box.Height, box.Height)));
            }
            else
            {
                int y = (box.Width - box.Height) / 2;
                return result.Copy(new Rectangle(new Point(box.X, box.Y - y), new Size(box.Width, box.Width)));
            }
        }
        public Image<Bgr, Byte> detectBlob(Image<Gray, Byte> img, out Rectangle box, out Point point)
        {
            img.Draw(new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)), new Gray(0), 5);
            Blob[] blobs = GetBlobs(img, 100, IntPtr.Zero, true, true);
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);

            // Skip the one with the biggest area
            IEnumerable<Blob> filtered = blobs.OrderByDescending(b => b.Area);
            // And the one with the worst perimeter-area ratio
            filtered = filtered.OrderBy(b => b.PerimeterLength / b.Area).Skip(1);

            result = img.Convert<Bgr, Byte>();
            box = filtered.ToArray()[0].BoundingBox;

            int pX = box.Width / 2;
            int pY = box.Height / 2;

            pX = (int)((1.0 * filtered.ToArray()[0].CentroidX * Screen.PrimaryScreen.Bounds.Width) / img.Width);
            pY = (int)((1.0 * filtered.ToArray()[0].CentroidY * Screen.PrimaryScreen.Bounds.Height) / img.Height);

            foreach (var b in blobs)
                b.Clear();

            point = new Point(pX, pY);
            if (box.Height > box.Width)
            {
                int x = (box.Height - box.Width) / 2;
                return result.Copy(new Rectangle(new Point(box.X - x, box.Y), new Size(box.Height, box.Height)));
            }
            else
            {
                int y = (box.Width - box.Height) / 2;
                return result.Copy(new Rectangle(new Point(box.X, box.Y - y), new Size(box.Width, box.Width)));
            }
        }
        public Image<Bgr, Byte> detectBlob(Image<Gray, Byte> img, out Image<Bgr, Byte> rec,out Point point)
        {
            img.Draw(new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)), new Gray(0), 5);
            Blob[] blobs = GetBlobs(img, 100, IntPtr.Zero, true, true);
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);

            // Skip the one with the biggest area
            IEnumerable<Blob> filtered = blobs.OrderByDescending(b => b.Area);
            // And the one with the worst perimeter-area ratio
            filtered = filtered.OrderBy(b => b.PerimeterLength / b.Area).Skip(1);
            rec = img.Convert<Bgr, Byte>();
            result = rec.Clone();
            Rectangle box = filtered.ToArray()[0].BoundingBox;
            rec.Draw(box, new Bgr(255, 0, 0), 1);
            int pX =0;
            int pY = 0;

            //pX = (int)((1.0 * filtered.ToArray()[0].CentroidX * Screen.PrimaryScreen.Bounds.Width) / img.Width);
            //pY = (int)((1.0 * filtered.ToArray()[0].CentroidY * Screen.PrimaryScreen.Bounds.Height) / img.Height);

            foreach (var b in blobs)
                b.Clear();

            point = new Point( pX, pY);
            if (box.Height > box.Width)
            {
                int x = (box.Height - box.Width) / 2;
                return result.Copy(new Rectangle(new Point(box.X - x, box.Y), new Size(box.Height, box.Height)));
            }
            else
            {
                int y = (box.Width - box.Height) / 2;
                return result.Copy(new Rectangle(new Point(box.X, box.Y - y), new Size(box.Width, box.Width)));
            }
        }

        public Image<Bgr, Byte> detectBlob(Image<Gray, Byte> img, out Image<Bgr, Byte> rec, out Rectangle box, out Point point)
        {
            img.Draw(new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)), new Gray(0), 5);
            Blob[] blobs = GetBlobs(img, 100, IntPtr.Zero, true, true);
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);

            // Skip the one with the biggest area
            IEnumerable<Blob> filtered = blobs.OrderByDescending(b => b.Area);
            // And the one with the worst perimeter-area ratio
            filtered = filtered.OrderBy(b => b.PerimeterLength / b.Area).Skip(1);
            rec = img.Convert<Bgr, Byte>();
            result = rec.Clone();
            box = filtered.ToArray()[0].BoundingBox;

            //rec.Draw(box, new Bgr(255, 255, 0), 1);
            int pX = (int)filtered.ToArray()[0].CentroidX;
            int pY = (int)filtered.ToArray()[0].CentroidY;

            int distand_error = 70;
            //pX = (int)((1.0 * filtered.ToArray()[0].CentroidX * Screen.PrimaryScreen.Bounds.Width) / (img.Width - distand_error) - distand_error);
            //pY = (int)((1.0 * filtered.ToArray()[0].CentroidY * Screen.PrimaryScreen.Bounds.Height) / (img.Height - distand_error) - distand_error);

            foreach (var b in blobs)
                b.Clear();

            //point = new Point((int)(1.0 * pX * Screen.PrimaryScreen.Bounds.Width) / (img.Width - distand_error), (int)(1.0 * pY * Screen.PrimaryScreen.Bounds.Height) / (img.Height - distand_error));
            point = new Point(pX,pY);

            if (box.Height > box.Width)
            {
                int x = pX - (box.Height / 2);
                int h = box.Height;

                if (x + box.Height > result.Width)
                {
                    int sisa = x + box.Height - result.Width;
                    x = pX - ((box.Height - 2 * sisa) / 2);
                    h = box.Height - (2 * sisa) - 1;
                }
                else if (x < 0)
                {
                    h = pX * 2;
                    x = 0;
                }

                Rectangle R = new Rectangle(new Point(x, box.Y), new Size(h, h));
                box = R;
                rec.Draw(R, new Bgr(255, 0, 0), 1);
                return result.Copy(R);
            }
            else if (box.Width > box.Height)
            {
                int y = pY - (box.Width / 2);
                int w = box.Width;
                if (y + box.Width > result.Height)
                {
                    int sisa = y + box.Width - result.Height;
                    y = pY - ((box.Width - 2 * sisa) / 2);
                    w = box.Width - (2 * sisa) - 2;
                }
                else if (y < 0)
                {
                    w = pY * 2;
                    y = 0;
                }
                Rectangle R = new Rectangle(new Point(box.X, y), new Size(w, w));
                rec.Draw(R, new Bgr(255, 0, 0), 1);
                box = R;
                return result.Copy(R);
            }
            else
            {
                Rectangle R = new Rectangle(new Point(box.X, box.Y), new Size(box.Width, box.Height));
                return result.Copy(R);
            }
        }

        public Image<Bgr, Byte> detectBlob(Image<Gray, Byte> img,out Image<Bgr, Byte> rec)
        {
            img.Draw(new Rectangle(new Point(0, 0), new Size(img.Width, img.Height)), new Gray(0), 5);
            Blob[] blobs = GetBlobs(img, 100, IntPtr.Zero, true, true);
            Image<Bgr, Byte> result = new Image<Bgr, Byte>(img.Width, img.Height);

            // Skip the one with the biggest area
            IEnumerable<Blob> filtered = blobs.OrderByDescending(b => b.Area);
            // And the one with the worst perimeter-area ratio
            filtered = filtered.OrderBy(b => b.PerimeterLength / b.Area).Skip(1);
            rec = img.Convert<Bgr, Byte>();
            result = rec.Clone();
            Rectangle box = filtered.ToArray()[0].BoundingBox;
            rec.Draw(filtered.ToArray()[0].BoundingBox, new Bgr(255, 0, 0), 1);
            foreach (var b in blobs)
                b.Clear();
                
            if (box.Height > box.Width)
            {
                int x = (box.Height - box.Width) / 2;
                return result.Copy(new Rectangle(new Point(box.X - x, box.Y), new Size(box.Height, box.Height)));
            }
            else
            {
                int y = (box.Width - box.Height) / 2;
                return result.Copy(new Rectangle(new Point(box.X, box.Y - y), new Size(box.Width, box.Width)));
            }
           // return rec.Copy(filtered.ToArray()[0].BoundingBox);
            //return DrawBlobs(img, filtered.ToArray(),
            //            false,                           // Fill
            //            false, new Bgr(255, 0, 0),        // Draw Bounding box
            //            false, new Bgr(0, 255, 0),       // Draw Convex hull 
            //            false, new Bgr(0, 0, 255),       // Draw Ellipse
            //            false, new Bgr(255, 0, 0),       // Draw centroid
            //            false, new Bgr(100, 100, 100)    // Draw Angle
            //            );
        }
        public Image<Bgr, Byte> DrawBlobs(Image<Gray, Byte> img, Blob[] blobs, bool fill, bool drawBoundingBox, Bgr BoundingBoxColor, bool drawConvexHull, Bgr ConvexHullColor, bool drawEllipse, Bgr EllipseColor, bool drawCentroid, Bgr CentroidColor, bool drawAngle, Bgr AngleColor)
        {
            Random r = new Random(0);
            Image<Bgr, Byte> crop = img.Convert<Bgr, Byte>();
            foreach (var b in blobs)
            {   
                if (fill)
                    b.FillBlob(crop, new MCvScalar(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));
                if (drawBoundingBox)
                    crop.Draw(b.BoundingBox, BoundingBoxColor, 1);
                if (drawConvexHull)
                    crop.DrawPolyline(b.ConvexHull, true, ConvexHullColor, 1);
                if (drawEllipse)
                    crop.Draw(b.BestFitEllipse, EllipseColor, 1);
                
                if (drawCentroid)
                {
                    crop.Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
                                           new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
                         CentroidColor, 1);
                    crop.Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
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
                    crop.Draw(new LineSegment2D(new Point((int)x1, (int)y1), new Point((int)x2, (int)y2)), AngleColor, 1);
                }
                crop = crop.Copy(b.BoundingBox);
                b.Clear();
                break;
            }
            return crop;
        }
        public Image<Gray, Byte> reduceSize(Image<Bgr, Byte> img)
        {            
            int h = 50;
            int w = 50;
            //if (img.Width > img.Height)
            //{
            //    w = 30;
            //    h = w * img.Height / img.Width;
            //}
            //else
            //{
            //    h = 30;
            //    w = h * img.Width / img.Height;
            //}
            //Image<Gray, Byte> skeleton = img.Convert<Gray, Byte>();
            //CvInvoke.cvDistTransform(skeleton, skeleton, DIST_TYPE.CV_DIST_L1, 5, null, IntPtr.Zero);
            //return skeleton.Resize(w, h, INTER.CV_INTER_AREA).Canny(new Gray(0), new Gray(255));
            return img.Convert<Gray,Byte>().Resize(w, h, INTER.CV_INTER_AREA);
        }
        public Image<Gray, Byte> reduceSize(Image<Gray, Byte> img)
        {
            int h = 50;
            int w = 50;
            //if (img.Width > img.Height)
            //{
            //    w = 30;
            //    h = w * img.Height / img.Width;
            //}
            //else
            //{
            //    h = 30;
            //    w = h * img.Width / img.Height;
            //}
            //Image<Gray, Byte> skeleton = img.Convert<Gray, Byte>();
            //CvInvoke.cvDistTransform(skeleton, skeleton, DIST_TYPE.CV_DIST_L1, 5, null, IntPtr.Zero);
            //return skeleton.Resize(w, h, INTER.CV_INTER_AREA).Canny(new Gray(0), new Gray(255));
            return img.Resize(w, h, INTER.CV_INTER_AREA);
        }

        public void saveSetting(int hmin, int hmax, int smin, int smax)
        {
            StreamWriter sw = new StreamWriter("setting/filter.fil");
            sw.WriteLine(hmin + "," + hmax + "," + smin + "," + smax);
            sw.WriteLine(yCrCb.cb_min + "," + yCrCb.cb_max + "," + yCrCb.cr_min + "," + yCrCb.cr_max);
            sw.Close();
        }

        public void saveSetting(int hmin, int hmax, int smin, int smax,Entity.YCrCb yCrCb)
        {
            StreamWriter sw = new StreamWriter("setting/filter.fil");
            sw.WriteLine(hmin + "," + hmax + "," + smin + "," + smax);
            sw.WriteLine(yCrCb.cb_min + "," + yCrCb.cb_max + "," + yCrCb.cr_min + "," + yCrCb.cr_max);
            sw.Close();
        }

        public String[] loadSetting()
        {
            StreamReader sr = new StreamReader("setting/filter.fil");
            String input = sr.ReadLine();
            String[] result = input.Split(',');
            String[] newresult = sr.ReadLine().Split(',');
            yCrCb = new Entity.YCrCb();
            yCrCb.cb_min = int.Parse(newresult[0]);
            yCrCb.cb_max = int.Parse(newresult[1]);
            yCrCb.cr_min = int.Parse(newresult[2]);
            yCrCb.cr_max = int.Parse(newresult[3]);

            sr.Close();
            return result;
        }
        public Point getCursor()
        {
            return cursor;
        }
        public Image<Bgr, Byte> extractFeature(Image<Bgr, Byte> image)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            Image<Gray, Byte> cache;
            cursor = new Point();
            //color segmentation
            cache = HSV(image, 30, 79, 65, 255);

            //filter noise
            //cache._Dilate(1);
            //cache._Erode(1);
            cache = Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                temp1 = detectBlob(cache, out temp2, out cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //pictureBox3.Image = temp2.ToBitmap();

            image.Dispose();
            cache.Dispose();
            temp2.Dispose();
            return temp1;
        }

        public HandShape.HandShape extractContourAndHull(Emgu.CV.Image<Gray, Byte> skin, out Emgu.CV.Image<Bgr, Byte> currentFrame)
        {
            Seq<Point> hull;
            Seq<Point> filteredHull;
            Seq<MCvConvexityDefect> defects;
            MCvConvexityDefect[] defectArray;
            Rectangle handRect;
            MCvBox2D box;
            Ellipse ellip;
            int maxRa = 0;
            Point maxSubcribedCirclePoint = new Point(0,0);

            currentFrame = skin.Convert<Bgr,Byte>();
            HandShape.HandShape handShape = new HandShape.HandShape();
            using (MemStorage storage = new MemStorage())
            {

                Contour<Point> contours = skin.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST, storage);
                Contour<Point> biggestContour = null;
                Double Result1 = 0;
                Double Result2 = 0;
                int fingerNum = 0;
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
                    Contour<Point> currentContour = biggestContour.ApproxPoly(biggestContour.Perimeter * 0.0025, storage);
                    
                    List<PointF> sites = new List<PointF>();

                    for (int i = 0; i < currentContour.Count(); i++)
                    {
                        sites.Add(new PointF(currentContour[i].X, currentContour[i].Y));
                    }

                    handShape.setContour(biggestContour);

                    //draw contour
                    currentFrame.Draw(currentContour, new Emgu.CV.Structure.Bgr(Color.LimeGreen), 2);
                    //biggestContour = currentContour;

                    for (int i = 0; i < biggestContour.Count(); i++)
                    {
                        //sites.Add(new PointF(biggestContour[i].X, biggestContour[i].Y));
                        Point countour = biggestContour[i];
                    }
                

                    hull = biggestContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    box = biggestContour.GetMinAreaRect();
                    
                    handShape.setCenter(box.center);
                    
                    PointF[] points = box.GetVertices();
                    handRect = box.MinAreaRect();


                    handShape.handrect = handRect;
                    /*
                    if (handRect.Width >= handRect.Height)
                    {
                        float offset = (handRect.Width - handRect.Height) / 2;
                        handShape.handrect = new Rectangle((int)handRect.Left, (int)(handRect.Top - offset), (int)handRect.Width, (int)handRect.Width);
                    }
                    else
                    {
                        float offset = (handRect.Height - handRect.Width) / 2;
                        handShape.handrect = new Rectangle((int)(handRect.Left - offset), (int)handRect.Top, (int)handRect.Height, (int)handRect.Height);
                    }

                    if (handShape.handrect.Width > 0)
                    {
                        handShape.ratio = 1.0f * 150 / handShape.handrect.Width;
                    }
                    */

                    //Console.WriteLine("width=>" + handShape.handrect.Width);
                    //handShape.handrect = new Rectangle((int)box.center, box.size);

                    //currentFrame.Draw(handShape.handrect, new Bgr(200, 200, 0), 1);
                    currentFrame.Draw(handRect, new Bgr(200, 0, 0), 1);
                    
                    //Console.WriteLine("hull length " + hull.Count());

                    //draw hull
                    currentFrame.DrawPolyline(hull.ToArray(), true, new Bgr(200, 125, 75), 2);

                    //draw hand center
                    currentFrame.Draw(new CircleF(new PointF(box.center.X, box.center.Y), 3), new Bgr(200, 125, 75), 2);

                    //ellip.MCvBox2D= CvInvoke.cvFitEllipse2(biggestContour.Ptr);
                    //currentFrame.Draw(new Ellipse(ellip.MCvBox2D), new Bgr(Color.LavenderBlush), 3);

                    PointF center;
                    float radius = 0;
                    CvInvoke.cvMinEnclosingCircle(biggestContour.Ptr, out  center, out  radius);
                    //currentFrame.Draw(new CircleF(center, radius), new Bgr(Color.Gold), 2);
                    //CvInvoke.cvPointPolygonTest(biggestContour.Ptr,

                    //currentFrame.Draw(new CircleF(new PointF(ellip.MCvBox2D.center.X, ellip.MCvBox2D.center.Y), 3), new Bgr(100, 25, 55), 2);
                    //currentFrame.Draw(ellip, new Bgr(Color.DeepPink), 2);

                    //CvInvoke.cvEllipse(currentFrame, new Point((int)ellip.MCvBox2D.center.X, (int)ellip.MCvBox2D.center.Y), new System.Drawing.Size((int)ellip.MCvBox2D.size.Width, (int)ellip.MCvBox2D.size.Height), ellip.MCvBox2D.angle, 0, 360, new MCvScalar(120, 233, 88), 1, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, 0);
                    //currentFrame.Draw(new Ellipse(new PointF(box.center.X, box.center.Y), new SizeF(box.size.Height, box.size.Width), box.angle), new Bgr(0, 0, 0), 2);


                    handShape.setImage(skin.Copy(new RectangleF(new PointF(center.X - radius, center.Y - radius), new SizeF(center.X + radius, center.Y + radius))));
                    

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
                    handShape.setConvexityDefects(defectArray);

                    double rmax = 0;

                    for (int i = 0; i < 5; i++)
                    {
                        handShape.fingersStartPoint[i] = new PointF(handShape.handrect.Left, handShape.handrect.Top);
                        handShape.fingersEndPoint[i] = new PointF(handShape.handrect.Left, handShape.handrect.Top);
                        handShape.fingersDepthPoint[i] = new PointF(handShape.handrect.Left, handShape.handrect.Top);
                    }

                    List<Voronoi2.GraphEdge> ge = makeVoronoiGraph(sites, skin.Width, skin.Height);
                    maxSubcribedCirclePoint = new Point((int)center.X, (int)center.Y);
                    for (int i = 0; i < ge.Count; i++)
                    {
                        try
                        {
                            
                            PointF p1 = new PointF((int)ge[i].x1, (int)ge[i].y1);
                            PointF p2 = new PointF((int)ge[i].x2, (int)ge[i].y2);
                            //Console.WriteLine(p1.X + "," + p1.Y + "," + p2.X + "," + p2.Y);
                            //currentFrame.Draw(new LineSegment2DF(p1,p2),new Bgr(Color.Aqua),1);
                            int minNeighbour = int.MaxValue;

                            double isInPoly = CvInvoke.cvPointPolygonTest(biggestContour.Ptr, p1, false);
                            double isInPoly2 = CvInvoke.cvPointPolygonTest(biggestContour.Ptr, p2, false);

                            if (isInPoly>=0)
                            {
                                
                                Point sitesPoint = new Point(0, 0);
                                Point minCenter = new Point(0, 0);
                                for (int j = 0; j < sites.Count; j++)
                                {
                                    
                                    //Pen pen = new Pen(Color.FromArgb(255, 255, j));
                                    float x = p1.X - sites[j].X;
                                    float y = p1.Y - sites[j].Y;
                                    int r = (int)Math.Sqrt((x * x) + (y * y));
                                    if (minNeighbour > r)
                                    {
                                        minNeighbour = r;
                                        minCenter.X = (int)p1.X;
                                        minCenter.Y = (int)p1.Y;
                                        sitesPoint.X = (int)sites[j].X;
                                        sitesPoint.Y = (int)sites[j].Y;
                                    }

                                    /*
                                    if (j > 0)
                                    {
                                        Point site1 = new Point((int)sites[j].X, (int)sites[j].Y);
                                        Point site2 = new Point((int)sites[j - 1].X, (int)sites[j - 1].Y);
                                        //Point point = new Point((int)p1.X, (int)p1.Y);
                                        r = (int)pDistance((int)p1.X, (int)p1.Y, site1.X, site1.Y, site2.X, site2.Y);
                                        if (minNeighbour > r)
                                        {
                                            minNeighbour = r;
                                            minCenter.X = (int)p1.X;
                                            minCenter.Y = (int)p1.Y;
                                            sitesPoint.X = (int)sites[j].X;
                                            sitesPoint.Y = (int)sites[j].Y;
                                        }
                                        //Console.WriteLine(distanceToVoronoi(site1, site2, point));
                                        //Console.WriteLine();
                                    }
                                    */
                                    
                                    if (isInPoly2 >= 0)
                                    {
                                        x = p2.X - sites[j].X;
                                        y = p2.Y - sites[j].Y;
                                        r = (int)Math.Sqrt((x * x) + (y * y));
                                        if (minNeighbour > r)
                                        {
                                            minNeighbour = r;
                                            minCenter.X = (int)p2.X;
                                            minCenter.Y = (int)p2.Y;
                                            sitesPoint.X = (int)sites[j].X;
                                            sitesPoint.Y = (int)sites[j].Y;
                                        }

                                        /*
                                        if (j > 0)
                                        {
                                            Point site1 = new Point((int)sites[j].X, (int)sites[j].Y);
                                            Point site2 = new Point((int)sites[j - 1].X, (int)sites[j - 1].Y);
                                            Point point = new Point((int)p2.X, (int)p2.Y);
                                            r = r = (int)pDistance((int)p2.X, (int)p2.Y, site1.X, site1.Y, site2.X, site2.Y);
                                            if (minNeighbour > r)
                                            {
                                                minNeighbour = r;
                                                minCenter.X = (int)p2.X;
                                                minCenter.Y = (int)p2.Y;
                                                sitesPoint.X = (int)sites[j].X;
                                                sitesPoint.Y = (int)sites[j].Y;
                                            }
                                            //Console.WriteLine(distanceToVoronoi(site1, site2, point));
                                            //Console.WriteLine();
                                        }
                                        */
                                    }
                                    //g.DrawEllipse(Pens.Blue, p1.X - (r / 2), p1.Y - (r / 2), r, r);
                                }
                                if (maxRa < minNeighbour)
                                {
                                    maxRa = minNeighbour;
                                    maxSubcribedCirclePoint = minCenter;
                                    //currentFrame.Draw(new LineSegment2DF(minCenter, sitesPoint), new Bgr(Color.Goldenrod), 2);
                                }
                            }
                            

                        }
                        catch
                        {
                            string s = "\nP " + i + ": " + ge[i].x1 + ", " + ge[i].y1 + " || " + ge[i].x2 + ", " + ge[i].y2;
                            //richTextBox1.Text += s;
                            Console.WriteLine(s);
                        }
                    }
                    //currentFrame.Draw(new CircleF(center, 4), new Bgr(Color.Green), 2);
                    handShape.setRadius(radius);
                    PointF[] pts = kalman.filterPoints(maxSubcribedCirclePoint);
                    maxSubcribedCirclePoint = new Point((int)pts[1].X, (int)pts[1].Y);

                    float thresholdCenterY = box.center.Y;
                    if (maxSubcribedCirclePoint != null)
                    {

                        thresholdCenterY = maxSubcribedCirclePoint.Y;
                    }

                    for (int i = 0; i < defects.Total; i++)
                    {
                        PointF startPoint = new PointF((float)defectArray[i].StartPoint.X,
                                                (float)defectArray[i].StartPoint.Y);

                        PointF depthPoint = new PointF((float)defectArray[i].DepthPoint.X, (float)defectArray[i].DepthPoint.Y);


                        PointF endPoint = new PointF((float)defectArray[i].EndPoint.X, (float)defectArray[i].EndPoint.Y);

                        LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                        LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                        CircleF startCircle = new CircleF(startPoint, 5f);

                        CircleF depthCircle = new CircleF(depthPoint, 5f);

                        CircleF endCircle = new CircleF(endPoint, 5f);

                        //Console.WriteLine(depthPoint.X+","+depthPoint.Y);

                        //Custom heuristic based on some experiment, double check it before use
                        /*
                        if ((startCircle.Center.Y < box.center.Y || depthCircle.Center.Y < box.center.Y)
                            && (startCircle.Center.Y < depthCircle.Center.Y)
                            && (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) 
                            + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > box.size.Height / 6.5))
                        {
                        */
                        if ((startCircle.Center.Y < thresholdCenterY || depthCircle.Center.Y < thresholdCenterY)
                            && (startCircle.Center.Y < depthCircle.Center.Y)
                            && (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2)
                            + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > maxRa))
                        {
                            fingerNum++;
                            currentFrame.Draw(startCircle, new Bgr(Color.Red), 2);
                            currentFrame.Draw(startDepthLine, new Bgr(Color.Magenta), 2);
                            currentFrame.Draw(depthEndLine, new Bgr(Color.Cyan), 2);
                            currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 5);


                            double dX = depthPoint.X - center.X;
                            double dY = depthPoint.Y - center.Y;
                            double multi = Math.Sqrt(dX * dX + dY * dY);
                            if (multi > rmax)
                            {
                                rmax = multi;
                            }
                            float x = box.center.X - startPoint.X;
                            float y = box.center.Y - startPoint.Y;
                            //handShape.convexityDefects[fingerNum] = defectArray[i];
                            if (fingerNum >= 0 && fingerNum < 5)
                            {
                                handShape.fingersStartPoint[fingerNum] = startPoint;
                                handShape.fingersEndPoint[fingerNum] = endPoint;
                                handShape.fingersDepthPoint[fingerNum] = depthPoint;
                                handShape.fingersPosision[fingerNum] = new PointF(x, y);
                            }
                        }


                        //currentFrame.Draw(startCircle, new Bgr(Color.Red), 2);
                        //currentFrame.Draw(startDepthLine, new Bgr(Color.Magenta), 2);
                        //currentFrame.Draw(depthEndLine, new Bgr(Color.Cyan), 2);
                        //currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 5);


                        /*
                        float x = Math.Abs(box.center.X - startPoint.X);
                        float y = Math.Abs(box.center.Y - startPoint.Y);
                        float r = (float)Math.Sqrt(x * x + y * y);
                        if (radius < r)
                        {
                            radius = r;
                        }
                        */

                        //sites.Add(depthPoint);
                    }
                }
            }

            
            
            try
            {
                if (maxSubcribedCirclePoint != null)
                {
                    handShape.maximumInscribedCircle = new CircleF(maxSubcribedCirclePoint, maxRa);
                    currentFrame.Draw(new CircleF(maxSubcribedCirclePoint, maxRa), new Bgr(Color.DarkGoldenrod), 2);
                    currentFrame.Draw(new CircleF(maxSubcribedCirclePoint, 2), new Bgr(Color.DarkGoldenrod), 2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return handShape;
        }

        public int[] settingFilter() {
            int[] val=new int[4];

            try
            {
                String[] temp = loadSetting();
                val[0] = int.Parse(temp[0]);
                val[1] = int.Parse(temp[1]);
                val[2] = int.Parse(temp[2]);
                val[3] = int.Parse(temp[3]);
            }
            catch (Exception ex)
            {
                val[0] = 29;
                val[1] = 90;
                val[2] = 59;
                val[3] = 255;
            }
            return val;
        }


        public List<Voronoi2.GraphEdge> makeVoronoiGraph(List<PointF> sites, int width, int height)
        {
            Voronoi2.Voronoi voroObject = new Voronoi2.Voronoi(0.1);
            double[] xVal = new double[sites.Count];
            double[] yVal = new double[sites.Count];
            for (int i = 0; i < sites.Count; i++)
            {
                xVal[i] = sites[i].X;
                yVal[i] = sites[i].Y;
            }
            
            return voroObject.generateVoronoi(xVal, yVal, 0, width, 0, height);

        }

        public float lineEquation(Point p0, Point p1, Point p)
        {
            float m = (p1.Y - p0.Y) / (p1.X - p0.X);
            int c = (int)Math.Round((-m * p0.X)) + p0.Y;

            return (m * p.X) - p.Y + c;
        }

        public double distanceToVoronoi(Point b, Point c, Point a)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double ab = Math.Sqrt((dx * dx) + (dy * dy));
            dx = b.X - c.X;
            dy = b.Y - c.Y;
            double bc = Math.Sqrt((dx * dx) + (dy * dy));
            dx = a.X - c.X;
            dy = a.Y - c.Y;
            double ac = Math.Sqrt((dx * dx) + (dy * dy));
            double aAbsenB = ((ab * ab) + (bc * bc) - (ac * ac)) / (2 * bc);
            double r = Math.Sqrt((ab * ab) - (aAbsenB * aAbsenB));
            Console.WriteLine(r);
            return r;
        }

        public double pDistance(int x, int y, int x1, int y1, int x2, int y2) 
        {

            int A = x - x1;
            int B = y - y1;
            int C = x2 - x1;
            int D = y2 - y1;

            int dot = A * C + B * D;
            int len_sq = C * C + D * D;
            double param = -1;
            if (len_sq != 0)
            { //in case of 0 length line
                param = dot / len_sq;
            }
            int xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + (int)(param * C);
                yy = y1 + (int)(param * D);
            }

            int dx = x - xx;
            int dy = y - yy;
            double r = Math.Sqrt(dx * dx + dy * dy);
            Console.WriteLine(r);
            return r;
        }

        public static double angleBetweenTwoPoints(PointF point1,PointF point2)
        {

            double deltaY = point2.Y - point1.Y;
            double deltaX = point2.X - point1.X;
            double angle1 = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
            angle1 = (angle1 + 360) % 360;
            //double angle2 = Math.Atan2(point2Y - fixedY, point2X - fixedX);

            return angle1;
        }

        public static int convertAngle(double angle,int offset)
        {
            return (int)(angle / offset);
        }
    }
}
