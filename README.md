```
Authors:			Hannah Larsen & Todd Oldham
StartDate:			05-April-2023
Course:				CS3500, University of Utah, School of Computing
GitHubIDs:			25hlarsen4, Destroyr-of-u
Repo:				https://github.com/uofu-cs3500-spring23/assignment8agario-larsen__oldham.git
CommitDate:			12-April-2023 8:30 pm
Solution:			Agario
Copyright:			CS3500, Hannah Larsen, Todd Oldham - This work may not be copied for use in academic coursework.
```


# Overview of the Agario Functionality:

This assignment connects to the provided Agario server and allows players to play as a circular object.
The players can move around the world eating "food" and other players if they are big enough. If the 
player isn't careful they themselves could get eaten by other bigger players. If the players are big enough they
can shoot a part of themselves off to eat targets that are trying to escape. If the player dies they can quit or play again.

# User Interface and Game Design Decisions:

The game has two views, one to start the game and one to play. The start is very simple because the only required information is the
server and the player name. All information about the game objects is provided by the server, so all we had to do was draw and update them,
which we did using a timer and timing capabilities of our computers. We used typical MAUI components such as editors, entries, and labels. One
important note is that to get the space bar to work for the split we used an entry. However the entry needs to be clicked on before the split
function can work, this is due to the fact that we have had problems with auto focusing an entry in MAUI. We followed the MVC laid out by the
assignment.

# Partnership:

The majority of the code was done using pair programming standards, the only things that were worked on individually
were, setting up files, updating comments or headers, just general improvements to the view and flow of the code and
documentation.


# Branching:

This project did not have a branch as we did not work on anything separately from one another and we followed along with
the instructions.

# Testing:

To test our project we compared it to the provided client for this assignment. If our game was able to do the same things as the provided client
then we felt that it was sufficient. We tested multiple times throughout the development process and we used a lot of debug statements to check
the condition of variables and objects to make sure that they did what we thought they were supposed to do.

# Time Expenditures:

Predicted hours: 12	   Actual hours: 12
Note: The time spent debugging, researching tools, and actually coding was split fairly evenly.
Note2: The drawing of the scene was more of a bottleneck than the networking data.

Both of our time estimation skills are improving throughout this semester, we have been able to more accurately predict the amount of
time the assignments will take. This assignment wasn't too bad in terms of complexity, but the instructions were a little hard to
follow which made implementation a little harder than we thought.
