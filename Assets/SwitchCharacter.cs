using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject currentCharacter;
    static int characterIndex;

    // Start is called before the first frame update
    void Start()
    {
        characterIndex = 0;
        currentCharacter = characters[0];
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnim an = new AttackAnim();
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

        
        
        //Debug.Log(an.getIsDead());

        if (an.getIsDead().Equals(true))
        {
            currentCharacter.SetActive(false);   
        }
    }

    public int character()
    {
        return characterIndex;
    }
}
