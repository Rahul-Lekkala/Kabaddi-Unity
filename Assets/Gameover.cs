using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gameover : MonoBehaviour
{
    public GameObject GameoverUI;
    public TextMeshProUGUI WinnerTeam;
    public TextMeshProUGUI Result;

    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed .....");
            //GameOver();
            //GameoverUI.SetActive(true);
        }
    }
    public void GameOver()
    {
        Timer t = new Timer();
        GameoverUI.SetActive(true);
        Time.timeScale = 1f;
        if (t.TeamAScoreValue > t.TeamBScoreValue)
        {
            WinnerTeam.text = "TeamA";
            Result.text = "CONGRATULATIONS";
        }
        else if (t.TeamAScoreValue < t.TeamBScoreValue)
        {
            WinnerTeam.text = "TeamB";
            Result.text = "CONGRATULATIONS";
        }
        else
        {
            //WinnerTeam.text = "Match Tied";
            Result.text = "MATCH TIED";
        }
    }
}
