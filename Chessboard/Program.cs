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
    
    private static int GetBoardSize()
    {
        const int minBoardSize = 2; // Minimum board size is 2*2 (A1-B2).
        const int maxBoardSize = 26; // Maximum board size is 26*26 (A1-Z26).

        while (true) // Prompt user until they enter a valid board size.
        {
            Console.WriteLine($"Hur stort bräde? ({minBoardSize}-{maxBoardSize})");
            var input = Console.ReadLine();

            // Validate that the user input can be parsed as int, and is within the board size.
            if (int.TryParse(input, out var size) && size is >= minBoardSize and <= maxBoardSize)
                return size;
            
            Console.WriteLine($"Ett fel uppstod! Värdet måste vara ett heltal mellan {minBoardSize} och {maxBoardSize}.");
        }
    }
    
    private static string GetUniqueString(string prompt, List<string> notAllowed)
    {
        while (true) // Prompt user until they input a single character string that doesn't already exist in the list.
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            // The user input is deemed valid if the string is not (!) null, is only one character and doesn't exist in the list.
            if (!string.IsNullOrWhiteSpace(input) && input.Length == 1 && !notAllowed.Contains(input)) 
                return input;
            
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
    
    private static (int columnNotation, int rowNotation) GetUniquePieceNotation(int maxBoardSize)
    {
        /*
         * Get the alphabetic equivalent of an int: (1 = A, 2 = B, etc.)
         * 
         * Typecast the decimal sum of '@' + maxBoardSize, to get the alphabetic equivalent of maxBoardSize.
         * In the ASCII table, the char '@' is 64 in decimal, which is the decimal preceding char 'A' at 65.
         *
         * Example:
         * If the integer value is "1", it is calculated as "64 + 1", is equal to 65, the decimal for char 'A'.
         * If the integer value is "2", it is calculated as "64 + 2", is equal to 66, the decimal for char 'B'.
         * In this context this works as is up until the decimal 90, which is the decimal for char 'Z'.
         */
        var maxColumnCharacter = (char)('@' + maxBoardSize);

        while (true) // Prompt user until they enter a valid chess notation.
        {
            Console.WriteLine($"Var ska pjäsen stå? (A1-{maxColumnCharacter}{maxBoardSize})");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                /*
                 * Get the numeric equivalent of a char: (A = 1, B = 2, etc.)
                 * 
                 * The char at index 0 is converted to uppercase, before its ASCII decimal is subtracted
                 * by the decimal of char '@', which is 64. The resulting value is then stored as an int.
                 *
                 * Example:
                 * If input[0] is char 'A', 65 in decimal, it is calculated like "65 - 64", is equal to 1.
                 * If input[0] is char 'B', 66 in decimal, it is calculated like "66 - 64", is equal to 2, and so on.
                 */
                var columnNotation = char.ToUpper(input[0]) - '@';

                // If the string can be parsed as an int starting at index "1", validate that the chosen column and row are within the board.
                if (int.TryParse(input[1..], out var rowNotation)
                    && columnNotation > 0 && columnNotation <= maxBoardSize
                    && rowNotation > 0 && rowNotation <= maxBoardSize)
                    return (columnNotation, rowNotation);
            }
            
            Console.WriteLine($"Ett fel uppstod! Inmatningen \"{input}\" är en ogiltig notation.");
        }
    }

    private static void PrintChessBoard(int boardSize, string blackSquares, string whiteSquares, string uniquePiece,
        int uniquePieceColumn, int uniquePieceRow)
    {
        for (var currentRow = 1; currentRow <= boardSize; currentRow++) // Loop through each row.
        {
            for (var currentColumn = 1; currentColumn <= boardSize; currentColumn++) // Loop through each column on the current row.
            {
                // Compare the current row and column against the unique piece's row and column.
                if (currentRow == uniquePieceRow && currentColumn == uniquePieceColumn)
                {
                    Console.Write(uniquePiece); // Print the unique piece, since we're on the correct notation.
                }
                else
                {
                    /*
                     * How alternating output is achieved:
                     *
                     * Since the nested/inner for-loop's starting value will reset back to "1" for each new row,
                     * whilst the outer for-loop continues to increment, the addition of the two integers must always
                     * alternate between even and uneven values.
                     *
                     * By using the sum of the row value and the column value against modulus (%) 2 is equal to 0,
                     * it becomes either true or false that the result is even. It then becomes possible to use this
                     * as a condition for an if-else statement in order to select the appropriate output to print to
                     * the console window. In this case an ternary operator was chosen as to not create too much nesting.
                     */
                    Console.Write((currentRow + currentColumn) % 2 == 0 ? blackSquares : whiteSquares);
                }
            }
            Console.WriteLine(); // Make a new line for the next row.
        }
    }
}