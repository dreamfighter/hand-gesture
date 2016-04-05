using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using Emgu.CV.Structure;

namespace Emgu.CV.BLOB
{
    /// <summary>
    /// A wrapper for CvBlobs
    /// </summary>
    public class Blob
    {
        IntPtr _ptr;
        #region Blob Features
        /// <summary>        
        /// </summary>
        public double CentroidX;
        /// <summary>        
        /// </summary>
        public double CentroidY;
        /// <summary>        
        /// </summary>
        public int Exterior;
        /// <summary>        
        /// </summary>
        public double Area;
        /// <summary>        
        /// </summary>
        public double PerimeterLength;
        /// <summary>        
        /// </summary>
        public double ExternPerimeter;
        /// <summary>        
        /// </summary>
        public int Parent;        
        // Moments
        /// <summary>        
        /// </summary>
        public double u10;
        /// <summary>        
        /// </summary>
        public double u01;
        /// <summary>        
        /// </summary>
        public double u20;
        /// <summary>        
        /// </summary>
        public double u02;
        /// <summary>        
        /// </summary>
        public double u11;
        /// <summary>        
        /// </summary>
        public double Angle;        
        // Bounding rect
        /// <summary>        
        /// </summary>
        public Rectangle BoundingBox;
        /// <summary>        
        /// </summary>
        public double Mean;
        /// <summary>        
        /// </summary>
        public double StdDev;
        #endregion

        /// <summary>
        /// Pointer to the unmanaged object
        /// </summary>
        public IntPtr Ptr
        {
            get { return _ptr; }
        }
        /// <summary>
        /// Blob Constructor
        /// </summary>
        /// <param name="ptr">Pointer to the unmanaged blob</param>
        public Blob(IntPtr ptr)
        {
            _ptr = ptr;
            GetFeatures();
        }
        /// <summary>
        /// Release blob
        /// </summary>
        public void Clear()
        {
            BlobsInvoke.CvClearBlob(Ptr);
        }

        Point[] edges = null;
        /// <summary>
        /// The blob's Edges
        /// </summary>    
        public Point[] Edges
        {
            get
            {
                if (edges == null)
                    edges = GetEdges();
                return edges;
            }
        }
        Point[] convexHull = null;
        /// <summary>
        /// The blob's Convex Hull
        /// </summary>        
        public Point[] ConvexHull
        {
            get
            {
                if (convexHull == null)
                    convexHull = GetConvexHull(Edges);
                return convexHull;
            }
        }
        Ellipse ellipse;
        /// <summary>
        /// The blob's best fit ellipse
        /// </summary>        
        public Ellipse BestFitEllipse
        {
            get
            {
                if (ellipse.MCvBox2D.size.IsEmpty)
                    ellipse = GetEllipse(Edges);
                return ellipse;
            }
        }


        /// <summary>
        /// Fill image with blob
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="color">Color to fill with.</param>
        public void FillBlob(IntPtr image, MCvScalar color)
        {
            BlobsInvoke.CvFillBlob(Ptr, image, color, 0, 0);
        }

        /// <summary>
        /// Get blob features
        /// </summary>
        void GetFeatures()
        {
            IntPtr ptr = BlobsInvoke.CvGetBlobFeatures(Ptr);
            BlobFeatures Features = (BlobFeatures)Marshal.PtrToStructure(ptr, typeof(BlobFeatures));
            CentroidX = Features.CentroidX;
            CentroidY = Features.CentroidY;
            Exterior = Features.Exterior;
            Area = Features.Area;
            PerimeterLength = Features.PerimeterLength;
            ExternPerimeter = Features.ExternPerimeter;
            Parent = Features.Parent;
            // Moments
            u10 = Features.u10;
            u01 = Features.u01;
            u20 = Features.u20;
            u02 = Features.u02;
            u11 = Features.u11;
            Angle = Features.Angle;
            // Bounding rect
            BoundingBox = Rectangle.FromLTRB((int)Features.MinX,
                                             (int)Features.MinY,
                                             (int)Features.MaxX,
                                             (int)Features.MaxY
                                             );

            Mean = Features.Mean;
            StdDev = Features.StdDev;

            BlobsInvoke.CvBlobFeaturesRelease(ptr);
        }

