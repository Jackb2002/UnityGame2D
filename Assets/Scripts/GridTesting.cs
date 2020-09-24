using CodeMonkey.Utils;
using UnityEngine;

public class GridTesting : MonoBehaviour
{
    private PathFinder PathFinder;

    private void Start()
    {
        PathFinder = new PathFinder(4, 4);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = UtilsClass.GetMouseWorldPosition();
            PathFinder.
                GetGrid().
                getCellInWorld(mouseWorldPos, out int x, out int y);
            Debug.Log("A* from 0,0 to " + $"{x},{y}");
            System.Collections.Generic.List<PathNode> path = PathFinder.FindPath(0, 0, x, y);
            Debug.Log(path.Count + " nodes to visit");
            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f,
                        new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green,10);

                }
            }
        }
    }
}
