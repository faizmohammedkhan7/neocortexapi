# ML 22/23 - 15 Approve Prediction for Multisequence Learning
### Software Engineering WiSe 22/23 Project

#### Team Name: Team_Matrix
### [Team Repository](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/tree/Team_Matrix/source/MySEProject)


**Project Description**
=============

1.Objective
-------------
- To Analyze existing code for learning of sequences and understanding the prediction approach.
- To Demonstrate the learning of sequences and predicting the next element of a sample sequence.
- To Read the sequences from a file and generate the result file at the end of prediction.
- To automate the encoder settings based on the largest value of the sequences.


2.Approach (Reading & Prediction)
-------------
We introduced a new method [RunPredictionMultiSequenceExperiment](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/a2d17054d50cc663905b3666276f1692a6659a29/source/Samples/NeoCortexApiSample/Program.cs#L107) which creates an instance of class [MultiSequence](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs).
- The Method takes in two arguments: Path for Training Sequence File & Path for Test Sequences File.
- This method is responsible for providing necessary data to the Prediction method such as encoder settings and the file paths. 
- The Max Value of the encoder settings is automatically set based on the largest value in the sequence file. This can be seen in the [MultiSequencePrediction](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs) class.

```csharp
             _encoderSettings["MaxVal"]= sequences.Values.SelectMany(list => list).Max();
```

Our class [MultiSequencePrediction](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs) has two main functions. 
- One method [**ReadSequences**](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/a2d17054d50cc663905b3666276f1692a6659a29/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs#L43) is to read the sequences from the file. The sequences could be training sequences or testing sequences.
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

- The Other function is [**PredictionExperiment**](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/a2d17054d50cc663905b3666276f1692a6659a29/source/Samples/NeoCortexApiSample/MultiSequenceLearningProject.cs#L27) which returns an object of type Predictor. This is the trained data after running multiple cycles. 
The function calls the [Run](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/a2d17054d50cc663905b3666276f1692a6659a29/source/Samples/NeoCortexApiSample/MultisequenceLearning.cs#L25) method from the [MultiSequenceLearning](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultisequenceLearning.cs) class which was implemented earlier and passes in the sequences and the encoder settings.

```csharp
             var predictor = newExperiment.Run(sequences, _encoderSettings);
```

 Once the prediction is done and the method returns a Predictor Object. This object along with a test sequence is passed to the [**PredictNextElement**](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/a2d17054d50cc663905b3666276f1692a6659a29/source/Samples/NeoCortexApiSample/Program.cs#L144) method in [Program.cs](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/Program.cs) where it iterates over each element in the test sequence and passes it in the **Predict** method of the [Predictor](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/NeoCortexApi/Predictor.cs) class.
Sample Test Sequence:
```csharp
             sequences.Add("T1", new List<double>(new double[] { 0.0, 1.0, 2.0, 3.0 }));
```

 It then predicts the next element based on the training sequences provided to it earlier and returns the sequence where it matches along with its accuracy. This data is then written to a result file at the location Here: neocortexapi/tree/Team_Matrix/source/MySEProject.
The result file is named based on the current timestamp.

The **PredictNextElement** method has the following changes:
- Added Functionality for showing accuracy of the prediction with respect to the sequence it is predicting from.
- Added functionality to write the results to a file and the name of the file is based on the current timestamp.

```csharp
            DateTime now = DateTime.Now;
            string filename = $"result-{now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}.txt";
            string result_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\MySEProject\", filename);
            StreamWriter writer = new StreamWriter(result_path);
            writer.WriteLine($"Predicted Sequence: {tokens}, predicted next element {tokens2.Last()} with Accuracy of {accuracy} %");
            writer.Close();
```

```csharp
             var accuracy = (matchCount*100) / totalCount;
            writer.WriteLine($"Predicted Sequence with accuracy of {accuracy} %");
```

Sample Result:
![image](https://user-images.githubusercontent.com/59792795/227899965-bb71ba6b-fda1-4627-a9bf-acb5adb2af30.png)
![image](https://user-images.githubusercontent.com/59792795/227914196-95c55a83-4abc-4e62-81d5-65643f26fd34.png)




                                        
                                         
                                        
