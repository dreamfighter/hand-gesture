using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HMM
{
    public class HiddenMarkovModel
    {
        private int observeState;
        private int hiddenState;
        private Double[,] A;
        private Double[,] B;
        private Double[] pi;
        private String classLabel;
        private int[][] seqObservations;
        private List<int[]> observationsList;
        private Double probabilityThreshold = 0;
        
        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public HiddenMarkovModel(String label, int hiddenState, int observeState)
        {
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            //this.pi[0] = 1.0;

            for (int i = 0; i < hiddenState; i++)
            {
                for (int j = 0; j < hiddenState; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    this.A[i, j] = 1.0 / hiddenState;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            double s = 0.0;
            Random random = new Random();
            for (int i = 0; i < hiddenState; i++)
            {
                this.pi[i] = random.Next(0, 20);
                //MessageBox.Show(this.pi[i].ToString());
                s += this.pi[i];
            }

            for (int i = 0; i < hiddenState; i++)
            {
                this.pi[i] = this.pi[i] / s;
            }
            //menulis ke txt file
            //writeModel();
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public HiddenMarkovModel(String label, int hiddenState, int observeState,int type)
        {
            switch (type)
            {
                case 0:
                    {
                        HiddenMarkovModelErgodic(label, hiddenState, observeState);
                        break;
                    }
                case 1:
                    {
                        HiddenMarkovModelLeftRight(label, hiddenState, observeState);
                        break;
                    }
                case 2:
                    {
                        if (hiddenState % 2 == 1)
                            HiddenMarkovModelParalelLeftRight2(label, hiddenState, observeState);
                        else
                            HiddenMarkovModelParalelLeftRight(label, hiddenState, observeState);
                        break;
                    }
                case 3:
                    {
                        HiddenMarkovModelFullLeftRight(label, hiddenState, observeState);
                        break;
                    }
            }
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="label">Label class dari sebuah model.</param>
        /// <param name="hiddenstate">Banyaknya jumlah hidden state.</param>
        /// <param name="observestate">Banyaknya jumlah observe state.</param>
        /// <param name="pi">matrix probabilitas distribusi.</param>
        /// <param name="transitions">matrix pobabilitas transitions.</param>
        /// <param name="emissions">matrix pobabilitas emissions.</param>
        public HiddenMarkovModel(String label, int hiddenstate, int observestate, double[] pi, double[,] transitions, double[,] emissions)
        {
            this.classLabel = label;
            this.observeState = hiddenstate;
            this.hiddenState = observestate;
            this.pi = pi;
            this.A = transitions;
            this.B = emissions;
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model type Ergodic.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public void HiddenMarkovModelErgodic(String label, int hiddenState, int observeState)
        {
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            //this.pi[0] = 1.0;

            for (int i = 0; i < hiddenState; i++)
            {
                for (int j = 0; j < hiddenState; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    this.A[i, j] = 1.0 / hiddenState;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            double s = 0.0;
            Random random = new Random();
            for (int i = 0; i < hiddenState; i++)
            {
                this.pi[i] = random.Next(0, 20);
                //MessageBox.Show(this.pi[i].ToString());
                s += this.pi[i];
            }

            for (int i = 0; i < hiddenState; i++)
            {
                this.pi[i] = this.pi[i] / s;
            }
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public void HiddenMarkovModelLeftRight(String label, int hiddenState, int observeState)
        {
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            this.pi[0] = 1.0;

            for (int i = 0; i < hiddenState; i++)
            {
                for (int j = 0; j < hiddenState; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    if (i == j || i + 1 == j)
                        this.A[i, j] = 0.5;
                    else
                        this.A[i, j] = 0;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            this.A[hiddenState - 1, hiddenState - 1] = 0.5;
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public void HiddenMarkovModelParalelLeftRight(String label, int hiddenState, int observeState)
        {
            if (hiddenState % 2 == 1)
                hiddenState++;
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            this.pi[0] = 0.5;
            this.pi[hiddenState / 2] = 0.5;

            for (int i = 0; i < hiddenState; i++)
            {
                for (int j = 0; j < hiddenState; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    if (i == j || ((j == i + (hiddenState / 2) + 1 || j == i + 1 || j == i - (hiddenState / 2) + 1)&&(i + 1) % (hiddenState / 2)!=0) )
                        this.A[i, j] = (double)1.0 / 3;
                    else
                        this.A[i, j] = 0;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            this.A[hiddenState - 1, hiddenState - 1] = 0.5;
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public void HiddenMarkovModelParalelLeftRight2(String label, int hiddenState, int observeState)
        {
            if (hiddenState % 2 == 0)
                hiddenState++;
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            this.pi[0] = 0.5;
            if (hiddenState > 0)
                this.pi[hiddenState / 2] = 0.5;

            for (int i = 0; i < hiddenState-1; i++)
            {
                for (int j = 0; j < hiddenState-1; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    if (i == j || ((j == i + (hiddenState - 1) / 2 + 1 || j == i + 1 || j == i - (hiddenState - 1) / 2 + 1) && (i + 1) % ((hiddenState - 1) / 2) != 0))
                        this.A[i, j] = (double)1.0 / 3;
                    else
                        this.A[i, j] = 0;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            for (int j = 0; j < observeState; j++)
            {
                //inisialisasi probabilitas setiap hidden state
                this.B[hiddenState - 1, j] = 1.0 / observeState;
            }
            this.A[hiddenState - 2, hiddenState - 1] = 0.5;
            this.A[hiddenState/2 - 1, hiddenState - 1] = 0.5;
            this.A[hiddenState - 2, hiddenState - 1] = 0.5;
            this.A[hiddenState - 1, hiddenState - 1] = 0.5;
        }

        /// <summary>
        ///   Inisialisasi Hidden Markov Model.
        /// </summary>
        /// <param name="observeState">Banyaknya jumlah hidden state.</param>
        /// <param name="hiddenState">Banyaknya jumlah observe state.</param>
        public void HiddenMarkovModelFullLeftRight(String label, int hiddenState, int observeState)
        {
            this.classLabel = label;
            this.hiddenState = hiddenState;
            this.observeState = observeState;
            this.pi = new Double[hiddenState];
            this.A = new Double[hiddenState, hiddenState];
            this.B = new Double[hiddenState, observeState];
            this.pi[0] = 1.0;

            for (int i = 0; i < hiddenState; i++)
            {
                for (int j = 0; j < hiddenState; j++)
                {
                    //inisialisasi probabilitas setiap observe state
                    if (i <= j)
                        this.A[i, j] = (double)1.0 / hiddenState - i;
                    else
                        this.A[i, j] = 0;
                }
                for (int j = 0; j < observeState; j++)
                {
                    //inisialisasi probabilitas setiap hidden state
                    this.B[i, j] = 1.0 / observeState;
                }
                //inisialisasi probabilitas distribusi pertama kali
                //this.pi[i] = 1.0 / observeState;
            }
            this.A[hiddenState - 1, hiddenState - 1] = 0.5;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public int getHiddenstate() { return this.hiddenState; }
        public int getObservestate() { return this.observeState; }
        public void setSeqObservations(List<int[]> observations)
        {
            this.seqObservations = new int[observations.Count][];
            for (int i = 0; i < observations.Count; i++)
                this.seqObservations[i] = observations[i];
        }

        public void pushObservation(int[] observations)
        {
            if (this.observationsList == null)
                this.observationsList = new List<int[]>();
            this.observationsList.Add(observations);
        }

        public double[] probabilities
        {
            get { return this.pi; }
            set { this.pi = value; }
        }

        public int[][] sequenceObservetion
        {
            get { return this.seqObservations; }
            set { this.seqObservations = value; }
        }

        public double[,] transitions
        {
            get { return this.A; }
            set { this.A = value; }
        }

        public double[,] emissions
        {
            get { return this.B; }
            set { this.B = value; }
        }

        public double threshold
        {
            get { return this.probabilityThreshold; }
            set { this.probabilityThreshold = value; }
        }

        public String label
        {
            get { return this.classLabel; }
            set { this.classLabel = value; }
        }

        /// <summary>
        ///   Menulis model ke file.
        /// </summary>
        /// <param name="filename">
        ///   directory file tempat model akan disimpan
        /// </param>
        public void writeModel(String filename)
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter(filename, true);

            tw.WriteLine("[model]");
            tw.WriteLine(this.classLabel + " " + this.hiddenState + " " + this.observeState);
            tw.WriteLine("[pi]");
            for (int i = 0; i < this.pi.Length; i++)
            {
                tw.Write(this.pi[i] + " ");
            }


            tw.WriteLine("");
            tw.WriteLine("[transitions]");
            for (int i = 0; i < this.hiddenState; i++)
            {
                for (int j = 0; j < this.hiddenState; j++)
                {
                    tw.Write(this.A[i, j] + " ");
                }
                tw.WriteLine("");
            }

            tw.WriteLine("[emissions]");
            for (int i = 0; i < this.hiddenState; i++)
            {
                for (int j = 0; j < this.observeState; j++)
                {
                    tw.Write(this.B[i, j] + " ");
                }
                tw.WriteLine("");
            }
            tw.WriteLine("[threshold]");
            tw.WriteLine(probabilityThreshold);
            tw.WriteLine("");

            // close the stream
            tw.Close();
        }

        /// <summary>
        ///   Menulis model ke file.
        /// </summary>
        public void writeModel()
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter("model/data-mdl.hmm",true);

            tw.WriteLine("[model]");
            tw.WriteLine(this.classLabel + " " + this.hiddenState + " " + this.observeState);
            tw.WriteLine("[pi]");
            for (int i = 0; i < this.pi.Length; i++)
            {
                tw.Write(this.pi[i] + " ");
            }


            tw.WriteLine("");
            tw.WriteLine("[transitions]");
            for (int i = 0; i < this.hiddenState; i++)
            {
                for (int j = 0; j < this.hiddenState; j++)
                {
                    tw.Write(this.A[i, j] + " ");
                }
                tw.WriteLine("");
            }

            tw.WriteLine("[emissions]");
            for (int i = 0; i < this.hiddenState; i++)
            {
                for (int j = 0; j < this.observeState; j++)
                {
                    tw.Write(this.B[i, j] + " ");
                }
                tw.WriteLine("");
            }
            tw.WriteLine("[threshold]");
            tw.WriteLine(probabilityThreshold);

            tw.WriteLine("");
            // close the stream
            tw.Close();
        }

        /// <summary>
        ///   Algoritma viterbi.
        /// </summary>
        /// <remarks>
        ///   Algoritma viterbi untuk mencari path terbaik dan probabilitas maksimum dari sebuah model.
        ///   diberikan sebuah observasi sequence O={1,2,3,..,0K} dan topologi model hmm.
        /// </remarks>
        /// <param name="observations">
        ///   sejumlah set observations sequence
        /// </param>
        /// <param name="path">
        ///   sejumlah set observations sequence
        /// </param>
        /// <returns>
        ///   besar probabilitas dari viterbi.
        /// </returns>
        public Double decoding(int[] observations, out int[] path)
        {
            int N = this.hiddenState;
            int T = observations.Length;
            Double minProb,prob;
            int minState;

            int[,] history = new int[N, T];
            Double[,] viterbi = new Double[N, T];

            //inisialisasi
            for (int i = 0; i < N; i++)
            {
                viterbi[i, 0] = -Math.Log(this.pi[i]) - Math.Log(this.B[i, observations[0]]);
            }
            
            //induksi
            for (int t = 1; t < T; t++)
            {
                for (int s = 0; s < N; s++)
                {
                    minProb = viterbi[0, t - 1] - Math.Log(A[0, s]);
                    minState = 0;
                    for (int i = 1; i < N; i++)
                    {
                        prob = viterbi[s, t - 1] - Math.Log(A[i, s]);
                        if (minProb > prob)
                        {
                            minProb = prob;
                            minState = i;
                        }
                    }
                    viterbi[s, t] = minProb - Math.Log(B[s, observations[t]]);
                    history[s, t] = minState;
                }
            }

            //terminasi
            minProb = viterbi[0, T - 1];
            minState = 0;
            for (int i = 1; i < N; i++)
            {
                if (minProb > viterbi[i, T - 1])
                {
                    minProb = viterbi[i, T - 1];
                    minState = i;
                }
            }

            //backtrack
            int[] statePath = new int[T];
            statePath[T - 1] = minState;
            for (int t = T - 2; t > 0; t--)
                statePath[t] = history[statePath[t + 1], t + 1];

            path = statePath;
            return Math.Exp(-minProb);
        }

        /// <summary>
        ///   Algoritma forward.
        /// </summary>
        /// <remarks>
        ///   Algoritma forward untuk evaluasi.
        /// </remarks>
        /// <param name="observations">
        ///   sejumlah set observations sequence
        /// </param>
        /// <returns>
        ///   besar probabilitas dari sebuah model.
        /// </returns>
        public Double calculateProbability(int[] observations)
        {
            int N = this.hiddenState;
            int T = observations.Length;
            Double minProb, prob;
            int minState;

            int[,] history = new int[N, T];
            Double[,] viterbi = new Double[N, T];

            //inisialisasi
            for (int i = 0; i < N; i++)
            {
                viterbi[i, 0] = -Math.Log(this.pi[i]) - Math.Log(this.B[i, observations[0]]);
            }

            //induksi
            for (int t = 1; t < T; t++)
            {
                for (int s = 0; s < N; s++)
                {
                    minProb = viterbi[0, t - 1] - Math.Log(A[0, s]);
                    minState = 0;
                    for (int i = 1; i < N; i++)
                    {
                        prob = viterbi[s, t - 1] - Math.Log(A[i, s]);
                        //Console.WriteLine("minProb,prob[" + minProb + "," + prob + "]");
                        if (minProb > prob)
                        {
                            minProb = prob;
                            minState = i;
                        }
                    }
                    //Console.WriteLine("observations[t]=>" + observations[t]);
                    int x = observations[t];
                    viterbi[s, t] = minProb - Math.Log(B[s, observations[t]]);
                    history[s, t] = minState;
                }
                //Application.DoEvents();
            }

            //terminasi
            minProb = viterbi[0, T - 1];
            minState = 0;
            for (int i = 1; i < N; i++)
            {
                //Console.WriteLine("minProb,viterbi[i, T - 1][" + minProb + "," + viterbi[i, T - 1] + "]");
                if (minProb > viterbi[i, T - 1])
                {
                    minProb = viterbi[i, T - 1];
                    minState = i;
                }
            }
            return Math.Exp(-minProb);
        }
        
        /// <summary>
        ///   Algoritma forward menggunakan scaling.
        /// </summary>
        /// <remarks>
        ///   Algoritma forward untuk proses learning.
        /// </remarks>
        /// <param name="observations">
        ///   sejumlah set observations sequence
        /// </param>
        /// <returns>
        ///   besar probabilitas matrix forward.
        /// </returns>
        public Double[,] forward(int[] obrservations)
        {
            int T = obrservations.Length;
            int N = this.hiddenState;
            double[,] fwrd = new double[N, T];
            double s = 0;

            for (int i = 0; i < N; i++)
            {
                fwrd[i, 0] = this.pi[i] * B[i, obrservations[0]];
                s += fwrd[i, 0];
            }

            for (int i = 0; i < N; i++)
                fwrd[i, 0] = fwrd[i, 0] / s;

            for (int t = 1; t < T; t++)
            {
                s = 0;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        fwrd[i, t] += fwrd[j, t - 1] * A[j, i] * B[i, obrservations[t]];
                    }
                    s += fwrd[i, t];
                }

                //scaling
                for (int i = 0; i < N; i++)
                    fwrd[i, t] = fwrd[i, t] / s;                               
            }
            return fwrd;
        }

        /// <summary>
        ///   Algoritma backward menggunakan scaling.
        /// </summary>
        /// <remarks>
        ///   Algoritma backward untuk proses learning.
        /// </remarks>
        /// <param name="observations">
        ///   sejumlah set observations sequence
        /// </param>
        /// <returns>
        ///   besar probabilitas matrix backward.
        /// </returns>
        public Double[,] backward(int[] observations)
        {
            int T = observations.Length;
            int N = this.hiddenState;
            double[,] bwrd = new double[N, T];
            double s = 0;

            for (int i = 0; i < N; i++)
            {
                bwrd[i, T - 1] = 1.0;
                s += bwrd[i, T - 1];
            }

            for (int i = 0; i < N; i++)
                bwrd[i, T - 1] = bwrd[i, T - 1] / s;

            for (int t = T - 2; t >= 0; t--)
            {
                s = 0;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        bwrd[i, t] += bwrd[j, t + 1] * A[i, j] * B[j, observations[t + 1]];
                    }
                    s += bwrd[i, t];
                }

                // Scaling
                for (int i = 0; i < N; i++)
                    bwrd[i, t] /= s;
            }
            return bwrd;
        }

        /// <summary>
        ///   Algoritma Baum-Welch untuk learning hidden Markov model.
        /// </summary>
        /// <remarks>
        ///   untuk menyelesaikan masalah learning. inputan sejumlah training observation sequences O = {o1, o2, ..., oK}
        ///   dan model dari HMM (jumlah hiddenstate dan observestate), outputnya adalah
        ///   parameter HMM M = (A, B, pi) yang terbaik untuk training data. 
        /// </remarks>
        /// <param name="iterations">
        ///   jumlah loop yang akan dijalankan.
        /// </param>
        /// <param name="limit">
        ///   kesamaan likelihood antara dua iterasi.
        ///   learning akan berhenti jika kesamaan likelihood sudah mencapai limit.
        /// </param>
        /// <returns>
        ///   besar likelihood dari training.
        /// </returns>
        public Double baumWelch(List<int[]> observetions, int iteration, double limit)
        {
            for (int i = 0; i < observetions.Count; i++)
                this.seqObservations[i] = observetions[i];

            return baumWelch(this.seqObservations, iteration, limit);
        }

        /// <summary>
        ///   Algoritma Baum-Welch untuk learning hidden Markov model.
        /// </summary>
        /// <remarks>
        ///   untuk menyelesaikan masalah learning. inputan sejumlah training observation sequences O = {o1, o2, ..., oK}
        ///   dan model dari HMM (jumlah hiddenstate dan observestate), outputnya adalah
        ///   parameter HMM M = (A, B, pi) yang terbaik untuk training data. 
        /// </remarks>
        /// <param name="iterations">
        ///   jumlah loop yang akan dijalankan.
        /// </param>
        /// <param name="limit">
        ///   kesamaan likelihood antara dua iterasi.
        ///   learning akan berhenti jika kesamaan likelihood sudah mencapai limit.
        /// </param>
        /// <returns>
        ///   besar likelihood dari training.
        /// </returns>
        public Double baumWelchList(int iteration, double limit)
        {
            this.seqObservations = new int[this.observationsList.Count][];
            for (int i = 0; i < this.observationsList.Count; i++)
                this.seqObservations[i] = this.observationsList[i];
            
            return baumWelch(this.seqObservations, iteration, limit);
        }

        /// <summary>
        ///   Algoritma Baum-Welch untuk learning hidden Markov model.
        /// </summary>
        /// <remarks>
        ///   untuk menyelesaikan masalah learning. inputan sejumlah training observation sequences O = {o1, o2, ..., oK}
        ///   dan model dari HMM (jumlah hiddenstate dan observestate), outputnya adalah
        ///   parameter HMM M = (A, B, pi) yang terbaik untuk training data. 
        /// </remarks>
        /// <param name="iterations">
        ///   jumlah loop yang akan dijalankan.
        /// </param>
        /// <param name="limit">
        ///   kesamaan likelihood antara dua iterasi.
        ///   learning akan berhenti jika kesamaan likelihood sudah mencapai limit.
        /// </param>
        /// <returns>
        ///   besar likelihood dari training.
        /// </returns>
        public Double baumWelch(int iteration, double limit)
        {
            return baumWelch(this.seqObservations, iteration, limit);
        }

        /// <summary>
        ///   Algoritma Baum-Welch untuk learning hidden Markov model.
        /// </summary>
        /// <remarks>
        ///   untuk menyelesaikan masalah learning. inputan sejumlah training observation sequences O = {o1, o2, ..., oK}
        ///   dan model dari HMM (jumlah hiddenstate dan observestate), outputnya adalah
        ///   parameter HMM M = (A, B, pi) yang terbaik untuk training data. 
        /// </remarks>
        /// <param name="observations">
        ///   sejumlah set observations sequence
        /// </param>
        /// <param name="iterations">
        ///   jumlah loop yang akan dijalankan.
        /// </param>
        /// <param name="limit">
        ///   kesamaan likelihood antara dua iterasi.
        ///   learning akan berhenti jika kesamaan likelihood sudah mencapai limit.
        /// </param>
        /// <returns>
        ///   besar likelihood dari training.
        /// </returns>
        public Double baumWelch(int[][] observations, int iterations, double limit)
        {
            if (iterations == 0 && limit == 0)
                throw new ArgumentException("Iteration dan Limit Tidak Boleh 0 .");

            int loop = 1;
            bool stop = false;
            int N = this.hiddenState;
            int S = this.observeState;
            int I = observations.Length;

            // Initialization
            double[][, ,] epsilon = new double[I][, ,];
            double[][,] gamma = new double[I][,];

            for (int i = 0; i < I; i++)
            {
                int T = observations[i].Length;
                epsilon[i] = new double[N, N, T];
                gamma[i] = new double[N, T];
            }

            // Calculate initial model likelihood
            double likelihood = estimationLikelihood(observations);

            do
            {
                for (int i = 0; i < I; i++)
                {
                    double[,] alpha = forward(observations[i]);
                    double[,] beta = backward(observations[i]);

                    int T = observations[i].Length;
                    for (int t = 0; t < T; t++)
                    {
                        double s = 0;
                        for (int j = 0; j < N; j++)
                        {
                            double g = alpha[j, t] * beta[j, t];
                            gamma[i][j, t] = g;
                            s += g;
                        }


                        if (s != 0)
                            for (int j = 0; j < N; j++)
                                gamma[i][j, t] = gamma[i][j, t] / s;
                    }

                    for (int t = 0; t < T - 1; t++)
                    {

                        double s = 0;
                        for (int j = 0; j < N; j++)
                        {
                            for (int k = 0; k < N; k++)
                            {
                                double e = alpha[j, t] * A[j, k] * beta[k, t + 1] * B[k, observations[i][t + 1]];
                                epsilon[i][j, k, t] = e;
                                s += e;
                            }
                        }


                        for (int j = 0; j < N; j++)
                            for (int k = 0; k < N; k++)
                                epsilon[i][j, k, t] = epsilon[i][j, k, t] / s;
                    }
                }

                for (int j = 0; j < N; j++)
                {
                    double s = 0;
                    for (int i = 0; i < I; i++)
                        s += gamma[i][j, 0];

                    pi[j] = s / I;
                }

                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        double gammaSum = 0;
                        double epsilonSum = 0;

                        for (int i = 0; i < I; i++)
                        {
                            int T = observations[i].Length;
                            for (int t = 0; t < T - 1; t++)
                                gammaSum += gamma[i][j, t];

                            for (int t = 0; t < T - 1; t++)
                                epsilonSum += epsilon[i][j, k, t];
                        }

                        A[j, k] = epsilonSum / gammaSum;
                    }
                }

                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < S; k++)
                    {
                        double sum1 = 0;
                        double sum2 = 0;

                        for (int i = 0; i < I; i++)
                        {
                            int T = observations[i].Length;
                            for (int t = 0; t < T; t++)
                                if (k == observations[i][t])
                                    sum1 += gamma[i][j, t];

                            for (int t = 0; t < T; t++)
                                sum2 += gamma[i][j, t];
                        }

                        double result = sum1 / sum2;
                        if (result == 0.0)
                            result = 1e-10;
                        B[j, k] = result;
                    }
                }

                // Calculate new model likelihood
                double newlikelihood = estimationLikelihood(observations);

                double distance = Math.Abs(newlikelihood - likelihood);
                if (distance < limit * likelihood)
                    stop = true;

                likelihood = newlikelihood;
                loop++;

            } while (!stop && loop != iterations);
            threshold = likelihood;
            return likelihood;
        }
        
        /// <summary>
        ///   mengestimasi likelihood dari sejumlah set observation untuk suatu model.
        /// </summary>
        /// <param name="observations">
        ///   sejumlah set observation.
        /// </param>
        /// <returns>
        ///   besar likelihood.
        /// </returns>
        private double estimationLikelihood(int[][] observations)
        {
            double average = 0;
            for (int i = 0; i < observations.Length; i++)
                average += calculateProbability(observations[i]);
            return average / observations.Length;
        }
    }
}
