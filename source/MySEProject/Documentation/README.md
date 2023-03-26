# ML 22/23 - 15 Approve Prediction for Multisequence Learning
### Software Engineering WiSe 22/23 Project

#### Team Name: Team_Matrix
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

The Other function is **PredictionExperiment** which returns an object of type Predictor. This is the trained data after running multiple cycles. The function calls the Run method from the [MultiSequenceLearning](https://github.com/faizmohammedkhan7/neocortexapi_Team_Matrix/blob/Team_Matrix/source/Samples/NeoCortexApiSample/MultisequenceLearning.cs) class which was implemented earlier and passes in the sequences and the encoder settings.
```csharp
             var predictor = newExperiment.Run(sequences, _encoderSettings);
```

Tasks to be done by each team member  -->
                                        To analyze existing code in MultisequenceLearning.cs & the method RunMultiSequenceLearningExperiment.
                                        To learn how sequence learning and prediction works.
                                        Play around with the ML.NET library and get familiar.

Added new method for testing sequence : RunMultiSequenceLearningExperiment()

21.01.23: Analyzed the Run() method and RunExperiment() method in MultisequenceLearning.cs file. Next step would be test them with string sequences.

//Implemented Code for reading the learning sequences and testing sequences from .txt file the code is implemented in RunPredictionMultiSequenceExperiment() method.

* And the Sequences are in source\MySEProject as trainingSequences.txt & testingData.txt

**RunPredictionMultiSequenceExperiment() method is called from the program.cs.

The method has an instantiated object project of type Project which is a class implemented in MultiSequencePrediction.

**The methods for reading test sequences and learning sequences are defined in MultiSequenceLearningProject.cs in the Project class.

The PredictionExperiment() method calls the Run() method from the MultiSequenceLearning class.

The Run() method takes in seqences and the encoderSettings as arguements.

** Executed few lines of code which helps to run the program successfully.

** Started Preparing Power Point Presentation , Prepared Few Slides.


                                        
                                         
                                        
