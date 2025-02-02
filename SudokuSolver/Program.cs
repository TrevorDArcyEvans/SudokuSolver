﻿namespace SudokuSolver;

using System;
using System.IO;

public static class Program
{
  public static void Main(string[] args)
  {
    var lines = File.ReadAllLines(args[0]);
    var sudoku = new char[9, 9];
    for (var i = 0; i < 9; i++)
    {
      var line = lines[i].Replace(" ", "").ToCharArray();
      for (var j = 0; j < 9; j++)
      {
        sudoku[i, j] = line[j];
      }
    }

    DumpBoard(sudoku);

    Console.WriteLine();
    Console.WriteLine("Attempting solution...");
    Console.WriteLine();

    if (Solve(sudoku))
    {
      DumpBoard(sudoku);
    }
    else
    {
      Console.WriteLine("Solution failed");
    }
  }

  private static void DumpBoard(char[,] sudoku)
  {
    for (var i = 0; i < sudoku.GetLength(0); i++)
    {
      for (var j = 0; j < sudoku.GetLength(1); j++)
      {
        Console.Write($"{sudoku[i, j]} ");
      }

      Console.WriteLine();
    }
  }

  private static bool Solve(char[,] board)
  {
    if (board == null || board.Length == 0)
    {
      return false;
    }

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
