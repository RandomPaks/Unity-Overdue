using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;

    // patrolling
    Vector3 walkPoint;
    bool isWalkPointSet = false;
    [SerializeField] float walkPointRange;

    // attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked = false;

    // states
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    bool playerInSightRange = false;
    bool playerInAttackRange = false; 

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        this.playerInSightRange = Physics.CheckSphere(this.transform.position, this.sightRange, playerLayer);
        this.playerInAttackRange = Physics.CheckSphere(this.transform.position, this.attackRange, playerLayer);

        if (!this.playerInSightRange && !this.playerInAttackRange)
        {
            this.PatrolState();
        }
        else if (this.playerInSightRange && !this.playerInAttackRange)
        {
            this.ChaseState();
        }
        else if (this.playerInSightRange && this.playerInAttackRange)
        {
            this.AttackState();
        }
    }

    private void PatrolState()
    {
        if (!this.isWalkPointSet)
        {
            this.SearchWalkPoint();
        }
        else
        {
            this.agent.SetDestination(this.walkPoint);
        }

        Vector3 distanceToWalkPoint = this.transform.position - this.walkPoint;
        // agent has reached destination
        if (distanceToWalkPoint.magnitude < 1f)
        {
            this.isWalkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-this.walkPointRange, this.walkPointRange);
        float randomZ = Random.Range(-this.walkPointRange, this.walkPointRange);

        this.walkPoint = new Vector3(this.transform.position.x + randomX, this.transform.position.y, this.transform.position.z + randomZ);

        if (Physics.Raycast(this.walkPoint, -this.transform.up, 2f, this.groundLayer))
        {
            this.isWalkPointSet = true;
        }
    }

    private void ChaseState()
    {
        this.agent.SetDestination(this.player.position);
    }

    private void AttackState()
    {
        // stop moving
        this.agent.SetDestination(this.transform.position);

        this.transform.LookAt(this.player);

        if (!this.alreadyAttacked)
        {
            Debug.Log("Attacked Player");
            this.alreadyAttacked = true;
            Invoke("ResetAttack", this.timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        this.alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.sightRange);
    }
}
