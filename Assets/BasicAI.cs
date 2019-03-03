using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] defenders;

    public GameObject[] charactersB;
    public GameObject[] defendersB;

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
    //public string [] playersDied;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        //won = false;
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player;
        Timer t = new Timer();
        SwitchCharacter sc = new SwitchCharacter();
               
        if (t.Team() == 0)
        {
            int i = sc.character();
            player = characters[i];
            AttackTeamA(player);
        }
        else if(t.Team() == 1)
        {
            int i = sc.defender();
            player = charactersB[i];
            AttackTeamB(player);
        }
    }
    
    void AttackTeamA(GameObject player)
    {
        int numberOfPlayersNotAttacking = 0;
        if (!won)
        {
            int numberOfPlayersAttacking = 0;
            for (int j = 0; j < defenders.Length; j++)
            {
                defender = defenders[j];
                float Ed = Vector3.Distance(defenders[j].transform.position, player.transform.position);
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
                    }
                    player.GetComponent<AttackAnim>().SetDead();
                    won = true;
                }
            }
        }
    }

    void AttackTeamB(GameObject player)
    {
        int numberOfPlayersNotAttacking = 0;
        if (!won)
        {
            int numberOfPlayersAttacking = 0;
            for (int j = 0; j < defendersB.Length; j++)
            {
                defender = defendersB[j];
                float Ed = Vector3.Distance(defendersB[j].transform.position, player.transform.position);
                defender.GetComponent<AIController>().enabled = true;

                if (Vector3.Distance(defendersB[j].transform.position, player.transform.position) < distance)
                {
                    defendersB[j].GetComponent<AIController>().isAlive = false;
                    Debug.Log(defendersB[j].GetComponent<AIController>().isAlive);
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
                if (numberOfPlayersNotAttacking == defendersB.Length)
                {
                    numberOfPlayersNotAttacking = 0;
                    attacked = false;
                }
                if (attacked == true && numberOfPlayersAttacking >= 3)
                {
                    for (int k = 0; k < characters.Length; k++)
                    {
                        defendersB[k].GetComponent<AIController>().Idle();
                        defendersB[k].GetComponent<AIController>().StopAllCoroutines();
                        defendersB[k].GetComponent<AIController>().enabled = false;
                    }
                    player.GetComponent<AttackAnim>().SetDead();
                    won = true;
                }
            }
        }
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
