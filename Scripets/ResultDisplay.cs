using UnityEngine;
using TMPro;
public class ResultDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI result_tag;
    [SerializeField] TextMeshProUGUI live_tag;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        result_tag.text = "Total Moves: " + gameStatus.GetMoves().ToString();
        live_tag.text = "Life Left: " + gameStatus.GetLive().ToString();
    }

}