using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace HandShape
{
    public class HandShape
    {
        private PointF center;
        private float radius;
        private Contour<Point> contour;
        public MCvConvexityDefect[] convexityDefects = new MCvConvexityDefect[5];
        private Image<Gray, Byte> image;
        public PointF[] fingersPosision = new PointF[5];
        public PointF[] fingersStartPoint = new PointF[5];
        public PointF[] fingersEndPoint = new PointF[5];
        public PointF[] fingersDepthPoint = new PointF[5];
        public Point[] feature;
        public CircleF maximumInscribedCircle;
        public CircleF minimumEnclosingCircle;
        public Rectangle handrect;
        public float ratio = 1f;

        public HandShape()
        {
            for (int i = 0; i < 5; i++)
            {
                convexityDefects[i] = new MCvConvexityDefect();
            }
        }

        public Point getStartPointAt(int i)
        {
            if (convexityDefects[i].StartPoint != null)
            {
                return convexityDefects[i].StartPoint;
            }
            return new Point(0,0);
        }


        public Point getEndPointAt(int i)
        {
            if (convexityDefects[i].EndPoint != null)
            {
                return convexityDefects[i].EndPoint;
            }
            return new Point(0, 0);
        }


        public Point getDepthPointAt(int i)
        {
            if (convexityDefects[i].DepthPoint != null)
            {
                return convexityDefects[i].DepthPoint;
            }
            return new Point(0, 0);
        }

        public void setCenter(PointF point)
        {
            this.center = point;
        }
        public PointF getCenter()
        {
            return this.center;
        }
        public void setRadius(float radius)
        {
            this.radius = radius;
        }
        public void setContour(Contour<Point> contour)
        {
            this.contour = contour;
        }
        public void setConvexityDefects(MCvConvexityDefect[] convexityDefects)
        {
            this.convexityDefects = convexityDefects;
        }
        public MCvConvexityDefect[] getConvexityDefects()
        {
            return this.convexityDefects;
        }
        public void setImage(Image<Gray, Byte> image)
        {
            this.image = image;
        }
        public Image<Gray, Byte> getImage()
        {
            return this.image;
        }
        public float getRadius()
        {
            return this.radius;
        }

        public String toString()
        {
            String feature = "";
            for (int i = 0; i < 5; i++)
            {
                feature += "";
            }
            return feature;
        }
    }
}
