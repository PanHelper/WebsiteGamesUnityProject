This is the README File for each of the games embedded into the PanHelper website.
Each of the games was created within their own scene of the same Unity Project.
Both games use Unity's New Input System to allow the player to close the start screen by
 pressing with either the left mouse button or any keyboard button.

Greek Trivia:
The game "Fact or Fiction? What Actually Happens in PanHellenic Life and What was Made Up
 for Animal House?" was programmed entirely in the GreekTrivia scene. Upon downloading
 the Unity project, this scene can be accessed from the Project tab in Assets/Scenes. 
The entire game was created within the Unity Editor in the UI Canvas, and every mechanic
 is controlled either by default Unity scripts that operate in the background, or by the
 GreekTriviaGameManager.cs script attached to the Game Manager object. 
The questions are kept track of in a List of Strings, named questions, whose size can be
 adjusted from the Unity Editor. The answers to the questions are stored in a List of
 Booleans, named answers, which can also be updated from the Editor, but must be the same
 length as the List of questions.
The number of questions that have been answered is stored in the variable questsDone.
 Every time a question is answered the function Answer() is called. This function 
 determines whether to conclude the game or provide the next question. 
Once 5 questions have been answered, the score is calculated and the results are revealed. 
All of the art in the game was drawn using IbisPaint. 

MonstersU Sorting:
The game "MonstersU Sorority Sorting" was programmed entirely in the MonstersU scene.
 Upon downloading the Unity project, this scene can be accessed from the Project tab in 
 Assets/Scenes.
The entire game was created within the Unity Editor in the UI Canvas, and every mechanic
 is controlled either by default Unity scripts that operate in the background, or by the
 MonstersUGameManager.cs script attached to the Game Manager object.
The questions are kept track of in a List of Strings, named questions, whose size can be
 adjusted from the Unity Editor. The answers to the questions are stored in a 2D Array of
 Strings, named answers. 2D Arrays cannot be edited in the Unity Editor, so answers must
 be hardcoded in the InitializeAnswers() function. The outer-length is the number of 
 questions, and the inner-length is the number of possible answers, which is always 3.
Each answer button has a designated value to pass into the Answer() function, which
 correlates to a specific MonstersU Sorority. The button pressed determines which of 3
 integer values is incremented, with the highest of these 3 at the conclusion of the
 questions being answered determining which Sorority the player was sorted into. To help
 eliminate obvious patterns, the 3 answer buttons are randomly assigned to one of 3
 predetermined locations in the UI Canvas (1 button per location) before every question.
All of the art in the game was drawn using IbisPaint.
The images displayed in on the Results Screen were taken from the following website: 
 https://screenrant.com/all-monsters-university-frats-sororities-ranked/