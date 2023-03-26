# ML 22/23 - 15 Approve Prediction for Multisequence Learning
### Software Engineering WiSe 22/23 Project

#### Team Name: Team_Matrix
### [Team Repository](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/tree/Team_Matrix/source/MySEProject)
**Team Members:**
- Faiz Mohammed Khan (faiz.khan@stud.fra-uas.de)
- Shiva Kumar Biru (shiva.biru@stud.fra-uas.de)
- Mohan Sai Ram Sarnala (mohan.sarnala@stud.fra-uas.de)


**Project Description**
=============

1.Objective
-------------
- To Demonstrate the learning of sequences and predicting the next element of a sample sequence.
- To Read the sequences from a file and generate the result file at the end of prediction.
- To automate the encoder settings based on the largest value of the sequences.

For Example reading sequences like 5,6,1,3,1.... etc from a .txt file and when given a sequence like 5,6,1 as an input should predict the next element as 3.

2.Approach (Reading & Prediction)
-------------
We introduced a new method [RunPredictionMultiSequenceExperiment](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/Program.cs) which creates an instance of class [MultiSequencePrediction](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs) this method is responsible for providing necesarry data to the Prediction method such as encoder settings. The Max Vlaue of the encoder settings is automatically set based on the largest value in the sequence file. This can be seen in the [MultiSequencePrediction](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs) class.
```csharp
             _encoderSettings["MaxVal"]= sequences.Values.SelectMany(list => list).Max();
```

Our class [MultiSequencePrediction](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs) has two main functions. One method **ReadSequences** is to read the sequences from the file. The sequences could be training sequences or testing sequences.
The sequences are automatically assigned a key/name.
```csharp
             int count = 1; //Defining the count for sequence Numbering
                while (!reader.EndOfStream)
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
```
 Sample sequences
 ```csharp
            sequences.Add("Seq1", new List<double>(new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 }));
            sequences.Add("Seq2", new List<double>(new double[] { 8.0, 1.0, 2.0, 9.0, 10.0, 7.0, 11.00 }));
```

The Other function is **PredictionExperiment** which returns an object of type Predictor. This is the trained data after running multiple cycles. The function calls the Run method from the [MultiSequenceLearning](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultisequenceLearning.cs) class which was implemented earlier and passes in the sequences and the encoder settings.
```csharp
             var predictor = newExperiment.Run(sequences, _encoderSettings);
```
 Once the prediction is done and the method returns a Predictor Object. This object along with a test sequence is passed to the **PredictNextElement** method in [Program.cs](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/Program.cs) where it iterates over each element in the test sequence and passes it in the **Predict** method of the [Predictor](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/NeoCortexApi/Predictor.cs) class.
 It then predicts the next element based on the training sequences provided to it earlier and returns the sequence where it matches along with its accuracy. This data is then written to a result file at the location Here: neocortexapi/tree/Team_Matrix/source/MySEProject.
The result file is named based on the current timestamp.
```csharp
            DateTime now = DateTime.Now;
            string filename = $"result-{now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}.txt";
            string result_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\MySEProject\", filename);
            StreamWriter writer = new StreamWriter(result_path);
            writer.WriteLine($"Predicted Sequence: {tokens}, predicted next element {tokens2.Last()} with Accuracy of {accuracy} %");
            writer.Close();
```




                                        
                                         
                                        
