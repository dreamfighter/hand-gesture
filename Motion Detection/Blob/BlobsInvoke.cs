using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Emgu.CV.Structure;

namespace Emgu.CV
{
    /// <summary>
    /// Library to invoke blob functions
    /// </summary>
    public static class BlobsInvoke
    {
        private const string BLOBS_LIBRARY = "cvblobs.dll";

        /// <summary>
        /// Returns an array of blobs from a single channel image
        /// </summary>
        /// <param name="inputImage">Input single channel image.</param>
        /// <param name="threshold">Binarization threshold </param>
        /// <param name="maskImage">Mask</param>
        /// <param name="borderColor">Image's border color (false=black or true=white).</param>
        /// <param name="findMoments">Calculate blob moments.</param>
        /// <param name="NumberOfBlobs">Outputs the number of blobs found</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern IntPtr CvGetBlobs(IntPtr inputImage,
                                                int threshold,
                                                IntPtr maskImage,
                                                bool borderColor,
                                                bool findMoments,
                                                out int NumberOfBlobs
                                                 );

        /// <summary>
        /// Releases a blob
        /// </summary>
        /// <param name="blob">The blob.</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern void CvClearBlob(IntPtr blob);

        /// <summary>
        /// Gets the blob features
        /// </summary>
        /// <param name="blob">The blob.</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern IntPtr CvGetBlobFeatures(IntPtr blob);

        /// <summary>
        /// Releases the blob features
        /// </summary>
        /// <param name="blobFeatures">The blob features.</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern IntPtr CvBlobFeaturesRelease(IntPtr blobFeatures);

        /// <summary>
        /// Gets the blob edges
        /// </summary>
        /// <param name="blob">The blob.</param>
        /// <param name="numberOfEdges">Outputs the number of edges.</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern IntPtr CvGetBlobEdges(IntPtr blob, out int numberOfEdges);


        /// <summary>
        /// Draws a blob into an image
        /// </summary>
        /// <param name="blob">The blob.</param>
        /// <param name="image">The image.</param>
        /// <param name="color">The fill color.</param>
        /// <param name="offsetX">The X offset.</param>
        /// <param name="offsetY">The Y offset.</param>
        [DllImport(BLOBS_LIBRARY)]
        public static extern void CvFillBlob(IntPtr blob, IntPtr image, MCvScalar color, int offsetX, int offsetY);
    }
}
