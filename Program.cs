namespace FinalProjectConnect4
{
    public class GameBoard
    {
        private string[,] board;
        private int rows;
        private int cols;

        public GameBoard(int rows = 7, int cols = 3)
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
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Hello, World!");
            }
        }
    }
}
