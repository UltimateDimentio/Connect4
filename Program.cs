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
                for (int j = 0; j < cols - 3; j++)
                {
                    if (board[i, j] != "." &&
                        board[i, j] == board[i, j + 1] &&
                        board[i, j + 1] == board[i, j + 2] &&
                        board[i, j + 2] == board[i, j + 3])
                    {
                        result = board[i, 0];
                        break;
                    }
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
            Controller controller = new Controller(6, 7, 0, 0);
            HumanPlayer player = new HumanPlayer('X', 0);
            string winner = null;
            bool playAgain = true;


            while (winner == null)
            {
                Console.Clear();
                controller.DisplayBoard();
                char currentPiece = player.PlayerTurn();
                Console.WriteLine($"Player {currentPiece}'s turn");
                Console.Write("Enter column (1-7): ");

                int col;
                if (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 7)
                {
                    Console.WriteLine("Invalid input, try again.");
                    player.Turns--;
                    continue;
                }

                col -= 1; // convert from 1-based input to 0-based array index
                controller.DropPiece(col, currentPiece.ToString());
                winner = controller.CheckWinner();
            }
            Console.Clear();
            controller.DisplayBoard();

            if (winner == "tie")
                Console.WriteLine("It's a tie!");
            else
                Console.WriteLine($"Player {winner} wins!");
        }
    }
}