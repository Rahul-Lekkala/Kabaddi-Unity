using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject[] characters;
    GameObject currentCharacter;
    int characterIndex;
    public Vector3 offset;

    public GameObject[] defenders;
    GameObject currentDefender;
    static int defenderIndex;

    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        offset.x = 1.45f;
        offset.y = 2.35f;
        offset.z = -12;
    }

    // Update is called once per frame
    void Update()
    {
        Timer t = new Timer();
        SwitchCharacter sc = new SwitchCharacter();
        if (t.Team() == 0)
        {
            int i = sc.character();
            currentCharacter = characters[i];
            //player = sc.currentCharacter;
            //Debug.Log(currentCharacter.transform.position);
            transform.position = currentCharacter.transform.position + offset;
        }
        else if (t.Team() == 1)
        {
            int i = sc.defender();
            currentDefender = defenders[i];
            transform.position = currentDefender.transform.position + offset;
        }
    }
}
