using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode][SelectionBase]
[RequireComponent(typeof(WayPoint))]

public class CubeEditor : MonoBehaviour
{
    WayPoint waypoint;

    public void Awake()
    {
        waypoint = GetComponent<WayPoint>();
    }
    void Update()
    {
        SnapTheGridPos();
        UpdateToTheLabel();

    }

    private void SnapTheGridPos()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(waypoint.GetGridPos().x*gridSize, 0f, waypoint.GetGridPos().y * gridSize);
    }

    private void UpdateToTheLabel()
    {
        
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string LabelName = waypoint.GetGridPos().x  + "," + waypoint.GetGridPos().y  ;
        textMesh.text = LabelName;
        gameObject.name = LabelName;
    }
}