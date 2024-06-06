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
        BulletPool.Instance.CreatePool(WeaponType, Weapon.GetBulletByWeaponType(WeaponType)); 
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
        Vector3 randomDirection;
        NavMeshHit navHit;
        bool validPositionFound = false;
        while (!validPositionFound)
        {
            randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            if (NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, -1))
            {
                validPositionFound = true; 
                agent.SetDestination(navHit.position);
            }
        }
       
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
