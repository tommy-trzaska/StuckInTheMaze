using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject teleporter;
    public Transform player;
    public int width = 10;
    public int height = 10;

    private Cell[,] grid;

	void Start ()
    {
        CreateGrid();
        GenerateMaze();
	}

    void CreateGrid()
    {
        float cellSize = floorPrefab.GetComponent<Renderer>().bounds.size.x;
        float wallHeight = wallPrefab.GetComponent<Renderer>().bounds.size.y;

        //Floor creation
        grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = Instantiate(floorPrefab).GetComponent<Cell>();
                grid[x, y].transform.position = new Vector3((-width / 2 + x) * cellSize, 0, (-height / 2 + y) * cellSize);
                grid[x, y].transform.parent = transform;
                grid[x, y].gridX = x;
                grid[x, y].gridY = y;
                grid[x, y].gameObject.name = "Cell (" + x + ", " + y + ")";
            }
        }

        //Walls creation
        GameObject[,] verticalWalls = new GameObject[width , height + 1];
        GameObject[,] horizontalWalls = new GameObject[width + 1, height];

        for (int x = 0; x < width + 1; x++)
        {
            for (int y = 0; y < height + 1; y++)
            {
                //Vertical walls
                if (x < width)
                {
                    verticalWalls[x, y] = (GameObject)Instantiate(wallPrefab);
                    verticalWalls[x, y].transform.position = new Vector3((-width / 2 + x) * cellSize, wallHeight / 2, (-height / 2 + y) * cellSize - cellSize / 2);
                    verticalWalls[x, y].transform.parent = transform;
                    verticalWalls[x, y].gameObject.name = "Vertical Wall (" + x + ", " + y + ")";

                    if (y > 0)
                        grid[x, y - 1].walls[0] = verticalWalls[x, y];
                    if (y < height)
                        grid[x, y].walls[2] = verticalWalls[x, y];
                }

                //Horizontal walls
                if (y < height)
                {
                    horizontalWalls[x, y] = (GameObject)Instantiate(wallPrefab);
                    horizontalWalls[x, y].transform.position = new Vector3((-width / 2 + x) * cellSize - cellSize / 2, wallHeight / 2, (-height / 2 + y) * cellSize);
                    horizontalWalls[x, y].transform.Rotate(90 * Vector3.up);
                    horizontalWalls[x, y].transform.parent = transform;
                    horizontalWalls[x, y].gameObject.name = "Horizontal Wall (" + x + ", " + y + ")";

                    if (x > 0)
                        grid[x - 1, y].walls[1] = horizontalWalls[x, y];
                    if (x < width)
                        grid[x, y].walls[3] = horizontalWalls[x, y];
                }
            }
        }
    }

    void GenerateMaze ()
    {
        List<Cell> path = new List<Cell>();
        Cell currentCell;

        //Find starting point
        currentCell = grid[Random.Range(0, width), Random.Range(0, height)];
        currentCell.visted = true;
        path.Add(currentCell);

        while(path.Count > 0)
        {
            currentCell = path[path.Count - 1];

            List<Cell> possibleConnections = FindPossibleConnections(currentCell.gridX, currentCell.gridY);

            if (possibleConnections.Count == 0)
                path.RemoveAt(path.Count - 1);
            else
            {
                Cell selectedCell = possibleConnections[Random.Range(0, possibleConnections.Count)];
                path.Add(selectedCell);
                selectedCell.visted = true;

                if (selectedCell.gridX > currentCell.gridX)
                    currentCell.walls[1].SetActive(false);

                if (selectedCell.gridX < currentCell.gridX)
                    currentCell.walls[3].SetActive(false);

                if (selectedCell.gridY > currentCell.gridY)
                    currentCell.walls[0].SetActive(false);

                if (selectedCell.gridY < currentCell.gridY)
                    currentCell.walls[2].SetActive(false);
            }
        }

        SetStartAndEnd();
    }

    List<Cell> FindPossibleConnections (int gridX, int gridY)
    {
        List<Cell> possibleConnections = new List<Cell>();

        if (gridX > 0)
        {
            if(!grid[gridX - 1, gridY].visted)
                possibleConnections.Add(grid[gridX - 1, gridY]);
        }
        if (gridX < width - 1)
        {
            if (!grid[gridX + 1, gridY].visted)
                possibleConnections.Add(grid[gridX + 1, gridY]);
        }
        if (gridY > 0)
        {
            if (!grid[gridX, gridY - 1].visted)
                possibleConnections.Add(grid[gridX, gridY - 1]);
        }
        if (gridY < height - 1)
        {
            if (!grid[gridX, gridY + 1].visted)
                possibleConnections.Add(grid[gridX, gridY + 1]);
        }

        return possibleConnections;
    }

    void SetStartAndEnd ()
    {
        List<Cell> deadEnds = FindDeadEnds();

        Cell startCell = deadEnds[Random.Range(0, deadEnds.Count)];
        player.position = new Vector3(startCell.transform.position.x, player.position.y, startCell.transform.position.z);

        float longestDistance = 0;
        Cell endCell = null;

        foreach(Cell c in deadEnds)
        {
            if(c != startCell)
            {
                float currentDistance = Vector3.Distance(startCell.transform.position, c.transform.position);

                if (currentDistance > longestDistance)
                {
                    longestDistance = currentDistance;
                    endCell = c;
                }
            }
        }

        Instantiate(teleporter, endCell.transform.position, Quaternion.identity);
    }

    List<Cell> FindDeadEnds ()
    {
        List<Cell> deadEnds = new List<Cell>();

        foreach(Cell c in grid)
        {
            if (CountActiveWalls(c) == 3)
                deadEnds.Add(c);
        }

        return deadEnds;
    }

    int CountActiveWalls (Cell cell)
    {
        int activeWalls = 0;

        foreach(GameObject wall in cell.walls)
        {
            if (wall.activeInHierarchy)
                activeWalls++;
        }

        return activeWalls;
    }
}
