
# Mr. Noteworthy's School of Classical Melodies
# A Serious Game for Interactive Ear Training - Game Outline
---

## Controls
The game features a very simple control scheme. The user needs to identify the missing notes of a given melody and inputs their answer by pressing keys on a digital piano using mouse clicks. There are no other interactions outside of clicking virtual buttons using the mouse. These controls are clearly explained in the tutorial level of the game. 

![alt text](https://github.com/Gubbings/SeriousGamesDev/blob/master/imgs/piano.png "Piano Controls")

## Game Mechanics
The game presents the user with a melody of 8 notes. One or more of these notes will be missing on the staff. The user's goal is to identify these notes using the digital piano that is provided for them. The user has 3 attempts per question. An attempt is defined as submitting all of the missing notes. If the submitted note is correct it will appear green and if it is incorrect it will appear red. Incorrect answers will reduce the score that can be received by completing the question. Each question is worth up to ten points if the user answers it without making any errors.

## Difficulty Settings
The user is able to choose between easy, normal and hard for the various levels in the game. All of the difficulty settings function identically. The more difficult settings require the user to identify a larger range of missing notes from the melody. In the easy difficulty only one note is missing. On normal three notes are missing and on hard all but one note is missing.

![alt text](https://github.com/Gubbings/SeriousGamesDev/blob/master/imgs/difficultySelection.PNG "Difficulty Selection")

## Game Flow and Levels
There are three modes that the user can access in the game: tutorial, practice and endless. These modes are accessed by clicking the appropriate menu button after entering the game. If the user has not yet completed the tutorial they but attempts to access one of the other modes they will be notified that it is recommended to complete the tutorial. They are able to ignore the recommendation and proceed regardless. This feature allows users familiar with music theory to play the game without completing the tutorial. 

The tutorial level focusses on introducing the way you play the game and what the various elements on the screen represent. It is very much focused on a novice user. After completing the tutorial the user will be directed to the practice mode.

The practice mode features nine manually input problems for the user to solve. This section uses examples that sound pleasant and span a large range of key signatures. This section was included to offset the randomness utilized in the endless mode which, while theoretically correct, can produce examples that are rather obscure to hear especially for novice users.

The endless mode procedurally generates an infinite supply of practice problems. The user can play in this mode indefinitely. Endless mode accurately follows the rules of music theory noted in the tutorial. It is possible for this mode to produce some examples that would be unlikely to appear in real compositions but filtering such examples is beyond the scope of this project. Both practice mode and endless mode follow the rules defined by the difficulty settings.
