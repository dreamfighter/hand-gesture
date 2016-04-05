using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SVM;
using HMM;
using HandShape;

namespace Motion_Detection_v2
{
    public partial class FormTrainingSvm : Form
    {
        private Image<Bgr, Byte>[] image;
        private Image<Gray, Byte> cache;
        private String[] svmClass;
        private ClassifierSvm svm;
        private Filtering.Filtering filter;
        private Point cursor = new Point();
        private int jumlah_data = 50;

        public FormTrainingSvm()
        {
            InitializeComponent();
            svm = new ClassifierSvm();
        }

        public void loadImage()
        {
            filter = new Filtering.Filtering();
            String[] dir = Directory.GetDirectories(@"image");
            svmClass = new String[jumlah_data * dir.Length];
            image = new Image<Bgr, Byte>[jumlah_data * dir.Length];
            TextWriter tw = new StreamWriter("model/temp.temp", false);
            int temp = 0;
            for (int i = 0; i < dir.Length; i++)
            {
                for (int j = 1; j <= jumlah_data; j++)
                {
                    Console.WriteLine(dir[i] + "\\image (" + j + ").bmp");
                    image[temp] = extractFeature(new Image<Bgr, Byte>(dir[i] + "\\image (" + j + ").bmp"));
                    //filter.reduceSize(image[temp]);
                    svmClass[temp] = dir[i].Split('\\')[1];
                    datasetSource.Rows.Add();

                    String[] s = addToTable(datasetSource.Rows[temp], svmClass[temp], filter.reduceSize(image[temp]), new Size(10, 10));
                    tw.WriteLine(s[0] + " " + s[1]);
                    image[temp].Dispose();
                    temp++;
                }
            }
            tw.Close();
        }

        public void loadImage(String filename)
        {
            filter = new Filtering.Filtering();
            String[] dir = Directory.GetDirectories(@"" + filename);
            svmClass = new String[jumlah_data * dir.Length];
            image = new Image<Bgr, Byte>[jumlah_data * dir.Length];
            TextWriter tw = new StreamWriter("model/temp.temp", false);
            int temp = 0;
            HandShape.HandShape handShape = null;
            for (int i = 0; i < dir.Length; i++)
            {
                for (int j = 1; j <= jumlah_data; j++)
                {
                    Console.WriteLine(dir[i] + "\\image (" + j + ").bmp");
                    image[temp] = extractFeature(new Image<Bgr, Byte>(dir[i] + "\\image (" + j + ").bmp"), out handShape);
                    //filter.reduceSize(image[temp]);
                    String[] sp = dir[i].Split('\\');
                    svmClass[temp] = sp[sp.Length - 1];
                    if (handShape != null && handShape.fingersPosision != null)
                    {
                        datasetSource.Rows.Add();

                        //String[] s = addToTable(datasetSource.Rows[temp], svmClass[temp], filter.reduceSize(image[temp]), new Size(10, 10));

                    
                        String[] s = addToTable(datasetSource.Rows[temp], svmClass[temp], handShape);
                        tw.WriteLine(s[0] + " " + s[1]);
                        image[temp].Dispose();
                        temp++;
                    }
                }
                Application.DoEvents();
            }
            tw.Close();
        }

        public List<String[]> readDataSet(String filename)
        {
            StreamReader input = new StreamReader(filename);
            StreamWriter output = new StreamWriter("model/temp.temp");
            List<String[]> rs =new List<String[]>();
            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(' ');
                String[] result = new String[2];
                String[] result1 = new String[2];
                result[0] = str[0];
                result1[0] = str[0];
                for (int i = 1; i < str.Length; i++)
                {
                    if (str[i] != "")
                    {
                        result1[1] += " " + str[i];
                        result[1] += str[i] + " ";
                    }
                }
                output.WriteLine(result[0] + result1[1]);
                rs.Add(result);
            }
            input.Close();
            output.Close();
            return rs;
        }

