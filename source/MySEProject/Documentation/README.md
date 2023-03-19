# ML 22/23 - 15 Approve Prediction for Multisequence Learning
### Software Engineering WiSe 22/23 Project

#### Team Name: Team_Matrix
###### Team Members: 
######            *  Faiz Mohammed Khan
######            *  Shiva Kumar Biru
######            *  Mohan Sai Ram Sarnala

### Project Description
=============

1. Objective
-------------
Multisequence Learning is the process of learning many sequences like:
1784, 4788, 9037, …

This can be used in industrial solutions to solve many problems. For example, the sequence ABCDDEE can be an array of music notes, and HTM can be used to recognize songs. 
For example being able to predict the next element of the sequence.

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


                                        
                                         
                                        
