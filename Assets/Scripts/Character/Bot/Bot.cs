using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public float patrolRadius = 20f; 
    public float changeDestinationDistance = 1f; 

    [SerializeField] public NavMeshAgent agent;
    public override void OnInit()
    {
        agent = GetComponent<NavMeshAgent>(); 
        MoveToRandomPosition(); 
        ChangeState(new BotIdleState());
        SetWeapon(Weapon);
        SetSkin(Skin); 
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

    public bool CheckPathPending()
    {
        return !agent.pathPending;
    }
    public void StopMoving()
    {
        agent.isStopped = true;
    }
    public void StartMoving()
    {
        agent.isStopped = false;
    }
}
