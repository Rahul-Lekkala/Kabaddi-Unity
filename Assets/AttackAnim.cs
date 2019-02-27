using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    float speed = 2;
    float rotSpeed = 50;
    float rot = 0f;
    float gravity = 8;
    static bool isDead;
    public static float value;

    //TODO:: If player crosses the line, disable SwitchCharacter

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //isDead = false;
    }

    void Update()
    {
        CrossBorder();
        Movement();
        GetInput();
        SetDead();
        //isDead = false;
        
    }

    public bool getIsDead()
    {
        return isDead;
    }

    void CrossBorder()
    {
        //Debug.Log(controller.transform.position.z);
        if(controller.transform.position.z>=1.56f)
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
        value = transform.position.z;
        Debug.Log(transform.position);
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
                else if (anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalkingBack") == false)
                {
                    anim.SetBool("isWalking", true);
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
                else if (anim.GetBool("isAttacking") == false && anim.GetBool("isDodging") == false && anim.GetBool("isWalking") == false)
                {
                    anim.SetBool("isWalkingBack", true);
                    anim.SetInteger("speed", 1);

                    moveDir = new Vector3(0, 0, -1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("isWalkingBack", false);
                anim.SetInteger("speed", 0);
                moveDir = new Vector3(0, 0, 0);
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

    void SetDead()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isDead = true;
        }
    }
}
