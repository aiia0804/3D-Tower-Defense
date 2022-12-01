using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float MovingSpeed = 1f;
    [SerializeField] int HP = 3;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] GameObject body;

    List<WayPoint> path;
    List<WayPoint> mypath = new List<WayPoint>();


    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        path = pathFinder.getPath();
        foreach (var way in path)
        {
            mypath.Add(way);
        }
        Invoke(nameof(GetALonewithPath), 0f);
    }

    private void GetALonewithPath()
    {
        StartCoroutine(FollowPath(mypath));
    }


    IEnumerator FollowPath(List<WayPoint> path)
    {
        SwitchDisplay();
        foreach (WayPoint point in path)
        {
            transform.position = point.transform.position;

            yield return new WaitForSeconds(MovingSpeed);
        }
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        HP = HP - 1;
        hitVFX.Play();

        if (HP <= 0)
        {
            var DVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(DVFX.gameObject, DVFX.main.duration);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        }
    }

    public void SwitchDisplay()
    {
        body.SetActive(!body.activeSelf);
    }
}
