using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
    public class Multisequence
    {
        private Dictionary<string, object> _encoderSettings;
        private string _trainSequencePath;
        public double encoder_max;
        // Getters/ Setters for above instance variables.
        public Dictionary<string, object> EncoderSettings { get { return _encoderSettings; } set { _encoderSettings = value; } }
        public string TrainSequencePath { get { return _trainSequencePath; } set { _trainSequencePath = value; } }

        public Predictor PredictionExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            
            //Code for reading the learning sequences from .txt file. The file has n rows which have numbers seperated by commas.
            sequences = ReadSequences(_trainSequencePath);
            //sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 8.0, 1.0, 2.0, 9.0, 10.0, 7.0, 11.00 }));

            MultiSequenceLearning newExperiment = new MultiSequenceLearning();
            _encoderSettings["MaxVal"]= sequences.Values.SelectMany(list => list).Max(); //sets the max value based on the max value from the sequences
            var predictor = newExperiment.Run(sequences, _encoderSettings);
            return predictor;
        }

        ///<summary>
        ///This method is for reading the training sequences for the model from a .txt file. The method returns a dictionary of sequences of type List<double>.
        ///</summary>
        public Dictionary<string, List<double>> ReadSequences(string sequencePath)
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
    }
}
