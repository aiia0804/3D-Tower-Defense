using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform ObjectToPan;
    [SerializeField] GameObject Fire;
    [SerializeField] float FireRange = 30f;

    public WayPoint baseWaypoint;

    void Update()
    {
        SetTargetEnemy();

        if (target)
        {
            ObjectToPan.LookAt(target);
            ProcessFire();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        Enemy[] scenesEnemy = FindObjectsOfType<Enemy>();
        if (scenesEnemy.Length == 0) { return; }

        Transform closetEnemy = scenesEnemy[0].transform;

        foreach (Enemy enemy in scenesEnemy)
        {
            closetEnemy = GetCloset(closetEnemy, enemy.transform);
        }

        target = closetEnemy;
    }

    private Transform GetCloset(Transform transformA, Transform transformB)
    {
        var distoA = Vector3.Distance(transform.position, transformA.position);
        var distoB = Vector3.Distance(transform.position, transformB.position);

        if (distoA < distoB)
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    private void ProcessFire()
    {
        bool DistanceDetect = Mathf.Abs(target.transform.position.x - this.transform.position.x) <= FireRange;

        if (DistanceDetect)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }

    }

    private void Shoot(bool shoot)
    {
        var Shouting = Fire.GetComponent<ParticleSystem>().emission;

        if (shoot)
        {
            Shouting.enabled = true;
        }
        else
        {
            Shouting.enabled = false;
        }
    }
}
