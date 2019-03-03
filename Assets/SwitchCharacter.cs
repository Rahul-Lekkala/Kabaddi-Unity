using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] defenders;

    public GameObject currentCharacter;
    public GameObject currentDefender;

    static int characterIndex;
    static int defenderIndex;

    public GameObject LosePosition;

    // Start is called before the first frame update
    void Start()
    {
        characterIndex = 0;
        defenderIndex = 0;
        currentCharacter = characters[0];
        currentDefender = defenders[0];
    }

    // Update is called once per frame
    void Update()
    {
        Timer t = new Timer();
        AttackAnim an = new AttackAnim();
        Debug.Log("Team Value = " + t.Team());
        if (t.Team() == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterIndex++;
                if (characterIndex == characters.Length)
                {
                    characterIndex = 0;
                }
                //currentCharacter.GetComponent<CharacterController>().enabled = false;
                currentCharacter.GetComponent<AttackAnim>().enabled = false;
                //characters[characterIndex].GetComponent<CharacterController>().enabled = true;
                characters[characterIndex].GetComponent<AttackAnim>().enabled = true;
                currentCharacter = characters[characterIndex];
            }
        }
        else if(t.Team() == 1)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                defenderIndex++;
                if (defenderIndex == defenders.Length)
                {
                    defenderIndex = 0;
                }
                //currentCharacter.GetComponent<CharacterController>().enabled = false;
                currentDefender.GetComponent<AttackAnim>().enabled = false;
                //characters[characterIndex].GetComponent<CharacterController>().enabled = true;
                defenders[defenderIndex].GetComponent<AttackAnim>().enabled = true;
                currentDefender = defenders[defenderIndex];
            }
        }
    }

    public int character()
    {
        return characterIndex;
    }

    public int defender()
    {
        return defenderIndex;
    }

    /*public void Lose()
    {
        currentCharacter.GetComponent<AttackAnim>().agent.SetDestination(LosePosition.transform.position);
        if (Vector3.Distance(transform.position, LosePosition.transform.position) <= 1.7f)
        {
            currentCharacter.gameObject.active = false;
        }
    }*/
}
