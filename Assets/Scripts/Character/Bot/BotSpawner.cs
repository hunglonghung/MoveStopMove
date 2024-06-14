using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class BotSpawner : MonoBehaviour
{
    public static BotSpawner Instance;
    [SerializeField] private int poolSize = 20;
    private int spawnedBotNumber = 0;
    [SerializeField] private int targetNumber = 50;
    private List<GameObject> botPools;
    public GameObject botPrefab; 
    public float spawnRadius = 50f;
    public float minDistance = 10f; // prevent dead when spawning
    [SerializeField] public Player player;
    private void Awake()
    {
        Instance = this;
        botPools = new List<GameObject>();
    }
    private void Start() 
    {
        CreateBotPool();
    }
    private void Update() 
    {
        SpawnBot();
        if(spawnedBotNumber == targetNumber && CheckBotRemaining() == 0)
        {
            player.isWin = true;
        }
    }
    public void CreateBotPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bot = Instantiate(botPrefab);
            bot.SetActive(false);
            botPools.Add(bot);
        }
    }
    public Vector3 GetValidPosition()
    {
        Vector3 randomPosition;
        bool validPosition;

        do
        {
            validPosition = true;
            randomPosition = UnityEngine.Random.insideUnitSphere * spawnRadius;
            randomPosition += transform.position;
            randomPosition.y = 2;
            foreach (GameObject spawnedBot in botPools)
            {
                if (spawnedBot != null && Vector3.Distance(randomPosition, spawnedBot.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
            // Player check
            if (Vector3.Distance(randomPosition, player.transform.position) < minDistance)
            {
                validPosition = false;
            }
            //check if in Navmesh
            validPosition = IsInNavMesh(randomPosition,3);

        } while (!validPosition);
        return randomPosition;
    }
    public void SpawnBot()
    {

        // Spawn bot 
        foreach (GameObject bot in botPools)
        {
            if (!bot.activeInHierarchy && spawnedBotNumber < targetNumber)
            {
                bot.SetActive(true);
                bot.GetComponent<Bot>().OnInit();
                bot.transform.position = GetValidPosition();
                spawnedBotNumber ++;
            }
        }
    }
    public int CheckBotRemaining()
    {
        int botCount = 0;
        foreach (GameObject bot in botPools)
        {
            if (bot.activeInHierarchy)
            {
                botCount ++;
            }
        }
        return botCount;
    }
    public bool IsInNavMesh(Vector3 point, float maxDistance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, maxDistance, NavMesh.AllAreas))
        {
            return true; 
        }
        return false; 
    }
    public void ReturnBot(GameObject bot)
    {
        bot.SetActive(false);
    }
}
