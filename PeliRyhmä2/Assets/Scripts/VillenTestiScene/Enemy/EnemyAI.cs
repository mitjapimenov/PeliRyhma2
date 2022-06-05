using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Transform lookPoint;

    public LayerMask whatIsGround, whatIsPlayer;

    public Animator animator;

    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Shoot
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    /*public GameObject projectile; */      //AMMUS KOODI

    //States
    public float sightRange, shootRange;
    public bool playerInSightRange, playerInShootRange;    

    private void Awake()
    {
        player = GameObject.Find("Player2").transform;
        lookPoint = GameObject.Find("LookPoint").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInShootRange = Physics.CheckSphere(transform.position, shootRange, whatIsPlayer);

        if (!playerInSightRange && !playerInShootRange)
        {
            Patrol();
            animator.Play("Armature|walk");
        }  
        
        if (playerInSightRange && !playerInShootRange)
        {
            ChasePlayer();
            animator.Play("Armature|walk");
        }   
            
        if (playerInSightRange && playerInShootRange)
        {
            ShootPlayer();
            animator.Play("Armature|attack");
        }            
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void ShootPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.GetChild(0).GetChild(1).GetChild(4).GetChild(2).GetChild(0).LookAt(player.position);
        //transform.GetChild(0).GetChild(1).GetChild(4).GetChild(2).GetChild(1).LookAt(player.position);
        //transform.LookAt(lookPoint.position);

        Vector3 lookAtPosition = lookPoint.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);

        if (!alreadyAttacked)
        {
            /*Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();*/      // AMMUS KOODI
            /*rb.AddForce(transform.forward * 32f, ForceMode.Impulse);  */      //AMMUS KOODI

            alreadyAttacked = true;
            Invoke(nameof(ResetShoot), timeBetweenAttacks);
        }

        if (!playerInSightRange && !playerInShootRange)
        {
            Patrol();
        }
    }

    private void ResetShoot()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
