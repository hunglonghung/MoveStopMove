using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public float patrolRadius = 20f; 
    public float changeDestinationDistance = 1f; 

    [SerializeField] public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        MoveToRandomPosition(); 
        ChangeState(new BotIdleState());
    }

    void Update()
    {
        if (currentState == null) return;
        currentState.OnExecute(this);
        UpdateNewPosition();
    }

    public void UpdateNewPosition()
    {
        if (!agent.pathPending && agent.remainingDistance <= changeDestinationDistance)
        {
            MoveToRandomPosition(); 
        }
    }

    public void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius; 
        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, -1); 
        agent.SetDestination(navHit.position); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Bullet")
        {
            GetComponent<Character>().isDead = true;
        }
    }

    public bool CheckPathPending()
    {
        return !agent.pathPending;
    }
}
