using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinigrid : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 gridSize;
    private GameObject[,] gridOfGrids;
    [SerializeField] GameObject grid;

    // Start is called before the first frame update
    void Start()
    {
        gridOfGrids = new GameObject[3, 3];
        gridOfGrids[0, 0] = Instantiate(grid);
        gridOfGrids[0, 1] = Instantiate(grid);
        gridOfGrids[0, 2] = Instantiate(grid);
        gridOfGrids[1, 0] = Instantiate(grid);
        gridOfGrids[1, 1] = grid;
        gridOfGrids[1, 2] = Instantiate(grid);
        gridOfGrids[2, 0] = Instantiate(grid);
        gridOfGrids[2, 1] = Instantiate(grid);
        gridOfGrids[2, 2] = Instantiate(grid);
        MoveGrids(Commons.Vec3To2(player.transform.position));
    }

    private void MoveGrids(Vector2 newCenter)
    {
        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                GameObject currentGrid = gridOfGrids[i, j];
                currentGrid.transform.position = CoordToPos(newCenter, i, j);
            }
        }
    }

    private Vector3 CoordToPos(Vector2 centerPos, int i, int j)
    {
        return new Vector2(
                    centerPos.x + (i - 1) * gridSize.x,
                    centerPos.y + (j - 1) * gridSize.y
                    );
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerGrid = GetPlayerGrid();
        if (playerGrid != new Vector2(0,0))
        {
            Vector2 newCenter = CoordToPos(grid.transform.position, playerGrid.x, playerGrid.y);
            MoveGrids(newCenter);
        }
    }

    private Vector2Int GetPlayerGrid()
    {
        float minX = grid.transform.position.x - gridSize.x / 2;
        float maxX = grid.transform.position.x + gridSize.x / 2;
        float minY = grid.transform.position.y - gridSize.y / 2;
        float maxY = grid.transform.position.y + gridSize.y / 2;

        float x = player.transform.position.x;
        int xc = x < minX ? 0 : x > maxX ? 2 : 1;
        float y = player.transform.position.y;
        int yc = y < minY ? 0 : y > maxY ? 2 : 1;

        return new Vector2Int(xc, yc);
    }
}
