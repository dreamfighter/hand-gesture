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
using Emgu.CV.Structure;
using SVM;
using HMM;

namespace Motion_Detection_v2
{
    public partial class FormTrainingHmm : Form
    {
        private Capture[] cap;
        private Capture tempCapture;
        private String[] hmmClass;
        private String[] classLabel;
        private int[] A;
        private int B;
        private int movieIndex = 0;
        private Image<Gray, Byte> cache;
        private Image<Gray, Byte> imageCapture = new Image<Gray, Byte>(0, 0);
        private List<int> seq;
        private Filtering.Filtering filter;
        private ClassifierSvm svm;
        private ClassifierHmm hmm;
        private Point cursor = new Point();
        private int imageName = 0;
        private long average = 0;
        private int iteration = 0;
        private int jumlah_data = 30;
        private int interval = 200;
        private int frameSum = 24;
        private int videoFrame = 0;
        private int[] val;
        private HandShape.HandShape handShape;


        public FormTrainingHmm()
        {
            InitializeComponent();
            settingSvmClassifier();
            svm = new ClassifierSvm();
            svm.readModel("model/temp.mdl");
            hmm = new ClassifierHmm();
            hmm.readModel("model/temp.hmm");
            videoDirectory.SelectedPath = "video_testing";
            Console.WriteLine("svm class count "+svm.model.NumberOfClasses);
        }

        public void loadVideo()
        {
            String[] dir = Directory.GetDirectories(@"video");
            cap = new Capture[jumlah_data * dir.Length];
            hmmClass = new String[jumlah_data * dir.Length];
            int temp=0;
            for (int i = 0; i < dir.Length; i++)
            {
                for (int j = 1; j <= jumlah_data; j += 1)
                {
                    Console.WriteLine(dir[i] + "\\video (" + j + ").avi");
                    cap[temp] = InitializeCamera(dir[i] + "\\video (" + j + ").avi");
                    hmmClass[temp] = dir[i].Split('\\')[1];
                    temp++;
                }
            }
            //timerCapture.Enabled = true;
            timerRate.Enabled = true;
        }

        public void loadImages(String filename)
        {
            filter = new Filtering.Filtering();
            String[] dir = Directory.GetDirectories(@"" + filename);
            //svmClass = new String[jumlah_data * dir.Length];
            //image = new Image<Bgr, Byte>[jumlah_data * dir.Length];
            //TextWriter tw = new StreamWriter("model/temp.temp", false);
            int temp = 0;
            HandShape.HandShape handShape = null;
            for (int i = 0; i < dir.Length; i++)
            {
                String[] dirSeq = Directory.GetDirectories(@"" + dir[i]);
                seq = new List<int>();
                for (int j = 0; j < dirSeq.Length; j++)
                {
                    String[] filesImage = Directory.GetFiles(@"" + dirSeq[j]);
                    for (int k = 0; k < filesImage.Length; k++)
                    {
                        Console.WriteLine(filesImage[k]);
                        extractFeature(new Image<Bgr, Byte>(filesImage[k]), out handShape);
                        //filter.reduceSize(image[temp]);
                        //String[] sp = dir[i].Split('\\');
                        //svmClass[temp] = sp[sp.Length - 1];
                        if (handShape != null && handShape.fingersPosision != null)
                        {
                            //datasetSource.Rows.Add();
                            int handStatus = svm.predict(handShape);
                            seq.Add(handStatus);
                            //String[] s = addToTable(datasetSource.Rows[temp], svmClass[temp], filter.reduceSize(image[temp]), new Size(10, 10));


                            //String[] s = addToTable(datasetSource.Rows[temp], svmClass[temp], handShape);
                            //tw.WriteLine(s[0] + " " + s[1]);
                            //image[temp].Dispose();
                            //temp++;
                        }
                    }

                    string lastFolderName = Path.GetFileName(Path.GetDirectoryName(dir[i]));
                    addImagesToTable(lastFolderName, temp, seq);
                    temp++;
                }

                Application.DoEvents();
            }
            //tw.Close();
        }

