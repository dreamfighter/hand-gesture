﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlobImageJava
{
    public class BlobAnalisis
    {
        private byte[][] COLOUR_ARRAY;

        private int width;
        private int height;

        private int[] labelBuffer;

        private int[] labelTable;
        private int[] xMinTable;
        private int[] xMaxTable;
        private int[] yMinTable;
        private int[] yMaxTable;
        private int[] massTable;



        public BlobAnalisis(int width, int height)
        {
            COLOUR_ARRAY = new byte[5][];
            byte[] col= new byte[3];
            col[0]=(byte)103;
            col[1]=(byte)121;
            col[2]=(byte)255;
            COLOUR_ARRAY[0] = col;
            col[0]=(byte)249;
            col[1]=(byte)255;
            col[2] = (byte)139;
            COLOUR_ARRAY[1] = col;
            col[0]=(byte)140;
            col[1]=(byte)255;
            col[2] = (byte)127;
            COLOUR_ARRAY[2] = col;
            col[0]=(byte)167;
            col[1]=(byte)254;
            col[2] = (byte)255;
            COLOUR_ARRAY[3] = col;
            col[0]=(byte)255;
            col[1]=(byte)111;
            col[2] = (byte)71;
            COLOUR_ARRAY[4] = col;

            this.width = width;
            this.height = height;

            labelBuffer = new int[width * height];

            // The maximum number of blobs is given by an image filled with equally spaced single pixel
            // blobs. For images with less blobs, memory will be wasted, but this approach is simpler and
            // probably quicker than dynamically resizing arrays
            int tableSize = width * height / 4;

            labelTable = new int[tableSize];
            xMinTable = new int[tableSize];
            xMaxTable = new int[tableSize];
            yMinTable = new int[tableSize];
            yMaxTable = new int[tableSize];
            massTable = new int[tableSize];
        }

        public List<BlobImageJava> detectBlobs(byte[] srcData, byte[] dstData, int minBlobMass, int maxBlobMass, byte matchVal, List<BlobImageJava> blobList)
        {
            if (dstData != null && dstData.Length != srcData.Length * 3)
                throw new ArgumentException("Bad array lengths: srcData 1 byte/pixel (mono), dstData 3 bytes/pixel (RGB)");

            // This is the neighbouring pixel pattern. For position X, A, B, C & D are checked
            // A B C
            // D X

            int srcPtr = 0;
            int aPtr = -width - 1;
            int bPtr = -width;
            int cPtr = -width + 1;
            int dPtr = -1;

            int label = 1;

            // Iterate through pixels looking for connected regions. Assigning labels
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    labelBuffer[srcPtr] = 0;

                    // Check if on foreground pixel
                    if (srcData[srcPtr] == matchVal)
                    {
                        // Find label for neighbours (0 if out of range)
                        int aLabel = (x > 0 && y > 0) ? labelTable[labelBuffer[aPtr]] : 0;
                        int bLabel = (y > 0) ? labelTable[labelBuffer[bPtr]] : 0;
                        int cLabel = (x < width - 1 && y > 0) ? labelTable[labelBuffer[cPtr]] : 0;
                        int dLabel = (x > 0) ? labelTable[labelBuffer[dPtr]] : 0;

                        // Look for label with least value
                        int min = int.MaxValue;
                        if (aLabel != 0 && aLabel < min) min = aLabel;
                        if (bLabel != 0 && bLabel < min) min = bLabel;
                        if (cLabel != 0 && cLabel < min) min = cLabel;
                        if (dLabel != 0 && dLabel < min) min = dLabel;

                        // If no neighbours in foreground
                        if (min == int.MaxValue)
                        {
                            labelBuffer[srcPtr] = label;
                            labelTable[label] = label;

                            // Initialise min/max x,y for label
                            yMinTable[label] = y;
                            yMaxTable[label] = y;
                            xMinTable[label] = x;
                            xMaxTable[label] = x;
                            massTable[label] = 1;

                            label++;
                        }

                        // Neighbour found
                        else
                        {
                            // Label pixel with lowest label from neighbours
                            labelBuffer[srcPtr] = min;

                            // Update min/max x,y for label
                            yMaxTable[min] = y;
                            massTable[min]++;
                            if (x < xMinTable[min]) xMinTable[min] = x;
                            if (x > xMaxTable[min]) xMaxTable[min] = x;

                            if (aLabel != 0) labelTable[aLabel] = min;
                            if (bLabel != 0) labelTable[bLabel] = min;
                            if (cLabel != 0) labelTable[cLabel] = min;
                            if (dLabel != 0) labelTable[dLabel] = min;
                        }
                    }

                    srcPtr++;
                    aPtr++;
                    bPtr++;
                    cPtr++;
                    dPtr++;
                }
            }

            // Iterate through labels pushing min/max x,y values towards minimum label
            if (blobList == null) blobList = new List<BlobImageJava>();

            for (int i = label - 1; i > 0; i--)
            {
                if (labelTable[i] != i)
                {
                    if (xMaxTable[i] > xMaxTable[labelTable[i]]) xMaxTable[labelTable[i]] = xMaxTable[i];
                    if (xMinTable[i] < xMinTable[labelTable[i]]) xMinTable[labelTable[i]] = xMinTable[i];
                    if (yMaxTable[i] > yMaxTable[labelTable[i]]) yMaxTable[labelTable[i]] = yMaxTable[i];
                    if (yMinTable[i] < yMinTable[labelTable[i]]) yMinTable[labelTable[i]] = yMinTable[i];
                    massTable[labelTable[i]] += massTable[i];

                    int l = i;
                    while (l != labelTable[l]) l = labelTable[l];
                    labelTable[i] = l;
                }
                else
                {
                    // Ignore blobs that butt against corners
                    if (i == labelBuffer[0]) continue;									// Top Left
                    if (i == labelBuffer[width]) continue;								// Top Right
                    if (i == labelBuffer[(width * height) - width + 1]) continue;	// Bottom Left
                    if (i == labelBuffer[(width * height) - 1]) continue;			// Bottom Right

                    if (massTable[i] >= minBlobMass && (massTable[i] <= maxBlobMass || maxBlobMass == -1))
                    {
                        BlobImageJava blob = new BlobImageJava(xMinTable[i], xMaxTable[i], yMinTable[i], yMaxTable[i], massTable[i]);
                        blobList.Add(blob);
                    }
                }
            }

            // If dst buffer provided, fill with coloured blobs
            if (dstData != null)
            {
                for (int i = label - 1; i > 0; i--)
                {
                    if (labelTable[i] != i)
                    {
                        int l = i;
                        while (l != labelTable[l]) l = labelTable[l];
                        labelTable[i] = l;
                    }
                }

                // Renumber lables into sequential numbers, starting with 0
                int newLabel = 0;
                for (int i = 1; i < label; i++)
                {
                    if (labelTable[i] == i) labelTable[i] = newLabel++;
                    else labelTable[i] = labelTable[labelTable[i]];
                }

                srcPtr = 0;
                int dstPtr = 0;
                while (srcPtr < srcData.Length)
                {
                    if (srcData[srcPtr] == matchVal)
                    {
                        int c = labelTable[labelBuffer[srcPtr]] % COLOUR_ARRAY.Length;
                        dstData[dstPtr] = COLOUR_ARRAY[c][0];
                        dstData[dstPtr + 1] = COLOUR_ARRAY[c][1];
                        dstData[dstPtr + 2] = COLOUR_ARRAY[c][2];
                    }
                    else
                    {
                        dstData[dstPtr] = 0;
                        dstData[dstPtr + 1] = 0;
                        dstData[dstPtr + 2] = 0;
                    }

                    srcPtr++;
                    dstPtr += 3;
                }
            }

            return blobList;
        }
    }
}