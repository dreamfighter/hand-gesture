using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;
using Emgu.CV.Reflection;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Emgu.CV
{
    /// <summary>
    /// A wrapper for IplImage
    /// </summary>
    /// <typeparam name="TColor">Color type of this image</typeparam>
    /// <typeparam name="TDepth">Depth of this image (either Byte, Single or Double)</typeparam>
    [Serializable]
    public class Image<Tcolor, TDepth> : CvArray<TDepth>, Emgu.CV.IImage, IEquatable<Image<Tcolor, TDepth>> where Tcolor : IColor, new()
    {
        
        private TDepth[, ,] _array;

        //...
        /// <summary>
        /// Draws a set of blobs in the image.
        /// </summary>
        /// <param name="blobs">Array of blobs to draw.</param>
        /// <param name="fill">Whether to fill the blobs.</param>
        /// <param name="drawBoundingBox">Whether to draw the bounding box.</param>        
        /// <param name="BoundingBoxColor">Color of the bounding box</param>
        /// <param name="drawConvexHull">Whether to draw the convex hull.</param>
        /// <param name="ConvexHullColor">Color of the convex hull</param>
        /// <param name="drawEllipse">Whether to draw the best fit ellipse.</param>        
        /// <param name="EllipseColor">Color of the ellipse</param>
        /// <param name="drawCentroid">Whether to draw the centroids.</param>
        /// <param name="CentroidColor">Color of the centroid</param>
        /// <param name="drawAngle">Whether to draw the angle.</param>        
        /// <param name="AngleColor">Color of the angle</param>        
        public void DrawBlobs(Emgu.BLOB.Blob[] blobs,
                              bool fill,
                              bool drawBoundingBox, Tcolor BoundingBoxColor,
                              bool drawConvexHull, Tcolor ConvexHullColor,
                              bool drawEllipse, Tcolor EllipseColor,
                              bool drawCentroid, Tcolor CentroidColor,
                              bool drawAngle, Tcolor AngleColor)
        {
            Random r = new Random(0);
            foreach (var b in blobs)
            {
                if (fill)
                    b.FillBlob(Ptr, new MCvScalar(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));
                if (drawBoundingBox)
                    Draw(b.BoundingBox, BoundingBoxColor, 1);
                if (drawConvexHull)
                    DrawPolyline(b.ConvexHull, true, ConvexHullColor, 1);
                if (drawEllipse)
                    Draw(b.BestFitEllipse, EllipseColor, 1);

                if (drawCentroid)
                {
                    Draw(new LineSegment2D(new Point((int)b.CentroidX - 4, (int)b.CentroidY),
                                           new Point((int)b.CentroidX + 4, (int)b.CentroidY)),
                         CentroidColor, 1);
                    Draw(new LineSegment2D(new Point((int)b.CentroidX, (int)b.CentroidY - 4),
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
                    Draw(new LineSegment2D(new Point((int)x1, (int)y1),
                                    new Point((int)x2, (int)y2)),
                         AngleColor, 1);
                }
            }
        }
        
        /// <summary>
        /// Returns an array of blobs from a single channel image
        /// </summary>        
        /// <param name="threshold">Binarization threshold </param>
        /// <param name="maskImage">Mask</param>
        /// <param name="borderColor">Image's border color (false=black or true=white).</param>
        /// <param name="findMoments">Calculate blob moments.</param>        
        public Emgu.BLOB.Blob[] GetBlobs(int threshold,
                               IntPtr maskImage,
                               bool borderColor,
                               bool findMoments)
        {
            int count;
            IntPtr vector = BlobsInvoke.CvGetBlobs(Ptr, threshold, maskImage, borderColor, findMoments, out count);
            IntPtr[] blobsPtrs = new IntPtr[count];
            GCHandle handle = GCHandle.Alloc(blobsPtrs, GCHandleType.Pinned);
            Emgu.Util.Toolbox.memcpy(handle.AddrOfPinnedObject(), vector, count * Marshal.SizeOf(typeof(IntPtr)));
            handle.Free();
            Blob[] Blobs = new Blob[count];
            for (int i = 0; i < blobsPtrs.Length; i++)
                Blobs[i] = new Blob(blobsPtrs[i]);
            return Blobs;
        }


    }
}