        public void writeDataSet(String filename)
        {
            StreamReader input = new StreamReader("model/temp.temp");
            StreamWriter output = new StreamWriter(filename);
            List<String[]> rs = new List<String[]>();
            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(' ');
                String[] result = new String[2];
                result[0] = str[0];
                for (int i = 1; i < str.Length; i++)
                    result[1] += " " + str[i];
                output.WriteLine(result[0] + result[1]);
            }
            input.Close();
            output.Close();
        }

        public String[] addToTable(System.Windows.Forms.DataGridViewRow dataset, String label, HandShape.HandShape handShape)
        {
            int temp = 1;
            String[] result = new String[2];
            String attrib = "";
            //int h = img.Height / size.Height;
            //int w = img.Width / size.Width;
            //double[] val = new double[h * w];
            //MCvConvexityDefect[] defectArray = handShape.getConvexityDefects().ElementAt(i)

            for (int i = 1; i < handShape.fingersStartPoint.Length; i++)
            {
                //PointF finggerPosition = handShape.fingersPosision[i];
                //MCvConvexityDefect defect = handShape.convexityDefects[i];

                /*
                float startX = handShape.fingersStartPoint[i].X - handShape.handrect.Left;
                float startY = handShape.fingersStartPoint[i].Y - handShape.handrect.Top;
                float endX = handShape.fingersEndPoint[i].X - handShape.handrect.Left;
                float endY = handShape.fingersEndPoint[i].Y - handShape.handrect.Top;
                float depthX = handShape.fingersDepthPoint[i].X - handShape.handrect.Left;
                float depthY = handShape.fingersDepthPoint[i].Y - handShape.handrect.Top;

                attrib += temp++ + ":" + startX + " " + temp++ + ":" + startY + " ";
                attrib += temp++ + ":" + endX + " " + temp++ + ":" + endY + " ";
                attrib += temp++ + ":" + depthX + " " + temp++ + ":" + depthY + " ";
                 */

                attrib += temp++ + ":" + Filtering.Filtering.angleBetweenTwoPoints(handShape.fingersStartPoint[i], handShape.fingersDepthPoint[i]) / 360 + " ";
                attrib += temp++ + ":" + Filtering.Filtering.angleBetweenTwoPoints(handShape.fingersEndPoint[i], handShape.fingersDepthPoint[i]) / 360 + " ";

                float x = handShape.fingersStartPoint[i].X - handShape.fingersDepthPoint[i].X;
                float y = handShape.fingersStartPoint[i].Y - handShape.fingersDepthPoint[i].Y;

                double r = Math.Sqrt((x * x) + (y * y));

                attrib += temp++ + ":" + (r / handShape.getRadius()) + " ";

                x = handShape.fingersEndPoint[i].X - handShape.fingersDepthPoint[i].X;
                y = handShape.fingersEndPoint[i].Y - handShape.fingersDepthPoint[i].Y;

                r = Math.Sqrt((x * x) + (y * y));

                attrib += temp++ + ":" + (r / handShape.getRadius()) + " ";

                //attrib += temp++ + ":" + Filtering.Filtering.angleBetweenTwoPoints(handShape.maximumInscribedCircle.Center, handShape.fingersDepthPoint[i]) + " ";
            }

            result[0] = label;
            result[1] = attrib;
            dataset.Cells[0].Value = label;
            dataset.Cells[1].Value = attrib;
            return result;
        }

        public String[] addToTable(System.Windows.Forms.DataGridViewRow dataset,String label, Image<Gray, Byte> img, Size size)
        {
            int temp = 1;
            String[] result = new String[2];
            String attrib = "";
            //int h = img.Height / size.Height;
            //int w = img.Width / size.Width;
            //double[] val = new double[h * w];


            for (int i = 0; i < img.Height / size.Height; i++)
            {
                for (int j = 0; j < img.Width / size.Width; j++)
                {
                    //val[temp - 1] = (double)img.Copy(new Rectangle(new Point(i * size.Height, j * size.Width), size)).GetAverage().Intensity;
                    attrib += temp + ":" + (double)img.Copy(new Rectangle(new Point(i * size.Height, j * size.Width), size)).GetAverage().Intensity / 255 + " ";
                    //tw.Write( ":" + img.Copy(new Rectangle(new Point(i * size.Height, j * size.Width), size)).GetAverage().Intensity + " ");
                    temp++;
                }
            }

            result[0] = label;
            result[1] = attrib;
            dataset.Cells[0].Value = label;
            dataset.Cells[1].Value = attrib;
            return result;
        }

        private double Transform(double input, double minValues, double maxValues, double lowerBound, double upperBound)
        {
            double outputStart = lowerBound;
            double outputScale = upperBound - lowerBound;

            double inputStart = minValues;
            double inputScale = maxValues - minValues;

            double tmp = input - inputStart;
            if (inputScale == 0)
                return 0;
            tmp /= inputScale;
            tmp *= outputScale;
            return tmp + outputStart;
        }

        public void addToTable(System.Windows.Forms.DataGridView dataset, List<String[]> rs)
        {
            for (int i = 0; i < rs.Count; i++)
            {
                String[] st = rs[i].ToArray();
                dataset.Rows.Add();
                dataset.Rows[i].Cells[0].Value = st[0];
                dataset.Rows[i].Cells[1].Value = st[1];         
            }
        }

        public Image<Bgr,Byte>extractFeature(Image<Bgr,Byte> image)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            HandShape.HandShape handShape = new HandShape.HandShape();
            //HandShape.HandShape handShape2 = new HandShape.HandShape();
            //color segmentation
            int[] val = filter.settingFilter();
            //cache = filter.filterYCrCb(image);

            //cache = filter.HSV(image, val[0], val[1], val[2], val[3]);
            cache = filter.YCrCb(image, filter.yCrCb.cr_min, filter.yCrCb.cr_max, filter.yCrCb.cb_min, filter.yCrCb.cb_max);
            //Console.WriteLine(filter.yCrCb.cr_min + "," + filter.yCrCb.cr_max + "," + filter.yCrCb.cb_min + "," + filter.yCrCb.cb_max);
            
            //filter noise
            //cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                //temp1 = filter.detectBlob(cache, out temp2, out cursor);
                handShape = filter.extractContourAndHull(cache, out temp2);
                cursor = new Point((int)handShape.getCenter().X, (int)handShape.getCenter().Y);
                //Image<Gray, Byte> resizedImg = handShape.getImage().Copy().Resize(100, 100, INTER.CV_INTER_AREA);
                //handShape = filter.extractContourAndHull(resizedImg, out temp2);
                temp1 = handShape.getImage().Convert<Bgr, Byte>();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("message=>"+ex.Message);
            }

            pictureBox3.Image = temp2.ToBitmap();
            //pictureBox3.Image = cache.ToBitmap();
            image.Dispose();
            cache.Dispose();
            temp2.Dispose();
            return temp1;
        }



        public Image<Bgr, Byte> extractFeature(Image<Bgr, Byte> image, out HandShape.HandShape handShape)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            //HandShape.HandShape handShape2 = new HandShape.HandShape();
            handShape = new HandShape.HandShape();
            //color segmentation
            int[] val = filter.settingFilter();
            //cache = filter.HSV(image, val[0], val[1], val[2], val[3]);
            //cache = filter.filterYCrCb(image);
            cache = filter.YCrCb(image, filter.yCrCb.cr_min, filter.yCrCb.cr_max, filter.yCrCb.cb_min, filter.yCrCb.cb_max);
            //Console.WriteLine(filter.yCrCb.cr_min + "," + filter.yCrCb.cr_max + "," + filter.yCrCb.cb_min + "," + filter.yCrCb.cb_max);
            //filter noise
            //cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                //temp1 = filter.detectBlob(cache, out temp2, out cursor);
                handShape = filter.extractContourAndHull(cache, out temp2);
                cursor = new Point((int)handShape.getCenter().X, (int)handShape.getCenter().Y);
                //Image<Gray, Byte> resizedImg = handShape.getImage().Copy().Resize(100, 100, INTER.CV_INTER_AREA);
                //handShape = filter.extractContourAndHull(resizedImg, out temp2);
                temp1 = handShape.getImage().Convert<Bgr, Byte>();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            pictureBox3.Image = temp2.ToBitmap();

            image.Dispose();
            cache.Dispose();
            //temp2.Dispose();
            return temp1;
        }

        private void trainingData_SelectedIndexChanged(object sender, EventArgs e)
        {
            datasetSource.Rows.Clear();
            if (trainingData.SelectedIndex == 2)
            {
                imageDirectory.SelectedPath = Environment.CurrentDirectory;
                
                if (DialogResult.OK == imageDirectory.ShowDialog())
                    loadImage(imageDirectory.SelectedPath);

            }
            else
            {
                if (DialogResult.OK == openDataDialog.ShowDialog())
                    addToTable(datasetSource, readDataSet(openDataDialog.FileName));
            }
        }

        private void butTrainHmm_Click(object sender, EventArgs e)
        {
            KernelType k=new KernelType();
            if (kernelType.SelectedIndex == 0)
                k = KernelType.RBF;
            else if (kernelType.SelectedIndex == 1)
                k = KernelType.LINEAR;
            else if (kernelType.SelectedIndex == 2)
                k = KernelType.POLY;
            else if (kernelType.SelectedIndex == 3)
                k = KernelType.SIGMOID;
            if (DialogResult.OK == saveModelDialog.ShowDialog())
                svm.learning(saveModelDialog.FileName, k, (double)numC.Value, (double)numGamma.Value);
        }

        private void butPredictHmm_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openModelDialog.ShowDialog())
            {
                StreamWriter sw = new StreamWriter("predict/svm-predict.csv");
                int total = datasetSource.RowCount - 1;
                int correct = 0;
                //svm.readModel("model/temp.mdl");
                svm.readModel(openModelDialog.FileName);
                for (int i = 0; i < datasetSource.RowCount - 1; i++)
                {
                    String temp = svm.predict(datasetSource.Rows[i].Cells[1]);
                    datasetSource.Rows[i].Cells[2].Value = temp;
                    if (datasetSource.Rows[i].Cells[0].Value.ToString() == temp)
                    {
                        datasetSource.Rows[i].Cells[3].Value = true;
                        correct++;
                    }
                    else
                        datasetSource.Rows[i].Cells[3].Value = false;
                    String[] modelName = openModelDialog.FileName.Split('\\');
                    sw.WriteLine("{0};{1};{2};{3}", modelName[modelName.Length - 1], datasetSource.Rows[i].Cells[0].Value.ToString(), temp, datasetSource.Rows[i].Cells[0].Value.ToString() == temp);
                }
                sw.Close();
                MessageBox.Show((1.0 * correct / total) * 100 + "%");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            datasetSource.Rows.Clear();
            loadImage("image_testing");
        }

        private void buttonSaveTraining_Click(object sender, EventArgs e)
        {            
            if (DialogResult.OK == saveDataDialog.ShowDialog())
                writeDataSet(saveDataDialog.FileName);
        }

        private void analisisBut_Click(object sender, EventArgs e)
        {
            if (kernelType.SelectedIndex == 0)
                svm.analysis(KernelType.RBF);
            else if (kernelType.SelectedIndex == 1)
                svm.analysis(KernelType.LINEAR);
            else if (kernelType.SelectedIndex == 2)
                svm.analysis(KernelType.POLY);
            else if (kernelType.SelectedIndex == 3)
                svm.analysis(KernelType.SIGMOID);

        }

        private void datasetSource_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            try
            {
                extractFeature(new Image<Bgr, Byte>(imageDirectory.SelectedPath + "\\" + datasetSource.Rows[e.RowIndex].Cells[0].Value.ToString() + "\\image (" + (e.RowIndex % jumlah_data + 1) + ").bmp")).ToBitmap();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void datasetSource_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                extractFeature(new Image<Bgr, Byte>(imageDirectory.SelectedPath + "\\" + datasetSource.Rows[e.RowIndex].Cells[0].Value.ToString() + "\\image (" + (e.RowIndex % jumlah_data + 1) + ").bmp")).ToBitmap();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
