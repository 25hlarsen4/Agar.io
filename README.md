```
Authors:			Hannah Larsen & Todd Oldham
StartDate:			05-April-2023
Course:				CS3500, University of Utah, School of Computing
GitHubIDs:			25hlarsen4, Destroyr-of-u
Solution:			Agario
Copyright:			CS3500, Hannah Larsen, Todd Oldham - This work may not be copied for use in academic coursework.
```


# Overview of the Agario Functionality:

This assignment connects to the provided Agario server and allows players to play as a circular object.
The players can move around the world eating "food" and other players if they are big enough. To move around, your player
will follow your mouse.
If the player isn't careful, they themselves could get eaten by other bigger players. 
If the player is big enough, they can shoot a part of themselves off, using the space bar, to eat targets that are trying to escape. 
If the player dies, they can choose to quit or play again.

# How to play:
- The application runs on Visual Studio NET7.0 and the MAUI workload must be downloaded
- First run the provided server (access through releases and note that it only runs on windows machines), then run this client, type your desired player name, type the
  name of the machine running the server (defaulted to localhost), hit enter, and begin the fun!
- To move faster, move the mouse further from your player.
- To shoot a part of yourself to catch another player, hit the space bar. (Note that due to MAUI issues, if the space bar is not working, 
you may have to first click the entry box in the top right corner and then hit space). 
- You can tell if you've hit the game world boundary and should change direction by looking if the coordinates in the top right corner
  are no longer changing (due to MAUI issues, the graphics view background color could not be changed to show the boundary visually)
