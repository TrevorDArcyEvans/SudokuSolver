namespace SudokuSolver;

public static class Program
{
  public static void Main(string[] args)
  {
    var sudoku = new[,]
    {
      { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
      { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
      { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
      { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
      { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
      { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
      { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
      { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
      { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
    };
    SolveSudoku(sudoku);
  }

  private static void SolveSudoku(char[,] board)
  {
    if (board == null || board.Length == 0)
    {
      return;
    }

    Solve(board);
  }

  private static bool Solve(char[,] board)
  {
    for (var i = 0; i < board.GetLength(0); i++)
    {
      for (var j = 0; j < board.GetLength(1); j++)
      {
        if (board[i, j] != '.')
        {
          continue;
        }

        for (var c = '1'; c <= '9'; c++)
        {
          if (!IsValid(board, i, j, c))
          {
            continue;
          }

          board[i, j] = c;

          if (Solve(board))
          {
            return true;
          }

          board[i, j] = '.';
        }

        return false;
      }
    }

    return true;
  }

  private static bool IsValid(char[,] board, int row, int col, char c)
  {
    for (var i = 0; i < 9; i++)
    {
      //check row  
      if (board[i, col] != '.' &&
          board[i, col] == c)
      {
        return false;
      }

      //check column  
      if (board[row, i] != '.' &&
          board[row, i] == c)
      {
        return false;
      }

      //check 3*3 block  
      if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != '.' &&
          board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
      {
        return false;
      }
    }

    return true;
  }
}
