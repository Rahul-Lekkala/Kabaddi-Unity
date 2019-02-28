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
    public float distance;
    public float alertDistance;
    public float speed;
    public float attackAngle;
    public List<GameObject> WayPoints;
    public float remainingDistance;
    private int selectedDestination;
    public int maxTime;
    public int minTime;
    public static bool attacked = false;
    public static bool won;
    public static int score;
    public static string playerName;
    public string [] playersDied;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        won = false;
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacter sc = new SwitchCharacter();
        int i = sc.character();
        player = characters[i];
        int numberOfPlayersNotAttacking = 0;
        if (!won)
        {
            int numberOfPlayersAttacking = 0;
            for (int j = 0; j < defenders.Length; j++)
            {
                defender = defenders[j];
                float Ed = Vector3.Distance(defenders[j].transform.position, player.transform.position);
                //Debug.Log(j + " Dist " + Ed);
                defender.GetComponent<AIController>().enabled = true;

                if (Vector3.Distance(defenders[j].transform.position, player.transform.position) < distance)
                {
                    defenders[j].GetComponent<AIController>().isAlive = false;
                    Debug.Log(defenders[j].GetComponent<AIController>().isAlive);
                    attacked = true;
                    numberOfPlayersAttacking++;
                    AttackedPlayers(numberOfPlayersAttacking);
                }
                if (attacked == true && defender.GetComponent<AIController>().agent == true)
                {
                    defender.GetComponent<AIController>().agent.SetDestination(player.transform.position);
                }
                
                if (Ed > attackingDistance && attacked)
                {
                    numberOfPlayersNotAttacking++;
                }
                if (numberOfPlayersNotAttacking == defenders.Length)
                {
                    numberOfPlayersNotAttacking = 0;
                    attacked = false;
                }
                if (attacked == true && numberOfPlayersAttacking >= 3)
                {
                    for (int k = 0; k < defenders.Length; k++)
                    {
                        defenders[k].GetComponent<AIController>().Idle();
                        defenders[k].GetComponent<AIController>().StopAllCoroutines();
                        defenders[k].GetComponent<AIController>().enabled = false;
                        playersDied[k] = defenders[k].name;
                        Debug.Log("Players died is :" + playersDied[k]);
                    }
                    player.GetComponent<AttackAnim>().SetDead();
                    won = true;

                    //AttackedPlayers(numberOfPlayersAttacking);
                }
            }
        }
        //Debug.Log("numberOfPlayersAttacking = " + numberOfPlayersAttacking);
    }
    
    public void AttackedPlayers(int x)
    {
        if (x > score)
        {
            score = x;
        }
        //Debug.Log("The score of team is :" + score);
    }
}
