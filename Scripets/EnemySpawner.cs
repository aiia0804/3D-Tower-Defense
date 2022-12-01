using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefb;
    [SerializeField] float GenerationTime = 2f;
    [SerializeField] Text EnemyCounter;
    [SerializeField] AudioClip GenerationSFX;
    [SerializeField] int enemyLimit;

    //[SerializeField] int QtyForPerWave =5;
    int EnemyCount = 0;
    GameStatus gameStatus;

    bool WaveFinsihed = false;
    void Start()
    {
        StartCoroutine(GenerateEnemy());
        EnemyCounter.text = "Enemy Generation: " + EnemyCount.ToString();
        gameStatus = FindObjectOfType<GameStatus>();
    }


    IEnumerator GenerateEnemy()
    {
        while (!WaveFinsihed)
        {
            EnemyCount++;
            EnemyCounter.text = "Enemy Generation: " + EnemyCount.ToString();

            GetComponent<AudioSource>().PlayOneShot(GenerationSFX);
            GameObject newEnemy = Instantiate(EnemyPrefb, this.transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            newEnemy.GetComponent<Enemy>().SwitchDisplay();
            if (EnemyCount >= enemyLimit)
            {
                WaveFinsihed = true;
                gameStatus.ToGetLive();
                Invoke(nameof(LevelFinished), 5f);
            }
            yield return new WaitForSeconds(GenerationTime);
        }
    }

    private void LevelFinished()
    {
        var scene = FindObjectOfType<SceneControl>();
        scene.loadNextScene();
    }
}


