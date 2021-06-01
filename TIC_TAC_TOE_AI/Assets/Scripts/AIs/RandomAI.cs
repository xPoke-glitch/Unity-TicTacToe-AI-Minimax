using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : AbstractAI
{
    public override void PickAMove()
    {
        
        Square[,] matrix = this.board.getMatrix();
        int i=0, j=0;
        do
        {
            i = Random.Range(0, 3);
            j = Random.Range(0, 3);
        } while (matrix[i, j].IsMarked);
        // Do the move
        matrix[i, j].MarkMoveAI();
        Debug.Log("AI MADE IS MOVE");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