        /// <summary>
        /// Get blob edges (NOT sorted)
        /// </summary>
        Point[] GetEdges()
        {
            int numEdges;
            IntPtr edges = BlobsInvoke.CvGetBlobEdges(Ptr, out numEdges);
            Point[] res = new Point[numEdges];
            GCHandle handle = GCHandle.Alloc(res, GCHandleType.Pinned);
            Emgu.Util.Toolbox.memcpy(handle.AddrOfPinnedObject(), edges, numEdges * Marshal.SizeOf(typeof(Point)));
            handle.Free();
            return res;
        }

        /// <summary>
        /// Get blob convex hull
        /// </summary>
        /// <param name="edges">The blob edges</param>
        Point[] GetConvexHull(Point[] edges)
        {
            Point[] hull;
            using (Emgu.CV.MemStorage storage = new Emgu.CV.MemStorage())
            {
                IntPtr seq = Marshal.AllocHGlobal(Emgu.CV.StructSize.MCvSeq);
                IntPtr block = Marshal.AllocHGlobal(Emgu.CV.StructSize.MCvSeqBlock);
                GCHandle handle = GCHandle.Alloc(edges, GCHandleType.Pinned);
                Emgu.CV.CvInvoke.cvMakeSeqHeaderForArray(
                   Emgu.CV.CvInvoke.CV_MAKETYPE((int)Emgu.CV.CvEnum.MAT_DEPTH.CV_32S, 2),
                   Emgu.CV.StructSize.MCvSeq,
                   Marshal.SizeOf(typeof(System.Drawing.Point)),
                   handle.AddrOfPinnedObject(),
                   edges.Length,
                   seq,
                   block);

                Emgu.CV.Seq<Point> convexHull = new Emgu.CV.Seq<Point>(Emgu.CV.CvInvoke.cvConvexHull2(seq, storage.Ptr, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE, 1), storage);
                handle.Free();
                Marshal.FreeHGlobal(seq);
                Marshal.FreeHGlobal(block);
                hull = convexHull.ToArray();

            }
            return hull;
        }

        /// <summary>
        /// Get blob best fit ellipse
        /// </summary>
        /// <param name="edges">The blob edges.</param>
        Ellipse GetEllipse(Point[] edges)
        {
            IntPtr seq = Marshal.AllocHGlobal(Emgu.CV.StructSize.MCvSeq);
            IntPtr block = Marshal.AllocHGlobal(Emgu.CV.StructSize.MCvSeqBlock);
            GCHandle handle = GCHandle.Alloc(edges, GCHandleType.Pinned);
            Emgu.CV.CvInvoke.cvMakeSeqHeaderForArray(
               Emgu.CV.CvInvoke.CV_MAKETYPE((int)Emgu.CV.CvEnum.MAT_DEPTH.CV_32S, 2),
               Emgu.CV.StructSize.MCvSeq,
               Marshal.SizeOf(typeof(System.Drawing.Point)),
               handle.AddrOfPinnedObject(),
               edges.Length,
               seq,
               block);
            Ellipse e = new Ellipse(Emgu.CV.CvInvoke.cvFitEllipse2(seq));
            handle.Free();
            Marshal.FreeHGlobal(seq);
            Marshal.FreeHGlobal(block);
            return e;
        }
    }













    /// <summary>
    /// Blob Features struct
    /// </summary>
    public struct BlobFeatures
    {
        /// <summary>
        /// </summary>
        public double CentroidX;
        /// <summary>
        /// </summary>
        public double CentroidY;
        /// <summary>
        /// </summary>
        public int Exterior;
        /// <summary>
        /// </summary>
        public double Area;
        /// <summary>
        /// </summary>
        public double PerimeterLength;
        /// <summary>
        /// </summary>
        public double ExternPerimeter;
        /// <summary>
        /// </summary>
        public int Parent;
        // Moments
        /// <summary>
        /// </summary>        
        public double u10;
        /// <summary>
        /// </summary>
        public double u01;
        /// <summary>
        /// </summary>
        public double u20;
        /// <summary>
        /// </summary>
        public double u02;
        /// <summary>
        /// </summary>
        public double u11;
        /// <summary>
        /// </summary>
        public double Angle;
        // Bounding rect
        /// <summary>
        /// </summary>
        public double MinX;
        /// <summary>
        /// </summary>
        public double MaxX;
        /// <summary>
        /// </summary>
        public double MinY;
        /// <summary>
        /// </summary>
        public double MaxY;
        /// <summary>
        /// </summary>
        public double Mean;
        /// <summary>
        /// </summary>
        public double StdDev;
    }

}
