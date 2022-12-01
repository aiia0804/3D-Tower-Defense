using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int totalMoves;
    private int totalLives;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddtoMoves(int move)
    {
        totalMoves += move;
    }

    public void ToGetLive()
    {
        totalLives += FindObjectOfType<BaseHealth>().ReturnHealth();
    }

    public int GetLive()
    {
        return totalLives;
    }

    public int GetMoves()
    {
        return totalMoves;
    }

    public void ResetResult()
    {
        totalLives = 0;
        totalMoves = 0;
    }



}
