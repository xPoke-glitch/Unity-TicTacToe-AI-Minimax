using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimaxAI : AbstractAI
{
    private int INFINITY = 99999;
    public override void PickAMove()
    {
        int bestScore = -INFINITY;
        int bestI = 0;
        int bestJ = 0;
        Square[,] matrix = board.getMatrix();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (!matrix[i, j].IsMarked)
                { // is the spot aviable?
                    matrix[i, j].MarkMoveAI(); // Mark for now to check the score of the move
                    int score = minimax(matrix, 0, false);
                    matrix[i, j].UndoMoveAI(); // Remove the mark, we don't know yet if it's optimal
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestI = i;
                        bestJ = j;
                    }
                }
            }
        }
        matrix[bestI, bestJ].MarkMoveAI();
    }

    private int minimax(Square[,] board, int depth, bool isMaximizing)
    {
        string result = CheckWin();
        if (!result.Equals("None"))
        {
            return evaluateResult(result);
        }

        if (isMaximizing) // AI turn (X)
        {
            int bestScore = -INFINITY;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    // is the spot aviable?
                    if (!board[i, j].IsMarked)
                    {
                        board[i, j].MarkMoveAI();
                        int score = minimax(board, depth + 1, false);
                        board[i, j].UndoMoveAI();
                        bestScore = max(score, bestScore);

                    }
                }
            }
            return bestScore;
        }
        else // Human turn (O)
        {
            int bestScore = INFINITY;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    // is the spot aviable?
                    if (!board[i, j].IsMarked)
                    {
                        board[i, j].MarkHumanMoveFromAI();
                        int score = minimax(board, depth + 1, true);
                        board[i, j].UndoHumanMoveFromAI();
                        bestScore = min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private int evaluateResult(string result)
    {
        if (result.Equals("Cross"))
        {
            return 10;
        }
        if (result.Equals("Circle"))
        {
            return -10;
        }
        return 0; // Tie
    }
    private int min(int x, int y)
    {
        if (x < y) return x;
        if (y < x) return y;
        return x;
    }
    private int max(int x, int y)
    {
        if (x > y)
            return x;
        if (y > x)
            return y;
        return x;
    }
    private string CheckWin()
    {
        Square[,] matrix = board.getMatrix();
        string winner = "None";
        // Horizontal
        for (int i = 0; i < 3; ++i)
        {

            if (matrix[i, 0].GetMarkerType().Equals(matrix[i, 1].GetMarkerType()) &&
                matrix[i, 1].GetMarkerType().Equals(matrix[i, 2].GetMarkerType()))
            {
                winner = matrix[i, 0].GetMarkerType();
            }
        }
        if (!winner.Equals("None"))
        {
            return winner;
        }
        // Vertical
        for (int i = 0; i < 3; ++i)
        {

            if (matrix[0, i].GetMarkerType().Equals(matrix[1, i].GetMarkerType()) &&
                matrix[1, i].GetMarkerType().Equals(matrix[2, i].GetMarkerType()))
            {
                winner = matrix[0, i].GetMarkerType();
            }
        }
        if (!winner.Equals("None"))
        {
            return winner;
        }
        // Main Diag
        if (matrix[0, 0].GetMarkerType().Equals(matrix[1, 1].GetMarkerType()) &&
               matrix[1, 1].GetMarkerType().Equals(matrix[2, 2].GetMarkerType()))
        {
            winner = matrix[0, 0].GetMarkerType();
        }
        if (!winner.Equals("None"))
        {
            return winner;
        }
        // Other Diag
        if (matrix[0, 2].GetMarkerType().Equals(matrix[1, 1].GetMarkerType()) &&
               matrix[1, 1].GetMarkerType().Equals(matrix[2, 0].GetMarkerType()))
        {
            winner = matrix[0, 2].GetMarkerType();
        }
        if (!winner.Equals("None"))
        {
            return winner;
        }
        // Tie
        if (board.aviableSquares == 0)
            return "Tie";
        return winner;
    }
}
