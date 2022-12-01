using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();
    [SerializeField] List<WayPoint> FirstWaypointList = new List<WayPoint>();
    [SerializeField] List<WayPoint> LastWaypointList = new List<WayPoint>();
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    Queue<WayPoint> quene = new Queue<WayPoint>();
    bool isRunning = true;
    WayPoint searchCenter;

    List<WayPoint> path = new List<WayPoint>();

    WayPoint last;
    WayPoint first;
    WayPoint[] Waypoints;
    bool notFirsedTime = false;



    public List<WayPoint> getPath()
    {
        //Get random route
        int lastIndex = Random.Range(0, LastWaypointList.Count);
        int firstIndex = Random.Range(0, FirstWaypointList.Count);
        last = LastWaypointList[lastIndex];
        first = FirstWaypointList[firstIndex];

        if (notFirsedTime)
        {
            ResetAllBlocks();
        }
        else
        {
            LoadBlocks();
        }

        if (path.Count == 0)
        {
            PathFind();
            PathCreate();
            SetColorForTheFirstAndLastWaypoints();
        }
        notFirsedTime = true;
        return path;


    }

    private void ResetAllBlocks()
    {
        path.Clear();
        isRunning = true;

        foreach (var way in Waypoints)
        {
            way.isExplored = false;
            way.exploredFrom = null;
            quene.Clear();
            //way.isPlaceable ##For single path
        }
    }

    private void PathCreate()
    {
        SetAsPath(last);

        WayPoint previousWayPoint = last.exploredFrom;
        while (previousWayPoint != first)
        {
            previousWayPoint = previousWayPoint.exploredFrom;
            SetAsPath(previousWayPoint);
        }
        SetAsPath(first);

        path.Reverse();
    }

    private void SetAsPath(WayPoint waypoint)
    {
        path.Add(waypoint);
        //waypoint.isPlaceable = false; ##For single path
    }
    private void PathFind()
    {
        quene.Enqueue(first);
        while (quene.Count > 0 && isRunning)
        {
            searchCenter = quene.Dequeue();
            searchCenter.isExplored = true;
            ExploreNeighbour();


            if (searchCenter == last)
            {
                isRunning = false;
            }
        }
    }

    private void ExploreNeighbour()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinate = searchCenter.GetGridPos() + direction;

            try
            {
                QueneNewNeighbours(explorationCoordinate);
            }

            catch
            {
                //Debug prurpose
            }

        }
    }

    private void QueneNewNeighbours(Vector2Int explorationCoordinate)
    {
        WayPoint Neighbour = grid[explorationCoordinate];
        if (Neighbour.isExplored || quene.Contains(Neighbour)) { }
        else
        {
            quene.Enqueue(Neighbour);
            Neighbour.exploredFrom = searchCenter;
        }
    }

    private void SetColorForTheFirstAndLastWaypoints()
    {
        first.setColor(Color.red);
        last.setColor(Color.black);
    }

    private void LoadBlocks()
    {
        Waypoints = FindObjectsOfType<WayPoint>();

        foreach (WayPoint wayPoint in Waypoints)
        {
            var gridPos = wayPoint.GetGridPos();

            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Waypoint Over Lapping" + wayPoint);
            }

            else
            {
                grid.Add(gridPos, wayPoint);
            }
        }
    }
}
