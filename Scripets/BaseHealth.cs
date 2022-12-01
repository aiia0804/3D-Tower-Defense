using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] Text HPDisplay;
    GameStatus gameStatus;

    private void Start()
    {
        HPDisplay.text = "HP :" + health.ToString();
    }
    public int ReturnHealth()
    {
        return health;
    }
    private void OnTriggerEnter(Collider other)
    {
        health--;
        HPDisplay.text = "HP :" + health.ToString();
    }
}
