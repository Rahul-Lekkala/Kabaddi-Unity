using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public Text timerValue;
    public float gameTimeLeft = 50.0f;
    public float timeLeft = 30;
    float rightBorderLimit = 2.4f;
    public Text TeamAScore;
    public Text TeamBScore;
    public int TeamAScoreValue;
    public int TeamBScoreValue;
    bool flag = true;
    bool inAction = false;
    bool gameTime = true;
    public GameObject[] defenders;
    GameObject defender;

    public GameObject GameoverUI;
    public TextMeshProUGUI WinnerTeam;
    public TextMeshProUGUI Result;

    void Update()
    {
        GameTimeLeft();
        AttackAnim attackAnim = new AttackAnim();
        timerValue.text = timeLeft.ToString();

        if (AttackAnim.value >= rightBorderLimit)
        {
            inAction = true;
            timeLeft = (timeLeft - Time.deltaTime);
            timerValue.text = ((int)timeLeft).ToString();
            UpdateLevelTimer(timeLeft);
            if (timeLeft <= 0.0f || BasicAI.won)
            {
                if (flag)
                {
                    TeamAScoreValue += 1;
                    TeamAScore.text = TeamAScoreValue.ToString();
                    flag = false;
                    //Screen.sleepTimeout = SleepTimeout.NeverSleep;
                }
            }
        }
        //Debug.Log("numberOfPlayersAttacking = " + BasicAI.score);
        if (inAction)
        {
            if (AttackAnim.value <= rightBorderLimit)
            {
                //BasicAI bi = new BasicAI();
                //Debug.Log("numberOfPlayersAttacking = " + BasicAI.score);
                int score = KillDefenders();
                TeamBScoreValue += score;
                TeamBScore.text = TeamBScoreValue.ToString();
                inAction = false;
                
            }
        }
        //if (basicai.won)
        //{
        //    teamascorevalue += 1;
        //    teamascore.text = teamascorevalue.tostring();
        //}
    }
    public void GameTimeLeft()
    {
        if (gameTime)
        {
            gameTimeLeft = (gameTimeLeft - Time.deltaTime);
            UpdateLevelTimer(gameTimeLeft);
            if (gameTimeLeft <= 0.0f)
            {
                //Gameover go = new Gameover();
                GameOver();
                gameTime = false;
            }
        }
    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
    }

    int KillDefenders()
    {
        int score = 0 ;
        for (int j = 0; j < defenders.Length; j++)
        {
            if(!defenders[j].GetComponent<AIController>().isAlive)
            {
                defenders[j].gameObject.active = false;
                score++;
            }
        }
        return score;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        //Timer t = new Timer();
        GameoverUI.SetActive(true);
        Time.timeScale = 1f;
        if (TeamAScoreValue > TeamBScoreValue)
        {
            WinnerTeam.text = "TeamA";
            Result.text = "CONGRATULATIONS";
        }
        else if (TeamAScoreValue < TeamBScoreValue)
        {
            WinnerTeam.text = "TeamB";
            Result.text = "CONGRATULATIONS";
        }
        else
        {
            Result.text = "MATCH TIED";
        }
    }
}