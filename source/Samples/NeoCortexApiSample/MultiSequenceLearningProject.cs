using System;
using System.Collections.Generic;
using System.IO;
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
        // Getters/ Setters for above instance variables.
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
                int count = 1; //Defining the count for sequence Numbering
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',', ' ');
                        foreach (var value in values)
                        {
                            sequence.Add(Convert.ToDouble(value));
                        }
                        string seqName = "seq" + count;
                        sequences.Add(seqName, sequence);
                        count++;
                        
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error reading file: " + e.Message);
                        
                    }
                    

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
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',', ' ');

                        foreach (var value in values)
                        {
                            testList.Add(Convert.ToDouble(value));
                        }
                        testSequences.Add(testList);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error reading test file: " + e.Message);
                    }
                }
            }
            return testSequences;
        }
    }
}
