# Chessboard
C# console application made for yet another beginners programming course. 


## Assignment description
* Create a new project in Visual Studio (Console Application → C# → .NET Core)
* Rename the project to "Chessboard"
* When the program starts, the user should be allowed to enter a number.
* You should then print a "chessboard" with as many rows and columns as the user indicated
* You print the board by allowing each black square to be represented by this character ◼︎ and each white square of this character ◻︎. You should be able to copy these characters from this task description.

If the squares appear strange, you can use this code:
```
// unicode to show the squares, and setting a unicode standard output
Console.OutputEncoding = System.Text.Encoding.Unicode;
```

## Example
* Example with 3 rows and columns
  * If the user enters 3 as a number ...
```
3
◻︎◼︎◻︎
◼︎◻︎◼︎
◻︎◼︎◻︎
```

* Example with 10 rows and columns
  * If the user enters 10 as a number ...
```
10
◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎
◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎
◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎
◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎
◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎
◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎
◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎
◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎
◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎
◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎◼︎◻︎
```

## Extra challenge
Does the assignment feel too easy? Did you finish quickly?

Here you have some ideas on how you can make the assignment a little more advanced. Test as much as you want with these suggestions!

* Choose characters for the squares
  * ♟️ Allow the user to choose which character to generate for each black and each white square. Maybe the user wants "S" for black squares and "V" for white squares instead.
* Place a piece
* ♟️ Allow the user to choose a square on which the piece should stand on, and on that particular place, write for example an "x" or why not use an UTF-8 character for a chess piece, for example any of these: ♛ ♜ ♞
In chess, all squares have a name. The first row is called 1 and the second row 2 etc. The first column is called A and the second b, etc. We can then combine these so if I say the E7 I mean the 5th column and 7th row and want my piece there.
* Demonstration of extra challenges
  * This is how it might look:
```
How large should the board be?
10
What should black squares look like?
S
What should white squares look like?
V
What should the piece look like?
_
Where should the piece stand? (ex. E4)
E6
SVSVSVSVSV
VSVSVSVSVS
SVSVSVSVSV
VSVSVSVSVS
SVSVSVSVSV
VSVS_SVSVS
SVSVSVSVSV
VSVSVSVSVS
SVSVSVSVSV
VSVSVSVSVS
```
