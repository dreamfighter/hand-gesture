using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.Util.TypeEnum;

namespace Motion_Detection_v2.Filtering
{
    class SkinFiltering
    {

        private Emgu.CV.Image<Gray, Byte> filterRGB(Emgu.CV.Image<Bgr, byte> image)
        {
            using (Emgu.CV.Image<Bgr, Byte> bgr = image)
            {
                Emgu.CV.Image<Gray, Byte>[] channels = bgr.Split();

                //channels[0] is the mask for hue less than 20 or larger than 160
                //Emgu.CV.CvInvoke.cvInRangeS(channels[0], new MCvScalar(20), new MCvScalar(160), channels[0]);
                //channels[0]._Not();

                Emgu.CV.CvInvoke.cvInRangeS(channels[0], new MCvScalar(20), new MCvScalar(255), channels[0]);
                Emgu.CV.CvInvoke.cvInRangeS(channels[1], new MCvScalar(40), new MCvScalar(255), channels[1]);
                Emgu.CV.CvInvoke.cvInRangeS(channels[2], new MCvScalar(95), new MCvScalar(255), channels[2]);

                //channels[1]._Not();
                //channels[2]._Not();
                //channels[1] is the mask for satuation of at least 10, this is mainly used to filter out white pixels
                //channels[1]._ThresholdBinary(new Gray(10), new Gray(255.0));

                Emgu.CV.CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
                Emgu.CV.CvInvoke.cvAnd(channels[0], channels[2], channels[0], IntPtr.Zero);
                //channels[2]._Not();
                channels[0].Dispose();
                //channels[2].Dispose();
                return channels[2];
            }
        }

        public Emgu.CV.Image<Gray, byte> detectSkinYcc(Emgu.CV.Image<Bgr, Byte> Img, Emgu.CV.IColor min, Emgu.CV.IColor max)
        {
            Emgu.CV.Image<Ycc, Byte> currentYCrCbFrame = Img.Convert<Ycc, Byte>();
            Emgu.CV.Image<Gray, byte> skin = new Emgu.CV.Image<Gray, byte>(Img.Width, Img.Height);
            skin = currentYCrCbFrame.InRange((Ycc)min, (Ycc)max);
            Emgu.CV.StructuringElementEx rect_12 = new Emgu.CV.StructuringElementEx(12, 12, 6, 6, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            //Emgu.CV.CvInvoke.cvErode(skin, skin, rect_12, 1);
            Emgu.CV.StructuringElementEx rect_6 = new Emgu.CV.StructuringElementEx(6, 6, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            //Emgu.CV.CvInvoke.cvDilate(skin, skin, rect_6, 2);
            return skin;
        }

        public Emgu.CV.Image<Gray, Byte> filterYCrCb(Emgu.CV.Image<Bgr, Byte> image, Double crmin, Double crmax, Double cbmin, Double cbmax)
        {
            using (Emgu.CV.Image<Ycc, Byte> ycc = image.Convert<Ycc, Byte>())
            {
                
                Emgu.CV.Image<Gray, Byte>[] channels = ycc.Split();

                //channels[0] is the mask for hue less than 20 or larger than 160
                //Emgu.CV.CvInvoke.cvInRangeS(channels[0], new MCvScalar(20), new MCvScalar(160), channels[0]);
                //channels[0]._Not();
                //return channels[2];
                Emgu.CV.CvInvoke.cvInRangeS(channels[1], new MCvScalar(cbmin), new MCvScalar(cbmax), channels[1]);
                Emgu.CV.CvInvoke.cvInRangeS(channels[2], new MCvScalar(crmin), new MCvScalar(crmax), channels[2]);

                //channels[1]._Not();
                //channels[2]._Not();
                //channels[1] is the mask for satuation of at least 10, this is mainly used to filter out white pixels
                //channels[1]._ThresholdBinary(new Gray(10), new Gray(255.0));

                Emgu.CV.CvInvoke.cvAnd(channels[1], channels[2], channels[1], IntPtr.Zero);
                channels[0].Dispose();
                channels[2].Dispose();
                return channels[1];
            }
        }

        public Emgu.CV.Image<Gray, Byte> filterHSV(Emgu.CV.Image<Bgr, Byte> image, Double hmin, Double hmax, Double smin, Double smax)
        {
            using (Emgu.CV.Image<Hsv, Byte> hsv = image.Convert<Hsv, Byte>())
            {
                hsv._EqualizeHist();
                Emgu.CV.Image<Gray, Byte>[] channels = hsv.Split();
                //channels[0] is the mask for hue less than 20 or larger than 160
                
                Emgu.CV.CvInvoke.cvInRangeS(channels[0], new MCvScalar(hmin), new MCvScalar(hmax), channels[0]);
                Emgu.CV.CvInvoke.cvInRangeS(channels[1], new MCvScalar(smin), new MCvScalar(smax), channels[1]);

                Emgu.CV.CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
                //channels[1].Dispose();
                //channels[3].Dispose();
                //channels[0]._ThresholdBinary(new Gray(100), new Gray(255.0));
                //channels[0]._Not();
                return channels[0];
            }
        }

        public void createContours(Emgu.CV.Image<Hsv, Byte> image) {
            //return image.
            //Emgu.CV.Contour<Point> contours=new Emgu.CV.Contour<Point> (3CHAIN_APPROX_METHOD method, RETR_TYPE type);
        
        }
    }
}
