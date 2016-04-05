using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using HandShape;
using Motion_Detection_v2.Filtering;

namespace SVM
{
    class ClassifierSvm
    {
        public Model model;

        public ClassifierSvm() { }
        public void createDataTraining(String filename, String dirImage, int clsCount, int count)
        {
            Motion_Detection_v2.Filtering.Filtering filter = new Motion_Detection_v2.Filtering.Filtering();
            for (int j = 0; j < clsCount; j++)
                for (int i = 1; i <= count; i++)
                {
                    Image<Bgr, Byte> img = new Image<Bgr, Byte>(dirImage + "/" + j + "/image (" + i + ").bmp");
                    //createDataTraining("training/" + filename, j.ToString(), filter.reduceSize(img));
                    createDataTraining("training/" + filename, j.ToString(), filter.reduceSize(img), new Size(10, 10));
                    img.Dispose();
                }
        }

        public void createDataTesting(String filename, String dirImage, int clsCount, int count)
        {
            Motion_Detection_v2.Filtering.Filtering filter = new Motion_Detection_v2.Filtering.Filtering();
            for (int j = 0; j < clsCount; j++)
                for (int i = 1; i <= count; i++)
                {
                    Image<Bgr, Byte> img = new Image<Bgr, Byte>(dirImage + "/" + j + "/image (" + i + ").bmp");
                    //createDataTraining("testing/" + filename, j.ToString(), filter.reduceSize(img));
                    createDataTraining("testing/" + filename, j.ToString(), filter.reduceSize(img), new Size(10, 10));
                    img.Dispose();
                }
        }

        public void createDataTraining(String filename, String label, Image<Gray, Byte> img, Size size)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(filename, true);

            // write a line of text to the file
            int temp = 1;
            tw.Write(label + " ");
            for (int i = 0; i < img.Height / size.Height; i++)
            {
                for (int j = 0; j < img.Width / size.Width; j++)
                {
                    //img.Copy(new Rectangle(new Point(i * 10, j * 10), size)).GetAverage().Intensity;
                    tw.Write(temp + ":" + img.Copy(new Rectangle(new Point(i * size.Height, j * size.Width), size)).GetAverage().Intensity + " ");
                    temp++;
                }
            }
            tw.WriteLine("");
            tw.Close();
        }

        public void createDataTraining(String filename, String label, Image<Gray, Byte> img)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(filename, true);

