/*
 * Author: Theodor Hägg (.NET23)
 * Website: https://github.com/etheoo98/Labb2-Chessboard
 * Date: September 20, 2023
 */

using System.Text;

namespace ChessBoard;

internal static class Program
{
    public static void Main()
    {
        // Encode I/O with unicode ("◼", "◻", "♛").
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;

        var boardSize = GetBoardSize();
        var blackSquares = GetUniqueString("Hur ska svarta rutor se ut?", new List<string>());
        var whiteSquares = GetUniqueString("Hur ska vita rutor se ut?", new List<string> { blackSquares });
        var uniquePiece = GetUniqueString("Hur ska pjäsen se ut?", new List<string> { blackSquares, whiteSquares });
        var (uniquePieceColumn, uniquePieceRow) = GetUniquePieceNotation(boardSize);

        PrintChessBoard(boardSize, blackSquares, whiteSquares, uniquePiece, uniquePieceColumn, uniquePieceRow);
    }

    /*
     * Explanation of the method's syntax:
     *
     * This method is set to "private", meaning it can only be accessed within the current class.
     * "static" is required because this method is called from another static method: Main().
     * The datatype this method returns is an integer, hence: "int".
     * The name of the method is "GetBoardSize", and it takes no parameters within its parentheses: "()".
     */
    private static int GetBoardSize()
    {
        const int minBoardSize = 2; // Minimum board size is 2*2 (A1-B2).
        const int maxBoardSize = 26; // Maximum board size is 26*26 (A1-Z26).

        while (true) // Prompt user until they enter a valid board size.
        {
            Console.WriteLine("Hur stort bräde?");
            var input = Console.ReadLine();

            // If it's true that the string can be parsed as an int, validate that the chosen size is allowed.
            if (int.TryParse(input, out var size) && size is >= minBoardSize and <= maxBoardSize)
                return size; // If the conditions are indeed true, the int is returned to the caller, exiting the loop.

            // This code is only reached if the user enters an invalid size. Explicitly typing "else" is not needed in this case.
            Console.WriteLine($"Ett fel uppstod! Värdet måste vara ett heltal mellan {minBoardSize} och {maxBoardSize}.");
        }
    }

    /*
     * Explanation of the method's syntax:
     *
     * This method is set to "private", meaning it can only be accessed within the current class.
     * "static" is required because this method is called from another static method: Main().
     * The datatype this method returns is a string, hence: "string".
     * The name of the method is "GetUniqueString".
     * Within the parentheses "()", we expect the caller to include a string, called "prompt" locally,
     * as well as a List of strings, called locally "notAllowed".
     */
    private static string GetUniqueString(string prompt, List<string> notAllowed)
    {
        while (true) // Prompt user until they input a single character string that doesn't already exist in the list.
        {
            Console.WriteLine(prompt); // The prompt will vary depending on the argument sent to this method.
            var input = Console.ReadLine();

            // The input is deemed valid if the string is not (!) null, is only one character and doesn't exist in the list.
            if (!string.IsNullOrWhiteSpace(input) && input.Length == 1 && !notAllowed.Contains(input))
                return input;

            // This code is only reached if the user enters an invalid input. An appropriate error message will be printed.
            if (string.IsNullOrWhiteSpace(input))
                Console.WriteLine("Ett fel uppstod! Inmatningen får inte vara tom.");
            else if (input.Length != 1)
                Console.WriteLine("Ett fel uppstod! Inmatningen måste bestå av endast en symbol.");
            else if (notAllowed.Contains(input))
                Console.WriteLine($"Ett fel uppstod! Inmatningen \"{input}\" finns redan.");
            else
                Console.WriteLine("Ett okänt fel uppstod!");
        }
    }

    /*
     * Explanation of the method's syntax:
     *
     * This method is set to "private", meaning it can only be accessed within the current class.
     * "static" is required because this method is called from another static method: Main().
     * The method returns two integers: "columnCoordinate" and "rowCoordinate".
     * The name of the method is "GetUniquePieceNotation".
     * Within the parentheses "()", we expect the caller to include an integer, called "maxBoardSize" locally.
     */
    private static (int columnCoordinate, int rowCoordinate) GetUniquePieceNotation(int maxBoardSize)
    {
        // Get the alphabetic counterpart of an int (1 = A, 2 = B, etc.)
        var maxColumnCharacter = (char)('A' + maxBoardSize - 1);

        while (true) // Prompt user until they enter a valid chess notation.
        {
            Console.WriteLine($"Var ska pjäsen stå? (mellan A1 och {maxColumnCharacter}{maxBoardSize})");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                // Get the numeric position of the first character at index "0", in a string (A = 1, B = 2, etc.)
                var columnNotation = char.ToUpper(input[0]) - 'A' + 1;

                // If the string can be parsed as an int starting at index "1", validate that the chosen column and row are within the board.
                if (int.TryParse(input[1..], out var rowNotation)
                    && columnNotation > 0 && columnNotation <= maxBoardSize
                    && rowNotation > 0 && rowNotation <= maxBoardSize)
                    return (columnNotation, rowNotation); // return both integers.
            }

            // This code is only reached if the user enters an invalid input.
            Console.WriteLine($"Ett fel uppstod! Inmatningen \"{input}\" är en ogiltig notation.");
        }
    }

    private static void PrintChessBoard(int boardSize, string blackSquares, string whiteSquares, string uniquePiece,
        int uniquePieceColumn, int uniquePieceRow)
    {
        for (var currentRow = 1; currentRow <= boardSize; currentRow++) // Loop through each row.
        {
            for (var currentColumn = 1; currentColumn <= boardSize; currentColumn++)
            {
                // Compare both axes in the current position against the unique piece axes.
                if (currentRow == uniquePieceRow && currentColumn == uniquePieceColumn)
                {
                    Console.Write(uniquePiece); // if true, print the unique piece.
                }
                else
                {
                    /*
                     * How alternating output is achieved:
                     *
                     * Since the nested (inner) for-loop's starting value will reset back to "1" for each new row,
                     * whilst the outer for-loop continues to increment, the addition of the two integers must alternate
                     * between even and uneven values during the start of new rows.
                     *
                     * By using the sum of the row value and the column value against modulus (%) 2 is equal to 0,
                     * it becomes either true or false that the result is even. It then becomes possible to use an
                     * if-else statement to select the appropriate string to print to the console window. In this case
                     * an ternary operator was chosen as to not create too much nesting.
                     */
                    Console.Write((currentRow + currentColumn) % 2 == 0 ? blackSquares : whiteSquares);
                }
            }
            // We reach this part of the code by printing all columns on the current row.
            Console.WriteLine(); // Make a new line for the next row.
        }
    }
}