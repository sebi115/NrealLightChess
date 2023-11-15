using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePosition : MonoBehaviour
{
    public string position;
    private string[] rowName = { "A", "B", "C", "D", "E", "F", "G", "H" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pos_x = this.transform.localPosition.x;
        float pos_y = this.transform.localPosition.y;

        if (pos_x < -0.0024 | pos_x > 0.0024)
        {
            Debug.Log("piece is outisde of chessboard");
        }
        if (0.0015 < pos_y | pos_y < -0.0015)
        {
            Debug.Log("piece is outisde of chessboard");
        }

    }
}
