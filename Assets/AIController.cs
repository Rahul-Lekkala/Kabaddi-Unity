using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject[] characters;
    GameObject player;
    int characterIndex;

    private Animator anim;

    public float attackingDistance;
    public float walkingDistance;
    public float alertDistance;
    public float speed;
    public float attackAngle;

    private NavMeshAgent agent;

    public List<GameObject> WayPoints;
    public float remainingDistance;
    private int selectedDestination;
    public int maxTime;
    public int minTime;
    bool attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacter sc = new SwitchCharacter();
        int i = sc.character();
        player = characters[i];
        float Ed = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(Ed);
        //SetPosition();

        if(agent.enabled == true && agent.remainingDistance < remainingDistance)
        {
            agent.enabled = true;
            anim.SetInteger("speed", 0);
            anim.SetBool("isWalking", false);
           // StartCoroutine(RandomMovementtoWayPoint());
            //Debug.Log("Arrived ...");
        }

        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if(Vector3.Distance(transform.position, player.transform.position) < alertDistance)
        {
            agent.enabled = false;
           // Dodge();
        }

        else if(Vector3.Distance(transform.position, player.transform.position) <= attackingDistance)// && Vector3.Distance(transform.position, player.transform.position) >= alertDistance)
        {
            //Debug.Log("Attacking");
            agent.enabled = false;
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
        
        anim.SetBool("isWalking", false);
        anim.SetBool("GrabLegs", true);
        anim.SetInteger("speed", 1);
        yield return new WaitForSeconds(2);
        
        // Surely dying 
        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            attacked = true;
            PlayerDying();
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
}
