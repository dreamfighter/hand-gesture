/*
 * SVM.NET Library
 * Copyright (C) 2008 Matthew Johnson
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace SVM
{
    /// <summary>
    /// This class contains routines which perform parameter selection for a model which uses C-SVC and
    /// an RBF kernel.
    /// </summary>
    public static class ParameterSelection
    {
        /// <summary>
        /// Default number of times to divide the data.
        /// </summary>
        public const int NFOLD = 5;
        /// <summary>
        /// Default minimum power of 2 for the C value (-5)
        /// </summary>
        public const int MIN_C = -5;
        /// <summary>
        /// Default maximum power of 2 for the C value (15)
        /// </summary>
        public const int MAX_C = 15;
        /// <summary>
        /// Default power iteration step for the C value (2)
        /// </summary>
        public const int C_STEP = 2;
        /// <summary>
        /// Default minimum power of 2 for the Gamma value (-15)
        /// </summary>
        public const int MIN_G = -15;
        /// <summary>
        /// Default maximum power of 2 for the Gamma Value (3)
        /// </summary>
        public const int MAX_G = 3;
        /// <summary>
        /// Default power iteration step for the Gamma value (2)
        /// </summary>
        public const int G_STEP = 2;

        /// <summary>
        /// Returns a logarithmic list of values from minimum power of 2 to the maximum power of 2 using the provided iteration size.
        /// </summary>
        /// <param name="minPower">The minimum power of 2</param>
        /// <param name="maxPower">The maximum power of 2</param>
        /// <param name="iteration">The iteration size to use in powers</param>
        /// <returns></returns>
        public static List<double> GetList(double minPower, double maxPower, double iteration)
        {
            List<double> list = new List<double>();
            for (double d = minPower; d <= maxPower; d += iteration)
                list.Add(Math.Pow(2, d));
            return list;
        }
        /*
        public static List<double> GetList(double c)
        {
            double substract=0;
            double add = 0;

            if (c < 0)
                substract = 0.25;
            else
                substract = -0.25;
            List<double> list = new List<double>();
            for (double d = minPower; d <= maxPower; d += iteration)
                list.Add(Math.Pow(2, d));
            return list;
        }
        */
        /// <summary>
        /// Performs a Grid parameter selection, trying all possible combinations of the two lists and returning the
        /// combination which performed best.  The default ranges of C and Gamma values are used.  Use this method if there is no validation data available, and it will
        /// divide it 5 times to allow 5-fold validation (training on 4/5 and validating on 1/5, 5 times).
        /// </summary>
        /// <param name="problem">The training data</param>
        /// <param name="parameters">The parameters to use when optimizing</param>
        /// <param name="outputFile">Output file for the parameter results.</param>
        /// <param name="C">The optimal C value will be put into this variable</param>
        /// <param name="Gamma">The optimal Gamma value will be put into this variable</param>
        public static void Grid(
            Problem problem,
            Parameter parameters,
            string outputFile,
            out double C,
            out double Gamma)
        {
            Grid(problem, parameters, GetList(MIN_C, MAX_C, C_STEP), GetList(MIN_G, MAX_G, G_STEP), outputFile, NFOLD, out C, out Gamma);
        }
        /// <summary>
        /// Performs a Grid parameter selection, trying all possible combinations of the two lists and returning the
        /// combination which performed best.  Use this method if there is no validation data available, and it will
        /// divide it 5 times to allow 5-fold validation (training on 4/5 and validating on 1/5, 5 times).
        /// </summary>
        /// <param name="problem">The training data</param>
        /// <param name="parameters">The parameters to use when optimizing</param>
        /// <param name="CValues">The set of C values to use</param>
        /// <param name="GammaValues">The set of Gamma values to use</param>
        /// <param name="outputFile">Output file for the parameter results.</param>
        /// <param name="C">The optimal C value will be put into this variable</param>
        /// <param name="Gamma">The optimal Gamma value will be put into this variable</param>
        public static void Grid(
            Problem problem,
            Parameter parameters,
            List<double> CValues,
            List<double> GammaValues,
            string outputFile,
            out double C,
            out double Gamma)
        {
            Grid(problem, parameters, CValues, GammaValues, outputFile, NFOLD, out C, out Gamma);
        }
        /// <summary>
        /// Performs a Grid parameter selection, trying all possible combinations of the two lists and returning the
        /// combination which performed best.  Use this method if validation data isn't available, as it will
        /// divide the training data and train on a portion of it and test on the rest.
        /// </summary>
        /// <param name="problem">The training data</param>
        /// <param name="parameters">The parameters to use when optimizing</param>
        /// <param name="CValues">The set of C values to use</param>
        /// <param name="GammaValues">The set of Gamma values to use</param>
        /// <param name="outputFile">Output file for the parameter results.</param>
        /// <param name="nrfold">The number of times the data should be divided for validation</param>
        /// <param name="C">The optimal C value will be placed in this variable</param>
        /// <param name="Gamma">The optimal Gamma value will be placed in this variable</param>
        public static void Grid(
            Problem problem,
            Parameter parameters,
            List<double> CValues, 
            List<double> GammaValues, 
            string outputFile,
            int nrfold,
            out double C,
            out double Gamma)
        {
            C = 0;
            Gamma = 0;
            double crossValidation = double.MinValue;            
            Problem valid = Problem.Read("model/fix-testing.train");
            StreamWriter output = null;
            if(outputFile != null)
                output = new StreamWriter(outputFile);
            for(int i=0; i<CValues.Count; i++)
                for (int j = 0; j < GammaValues.Count; j++)
                {
                    parameters.C = CValues[i];
                    parameters.Gamma = GammaValues[j];

                    double test = Training.PerformCrossValidation(problem, parameters, nrfold);

                    Model model = Training.Train(problem, parameters);

                    double test1 = Prediction.Predict(valid, "tmp1.txt", model, false);

                    Console.Write("{0} {1} {2} {3}", parameters.C, parameters.Gamma, test,test1);
                    if (output != null)
                        output.WriteLine("{0} {1} {2} {3}", parameters.C, parameters.Gamma, test, test1);
                    if (test1 > crossValidation)
                    {
                        C = parameters.C;
                        Gamma = parameters.Gamma;
                        crossValidation = test1;
                        Console.WriteLine(" New Maximum!");
                    }
                    else Console.WriteLine();
                }
            if(output != null)
                output.Close();
        }
        /// <summary>
        /// Performs a Grid parameter selection, trying all possible combinations of the two lists and returning the
        /// combination which performed best.  Uses the default values of C and Gamma.
        /// </summary>
        /// <param name="problem">The training data</param>
        /// <param name="validation">The validation data</param>
        /// <param name="parameters">The parameters to use when optimizing</param>
        /// <param name="outputFile">The output file for the parameter results</param>
        /// <param name="C">The optimal C value will be placed in this variable</param>
        /// <param name="Gamma">The optimal Gamma value will be placed in this variable</param>
        public static void Grid(
            Problem problem,
            Problem validation,
            Parameter parameters,
            string outputFile,
            out double C,
            out double Gamma)
        {
            //Grid(problem, validation, parameters, GetList(MIN_C, MAX_C, C_STEP), GetList(MIN_G, MAX_G, G_STEP), outputFile, out C, out Gamma);
            Grid(problem, validation, parameters, GetList(-3, 3, 0.25), GetList(MIN_G, MAX_G, 2), outputFile, out C, out Gamma);
        }

        /// <summary>
        /// Performs a Grid parameter selection, trying all possible combinations of the two lists and returning the
        /// combination which performed best.
        /// </summary>
        /// <param name="problem">The training data</param>
        /// <param name="validation">The validation data</param>
        /// <param name="parameters">The parameters to use when optimizing</param>
        /// <param name="CValues">The C values to use</param>
        /// <param name="GammaValues">The Gamma values to use</param>
        /// <param name="outputFile">The output file for the parameter results</param>
        /// <param name="C">The optimal C value will be placed in this variable</param>
        /// <param name="Gamma">The optimal Gamma value will be placed in this variable</param>
        public static void Grid(
            Problem problem,
            Problem validation,
            Parameter parameters,
            List<double> CValues,
            List<double> GammaValues,
            string outputFile,
            out double C,
            out double Gamma)
        {
            C = 0;
            Gamma = 0;
            double maxScore = double.MinValue;
            StreamWriter output = null;
            if(outputFile != null)
                output = new StreamWriter(outputFile);
            for (int i = 0; i < CValues.Count; i++)
            {
                double temp_g = 0;
                double temp_training = 0;
                double temp_testing = 0;
                for (int j = 0; j < GammaValues.Count; j++)
                {
                    //parameters.KernelType = KernelType.RBF;
                    //parameters.C = CValues[i];
                    parameters.C = CValues[i];
                    //parameters.C = i;
                    parameters.Gamma = GammaValues[j];
                    //parameters.Gamma = 1.0;
                    Model model = Training.Train(problem, parameters);
                    double test = Prediction.Predict(validation, "tmp.txt", model, false);
                    double test1 = Prediction.Predict(problem, "tmp1.txt", model, false);
                    bool akurasi = false;
                    Console.Write("{0} {1} {2} {3}", parameters.C, parameters.Gamma, test1 * 100, test * 100);
                    if (test > maxScore)
                    {
                        Model.Write("model/" + parameters.KernelType.ToString() + "[c" + Math.Log(CValues[i], 2) + "g" + Math.Log(GammaValues[j], 2) + "].mdl", model);
                        akurasi = true;
                        C = parameters.C;
                        Gamma = parameters.Gamma;
                        maxScore = test;
                        Console.WriteLine(" New Maximum!");

                    }
                    if (temp_testing < test)
                    {
                        temp_testing = test;
                        temp_training = test1;
                        temp_g = parameters.Gamma;
                    }
                    Console.WriteLine();
                }
                if (output != null)
                    output.WriteLine("{0};{1};{2};{3}", Math.Log(CValues[i], 2), Math.Log(temp_g,2), temp_training * 100, temp_testing * 100);

            }
            if(output != null)
                output.Close();
        }
    }
}
