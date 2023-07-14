using TMPro;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;
    public Transform transPosition;
    private int[,] gridArray;
    
    public Grid(int width, int height, float cellSize, Transform transPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.transPosition = transPosition;

        gridArray = new int [this.width, this.height];
        int number = 1; // Start from number 1

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = number; // Assign the current number

                number++; // Increment the number for the next grid space

                if (gridArray[x, y] != 0) // Exclude the 0 values
                {
                    Debug.Log(x + "," + y);
                    UtilityScript.CreateWorldText(gridArray[x, y].ToString(), null, WorldPosition(x, y), 30,
                        Color.white,
                        TextAlignmentOptions.Center);
                }
            }
        }
    }

    private Vector3 WorldPosition(int x, int y)
    {
        return transPosition.position + new Vector3(x * cellSize, 0f, y * cellSize);
    }
}
