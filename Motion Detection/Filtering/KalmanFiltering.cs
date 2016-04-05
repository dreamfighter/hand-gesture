using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util.TypeEnum;
using System.Drawing;

namespace Motion_Detection_v2.Filtering
{
    public class KalmanFiltering
    {
        private List<PointF> mousePoints;
        private List<PointF> kalmanPoints;
        private Kalman kal;
        private SyntheticData syntheticData;
        public float px;
        public float py;
        public float cx;
        public float cy;

        public KalmanFiltering()
        {
            mousePoints = new List<PointF>();
            kalmanPoints = new List<PointF>();
            kal = new Kalman(4, 2, 0);
            syntheticData = new SyntheticData();
            Matrix<float> state = new Matrix<float>(new float[]
                {
                    0.0f, 0.0f, 0.0f, 0.0f
                });
            kal.CorrectedState = state;
            kal.TransitionMatrix = syntheticData.transitionMatrix;
            kal.MeasurementNoiseCovariance = syntheticData.measurementNoise;
            kal.ProcessNoiseCovariance = syntheticData.processNoise;
            kal.ErrorCovariancePost = syntheticData.errorCovariancePost;
            kal.MeasurementMatrix = syntheticData.measurementMatrix;
        }

        public PointF[] filterPoints(PointF pt)
        {
            syntheticData.state[0, 0] = pt.X;
            syntheticData.state[1, 0] = pt.Y;
            Matrix<float> prediction = kal.Predict();
            PointF predictPoint = new PointF(prediction[0, 0], prediction[1, 0]);
            PointF measurePoint = new PointF(syntheticData.GetMeasurement()[0, 0],
                syntheticData.GetMeasurement()[1, 0]);
            Matrix<float> estimated = kal.Correct(syntheticData.GetMeasurement());
            PointF estimatedPoint = new PointF(estimated[0, 0], estimated[1, 0]);
            syntheticData.GoToNextState();
            PointF[] results = new PointF[2];
            results[0] = predictPoint;
            results[1] = estimatedPoint;
            px = predictPoint.X;
            py = predictPoint.Y;
            cx = estimatedPoint.X;
            cy = estimatedPoint.Y;
            return results;
        }

        public class SyntheticData
        {
            public Matrix<float> state;
            public Matrix<float> transitionMatrix;
            public Matrix<float> measurementMatrix;
            public Matrix<float> processNoise;
            public Matrix<float> measurementNoise;
            public Matrix<float> errorCovariancePost;

            public SyntheticData()
            {
                state = new Matrix<float>(4, 1);
                state[0, 0] = 0f; // x-pos
                state[1, 0] = 0f; // y-pos
                state[2, 0] = 0f; // x-velocity
                state[3, 0] = 0f; // y-velocity
                transitionMatrix = new Matrix<float>(new float[,]
                    {
                        {1, 0, 1, 0},  // x-pos, y-pos, x-velocity, y-velocity
                        {0, 1, 0, 1},
                        {0, 0, 1, 0},
                        {0, 0, 0, 1}
                    });
                measurementMatrix = new Matrix<float>(new float[,]
                    {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 }
                    });
                measurementMatrix.SetIdentity();
                processNoise = new Matrix<float>(4, 4); //Linked to the size of the transition matrix
                processNoise.SetIdentity(new MCvScalar(1.0e-4)); //The smaller the value the more resistance to noise 
                measurementNoise = new Matrix<float>(2, 2); //Fixed accordiong to input data 
                measurementNoise.SetIdentity(new MCvScalar(1.0e-1));
                errorCovariancePost = new Matrix<float>(4, 4); //Linked to the size of the transition matrix
                errorCovariancePost.SetIdentity();
            }

            public Matrix<float> GetMeasurement()
            {
                Matrix<float> measurementNoise = new Matrix<float>(2, 1);
                measurementNoise.SetRandNormal(new MCvScalar(), new MCvScalar(Math.Sqrt(measurementNoise[0, 0])));
                return measurementMatrix * state + measurementNoise;
            }

            public void GoToNextState()
            {
                Matrix<float> processNoise = new Matrix<float>(4, 1);
                processNoise.SetRandNormal(new MCvScalar(), new MCvScalar(processNoise[0, 0]));
                state = transitionMatrix * state + processNoise;
            }
        }
    }
}
