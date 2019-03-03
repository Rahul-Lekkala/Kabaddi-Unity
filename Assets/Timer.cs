using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject[] teams;
    public GameObject[] characters;
    public GameObject[] charactersB;
    public GameObject[] defenders;
    public GameObject[] defendersB;

    GameObject player;
    public GameObject Opponent;

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
    
    GameObject defender;
    public GameObject GameoverUI;
    public TextMeshProUGUI WinnerTeam;
    public TextMeshProUGUI Result;
    public Texture3D texture;

    public Material[] material;
    Renderer rend;
    static int team;

    private void Start()
    {
        team = 0;
        //rend.enabled = true;
        //rend.sharedMaterial = material[0];
    }
    void Update()
    { 
        GameTimeLeft();

        SwitchCharacter sc = new SwitchCharacter();
        GameObject player;
        float limit = 0f ;
        if (team == 0)
        {
            int i = sc.character();
            player = characters[i];
            limit = player.GetComponent<AttackAnim>().Value();
        }
        else if (team == 1)
        {
            int i = sc.defender();
            player = charactersB[i];
            limit = player.GetComponent<AttackAnim>().Value();
        }

        //AttackAnim attackAnim = new AttackAnim();
        timerValue.text = timeLeft.ToString();

        if (limit >= rightBorderLimit)
        {
            inAction = true;
            timeLeft = (timeLeft - Time.deltaTime);
            timerValue.text = ((int)timeLeft).ToString();
            UpdateLevelTimer(timeLeft);
            if (timeLeft <= 0.0f || BasicAI.won)
            {
                if (flag)
                {
                    if (team == 0)
                    {
                        TeamAScoreValue += 1;
                        TeamAScore.text = TeamAScoreValue.ToString();
                    }
                    else if(team == 1)
                    {
                        TeamBScoreValue += 1;
                        TeamBScore.text = TeamBScoreValue.ToString();
                    }
                    flag = false;
                }
                //SwitchTeam();
            }
            
        }
        //Debug.Log("numberOfPlayersAttacking = " + BasicAI.score);
        if (inAction)
        {
            if (limit <= rightBorderLimit)
            {
                //BasicAI bi = new BasicAI();
                //Debug.Log("numberOfPlayersAttacking = " + BasicAI.score);
                
                if (team == 0)
                {
                    int score = KillDefenders();
                    TeamBScoreValue += score;
                    TeamBScore.text = TeamBScoreValue.ToString();
                }
                else if (team == 1)
                {
                    int score = KillDefendersB();
                    TeamAScoreValue += score;
                    TeamAScore.text = TeamAScoreValue.ToString();
                }
                    inAction = false;
                if(team==0)
                {
                    team = 1;
                }
                else
                {
                    team = 0;
                }
                SwitchTeam();
            }
        }
        //if (basicai.won)
        //{
        //    teamascorevalue += 1;
        //    teamascore.text = teamascorevalue.tostring();
        //}
    }
    public void SwitchTeam()
    {
        BasicAI.won = false;
        //teams[0].gameObject.active = false;
        for (int j = 0; j < teams.Length; j++)
        {
            //if(teams[j].gameObject.active)
            //{
                teams[j].gameObject.active = !teams[j].gameObject.active;
            //}
        }
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
                charactersB[j].gameObject.active = false;
                score++;
            }
        }

        int peopleAlive = score;
        for(int i=0;i<characters.Length;i++)
        {
            if((!defendersB[i].gameObject.active||!characters[i].gameObject.active) && peopleAlive > 0)
            {
                defendersB[i].gameObject.active = true;
                characters[i].gameObject.active = true;
                peopleAlive--;
            }
        }

        return score;
    }

    int KillDefendersB()
    {
        int score = 0;
        for (int j = 0; j < defendersB.Length; j++)
        {
            if (!defendersB[j].GetComponent<AIController>().isAlive)
            {
                defendersB[j].gameObject.active = false;
                characters[j].gameObject.active = false;
                score++;
            }
        }

        int peopleAlive = score;
        for (int i = 0; i < characters.Length; i++)
        {
            if ((!defenders[i].gameObject.active||!charactersB[i].gameObject.active) && peopleAlive>0)
            {
                defenders[i].gameObject.active = true;
                charactersB[i].gameObject.active = true;
                peopleAlive--;
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

    public int Team()
    {
        return team;
    }
}