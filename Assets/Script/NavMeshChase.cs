using UnityEngine;
using UnityEngine.AI;

public class NavMeshChase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool chasing = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[0].position);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 2f)
        {
            if (!chasing)
            {
                chasing = true;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            if (chasing)
            {
                chasing = false;
                agent.SetDestination(patrolPoints[currentPoint].position);
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolPoints.Length > 0)
            {
                int nextPoint = Random.Range(0, patrolPoints.Length);
                if (patrolPoints.Length > 1)
                {
                    while (nextPoint == currentPoint)
                        nextPoint = Random.Range(0, patrolPoints.Length);
                }
                currentPoint = nextPoint;
                agent.SetDestination(patrolPoints[currentPoint].position);
            }
        }
    }
}