using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateChessBoard : MonoBehaviour
{
    private int rows = 8;
    private int columns = 8;
    private float squareSize = 10.0f;
    private string[] rowName = { "A", "B", "C", "D", "E", "F", "G", "H" };


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Editor causes this Awake");

        // Loop to create chessboard squares
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.transform.position = new Vector3(col * squareSize, 0, row * squareSize);
                int colName = col + 1;
                plane.name = rowName[row] + colName;
                // Alternate square colors

                plane.GetComponent<Renderer>().material.color = (row + col) % 2 == 0 ? Color.white : Color.black;

                // Create chess pieces if it's a starting position
                if (row == 1 || row == 6)
                {
                    GameObject pieces = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    pieces.transform.position = new Vector3(col * squareSize, 1, row * squareSize);
                    pieces.transform.localScale = new Vector3(2, 2, 2);
                }
                else if (row == 0 || row == 7)
                {
                    GameObject pieces = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    pieces.transform.position = new Vector3(col * squareSize, 2, row * squareSize);
                    pieces.transform.localScale = new Vector3(4, 4, 4);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
