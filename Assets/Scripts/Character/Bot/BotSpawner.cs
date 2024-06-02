using UnityEngine;
using UnityEngine.AI;

public class BotSpawner : MonoBehaviour
{
    public BotList botList; 
    public GameObject botPrefab; 
    public int numberOfBots = 10;
    public float spawnRadius = 20f;

    void Start()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            SpawnBot();
        }
    }

    void SpawnBot()
    {

        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition += transform.position;
        randomPosition.y = 0; 
        GameObject bot = Instantiate(botPrefab, randomPosition, Quaternion.identity);
    }
}
