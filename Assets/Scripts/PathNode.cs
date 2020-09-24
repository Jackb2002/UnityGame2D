using System;

public class PathNode
{
    private readonly Grid<PathNode> grid;

    public int x { get; private set; }
    public int y { get; private set; }

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{x},{y}";
    }

    internal void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
