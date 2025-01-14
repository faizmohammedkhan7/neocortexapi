﻿using NeoCortexApi;
using NeoCortexApi.Encoders;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using static NeoCortexApiSample.MultiSequenceLearning;
using MultiSequencePrediction;
using System.ComponentModel;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace NeoCortexApiSample
{
    class Program
    {
        /// <summary>
        /// This sample shows a typical experiment code for SP and TM.
        /// You must start this code in debugger to follow the trace.
        /// and TM.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SpatialPatternLearning experiment = new SpatialPatternLearning();
            //experiment.Run();

            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SequenceLearning experiment = new SequenceLearning();
            //experiment.Run();

            //RunMultiSimpleSequenceLearningExperiment();
            //RunMultiSequenceLearningExperiment();

            // string path = ".//.//" + System.IO.Directory.GetCurrent‌​Directory();
            string sequencePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\MySEProject/trainingSequences.txt"));

            string testSequencePath = Path.GetFullPath(Path.Combine(Directory.GetCurrent‌​Directory(), @"..\..\..\..\..\MySEProject/testingData.txt"));
            RunPredictionMultiSequenceExperiment(sequencePath, testSequencePath);
        }

        private static void RunMultiSimpleSequenceLearningExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            sequences.Add("S1", new List<double>(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, }));
            sequences.Add("S2", new List<double>(new double[] { 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0 }));

            //
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);         
        }


        /// <summary>
        /// This example demonstrates how to learn two sequences and how to use the prediction mechanism.
        /// First, two sequences are learned.
        /// Second, three short sequences with three elements each are created und used for prediction. The predictor used by experiment privides to the HTM every element of every predicting sequence.
        /// The predictor tries to predict the next element.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            //sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 0.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 4.0, 3.0, 7.0, 1.0, 9.0, 12.0, 11.0, 12.0, 13.0, 14.0, 11.0, 12.0, 14.0, 5.0, 7.0, 6.0, 9.0, 3.0, 4.0, 3.0, 4.0, 3.0, 4.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 0.8, 2.0, 0.0, 3.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 2.0, 7.0, 1.0, 9.0, 11.0, 11.0, 10.0, 13.0, 14.0, 11.0, 7.0, 6.0, 5.0, 7.0, 6.0, 5.0, 3.0, 2.0, 3.0, 4.0, 3.0, 4.0 }));

            sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 }));
            sequences.Add("S2", new List<double>(new double[] { 8.0, 1.0, 2.0, 9.0, 10.0, 7.0, 11.00 }));

            //
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);

            //
            // These list are used to see how the prediction works.
            // Predictor is traversing the list element by element. 
            // By providing more elements to the prediction, the predictor delivers more precise result.
            var list1 = new double[] { 1.0, 2.0, 3.0, 4.0, 2.0, 5.0, };
            var list2 = new double[] { 2.0, 3.0, 4.0 };
            var list3 = new double[] { 8.0, 1.0, 2.0 };

            //predictor.Reset();
            //PredictNextElement(predictor, list1);

            //predictor.Reset();
            //PredictNextElement(predictor, list2);

            //predictor.Reset();
            //PredictNextElement(predictor, list3);
        }

        ///------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// This is the method which will contain the implementation by our team for the Project. It takes the paths for sequences to be trained and
        /// sequences to be tested as arguements.
        /// Team_Matrix
        /// </summary>
        private static void RunPredictionMultiSequenceExperiment(string sequencePath, string testSequencePath)
        {

            // Instantiating project class from MultiSequencePrediction
            Multisequence project = new Multisequence();

            
            Dictionary<string, List<double>> testSequences = project.ReadSequences(testSequencePath);


            // Defining the encoder settings for the experiment
            Dictionary<string, object> encoderSettings = new Dictionary<string, object>()
            {
                { "W", 15},
                { "N", 100},
                { "Radius", -1.0},
                { "MinVal", 0.0},
                { "Periodic", false},
                { "Name", "scalar"},
                { "ClipInput", false},
                { "MaxVal", 99.0} //Only double value, the code in the PredictionExperiment method automatically sets the maximum value from the sequences
            };
            project.TrainSequencePath = sequencePath;
            project.EncoderSettings = encoderSettings;

            // Getting the Predictor Object feeding into prediction next element for every sequence in the test sequence file
            Predictor predictor = project.PredictionExperiment();
            for (int i = 0; i< testSequences.Count; i++)
            {
                predictor.Reset();
                Debug.WriteLine("------------------------------");
                Debug.WriteLine($"Predicting for Sequence {testSequences.ElementAt(i).Key}");
                PredictNextElement(predictor, testSequences.ElementAt(i).Value);
            }  
        }


        private static void PredictNextElement(Predictor predictor, List<double> list)
        {
            Debug.WriteLine("------------------------------");
            DateTime now = DateTime.Now;
            string filename = $"result-{now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}.txt";
            string result_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\MySEProject\", filename);
            StreamWriter writer = new StreamWriter(result_path);
            int matchCount = 0;
            int totalCount = 0;
            foreach (var item in list)
            {
                var res = predictor.Predict(item);

                if (res.Count > 0)
                {
                    foreach (var pred in res)
                    {
                        Debug.WriteLine($"{pred.PredictedInput} - {pred.Similarity} %");
                    }

                    var tokens = res.First().PredictedInput;
                    var tokens2 = res.First().PredictedInput.Split('-');
                    var similarity = res.First().Similarity;
                    Debug.WriteLine($"Predicted Sequence: {tokens}, predicted next element {tokens2.Last()} with similarity of {similarity} %");
                    writer.WriteLine($"Predicted Sequence: {tokens}, predicted next element {tokens2.Last()} with similarity of {similarity} %");
                    writer.WriteLine("--------------------------------------");
                    matchCount += 1;

                }
                else
                    Debug.WriteLine("Nothing predicted :(");
                totalCount += 1;
            }
            var accuracy = (matchCount*100) / totalCount;
            writer.WriteLine($"Predicted Sequence with accuracy of {accuracy} %");
            writer.Close();
            Debug.WriteLine("------------------------------");
            Console.WriteLine("------------------------------");
        }
    }
}