        public void loadVideo(String filename)
        {
            String[] dir = Directory.GetDirectories(@"" + filename);
            cap = new Capture[jumlah_data * dir.Length];
            hmmClass = new String[jumlah_data * dir.Length];
            int temp = 0;
            for (int i = 0; i < dir.Length; i++)
            {
                if (!Directory.Exists(dir[i]))
                {
                    //Directory.CreateDirectory(path);
                    continue;
                }
                String[] filesVideo = Directory.GetFiles(@"" + dir[i]);
                for (int j = 0; j < jumlah_data; j += 1)
                {
                    //Console.WriteLine(dir[i] + "\\video (" + j + ").avi");
                    //cap[temp] = InitializeCamera(dir[i] + "\\video (" + j + ").avi");
                    Console.WriteLine(filesVideo[j]);
                    cap[temp] = InitializeCamera(filesVideo[j]);
                    String[] sp = dir[i].Split('\\');
                    hmmClass[temp] = sp[sp.Length - 1];
                    temp++;
                }
                Application.DoEvents();
            }
            timerRate.Interval = (int)intervalNum.Value;
            timerCapture.Interval = 42;

            timerCapture.Enabled = true;
            timerRate.Enabled = true;
            movieIndex = 0;
        }

        public Capture InitializeCamera(String filename)
        {
            //cap = new Capture(1);
            Capture cap = new Capture(filename);
            //cap.FlipHorizontal = true;
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 240);
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 320);
            return cap;
        }

        public void InitializeHmm()
        {
            List<String> temp = new List<string>();
            String t = "";
            for (int i = 0; i < sequenceSource.RowCount-1; i++)
            {
                if ((string)sequenceSource.Rows[i].Cells[0].Value != t)
                {
                    t = (string)sequenceSource.Rows[i].Cells[0].Value;
                    temp.Add((string)sequenceSource.Rows[i].Cells[0].Value);
                }
            }
            classLabel = temp.ToArray();
            A = new int[temp.Count];
            B = svm.model.NumberOfClasses;
            for (int i = 0; i < temp.Count; i++)
                A[i] = (int)stateCount.Value;
        }

        public ClassifierHmm InitializeHmm(int type, int hiddenstate)
        {
            List<String> temp = new List<string>();
            String t = "";
            for (int i = 0; i < sequenceSource.RowCount - 1; i++)
            {
                if ((string)sequenceSource.Rows[i].Cells[0].Value != t)
                {
                    t = (string)sequenceSource.Rows[i].Cells[0].Value;
                    temp.Add((string)sequenceSource.Rows[i].Cells[0].Value);
                }
            }
            classLabel = temp.ToArray();
            B = svm.model.NumberOfClasses;
            A = new int[temp.Count];
            for (int i = 0; i < temp.Count; i++)
                A[i] = hiddenstate;
            return new ClassifierHmm(classLabel, A, B, type);
        }

        public ClassifierHmm InitializeHmm(int type, int hiddenstate, HmmProblem[] prob)
        {

            List<String> temp = new List<string>();
            String t = "";
            for (int i = 0; i < prob.Length; i++)
            {
                if (prob[i].Label != t)
                {
                    t = prob[i].Label;
                    temp.Add(prob[i].Label);
                }
            }
            classLabel = temp.ToArray();
            B = svm.model.NumberOfClasses;
            A = new int[temp.Count];
            for (int i = 0; i < temp.Count; i++)
                A[i] = hiddenstate;
            ClassifierHmm hmmC = new ClassifierHmm(classLabel, A, B, type);
            hmmC.readObservation(prob);
            return hmmC;
        }

        public void addImagesToTable(String motionClass, int rowIndex,List<int> O)
        {
            String temp = O[0].ToString();
            for (int i = 1; i < O.Count; i++)
            {
                temp += "-" + O[i];
            }
            sequenceSource.Rows.Add();
            DataGridViewRow row = sequenceSource.Rows[rowIndex];
            row.Cells[0].Value = motionClass;
            row.Cells[1].Value = temp;
            Console.WriteLine(temp);
        }

        public void addToTable(List<int> O)
        {
            String temp = O[0].ToString();
            for (int i = 1; i < O.Count; i++)
            {
                temp += "-" + O[i];
            }
            sequenceSource.Rows.Add();
            DataGridViewRow row = sequenceSource.Rows[movieIndex];
            row.Cells[0].Value = hmmClass[movieIndex];
            row.Cells[1].Value = temp;
            Console.WriteLine(temp);
        }

        public void addToTable(List<String[]> O)
        {
            String temp = O[0].ToString();
            for (int i = 0; i < O.Count; i++)
            {                
                sequenceSource.Rows.Add();
                DataGridViewRow row = sequenceSource.Rows[i];
                row.Cells[0].Value = O[i][0];
                row.Cells[1].Value = O[i][1];
                Console.WriteLine(O[i][1]);
            }
        }

        private void timerCapture_Tick(object sender, EventArgs e)
        {
            long start, end;
            iteration++;
            start = DateTime.Now.Ticks;
            using (Image<Bgr, Byte> image = cap[movieIndex].QueryFrame())
            {
                if (image != null)
                {
                    if (checkSave.Checked)
                    {
                        imageName++;
                        image.Save(butChooseDir.Text + "/video (" + movieIndex + ") image (" + imageName + ").bmp");
                    }

                    //feature extraction
                    //Image<Gray, Byte> result = filter.reduceSize(temp1);
                    //Image<Gray, Byte> result = filter.reduceSize(extractFeature(image));
                    //imageCapture = filter.reduceSize(extractFeature(image));
                    imageCapture = extractFeature(image, out handShape).Convert<Gray,Byte>();
                    
                    //result.Dispose();
                    image.Dispose();
                }
                else
                {
                    cap[movieIndex].Dispose();
                    addToTable(seq);
                    movieIndex++;
                    imageName = 0;
                    imageCapture = null;
                    seq = new List<int>();
                    if (cap.Length <= movieIndex)
                    {
                        timerCapture.Enabled = false;
                        timerRate.Enabled = false;
                        //imageCapture.Dispose();
                        Console.WriteLine((double)average / iteration + "");
                        //MessageBox.Show((double)average / iteration + "");
                    }
                }
            }
            end = DateTime.Now.Ticks;
            average += end - start;
        }

        private void timerRate_Tick(object sender, EventArgs e)
        {
            try
            {
                //SVM clasification
                if (imageCapture != null)
                {
                    //int handStatus = svm.predict(imageCapture, new Size(10, 10));
                    int handStatus = svm.predict(handShape);
                    seq.Add(handStatus);
                    /*
                    if (imageCapture.GetAverage().Intensity != 0 && imageCapture.Convert<Gray, Byte>().GetSum().Intensity > 10000)
                    {
                        seq.Add(handStatus);
                        Console.WriteLine(handStatus + "");
                    }
                    */
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void butTrainHmm_Click(object sender, EventArgs e)
        {
            InitializeHmm();
            hmm = new ClassifierHmm(classLabel, A, B, "model/data-mdl.hmm", hmmType.SelectedIndex);
            hmm.readObservation(sequenceSource);
            hmm.learning(200, 0.1);
            if (DialogResult.OK == saveModelDialog.ShowDialog())
                hmm.writeModel(saveModelDialog.FileName);
            MessageBox.Show("Finish");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void trainingData_SelectedIndexChanged(object sender, EventArgs e)
        {
            sequenceSource.Rows.Clear();
            if (trainingData.SelectedIndex == 2)
            {
                videoDirectory.SelectedPath = Environment.CurrentDirectory;
                if (DialogResult.OK == videoDirectory.ShowDialog())
                {
                    jumlah_data = (int)dataCount.Value;
                    loadVideo(videoDirectory.SelectedPath);
                    filter = new Filtering.Filtering();
                    val = filter.settingFilter();
                    seq = new List<int>();
                }
            }
            else if (trainingData.SelectedIndex == 1)
            {
                if (DialogResult.OK == openTrainingDialog.ShowDialog())
                    addToTable(openObservation(openTrainingDialog.FileName));
            }
            else if (trainingData.SelectedIndex == 3)
            {
                videoDirectory.SelectedPath = Environment.CurrentDirectory;
                if (DialogResult.OK == videoDirectory.ShowDialog())
                {
                    //jumlah_data = (int)dataCount.Value;
                    loadImages(videoDirectory.SelectedPath);
                    filter = new Filtering.Filtering();
                    val = filter.settingFilter();
                    seq = new List<int>();
                }
            }
        }
        
        /// <summary>
        ///   Membaca sejumlah observation dari file data training.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public List<String[]> openObservation(String filename)
        {
            StreamReader input = new StreamReader(filename);
            List<String[]> st = new List<String[]>();

            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(':');
                if (str[0] != "")
                {
                    st.Add(str);
                }
            }
            input.Close();
            return st;
        }

        public HmmProblem[] readProblem(String filename)
        {
            StreamReader input = new StreamReader(filename);
            List<HmmProblem> prob = new List<HmmProblem>();

            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(':');
                if (str[0] != "")
                {
                    //Console.WriteLine("str[1] " + str[1]);
                    String[] O = str[1].Split('-');
                    int[] observation = new int[O.Length];
                    for (int i = 0; i < O.Length; i++)
                        observation[i] = int.Parse(O[i]);
                    prob.Add(new HmmProblem(str[0], observation));
                }
            }
            input.Close();
            return prob.ToArray();
        }
        
        private void settingSvmClassifier()
        {
            DirectoryInfo di = new DirectoryInfo("model");
            FileInfo[] rgFiles = di.GetFiles("*.mdl");
            foreach (FileInfo fi in rgFiles)
            {
                svmParams.Items.Add(fi.Name);
            }
        }

        private void buttonSaveTraining_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveTrainingDialog.ShowDialog())
                hmm.writeObservation(saveTrainingDialog.FileName, sequenceSource);           
        }

        private void butPredictHmm_Click(object sender, EventArgs e)
        {
            int total = sequenceSource.RowCount - 1;
            int correct = 0;
            Dictionary<String, Double> threshold = new Dictionary<string, double>();
            for (int i = 0; i < sequenceSource.RowCount - 1; i++)
            {
                String[] str = sequenceSource.Rows[i].Cells[1].Value.ToString().Split('-');
                double prob = 0;
                int[] O = new int[str.Length];
                for (int j = 0; j < str.Length; j++)
                    O[j] = int.Parse(str[j]);

                String result = hmm.predict(O, out prob);
                if (!threshold.ContainsKey(result))
                {
                    threshold.Add(result, 0);
                }
                threshold[result] += prob;

                sequenceSource.Rows[i].Cells[2].Value = result;
                sequenceSource.Rows[i].Cells[4].Value = prob;
                if ((string)sequenceSource.Rows[i].Cells[0].Value == result)
                {
                    sequenceSource.Rows[i].Cells[3].Value = true;
                    correct++;
                }
                else {
                    sequenceSource.Rows[i].Cells[3].Value = false;
                }

            }
            hmm.calculateThreshold(threshold, jumlah_data);
            hmm.writeModel(openModelDialog.FileName);

            MessageBox.Show((1.0 * correct / total) * 100 + "%");
            if (DialogResult.OK == savePredictDialog.ShowDialog())
                hmm.savePredictionResult(savePredictDialog.FileName, sequenceSource, (1.0 * correct / total) * 100);
            
        }

        private void butOpenHmmModel_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openModelDialog.ShowDialog())
            {
                hmm.readModel(openModelDialog.FileName);
                butOpenHmmModel.Text = openModelDialog.FileName;
            }
        }

        private void butChooseDir_Click(object sender, EventArgs e)
        {
            imageDirectory.SelectedPath = Environment.CurrentDirectory;
            if (DialogResult.OK == imageDirectory.ShowDialog())
                butChooseDir.Text = imageDirectory.SelectedPath;
        }

        private void analisisBut_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(analysisThreading));
            //t.Start();
            analysisThreading();
            hmm.readModel("model/temp.hmm");
            //analysis("training/tes2.sequence", "training/testing.sequence");
        }

        public void analysis() {
            double max = 0;
            for (int type = 0; type <= 3; type++)
            {
                for (int i = 1; i < 10; i++)
                {
                    try
                    {
                        ClassifierHmm hmmTemp = InitializeHmm(type, i);
                        hmmTemp.readObservation(sequenceSource);
                        hmmTemp.learning(200, 0.1);
                        
                        double temp = gridSelection(hmmTemp);
                        Console.Write("{0} {1} {2} ", type, i, temp);
                        if (max < temp)
                        {
                            max = temp;
                            Console.Write(" <- New Maximum!");
                        }
                        Console.WriteLine("");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void analysis(String trainset,String testset)
        {
            HmmProblem[] probtrain = readProblem(trainset);
            HmmProblem[] probtest = readProblem(testset);
            StreamWriter input = new StreamWriter("params/params.csv");

            double max = 0;
            double max1 = 0;
            for (int type = 0; type <= 3; type++)
            {
                for (int i = 1; i < 10; i++)
                {
                    try
                    {
                        ClassifierHmm hmmTemp = InitializeHmm(type, i, probtrain);
                        hmmTemp.learning(200, 0.1);
                        double temp = hmmTemp.predict(probtrain);
                        double temp1 = hmmTemp.predict(probtest);
                        bool maxs = false;
                        Console.Write("{0} {1} {2} {3}", type, i, temp, temp1);
                        if (max < temp1)
                        {
                            maxs = true;
                            max = temp1;
                            Console.Write(" <- New Maximum testing!");
                        }
                        if (max1 < temp)
                        {
                            max1 = temp;
                            Console.Write(" <- New Maximum training!");
                        }
                        input.WriteLine("{0};{1};{2};{3};{4}", type, i, temp * 100, temp1 * 100, maxs);
                        Console.WriteLine("");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            input.Close();
        }

        public void analysisThreading()
        {
            HmmProblem[] probtrain = readProblem("sequence/training.sequence");
            HmmProblem[] probtest = readProblem("sequence/testing.sequence");
            StreamWriter input = new StreamWriter("params/params-hmm.csv");
            ClassifierHmm hmmmodel=new ClassifierHmm();
            double max = 0;
            double max1 = 0;
            for (int type = 0; type <= 3; type++)
            {
                for (int i = 2; i < 10; i++)
                {
                    //try
                    //{
                        ClassifierHmm hmmTemp = InitializeHmm(type, i, probtrain);
                        hmmTemp.learning(200, 0.01);
                        double temp = hmmTemp.predict(probtrain);
                        double temp1 = hmmTemp.predict(probtest);
                        bool maxs = false;
                        Console.Write("{0} {1} {2} {3}", type, i, temp, temp1);
                        if (max < temp1)
                        {
                            hmmmodel = hmmTemp;
                            Console.WriteLine("here 1");
                            hmmmodel.writeModel("model/" + svmParams.Items[svmParams.SelectedIndex].ToString() + "-" + hmmType.Items[type].ToString() + i + ".hmm");
                            maxs = true;
                            Console.WriteLine("here 2");
                            max = temp1;
                            Console.Write(" <- New Maximum testing!");
                        }
                        if (max1 < temp)
                        {
                            max1 = temp;
                            Console.Write(" <- New Maximum training!");
                        }
                        Console.WriteLine("here 3");
                        input.WriteLine("{0};{1};{2};{3};{4};{5}", svmParams.Items[svmParams.SelectedIndex].ToString(), hmmType.Items[type].ToString(), i, temp * 100, temp1 * 100, maxs);

                        Console.WriteLine("here 4");
                        Console.WriteLine("");
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("wow "+ex.Message);
                    //}
                }
            }
            input.Close();
            hmmmodel.writeModel("model/temp.hmm");
        }

        private double gridSelection(ClassifierHmm hmmcalsifier)
        {
            int total = sequenceSource.RowCount - 1;
            int correct = 0;
            for (int i = 0; i < sequenceSource.RowCount - 1; i++)
            {
                String[] str = sequenceSource.Rows[i].Cells[1].Value.ToString().Split('-');
                int[] O = new int[str.Length];
                for (int j = 0; j < str.Length; j++)
                    O[j] = int.Parse(str[j]);

                String result = hmmcalsifier.predict(O);

                sequenceSource.Rows[i].Cells[2].Value = result;
                if ((string)sequenceSource.Rows[i].Cells[0].Value == result)
                {
                    sequenceSource.Rows[i].Cells[3].Value = true;
                    correct++;
                }
                else
                {
                    sequenceSource.Rows[i].Cells[3].Value = false;
                }
            }
            return (double)correct / total;
        }

        public Image<Bgr, Byte> extractFeature(Image<Bgr, Byte> image)
        {
            Image<Bgr, Byte> temp1 = new Image<Bgr, Byte>(320, 240);
            Image<Bgr, Byte> temp2 = new Image<Bgr, Byte>(320, 240);
            //color segmentation
            int[] val = filter.settingFilter();
            // cache = filter.HSV(image, val[0], val[1], val[2], val[3]);
            cache = filter.YCrCb(image, filter.yCrCb.cr_min, filter.yCrCb.cr_max, filter.yCrCb.cb_min, filter.yCrCb.cb_max);

            //filter noise
            //cache._Dilate(1);
            //cache._Erode(1);
            cache = filter.Median(cache, 7);
            //cache = filter.Mean(cache, 7);

            //get Blob
            try
            {
                //temp1 = filter.camShift(cache, out temp2, out cursor);
                temp1 = filter.detectBlob(cache, out temp2, out cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            pictureBox3.Image = temp2.ToBitmap();

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
            // cache = filter.HSV(image, val[0], val[1], val[2], val[3]);
            cache = filter.YCrCb(image, filter.yCrCb.cr_min, filter.yCrCb.cr_max, filter.yCrCb.cb_min, filter.yCrCb.cb_max);

            //filter noise
            cache._Dilate(1);
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

        private void sequenceSource_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String[] filesImage = Directory.GetFiles(@"" + videoDirectory.SelectedPath + "\\" + sequenceSource.Rows[e.RowIndex].Cells[0].Value.ToString());
                //tempCapture = InitializeCamera(videoDirectory.SelectedPath + "\\" + sequenceSource.Rows[e.RowIndex].Cells[0].Value.ToString() + "\\video (" + (e.RowIndex % jumlah_data +1) + ").avi");
                tempCapture = InitializeCamera(filesImage[(e.RowIndex % jumlah_data + 1)]);
                timerTick.Interval = interval;
                timerTick.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            using (Image<Bgr, Byte> image = tempCapture.QueryFrame())
            {
                if (videoFrame > frameSum)
                {
                    timerTick.Enabled = false;
                    videoFrame = 0;
                }
                try
                {
                    extractFeature(image, out handShape);
                }
                catch (Exception ex) { }
                videoFrame++;
            }
        }

        private void svmParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            svm.readModel("model/" + svmParams.Items[svmParams.SelectedIndex].ToString());
        }
    }
}
