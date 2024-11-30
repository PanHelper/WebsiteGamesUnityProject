This is the README File for each of the games embedded into the PanHelper website.
Each of the games was created within their own scene of the same Unity Project.
Both games use Unity's New Input System to allow the player to close the start screen by pressing with either the left mouse button or any keyboard button.

Greek Trivia:
The game "Fact or Fiction? What Actually Happens in PanHellenic Life and What was Made Up for Animal House?" was programmed entirely in the GreekTrivia scene. Upon downloading the Unity project, this scene can be accessed from the Project tab in Assets/Scenes. 
The entire game was created within the Unity Editor in the UI Canvas, and every mechanic is controlled either by default Unity scripts that operate in the background, or by the GreekTriviaGameManager.cs script attached to the GameManager object. 
The questions are kept track of in an Array of Strings, named [insert name here], whose size can be adjusted from the Unity Editor. The answers to the questions are stored in an Array of Booleans, named [insert name here], which can also be updated from the Editor, but must be the same length as the Array of questions.
The number of questions that have been answered is stored in the variable [numAnswered]. Every time a question is answered the function [insert function name here] is called. This function determines whether to conclude the game or provide the next question. 
Once 5 questions have been answered, the score is calculated and the results are revealed. 
All of the art in the game was drawn using IbisPaint. 

MonstersU Sorting:
The game "MonstersU Sorority Sorting" was programmed entirely in the [insert scene here] scene. Upon downloading the Unity project, this scene can be accessed from the Project tab in Assets/Scenes.
The entire game was created within the Unity Editor in the UI Canvas, and every mechanic is controlled either by default Unity scripts that operate in the background, or by the [MonstersU]GameManager.cs script attached to the GameManager object.
The questions are kept track of in an Array of Strings, named [insert name here], whose size can be adjusted from the Unity Editor. The answers to the questions are stored in [how the questions are stored] …
