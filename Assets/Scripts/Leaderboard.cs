using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Leaderboard : MonoBehaviour
{
    public Text leaderBoardText;
    // Start is called before the first frame update
    void OnEnable()
    {
        string context = "[ 내 점수 ]\n\n";
        for (int i = 0; i< DBManager.Instance.playerScores.Count; i++)
        {
            context+= $"{i+1}. {DBManager.Instance.playerScores[i].timestamp}   {DBManager.Instance.playerScores[i].score}점\n";
        }
        leaderBoardText.text = context;
    }
}
