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
-------------
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


                                        
                                         
                                        
