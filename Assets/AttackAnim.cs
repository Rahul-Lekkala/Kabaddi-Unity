using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAnim : MonoBehaviour
{
    public GameObject[] defenders;
    GameObject defender;

    float speed = 2;
    float rotSpeed = 50;
    float rot = 0f;
    float gravity = 8;
    public bool isDead;
    public float value;
    public float distance;

    //TODO:: If player crosses the line, disable SwitchCharacter

    Vector3 moveDir = Vector3.zero;
    Vector3 startPosition;

    CharacterController controller;
    Animator anim;
    AIController aIController;

    public NavMeshAgent agent;
    public GameObject LosePosition;

    public GameObject Opponent;

    public float Value()
    {
        return value;
    }
    void Start()
    {
        //startPosition = transform.position;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        aIController = new AIController();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        isDead = false;
    }

    void Awake()
    {
        startPosition = transform.position;
        Debug.Log("The starting position of the players" + startPosition);
    }

    public void ChangePosition()
    {
        controller.enabled = false;
        controller.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        controller.enabled = true;

    }

    void Update()
    {

        CrossBorder();
        Movement();
        GetInput();
        if (isDead == true)
        {
            Dead();
            //RemovingPlayers();
        }

    }

    void RemovingPlayers()
    {
        //int numberOfPlayersAttacking = 0;
        //for (int j = 0; j < defenders.Length; j++)
        //{
        //    defender = defenders[j];
        //    float Ed = Vector3.Distance(defenders[j].transform.position, transform.position);
        //    //Debug.Log(j + " Dist " + Ed);
        //    //if(defender.GetComponent<AIController>().attacked==true)
        //    if (Ed < distance)
        //    {
        //        numberOfPlayersAttacking++;
        //    }
        //}
    }

    void Dead()
    {
        //BasicAI bi = new BasicAI();
        //bi.Won();
        Debug.Log("Dead...");
        agent.enabled = true;
        //agent.SetDestination(LosePosition.transform.position);
        anim.SetBool("isLost", true);
        anim.SetBool("AttackedMove", false);
        anim.SetBool("AttackedIdle", false);
        anim.SetBool("isAttacked", true);
        //SwitchCharacter sc = new SwitchCharacter();
        //sc.Lose();

        //Opponent.GetComponent<BasicAI>().enabled = false; 
        agent.enabled = true;
        agent.SetDestination(LosePosition.transform.position);
        if (Vector3.Distance(transform.position, LosePosition.transform.position) <= 1.7f)
        {
            //transform.gameObject.active = false;
        }

        transform.gameObject.active = false;

    }

    void CrossBorder()
    {
        //Debug.Log(controller.transform.position.z);
        if (controller.transform.position.z >= 1.56f)
        {
            //Debug.Log("Border Crossed");
        }
    }

    public void updatedposition()
    {
        value = transform.position.z;
    }
    void Movement()
    {
        //agent.enabled = true;
        // agent.SetDestination(LosePosition.transform.position);
        value = transform.position.z;
        //Debug.Log(transform.position);
        //Debug.Log("Movement called");
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("isAttacking") == true)
                {
                    return;
                }
                if (anim.GetBool("isDodging") == true)
                {
                    return;
                }
                if (anim.GetBool("isWalkingBack") == true)
                {
                    return;
                }
                else if (anim.GetBool("isAttacked") == false && anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalkingBack") == false)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetInteger("speed", 1);

                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
                else if (anim.GetBool("isAttacked") == true && anim.GetBool("isLost") == false && anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalkingBack") == false)
                {
                    anim.SetBool("AttackedMove", true);
                    anim.SetInteger("speed", 1);

                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetInteger("speed", 0);
                moveDir = new Vector3(0, 0, 0);
                if (BasicAI.attacked == true)
                {
                    anim.SetBool("AttackedMove", false);
                    anim.SetBool("AttackedIdle", true);
                    anim.SetInteger("speed", 1);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("isAttacking") == true)
                {
                    return;
                }
                if (anim.GetBool("isDodging") == true)
                {
                    return;
                }
                if (anim.GetBool("isWalking") == true)
                {
                    return;
                }
                else if (anim.GetBool("isAttacked") == false && anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalking") == false)
                {
                    anim.SetBool("isWalkingBack", true);
                    anim.SetInteger("speed", 1);

                    moveDir = new Vector3(0, 0, -1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
                else if (anim.GetBool("isAttacked") == true && anim.GetBool("isLost") == false && anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalkingBack") == false)
                {
                    anim.SetBool("AttackedMove", true);
                    anim.SetInteger("speed", 1);

                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("isWalkingBack", false);
                anim.SetInteger("speed", 0);
                moveDir = new Vector3(0, 0, 0);
                if (BasicAI.attacked == true)
                {
                    anim.SetBool("AttackedMove", false);
                    anim.SetBool("AttackedIdle", true);
                    anim.SetInteger("speed", 1);
                }
            }
            if (BasicAI.attacked == true)
            {
                // Debug.Log("Attacked is working...");
                if (anim.GetBool("isLost") == false)
                {
                    Attacked();
                }
            }
            if (BasicAI.attacked == false)
            {
                // Debug.Log("... Not Attacking");
                //anim.SetInteger("speed", 0);
                anim.SetBool("isAttacked", false);
                anim.SetBool("AttackedIdle", false);
                anim.SetBool("AttackedMove", false);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                if (anim.GetBool("isWalking") == true)
                {
                    anim.SetBool("isWalking", false);
                }
                if (anim.GetBool("isWalkingBack") == true)
                {
                    anim.SetBool("isWalkingBack", false);
                }
                if (anim.GetBool("isWalking") == false && anim.GetBool("isWalkingBack") == false)
                {
                    Attacking();
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (anim.GetBool("isWalking") == true)
                {
                    anim.SetBool("isWalking", false);
                }
                if (anim.GetBool("isWalkingBack") == true)
                {
                    anim.SetBool("isWalkingBack", false);
                }
                if (anim.GetBool("isWalking") == false && anim.GetBool("isWalkingBack") == false)
                {
                    Dodging();
                }
            }
        }
    }

    void Attacked()
    {
        StartCoroutine(AttackedRoutine());
    }

    IEnumerator AttackedRoutine()
    {
        anim.SetBool("isAttacked", true);
        anim.SetBool("AttackedIdle", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(2);
        anim.SetBool("AttackedIdle", false);
        anim.SetBool("AttackedMove", true);
        //anim.SetInteger("speed", 0);
    }

    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("isAttacking", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(1);
        anim.SetBool("isAttacking", false);
        anim.SetInteger("speed", 0);
    }

    void Dodging()
    {
        StartCoroutine(DodgeRoutine());
    }

    IEnumerator DodgeRoutine()
    {
        anim.SetBool("isDodging", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(1);
        anim.SetBool("isDodging", false);
        anim.SetInteger("speed", 0);
    }

    public void SetDead()
    {
        isDead = true;
        return;
    }
}
