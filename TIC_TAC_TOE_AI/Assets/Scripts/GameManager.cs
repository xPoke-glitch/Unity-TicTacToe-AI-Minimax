using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private AbstractAI AI;
    [SerializeField] private ModalWindowManager window;
    public bool DidHumanMove { get; set; }
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            Game();
        }
    }

    private void Game()
    {
        if (DidHumanMove)
        {
            // Check if someone won or tie
            if (!CheckWin().Equals("None")) {
                Debug.Log(CheckWin());
                // Stop game
                isGameOver = true;
                // UI code
                if (CheckWin().Equals("Tie"))
                {
                    window.titleText = "Game is Over";
                    window.descriptionText = "TIE";
                    window.UpdateUI();
                    window.OpenWindow();
                }
                else
                {
                    window.titleText = "Game is Over";
                    window.descriptionText = CheckWin();
                    window.UpdateUI();
                    window.OpenWindow();
                }
                return;
            }
            else // if not
            {
                // AI make the move
                AI.PickAMove();
                // Check if someone won or tie
                if (!CheckWin().Equals("None"))
                {
                    Debug.Log(CheckWin());
                    // Stop game
                    isGameOver = true;
                    // UI code
                    if (CheckWin().Equals("Tie")) {
                        window.titleText = "Game is Over";
                        window.descriptionText = "TIE";
                        window.UpdateUI();
                        window.OpenWindow();
                    }
                    else
                    {
                        window.titleText = "Game is Over";
                        window.descriptionText = CheckWin();
                        window.UpdateUI();
                        window.OpenWindow();
                    }
                    return;
                }
            }
            // Wait for the next human move
            DidHumanMove = false; 
        }
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

    public void GameReset()
    {
        board.aviableSquares = 9;
        Square[,] matrix = board.getMatrix();
        for(int i = 0; i < 3; ++i)
        {
            for(int j=0; j<3; ++j)
            {
                matrix[i, j].ResetSquare();
            }
        }
        isGameOver = false;
        DidHumanMove = false;
        window.CloseWindow();
    }
}
