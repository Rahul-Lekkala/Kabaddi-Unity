using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] defenders;
    GameObject player;
    GameObject defender;
    int characterIndex;

    private Animator anim;

    public float attackingDistance = 10f;
    public float walkingDistance;
    public float alertDistance;
    public float speed;
    public float attackAngle;

    //private UnityEngine.AI.NavMeshAgent agent;

    public List<GameObject> WayPoints;
    public float remainingDistance;
    private int selectedDestination;
    public int maxTime;
    public int minTime;

    public static bool attacked = false;
    bool won = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacter sc = new SwitchCharacter();
        int i = sc.character();
        player = characters[i];
        int numberOfPlayersAttacking = 0;
        int numberOfPlayersNotAttacking = 0;

        for (int j = 0; j < defenders.Length && !won ; j++)
        {
            defender = defenders[j];
            float Ed = Vector3.Distance(defenders[j].transform.position, player.transform.position);
            //Debug.Log(j + " Dist " + Ed);
            defender.GetComponent<AIController>().enabled = true;
            //if(defender.GetComponent<AIController>().attacked==true)
            if(Ed<2f)
            {

                attacked = true;
                numberOfPlayersAttacking++;
/*
                if (Ed < attackingDistance)
                {
                    numberOfPlayersAttacking++;
                }
  */          }
            if(attacked==true && defender.GetComponent<AIController>().agent == true)
            {
                defender.GetComponent<AIController>().agent.SetDestination(player.transform.position);
            }
           //if(numberOfPlayersAttacking==defenders.Length)
         //  {
            //    numberOfPlayersAttacking = 0;
           // }
            if(Ed>attackingDistance)
            {
                numberOfPlayersNotAttacking++;
            }
            if(numberOfPlayersNotAttacking==defenders.Length)
            {
                numberOfPlayersNotAttacking = 0;
                attacked = false;
            }
            if(attacked==true && numberOfPlayersAttacking>=3)
            {
                for (int k = 0; k < defenders.Length; k++)
                {
                    defenders[k].GetComponent<AIController>().Idle();
                    defenders[k].GetComponent<AIController>().StopAllCoroutines();
                    defenders[k].GetComponent<AIController>().enabled = false;
                }
                player.GetComponent<AttackAnim>().SetDead();
                //defender.GetComponent<AIController>().enabled = false;
                //return;
                won = true;
            }
            Debug.Log("numberOfPlayersAttacking = "+numberOfPlayersAttacking);
            Debug.Log("distance "+ Ed);
        }
        
    }
    
    public void Won()
    {
        GameObject defender;
        //global defenders;
        for (int j = 0; j < defenders.Length; j++)
        {
            defender = defenders[j];
            defender.GetComponent<AIController>().enabled = false;
        }
    }
}
