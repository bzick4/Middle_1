using UnityEngine;
using UnityEngine.AI;

public class NavMeshPatrol
 : MonoBehaviour
{
    [SerializeField] Transform _Player;
    [SerializeField] Transform[] _PatrolPoints;
    [SerializeField] float _Distance = 5f;
    private int _currentPoint = 0;
    private bool _isChasing = false;
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    private void Awake()
    {
        if (_PatrolPoints.Length > 0)
            _agent.SetDestination(_PatrolPoints[0].position);
    }

    private void Update()
    {
        ChaseAgent();
    }

    private void PatrolAgent()
    {
        int nextPoint = Random.Range(0, _PatrolPoints.Length);

        if (_PatrolPoints.Length > 1)
        {
            while (nextPoint == _currentPoint)
                nextPoint = Random.Range(0, _PatrolPoints.Length);
        }
        _currentPoint = nextPoint;
        _agent.SetDestination(_PatrolPoints[_currentPoint].position);
            
    }

    private void ChaseAgent()
    {
         if (_Player == null) return;

        float distance = Vector3.Distance(transform.position, _Player.position);

        if (distance <= _Distance)
        {
            if (!_isChasing)
            {
                _isChasing = true;
                _agent.speed = 5f;
                _agent.SetDestination(_Player.position);
            }
        }
        else
        {
            if (_isChasing)
            {
                _isChasing = false;
                _agent.speed = 2f; // Reset speed for patrolling
                _agent.SetDestination(_PatrolPoints[_currentPoint].position);
            }
            
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                PatrolAgent();
        }
    }
}

