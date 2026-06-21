namespace Connect4
{
    public class GameBoard
    {
        private string[,] board;
        private int rows;
        private int cols;
        private GameLogic logic;

        public string[,] GetBoard() => board;

        public GameBoard(int rows = 6, int cols = 7)
        {
            this.rows = rows;
            this.cols = cols;
            board = new string[rows, cols];
            logic = new GameLogic(rows, cols);
            InitializeBoard();
        }

        // Fills every cell with "." to represent an empty board
        private void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    board[i, j] = ".";
        }

        // Drops a piece into the lowest available row in the given column
        // Returns false if the column is full
        public bool DropPiece(int col, string piece)
        {
            for (int i = rows - 1; i >= 0; i--)
            {
                if (board[i, col] == ".")
                {
                    board[i, col] = piece;
                    return true;
                }
            }
            return false;
        }

        // Prints the board to the console with column numbers at the bottom
        public void DisplayBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine("|");
            }
            Console.Write("   ");
            for (int j = 1; j <= cols; j++)
                Console.Write(j + " ");
            Console.WriteLine();
        }

        // Delegates winner checking to GameLogic
        public string CheckWinner()
        {
            return logic.HasWinner(board);
        }
    }

    public class GameLogic
    {
        private int rows;
        private int cols;

        public GameLogic(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        // Checks all win conditions and returns the winning piece, "tie", or null
        public string HasWinner(string[,] board)
        {
            string result = null;

            // Row check - only checks first 4 columns, needs fixing later
            for (int i = 0; i < rows; i++)
            {
                if (board[i, 0] != "." &&
                    board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2] &&
                    board[i, 2] == board[i, 3])
                {
                    result = board[i, 0];
                    break;
                }
            }

            // Diagonal check (top left to bottom right)
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < cols - 3; j++)
                {
                    if (board[i, j] != "." &&
                        board[i, j] == board[i + 1, j + 1] &&
                        board[i + 1, j + 1] == board[i + 2, j + 2] &&
                        board[i + 2, j + 2] == board[i + 3, j + 3])
                    {
                        result = board[i, j];
                        break;
                    }
                }
            }

            // Diagonal check (top right to bottom left)
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 3; j < cols; j++)
                {
                    if (board[i, j] != "." &&
                        board[i, j] == board[i + 1, j - 1] &&
                        board[i + 1, j - 1] == board[i + 2, j - 2] &&
                        board[i + 2, j - 2] == board[i + 3, j - 3])
                    {
                        result = board[i, j];
                        break;
                    }
                }
            }

            // Column check
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows - 3; j++)
                {
                    if (board[j, i] != "." &&
                        board[j, i] == board[j + 1, i] &&
                        board[j + 1, i] == board[j + 2, i] &&
                        board[j + 2, i] == board[j + 3, i])
                    {
                        result = board[j, i];
                        break;
                    }
                }
            }

            // If no winner and board is full, it's a tie
            if (result == null && IsBoardFull(board))
                result = "tie";

            return result;
        }

        // Returns true if no "." cells remain on the board
        private bool IsBoardFull(string[,] board)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (board[i, j] == ".") return false;

            return true;
        }
    }

    // Controller inherits GameBoard and handles board updates from player input
    class Controller : GameBoard
    {
        protected int Col;
        protected int Row;

        public Controller(int rows, int cols, int row, int col) : base(rows, cols)
        {
            Col = col;
            Row = row;
        }

        // Updates the board by dropping a piece into the correct row
        public void updateBoard(string[,] board, int row, int col, HumanPlayer currentPlayer)
        {
            DisplayBoard();
            if (board[row, col] == ".")
            {
                int maxRow = 6;
                int rowChange = row - 1; // fixed: was row - 1, caused index out of range
                for (int i = 0; i < maxRow; i++)
                {
                    if (rowChange < 0) break; // prevents index going below 0
                    if (board[rowChange, col] == ".")
                    {
                        board[rowChange, col] = currentPlayer.Player.ToString(); // fixed: was currentPlayer, can't assign object to string
                        break;
                    }
                    else
                    {
                        rowChange--;
                    }
                }
            }
        }
    }

    // Interface defining what every player type must implement
    interface IPlayer
    {
        char PlayerTurn();
        bool TakeTurn(GameBoard board, int col);
    }

    // Represents a human player, tracks whose turn it is via Turns counter
    public class HumanPlayer : IPlayer // fixed: added public so Controller can access it
    {
        public char Player { get; set; }
        public int Turns { get; set; }

        public HumanPlayer(char player, int turns)
        {
            Player = player;
            Turns = turns;
        }

        // Alternates between X and O based on turn count
        public char PlayerTurn()
        {
            Turns++;
            if (Turns % 2 == 1)
                return Player = 'X';
            else
                return Player = 'O';
        }

        public bool TakeTurn(GameBoard board, int col)
        {
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}