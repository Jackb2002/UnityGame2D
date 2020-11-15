using CodeMonkey.Utils;
using UnityEngine;

public class Grid<T>
{
    private readonly int width;
    private readonly int height;
    private readonly float cellSize;
    private readonly Vector3 originPosition;
    private readonly T[,] gridArray;
    private readonly TextMesh[,] debugTextArray;
    private readonly bool showDebug;

    public Grid(int Width, int Height, float CellSize, Vector3 OriginPosition, bool ShowDebug)
    {
        width = Width;
        height = Height;
        cellSize = CellSize;
        originPosition = OriginPosition;
        gridArray = new T[width, height];
        debugTextArray = new TextMesh[width, height];
        showDebug = ShowDebug;

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = default(T);
                if (showDebug)
                {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(),
                           null,
                           GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f,
                           20,
                           Color.white,
                           TextAnchor.MiddleCenter);
                }
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetIndexFromPosition(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPosition).y / cellSize);
    }

    public bool SetGridObject(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < GetWidth() && y < GetHeight())
        {
            gridArray[x, y] = value;
            if (showDebug)
            {
                debugTextArray[x, y].text = gridArray[x, y].ToString();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public T GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < GetWidth() && y < GetHeight())
        {
            return gridArray[x, y];
        }
        else
        {
            Debug.LogWarning($"Grid couldn't return tile at {x}:{y}");
            return default;
        }
    }

    public void SetGridObject(Vector3 worldPos, T value)
    {
        GetIndexFromPosition(worldPos, out int x, out int y);
        SetGridObject(x, y, value);
    }

    public T GetGridObject(Vector3 worldPos)
    {
        GetIndexFromPosition(worldPos, out int x, out int y);
        return GetGridObject(x, y);
    }

}