            // write a line of text to the file
            int temp = 1;
            tw.Write(label + " ");
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
            tw.Close();
        }

        public void readModel()
        {
            model = Model.Read("model/data-svm.mdl");
        }

        public void readModel(String filename)
        {
            model = Model.Read(filename);
        }

        public void learningScaling(String filename)
        {
            Problem train = Problem.Read("training/" + filename);
            Parameter parameters = new Parameter();

            double C;
            double Gamma;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.
            RangeTransform rt = RangeTransform.Compute(train);
            RangeTransform.Write("scaling/data-svm.train.scale", rt);
            ParameterSelection.Grid(Scaling.Scale(rt, train), parameters, "params/data-svm.params", out C, out Gamma);
            //System.Windows.Forms.MessageBox.Show(C.ToString() + "," + Gamma.ToString());
            parameters.C = C;
            parameters.Gamma = Gamma;
            Training.PerformCrossValidation(Scaling.Scale(rt, train), parameters, 5);
            Model model = Training.Train(Scaling.Scale(rt,train), parameters);
            
            //Model model = Training.Train(train, parameters);
            Model.Write("model/data-svm.mdl", model);
        }

        public void learning(String filename)
        {            
            Problem train = Problem.Read("training/" + filename);
            Parameter parameters = new Parameter();

            double C;
            double Gamma;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.
            ParameterSelection.Grid(train, parameters, "params/data-svm.params", out C, out Gamma);
            parameters.C = C;
            parameters.Gamma = Gamma;
            Training.PerformCrossValidation(train, parameters, 5);
            Model model = Training.Train(train, parameters);
            Model.Write("model/data-svm.mdl", model);
        }

        public void learning(KernelType kernel)
        {
            Problem train = Problem.Read("model/temp.temp");
            Parameter parameters = new Parameter();

            double C;
            double Gamma;
            parameters.KernelType = kernel;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.
            ParameterSelection.Grid(train, parameters, "params/data-svm.params", out C, out Gamma);
            parameters.C = C;
            parameters.Gamma = Gamma;
            //parameters.C = 2;
            //parameters.Gamma = 1;
            //Training.PerformCrossValidation(train, parameters, 5);
            Model model = Training.Train(train, parameters);
            Model.Write("model/temp.mdl", model);
        }

        public void learning(String filename,KernelType kernel,Double C,Double Gamma)
        {
            Problem train = Problem.Read("model/temp.temp");
            Problem valid = Problem.Read("model/fix-testing.train");
            Parameter parameters = new Parameter();

            parameters.KernelType = kernel;
            parameters.C = C;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.
            //ParameterSelection.Grid(train, parameters, "params/data-svm.params", out C, out Gamma);
            //ParameterSelection.Grid(train, valid, parameters, "params/params-svm.csv", out C, out Gamma);
            //parameters.C = C;
            parameters.Gamma = Gamma;
            //parameters.C = 2;
            //parameters.Gamma = 1;
            //Training.PerformCrossValidation(train, parameters, 5);
            Model model = Training.Train(train, parameters);
            Model.Write(filename, model);
            Model.Write("model/temp.mdl", model);
        }

        public void analysis(KernelType kernel)
        {
            Problem train = Problem.Read("model/temp.temp");
            Problem valid = Problem.Read("model/fix-testing.train");
            Parameter parameters = new Parameter();

            double C;
            double Gamma;
            
            parameters.KernelType = kernel;
            // This will do a grid optimization to find the best parameters
            // and store them in C and Gamma, outputting the entire
            // search to params.txt.

            /*
            RangeTransform rt = RangeTransform.Compute(train, 0, 1);
            train = Scaling.Scale(rt, train);
            Problem.Write("scaling/scale-train.train", train);

            rt = RangeTransform.Compute(valid, 0, 1);
            valid = Scaling.Scale(rt, valid);
            Problem.Write("scaling/valid-train.train", valid);
            */

            ParameterSelection.Grid(train, valid, parameters, "model/temp.csv", out C, out Gamma);
            //ParameterSelection.Grid(train, parameters, "model/temp.csv", out C, out Gamma);
            parameters.C = C;
            parameters.Gamma = Gamma;
            //Training.PerformCrossValidation(train, parameters, 5);
            Model model = Training.Train(train, parameters);
            Model.Write("model/temp.mdl", model);
        }

        public double predictscaling(String filename)
        {
            Problem test;
            try
            {
                test = Problem.Read("training/" + filename);
            }
            catch (Exception ex)
            {
                test = Problem.Read("testing/" + filename);
            }
            Model model = Model.Read("model/data-svm.mdl");
            RangeTransform rt = RangeTransform.Compute(test);
            //RangeTransform.Write("scaling/data-2410.test.scale", rt);
            //Problem.Write("training/data-2410.train.scale", Scaling.Scale(rt, test));
            //MessageBox.Show(Prediction.Predict(Scaling.Scale(rt, test), "predict/data-2410.train.predict", model, true) * 100 + "%");
            return Prediction.Predict(Scaling.Scale(rt, test), "predict/" + filename + ".predict", model, true) * 100;
            //MessageBox.Show(Prediction.Predict(test, "predict/" + filename + ".predict", model, true) * 100 + "%");
        }

        public double predict(String filename)
        {
            Problem test;
            try
            {
                test = Problem.Read("training/" + filename);
            }
            catch (Exception ex)
            {
                test = Problem.Read("testing/" + filename);
            }
            Model model = Model.Read("model/data-svm.mdl");
            //RangeTransform rt = RangeTransform.Compute(test);
            //RangeTransform.Write("scaling/data-2410.test.scale", rt);
            //Problem.Write("training/data-2410.train.scale", Scaling.Scale(rt, test));
            //MessageBox.Show(Prediction.Predict(Scaling.Scale(rt, test), "predict/data-2410.train.predict", model, true) * 100 + "%");
            return Prediction.Predict(test, "predict/" + filename + ".predict", model, true) * 100;
            //MessageBox.Show(Prediction.Predict(test, "predict/" + filename + ".predict", model, true) * 100 + "%");
        }

        public int predict(Image<Gray, Byte> img)
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
                try
                {
                    x[i] = new Node(index[i], val[i]);
                }
                catch (Exception ex) {
                    x[i] = new Node(index[i], 0);
                }
                //info.Text += index[i] + ":" + val[i] + System.Environment.NewLine;
            }

            Model model = Model.Read("model/data-svm.mdl");
            return int.Parse(Prediction.Predict(model, x) + "");
        }

        public int predict(Image<Gray, Byte> img, Size size)
        {
            int temp = 1;
            int n = (img.Height / size.Height) * (img.Width / size.Width);
            Node[] x = new Node[n];

            for (int i = 0; i < img.Height / size.Height; i++)
            {
                for (int j = 0; j < img.Width / size.Width; j++)
                {
                    x[temp - 1] = new Node(temp, (double)img.Copy(new Rectangle(new Point(i * size.Height, j * size.Width), size)).GetAverage().Intensity / 255);
                    temp++;
                }
            }
            return int.Parse(Prediction.Predict(model, x) + "");
        }


        public int predict(HandShape.HandShape handShape)
        {
            int temp = 1;
            //int n = (handShape.fingersStartPoint.Length * 2) * 3;
            int n = ((handShape.fingersStartPoint.Length-1) * 4);
            Node[] x = new Node[n];

            for (int i = 1; i < handShape.fingersStartPoint.Length; i++)
            {
                /*
                float startX = handShape.fingersStartPoint[i].X - handShape.handrect.Left;
                float startY = handShape.fingersStartPoint[i].Y - handShape.handrect.Top;
                float endX = handShape.fingersEndPoint[i].X - handShape.handrect.Left;
                float endY = handShape.fingersEndPoint[i].Y - handShape.handrect.Top;
                float depthX = handShape.fingersDepthPoint[i].X - handShape.handrect.Left;
                float depthY = handShape.fingersDepthPoint[i].Y - handShape.handrect.Top;

                x[temp] = new Node(temp++, startX);
                x[temp] = new Node(temp++, startY);
                x[temp] = new Node(temp++, endX);
                x[temp] = new Node(temp++, endY);
                x[temp] = new Node(temp++, depthX);
                x[temp] = new Node(temp++, depthY);
                */


                x[temp-1] = new Node(temp++, (double)Filtering.angleBetweenTwoPoints(handShape.fingersStartPoint[i], handShape.fingersDepthPoint[i]) / 360);
                //Console.Write(temp + ":" + Filtering.angleBetweenTwoPoints(handShape.fingersStartPoint[i], handShape.fingersDepthPoint[i]) / 360 + " ");
                x[temp-1] = new Node(temp++, (double)Filtering.angleBetweenTwoPoints(handShape.fingersEndPoint[i], handShape.fingersDepthPoint[i]) / 360);
                //x[temp] = new Node(temp++,Filtering.angleBetweenTwoPoints(handShape.maximumInscribedCircle.Center, handShape.fingersDepthPoint[i]));

                float x2 = handShape.fingersStartPoint[i].X - handShape.fingersDepthPoint[i].X;
                float y2 = handShape.fingersStartPoint[i].Y - handShape.fingersDepthPoint[i].Y;

                double r = Math.Sqrt((x2 * x2) + (y2 * y2));

                x[temp - 1] = new Node(temp++, (r / handShape.getRadius()));

                x2 = handShape.fingersEndPoint[i].X - handShape.fingersDepthPoint[i].X;
                y2 = handShape.fingersEndPoint[i].Y - handShape.fingersDepthPoint[i].Y;

                r = Math.Sqrt((x2 * x2) + (y2 * y2));

                x[temp - 1] = new Node(temp++, (r / handShape.getRadius()));
                    //temp++;
                //Console.Write(temp + ":" + Filtering.angleBetweenTwoPoints(handShape.fingersEndPoint[i], handShape.fingersDepthPoint[i]) / 360 + " ");
            }
            //x[temp] = new Node(temp++, 0);
            //x[temp] = new Node(temp++, 0);
            /*
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("whats is this=>["+x[i]._index+"]" + x[i]._value);
            }
            Console.WriteLine();
            */
            return int.Parse(Prediction.Predict(model, x) + "");
        }

        public String predict(System.Windows.Forms.DataGridViewCell attrb)
        {
            if (attrb.Value == null)
            {
                return "";
            }
            String[] temp = attrb.Value.ToString().Split(' ');
            Node[] x = new Node[temp.Length-1];
            for (int i = 0; i < temp.Length-1; i++)
            {
                if (temp[i] != "")
                {
                    String[] t = temp[i].Split(':');
                    x[i] = new Node(int.Parse(t[0]), double.Parse(t[1]));
                }
            }
            /*
            for (int i = 0; i < temp.Length-1; i++)
            {
                Console.WriteLine("whats is this=>[" + x[i]._index + "]" + x[i]._value);
            }
            */
            return Prediction.Predict(model, x) + "";
        }

        public int predictByAttrb(String attrb)
        {
            if (attrb == null)
            {
                return 0;
            }
            String[] temp = attrb.Split(' ');
            Node[] x = new Node[temp.Length - 1];
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "")
                {
                    String[] t = temp[i].Split(':');
                    x[i] = new Node(int.Parse(t[0]), double.Parse(t[1]));
                }
            }
            return int.Parse(Prediction.Predict(model, x) + "");
        }
    }
}
