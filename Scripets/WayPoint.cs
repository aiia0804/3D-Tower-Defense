using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    const int gridSize = 10;
    Vector2Int gridPos;

    //It is nessecary to set as public
    public bool isExplored = false;
    public WayPoint exploredFrom;
    public bool isPlaceable = true;


    public Vector2Int GetGridPos()
    {
        gridPos = new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
        );
        return gridPos;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public void setColor(Color color)
    {
        MeshRenderer topMeshRender = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRender.material.color = color;

    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            FindObjectOfType<TowerFactory>().ReplaceTower(this);
        }
        else
        {
            print("Can't place here");
        }
    }
}
