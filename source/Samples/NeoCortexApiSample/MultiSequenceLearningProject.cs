using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi;
using NeoCortexApi.Classifiers;
using NeoCortexApi.Encoders;
using NeoCortexApi.Entities;
using NeoCortexApi.Network;
using NeoCortexApiSample;

namespace MultiSequencePrediction
{
    class Project
    {
        private Dictionary<string, object> _encoderSettings;
        private string _trainSequencePath;
        public Dictionary<string, object> EncoderSettings { get { return _encoderSettings; } set { _encoderSettings = value; } }
        public string TrainSequencePath { get { return _trainSequencePath; } set { _trainSequencePath = value; } }

        public Predictor PredictionExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            /*Code for reading the learning sequences from .txt file. The file has n rows which have numbers seperated by commas.*/
            sequences = readSequences(_trainSequencePath);

            MultiSequenceLearning newExperiment = new MultiSequenceLearning();
            
            var predictor = newExperiment.Run(sequences, _encoderSettings);
            return predictor;
        }

        /*This method is for reading the training sequences for the model from a .txt file. The method returns a dictionary of sequences of type List<double>.*/
        public Dictionary<string, List<double>> readSequences(string sequencePath)
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            var sequence = new List<double>();
            using (StreamReader reader = new(sequencePath))
            {
                int count = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Console.WriteLine(line);

                    foreach (var value in values)
                    {
                        sequence.Add(Convert.ToDouble(value));
                     
                    }
                    string seqName = "seq" + count;
                    sequences.Add(seqName, sequence);
                    count++;

                }
                return sequences;
            }
        }

        /*This method is for reading the testing sequences for the model from a .txt file. The method returns a list of sequences of type List<double>.*/
        public List<List<double>> readTestSequences(string path)
        {
            var testSequences = new List<List<double>>();
            var testList = new List<double>();


            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Console.WriteLine(line);

                    foreach (var value in values)
                    {
                        testList.Add(Convert.ToDouble(value));
                    }
                    testSequences.Add(testList);
                }
            }
            return testSequences;
        }
    }
}
