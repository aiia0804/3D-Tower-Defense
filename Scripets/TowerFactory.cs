using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int TowerLimit = 5;
    [SerializeField] Tower towerPrefeb;
    [SerializeField] bool overPlaceLimit = false;
    [SerializeField] int placeqty = 0;
    [SerializeField] Transform InstantiateParent;

    [SerializeField] Text moveText;

    Queue<Tower> towerQUeene = new Queue<Tower>();
    int moveTimes = 0;

    GameStatus gameStatus;

    private void Start()
    {
        moveText.text = "Total Move: " + moveTimes;
        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void ReplaceTower(WayPoint waypoint)
    {
        gameStatus.AddtoMoves(1);
        moveTimes++;
        moveText.text = "Total Move: " + moveTimes;
        if (!overPlaceLimit)
        {
            InstantiateNewTower(waypoint);
        }
        else
        {
            MoveExistingTower(waypoint);
        }

        placeqty++;
        if (placeqty == TowerLimit)
        {
            overPlaceLimit = true;
        }

    }
    private void InstantiateNewTower(WayPoint waypoint)
    {
        Tower newtower = Instantiate(towerPrefeb, waypoint.transform.position, Quaternion.identity) as Tower;
        newtower.transform.parent = InstantiateParent.transform;
        waypoint.isPlaceable = false;

        newtower.baseWaypoint = waypoint;
        towerQUeene.Enqueue(newtower);

    }

    private void MoveExistingTower(WayPoint newBaseWayPoint)
    {
        var olderTower = towerQUeene.Dequeue();
        olderTower.baseWaypoint.isPlaceable = true;
        newBaseWayPoint.isPlaceable = false;

        olderTower.baseWaypoint = newBaseWayPoint;
        olderTower.transform.position = newBaseWayPoint.transform.position;

        towerQUeene.Enqueue(olderTower);
    }


}
