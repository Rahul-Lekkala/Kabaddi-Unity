using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject[] characters;
    GameObject player;
    int characterIndex;
    CharacterController controller;
    Vector3 moveDir = Vector3.zero;

    private Animator anim;

    public float attackingDistance=10f;
    public float walkingDistance;
    public float alertDistance;
    public float speed;
    public float attackAngle;
    public static List<Vector3> startPositionList;

    public NavMeshAgent agent;

    public List<GameObject> WayPoints;
    public float remainingDistance;
    private int selectedDestination;
    public int maxTime;
    public int minTime;

    public bool attacked = false;
    public bool isAlive;
    // Start is called before the first frame update

    public Vector3 startPosition;
    // Start is called before the first frame update

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

    void Start()
    {
        //startPosition = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        isAlive = true;
        startPositionList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //ChangePosition();
        SwitchCharacter sc = new SwitchCharacter();
        int i = sc.character();
        player = characters[i];
        float Ed = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(Ed);
        //SetPosition();

        /*if(agent.enabled == true && agent.remainingDistance < remainingDistance)
        {
            agent.enabled = true;
            anim.SetInteger("speed", 0);
            anim.SetBool("isWalking", false);
           // StartCoroutine(RandomMovementtoWayPoint());
            //Debug.Log("Arrived ...");
        }*/

        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        /*if(Vector3.Distance(transform.position, player.transform.position) < alertDistance)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                FaceTarget();
                anim.SetBool("isProning", true);
                anim.SetInteger("speed", 1);
            }
            //agent.enabled = false;
           // Dodge();
        }*/

        if(Vector3.Distance(transform.position, player.transform.position) <= attackingDistance)// && Vector3.Distance(transform.position, player.transform.position) >= alertDistance)
        {
            //Debug.Log("In Red Circle "+ Vector3.Distance(transform.position, player.transform.position));
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
            FaceTarget();
            if (Vector3.Distance(transform.position, player.transform.position) <= 1.7f)//stoppingDistance)
            {
                //Debug.Log("Less Than Rem Dist : "+ Vector3.Distance(transform.position, player.transform.position));
                FaceTarget();
                anim.SetBool("isWalking", false);
                anim.SetBool("GrabLegs", false);
                anim.SetBool("isProning", true);
                anim.SetInteger("speed", 1);
                anim.Play("Prone Idle");
            }
            //Debug.Log("Attacking");
            //agent.enabled = false;
            //Vector3 targetDir = player.transform.position - transform.position;
            //float angle = Vector3.Angle(targetDir, transform.forward);
            //Debug.Log(angle);
            //if (angle < attackAngle)
            //{
            //    
            
            Attack();
            //}
        }
        // Idle
        else if(Vector3.Distance(transform.position, player.transform.position) > attackingDistance)
        {
            //Debug.Log("Outside circle " + Vector3.Distance(transform.position, player.transform.position));
            anim.SetInteger("speed", 0);
            anim.SetBool("isWalking", false);
            anim.SetBool("GrabLegs", false);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isDodging", false);
            StartCoroutine(RandomMovementtoWayPoint());
        }
        /*else
        {
            agent.enabled = true;
            Move();
            if(Vector3.Distance(transform.position, player.transform.position) <= attackingDistance)
            {
                agent.enabled = false;
                anim.SetBool("isWalking", false);
                anim.SetInteger("speed", 0);
                Attack();
                anim.SetBool("isWalking", false);
            }
        }*/

        if(attacked==true)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }
    }

    public void Idle()
    {
        anim.SetInteger("speed", 0);
        anim.SetBool("isWalking", false);
        anim.SetBool("GrabLegs", false);
        anim.SetBool("isWalkingBack", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDodging", false);
        anim.SetBool("isProning", false);
    }

    void Move()
    {
        //Debug.Log(player.transform.position);
        //agent.enabled = true;
        agent.SetDestination(player.transform.position);

        anim.SetBool("isWalking", true);
        anim.SetInteger("speed", 1);
    }

    void Dodge()
    {
        StartCoroutine(DodgeRoutine());
    }

    IEnumerator DodgeRoutine()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isProning", false);
        anim.SetBool("isDodging", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(1);
        anim.SetBool("isDodging", false);
        anim.SetInteger("speed", 0);
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        // Escape function
        //agent.enabled = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isProning", false);
        anim.SetBool("GrabLegs", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(3);

        // Surely dying 
        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            attacked = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("GrabLegs", false);
            anim.SetBool("isProning", true);
            //anim.SetBool("AttackedIdle", true);
            //anim.SetBool("AttackedMove", true);
            //PlayerDying();
        }
        else
        {
            anim.SetBool("isProning", false);
            attacked = false;
        }
        anim.SetBool("GrabLegs", false);
        anim.SetInteger("speed", 0);
        
    }

    void PlayerDying()
    {
        player.gameObject.active = false;
    }

    void SetPosition()
    {
        var center = player.transform.position;
        for (int i = 0; i < characters.Length; i++)
        {
            var pos = RandomCircle(center, 10);
            // make the object face the center
            var rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            //Instantiate(transform, pos, rot);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        // create random angle between 0 to 360 degrees 
        var ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos; 
    }

    public IEnumerator RandomMovementtoWayPoint()
    {
        int index = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(index);
        int index2 = Random.Range(1, 3);

        //Debug.Log(WayPoints.Count);
        switch(index2)
        {
            case 1:
                StartCoroutine(RandomMovementtoWayPoint());
                break;
            case 2:
                agent.enabled = true;

                int lastDestination = selectedDestination;
                selectedDestination = Random.Range(0, WayPoints.Count);
                //Debug.Log("Last Dest = "+ lastDestination);
                while(selectedDestination == lastDestination)
                    selectedDestination = Random.Range(0, WayPoints.Count);
                //Debug.Log("Selected Dest = " + selectedDestination);
                agent.SetDestination(WayPoints[selectedDestination].transform.position);
                anim.SetBool("isWalking", true);
                anim.SetInteger("speed", 1);
                anim.SetBool("GrabLegs", false);
                anim.SetBool("isWalkingBack", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isDodging", false);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackingDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,alertDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkingDistance);
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }
}
