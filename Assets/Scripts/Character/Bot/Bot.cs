using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public float patrolRadius = 20f; // Bán kính tuần tra của bot
    public float changeDestinationDistance = 1f; // Khoảng cách để thay đổi điểm đến

    public NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        MoveToRandomPosition(); 
        OnInit(); // Khởi tạo bot để đảm bảo trạng thái ban đầu được thiết lập
    }

    public void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= changeDestinationDistance)
        {
            MoveToRandomPosition(); 
        }
        
        // Log trạng thái hiện tại và tên animation để debug
        if (currentState != null)
        {
            Debug.Log("Current State: " + currentState.GetType().Name);
        }
        else
        {
            Debug.Log("Current State: null");
        }

        Debug.Log("Current Anim Name: " + currentAnimName);
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
}
