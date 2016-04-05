using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMM
{
    public class HmmProblem
    {
        public String Label;
        public int[] Observation;

        public HmmProblem() { }

        public HmmProblem(String label,int[] observation) {
            this.Label = label;
            this.Observation = observation;
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
    }
}
