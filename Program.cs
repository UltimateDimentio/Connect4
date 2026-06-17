namespace FinalProjectConnect4
{
    public class GameBoard
    {
        private string[,] board;
        private int rows;
        private int cols;

        public GameBoard(int rows = 6, int cols = 7)
        {
            this.rows = rows;
            this.cols = cols;
            board = new string[rows, cols];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    board[i, j] = "";
        }

        public string HasWinner()
        {
            string result = null;

            // Row check
            for (int i = 0; i < rows; i++)
            {
                if (board[i, 0] != "" &&
                    board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2] &&
                    board[i, 2] == board[i,3])
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
            //Tie Check
            if (result == null && IsBoardFull())
                result = "tie";

            return result;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (board[i, j] == "") return false;

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

