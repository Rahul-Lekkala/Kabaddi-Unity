using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject[] characters;
    GameObject currentCharacter;
    int characterIndex;
    public Vector3 offset;
    
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        offset.x = 1.45f;
        offset.y = 2.84f;
        offset.z = -5.93f;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacter sc = new SwitchCharacter();
        int i = sc.character();
        currentCharacter = characters[i];
        //player = sc.currentCharacter;
        //Debug.Log(currentCharacter.transform.position);
        transform.position = currentCharacter.transform.position + offset; 
    }
}
