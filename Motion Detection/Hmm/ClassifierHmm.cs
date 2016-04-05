using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HMM
{
    public class ClassifierHmm
    {
        public HiddenMarkovModel[] model;
        private String filename = "model/data-mdl.hmm";

        public ClassifierHmm() { }

        public ClassifierHmm(String[] label, int[] state, int symbol)
        {
            model = new HiddenMarkovModel[label.Length];
            for (int i = 0; i < label.Length; i++)
            {
                model[i] = new HiddenMarkovModel(label[i], state[i], symbol);
            }
            writeModel(this.filename);
        }

        public ClassifierHmm(String[] label, int[] state, int symbol, String filename)
        {
            model = new HiddenMarkovModel[label.Length];
            for (int i = 0; i < label.Length; i++)
            {
                model[i] = new HiddenMarkovModel(label[i], state[i], symbol);
            }
            writeModel(filename);
        }

        public ClassifierHmm(String[] label, int[] state, int symbol, String filename,int type)
        {
            model = new HiddenMarkovModel[label.Length];
            for (int i = 0; i < label.Length; i++)
            {
                model[i] = new HiddenMarkovModel(label[i], state[i], symbol, type);
            }
            writeModel(filename);
        }

        public ClassifierHmm(String[] label, int[] state, int symbol, int type)
        {
            model = new HiddenMarkovModel[label.Length];
            for (int i = 0; i < label.Length; i++)
            {
                model[i] = new HiddenMarkovModel(label[i], state[i], symbol, type);
            }
        }

        public void setFilename(String filename)
        {
            this.filename = filename;
        }

        /// <summary>
        ///   Menulis model tiap class.
        /// </summary>
        /// <param name="filename">nama file directory dimana model akan disimpan.</param>
        public void writeModel(String filename)
        {
            TextWriter tw = new StreamWriter(filename);
            tw.WriteLine(model.Length);
            tw.Close();

            for (int i = 0; i < model.Length; i++)
                model[i].writeModel(filename);
        }

        /// <summary>
        ///   Membaca model tiap class.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public void readModel()
        {
            StreamReader input = new StreamReader(this.filename);
            int hmmSum = int.Parse(input.ReadLine());
            model = new HiddenMarkovModel[hmmSum];
            int i = -1;

            while (!input.EndOfStream)
            {
                String str = input.ReadLine();
                if (str == "[model]")
                {
                    i++;
                    String[] mdl = input.ReadLine().Split(' ');
                    int hiddenState = int.Parse(mdl[1]);
                    int observestate = int.Parse(mdl[2]);
                    model[i] = new HiddenMarkovModel(mdl[0], hiddenState, observestate);
                }
                else if (str == "[pi]")
                {
                    String[] pi = input.ReadLine().Split(' ');
                    Double[] probabilities = new Double[pi.Length];
                    for (int j = 0; j < pi.Length; j++)
                        if (pi[j] != "")
                            probabilities[j] = double.Parse(pi[j]);

                    model[i].probabilities = probabilities;
                }
                else if (str == "[transitions]")
                {
                    for (int j = 0; j < model[i].getHiddenstate(); j++)
                    {
                        String[] A = input.ReadLine().Split(' ');
                        for (int k = 0; k < A.Length; k++)
                            if (A[k] != "")
                                model[i].transitions[j, k] = double.Parse(A[k]);
                    }
                }
                else if (str == "[emissions]")
                {
                    for (int j = 0; j < model[i].getHiddenstate(); j++)
                    {
                        String[] B = input.ReadLine().Split(' ');
                        for (int k = 0; k < B.Length; k++)
                            if (B[k] != "")
                                model[i].emissions[j, k] = double.Parse(B[k]);
                    }

                }
                else if (str == "[threshold]")
                {
                    double threshold = Double.Parse(input.ReadLine());
                    model[i].threshold = threshold;
                }
            }
            input.Close();
        }

        /// <summary>
        ///   Membaca model tiap class.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public void readModel(String filename)
        {
            StreamReader input = new StreamReader(filename);
            int hmmSum = int.Parse(input.ReadLine());
            model = new HiddenMarkovModel[hmmSum];
            int i = -1;

            while (!input.EndOfStream)
            {
                String str = input.ReadLine();
                if (str == "[model]")
                {
                    i++;
                    String[] mdl=  input.ReadLine().Split(' ');
                    int hiddenState = int.Parse(mdl[1]);
                    int observestate = int.Parse(mdl[2]);
                    model[i] = new HiddenMarkovModel(mdl[0], hiddenState, observestate);
                }
                else if (str == "[pi]")
                {
                    String[] pi = input.ReadLine().Split(' ');
                    Double[] probabilities=new Double[pi.Length];
                    for (int j = 0; j < pi.Length; j++)
                        if (pi[j] != "")
                            probabilities[j] = double.Parse(pi[j]);

                    model[i].probabilities = probabilities;
                }
                else if (str == "[transitions]")
                {
                    for (int j = 0; j < model[i].getHiddenstate(); j++)
                    {
                        String[] A = input.ReadLine().Split(' ');
                        for (int k = 0; k < A.Length; k++)
                            if (A[k] != "")
                                model[i].transitions[j, k] = double.Parse(A[k]);
                    }
                }
                else if (str == "[emissions]")
                {
                    for (int j = 0; j < model[i].getHiddenstate(); j++)
                    {
                        String[] B = input.ReadLine().Split(' ');
                        for (int k = 0; k < B.Length; k++)
                            if (B[k] != "")
                                model[i].emissions[j, k] = double.Parse(B[k]);
                    }

                }
                else if (str == "[threshold]")
                {
                    double threshold = Double.Parse(input.ReadLine());
                    model[i].threshold = threshold;
                }
            }
            input.Close();
        }

        /// <summary>
        ///   Membaca sejumlah observation dari file data training.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public void readObservation(String filename)
        {
            StreamReader input = new StreamReader(filename);
            
            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(':');
                for(int i=0;i<model.Length;i++)
                    if (model[i].label == str[0])
                    {
                        String[] temp = str[1].Split('-');
                        int[] O = new int[temp.Length];
                        for (int j = 0; j < temp.Length; j++)
                        {
                            O[j] = int.Parse(temp[j]);
                        }
                        model[i].pushObservation(O);
                    }
            }
        }

        /// <summary>
        ///   Membaca sejumlah observation dari file data training.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public void readObservation(HmmProblem[] prob)
        {
            int j =0;
            int i=0;
            for (j = 0; j < prob.Length; j++)
            {
                for (i = 0; i < model.Length; i++)
                    if (model[i].label == prob[j].Label)
                    {
                        model[i].pushObservation(prob[j].Observation);
                    }
            }
        }

        /// <summary>
        ///   Membaca sejumlah observation dari data grid panel.
        /// </summary>
        /// <param name="filename">nama file directory dimana model disimpan.</param>
        public void readObservation(System.Windows.Forms.DataGridView source)
        {
            for (int j = 0; j < source.RowCount; j++)
            {
                for (int i = 0; i < model.Length; i++)
                    if (model[i].label == (string)source.Rows[j].Cells[0].Value)
                    {
                        String[] temp = source.Rows[j].Cells[1].Value.ToString().Split('-');
                        int[] O = new int[temp.Length];
                        for (int k = 0; k < temp.Length; k++)
                        {
                            O[k] = int.Parse(temp[k]);
                        }
                        model[i].pushObservation(O);
                    }
            }
        }

        public void writeObservation(String filename,String cls,int[] observe)
        {
            TextWriter tw = new StreamWriter(filename, true);
            if (observe.Length > 0)
            {
                tw.Write(cls + ":" + observe[0]);
                for (int i = 1; i < observe.Length; i++)
                    tw.Write("-" + observe[i]);
            }
            tw.Close();
        }

        public void writeObservation(String filename, System.Windows.Forms.DataGridView source)
        {
            TextWriter tw = new StreamWriter(filename, false);
            if (source.RowCount > 0)
            {
                for (int i = 0; i < source.RowCount-1; i++)
                {
                    try
                    {
                        tw.WriteLine(source.Rows[i].Cells[0].Value.ToString() + ":" + source.Rows[i].Cells[1].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }
            tw.Close();
        }

        public void learning(int iteration,double limit)
        {
            for (int i = 0; i < model.Length; i++)
                model[i].baumWelchList(iteration, limit);

            writeModel(this.filename);
        }

        public void learning() {
            for (int i = 0; i < model.Length; i++)
                model[i].baumWelchList(50, 0.01);

            writeModel(this.filename);
        }

        public String predict(int[] observations)
        {

            double max = 0;
            double probability = 0;
            double threshold = 10.0;
            String result = "";
            for (int i = 0; i < model.Length; i++)
            {
                probability = model[i].calculateProbability(observations);
                if (max < probability)
                {
                    max = probability;
                    result = model[i].label;
                }
            }
            //if (-Math.Log(max) < threshold)
                //return "X";
            return result;
        }

        public String predict(int[] observations,out double prob)
        {

            double max = 0;
            double probability = 0;
            double threshold = 10.0;
            String result = "";
            HiddenMarkovModel mdl = null;
            for (int i = 0; i < model.Length; i++)
            {
                probability = model[i].calculateProbability(observations);
                if (max < probability)
                {
                    mdl = model[i];
                    max = probability;
                    result = model[i].label;
                }
            }
            prob = -Math.Log(max);
            if (prob < mdl.threshold)
                return "X";
            return result;
        }

        public void calculateThreshold(Dictionary<String, Double> threshold,int dataCount)
        {
            for (int i = 0; i < model.Length; i++)
            {
                model[i].threshold = threshold[model[i].label] / dataCount;
            }
        }

        public String predict(HiddenMarkovModel[] mdl,int[] observations)
        {

            double max = 0;
            double probability = 0;
            String result = "";
            for (int i = 0; i < mdl.Length; i++)
            {
                probability = mdl[i].calculateProbability(observations);
                if (max < probability)
                {
                    max = probability;
                    result = mdl[i].label;
                }
            }
            return result;
        }

        public Double predict(String filename)
        {
            StreamReader input = new StreamReader(filename);
            int correct = 0;
            int sumData = 0;
            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(':');
                String[] temp = str[1].Split('-');
                int[] O = new int[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    O[j] = int.Parse(temp[j]);
                }
                if (str[0] == predict(O))
                    correct++;
                sumData++;
            }
            input.Close();
            Double acuracy = 1.0 * correct/sumData;
            return acuracy * 100;
        }

        public Double predict(HiddenMarkovModel[] mdl,String filename)
        {
            StreamReader input = new StreamReader(filename);
            int correct = 0;
            int sumData = 0;
            while (!input.EndOfStream)
            {
                String[] str = input.ReadLine().Split(':');
                String[] temp = str[1].Split('-');
                int[] O = new int[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    O[j] = int.Parse(temp[j]);
                }
                if (str[0] == predict(mdl,O))
                    correct++;
                sumData++;
            }
            input.Close();
            Double acuracy = 1.0 * correct / sumData;
            return acuracy * 100;
        }

        public Double predict(HmmProblem[] prob)
        {
            int correct = 0;
            for (int j = 0; j < prob.Length; j++)
            {
                if (prob[j].Label == predict(prob[j].Observation))
                    correct++;
            }
            return (double)correct / prob.Length;
        }

        public void savePredictionResult(String filename, System.Windows.Forms.DataGridView source)
        {
            TextWriter tw = new StreamWriter(filename, false);
            if (source.RowCount > 0)
            {
                for (int i = 0; i < source.RowCount - 1; i++)
                {
                    try
                    {
                        tw.WriteLine(source.Rows[i].Cells[0].Value.ToString() + ";" + source.Rows[i].Cells[1].Value.ToString() + ";" + source.Rows[i].Cells[2].Value.ToString() + ";" + source.Rows[i].Cells[3].Value.ToString() + ";" + source.Rows[i].Cells[4].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }
            tw.Close();
        }

        public void savePredictionResult(String filename, System.Windows.Forms.DataGridView source, double result)
        {
            TextWriter tw = new StreamWriter(filename, false);
            if (source.RowCount > 0)
            {
                for (int i = 0; i < source.RowCount - 1; i++)
                {
                    try
                    {
                        tw.WriteLine(source.Rows[i].Cells[0].Value.ToString() + ";" + source.Rows[i].Cells[1].Value.ToString() + ";" + source.Rows[i].Cells[2].Value.ToString() + ";" + source.Rows[i].Cells[3].Value.ToString() + ";" + source.Rows[i].Cells[4].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                tw.WriteLine("Akurasi;" + result);
            }
            tw.Close();
        }
    }
}
