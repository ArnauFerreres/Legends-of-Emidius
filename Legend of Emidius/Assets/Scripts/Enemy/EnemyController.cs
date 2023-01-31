using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum MovementStates
    {
        Initial, Patrolling, Chase, Attack
    }

    private MovementStates enemyState;


    [Header("Patrolling Settings")]
    [SerializeField] private float patrollingStopTime = 2f;
    [SerializeField] private List<Transform> patrolList;

    Vector3 walkPoint;
    bool walkPointSet;
    float currentPatrollingStopTime;
    int currentPatrollingPosition = 0;
    NavMeshAgent agent;

    [Header("Chase Settings")]
    [SerializeField] private float sightRange = 7f;
    [SerializeField] private LayerMask playerLayer;
    bool playerInSightRange;

    Transform player;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.75f;
    [SerializeField] private float timeBetweenAtttacks = 0.75f;
    bool alreadyAttack;
    bool playerInAttackRange;

    [Header("Animation Settings")]
    [SerializeField] private float acceleration = 1.5f;
    [SerializeField] private float deceleration = 0.75f;
    [SerializeField] private SphereCollider sphereCollider;
    float enemySpeed;
    Animator anim;


    void Start()
    {
        enemyState = MovementStates.Initial;

        agent=GetComponent<NavMeshAgent>();
        currentPatrollingStopTime = patrollingStopTime;
        walkPointSet = true;
        walkPoint = patrolList[currentPatrollingPosition].position;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        StateUpdate();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }
    private void ChangeState(MovementStates newState)
    {
        //outcoming
        switch (enemyState)
        {
            case MovementStates.Initial:
                break;
            case MovementStates.Patrolling:
                break;
            case MovementStates.Chase:
                break;
            case MovementStates.Attack:
                break;
            default:
                break;
        }

        //incoming
        switch (newState)
        {
            case MovementStates.Initial:
                break;
            case MovementStates.Patrolling:
                agent.speed = 1.75f;
                agent.angularSpeed = 150;
                break;
            case MovementStates.Chase:
                agent.speed = 3.5f;
                agent.angularSpeed = 250;
                break;
            case MovementStates.Attack:
                break;
            default:
                break;
        }
        enemyState = newState;
    }
    private void StateUpdate()
    {
        switch (enemyState)
        {
            case MovementStates.Initial:
                ChangeState(MovementStates.Patrolling);
                break;
            case MovementStates.Patrolling:
                Patrolling();
                if(playerInSightRange && !PathInvalid())
                    ChangeState(MovementStates.Chase);
                break;
            case MovementStates.Chase:
                ChasePlayer();
                if(!playerInSightRange)
                    ChangeState(MovementStates.Patrolling);
                if(playerInSightRange)
                    ChangeState(MovementStates.Attack);
                break;
            case MovementStates.Attack:
                if(!playerInAttackRange && !alreadyAttack)
                    ChangeState(MovementStates.Chase);
                AttackPlayer();
                break;
            default:
                break;
        }
    }

    private void Patrolling()
    {
        if(!walkPointSet) SearchPatrolPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude <0.1f)
        {
            if(currentPatrollingStopTime <= 0)
            {
                walkPointSet = false;
            }
            else
            {
                currentPatrollingStopTime -= Time.deltaTime;
            }
            if(enemySpeed > 0)
                enemySpeed -= deceleration *Time.deltaTime;
            else
                enemySpeed = 0;
        }
        else
        {
            if (enemySpeed > 0.5f)
                enemySpeed -= deceleration * Time.deltaTime;
            else
            {
                if(enemySpeed < 0.5f)
                    enemySpeed+=acceleration *Time.deltaTime;
                else
                enemySpeed = 0.5f;
            }

        }
        anim.SetFloat("Speed", enemySpeed);
    }

    private void SearchPatrolPoint()
    {
        currentPatrollingStopTime = patrollingStopTime;
        currentPatrollingPosition++;

        if(currentPatrollingPosition >= patrolList.Count)
            currentPatrollingPosition = 0;

        walkPoint = patrolList[currentPatrollingPosition].position;
        walkPointSet = true;

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        if (PathInvalid())
            ChangeState(MovementStates.Patrolling);

        if (enemySpeed < 0.85f)
            enemySpeed += acceleration * Time.deltaTime;
        else
            enemySpeed = 0.85f;
        anim.SetFloat("Speed", enemySpeed);
    }


    private bool PathInvalid()
    {
        NavMeshPath path = agent.path;
        agent.CalculatePath(player.position, path);

        if (path.status == NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }

    private void AttackPlayer()
    {
        if (enemyState == MovementStates.Chase)
            return;
        agent.SetDestination(transform.position);

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.LookAt(targetPosition);
        if(!alreadyAttack)
        {
            Debug.Log("Attacking player");
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAtttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
