namespace FinalProjectConnect4
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

        private void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    board[i, j] = ".";
        }

        //Displays the board
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

        public string HasWinner(string[,] board)
        {
            string result = null;

            // Row check
            for (int i = 0; i < rows; i++)
            {
                if (board[i, 0] != "" &&
                    board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2] &&
                    board[i, 2] == board[i, 3])
                {
                    result = board[i, 0];
                    break;
                }
            }

            // Diagonal check (top left - bottom right)
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < cols - 3; j++)
                {
                    if (board[i, j] != "" &&
                        board[i, j] == board[i + 1, j + 1] &&
                        board[i + 1, j + 1] == board[i + 2, j + 2] &&
                        board[i + 2, j + 2] == board[i + 3, j + 3])
                    {
                        result = board[i, j];
                        break;
                    }
                }
            }

            // Diagonal check (top right - bottom left)
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 3; j < cols; j++)
                {
                    if (board[i, j] != "" &&
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
                    if (board[j, i] != "" &&
                        board[j, i] == board[j + 1, i] &&
                        board[j + 1, i] == board[j + 2, i] &&
                        board[j + 2, i] == board[j + 3, i])
                    {
                        result = board[j, i];
                        break;
                    }
                }
            }

            if (result == null && IsBoardFull(board))
                result = "tie";

            return result;
        }

        private bool IsBoardFull(string[,] board)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (board[i, j] == "") return false;

            return true;
        }
    }
    class Controller : GameBoard
    {
        protected int Col;
        protected int Row;
        public Controller(int rows, int cols, int row, int col) : base(rows, cols)
        {
            Col = col;
            Row = row;
        }
        public void updateBoard(string[,] board, int row, int col, HumanPlayer currentPlayer)
        {
            Console.Clear();
            DisplayBoard();
            //This only checks if the spot is available and there are still errors in the code
            if (board[row, col] == ".")
            {
                int maxRow = 6;
                int rowChange = 7;
                for (int i = 0; i < maxRow; i++)
                {
                    if (board[rowChange, col] == ".")
                    {
                        board[rowChange, col] = currentPlayer;
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
    interface IPlayer
    {
        char PlayerTurn();
        bool TakeTurn(GameBoard board, int col);
    }
    class HumanPlayer : IPlayer
    {
        public char Player { get; set; }
        public int Turns { get; set; }

        public HumanPlayer(char player, int turns)
        {
            Player = player;
            Turns = turns;
        }

        public char PlayerTurn()
        {
            Turns++;
            if (Turns % 2 == 1)
            {
                return Player = 'X';
            }
            else
            {
                return Player = 'O';
            }
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
            GameBoard board = new GameBoard();
            board.DisplayBoard();
        }
    }
}