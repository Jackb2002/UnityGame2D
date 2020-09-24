using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private const int MOVE_STRAIGHT = 10;
    private const int MOVE_DIAGONAL = 14;

    private readonly Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PathFinder(int width, int height)
    {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero,
            (Grid<PathNode> g, int x, int y) => new PathNode(grid, x, y), true);
    }

    public PathFinder(int width, int height , Grid<PathNode> grid)
    {
        this.grid = grid;
    }

    public Grid<PathNode> GetGrid()
    {
        Debug.Log(grid);
        return grid ?? null;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);

                pathNode.gCost = int.MaxValue;
                pathNode.cameFromNode = null;


            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                //final node
                return CalculatePath(endNode);
            }
            else
            {
                openList.Remove(currentNode);
                closedList.Add(currentNode);


                foreach (PathNode neighbourNode in GetNeighbours(currentNode))
                {
                    if (closedList.Contains(neighbourNode))
                    {
                        continue;
                    }

                    int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }
        }

        //out of nodes, no route

        return null;
    }

    private List<PathNode> GetNeighbours(PathNode currentNode)
    {
        List<PathNode> NeighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            NeighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y - 1 >= 0)
            {
                NeighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }

            if (currentNode.y + 1 >= grid.GetHeight())
            {
                NeighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }
        if (currentNode.x + 1 >= grid.GetWidth())
        {
            NeighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0)
            {
                NeighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            }

            if (currentNode.y + 1 >= grid.GetHeight())
            {
                NeighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }
        if (currentNode.y - 1 >= 0)
        {
            NeighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }

        if (currentNode.y + 1 >= grid.GetHeight())
        {
            NeighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return NeighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistance(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pNodeList)
    {
        PathNode lowestFCostNode = pNodeList[0];
        for (int i = 0; i < pNodeList.Count; i++)
        {
            if (pNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
