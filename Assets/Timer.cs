using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

    //private int timer = 30;
    //public CharacterController player;
    public Text timerValue;
    public float timeLeft = 30;
    float rightBorderLimit = 2.4f;

    //public GameObject[] characters;
    //GameObject currentCharacter;
    //int characterIndex;

    // Update is called once per frame
    void Update()
    {
        //SwitchCharacter sc = new SwitchCharacter();
        //int i = sc.character();
        //currentCharacter = characters[i];

        //player = currentCharacter.GetComponent<CharacterController>();

        //Debug.Log("*******" + playerController.value);
        //Debug.Log("----------------" + playerController.value.z);
        AttackAnim attackAnim = new AttackAnim();
        timerValue.text = timeLeft.ToString();
        //Debug.Log("AttackAnim.value"+ AttackAnim.value);
        if (AttackAnim.value >= rightBorderLimit)
        {
            timeLeft = (timeLeft - Time.deltaTime);
            //yield return new WaitForSeconds(1.0f);
            timerValue.text = timeLeft.ToString();
            UpdateLevelTimer(timeLeft);
            if (timeLeft <= 0.0f)
            {
                //Debug.Log("Danger Zone :");
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
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
}