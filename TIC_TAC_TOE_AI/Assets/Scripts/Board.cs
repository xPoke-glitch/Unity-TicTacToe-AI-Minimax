using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Square[] squares;
    private Square[,] matrix = new Square[3,3];
    public int aviableSquares { get; set; }

    void Start()
    {
        aviableSquares = 9;
        int x = 0;
        for(int i=0; i<3; ++i)
        {
            for(int j = 0; j < 3; ++j)
            {
                matrix[i, j] = squares[x];
                x++;
            }
        }
    }

    public Square[,] getMatrix() {
        return this.matrix;
    }
}
