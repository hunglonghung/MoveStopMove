using UnityEngine;
using System.Collections.Generic;
using System;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab; 
    public int numberOfBots = 50;
    public float spawnRadius = 20f;
    public float minDistance = 10f; // prevent dead when spawning
    [SerializeField] public List<GameObject> UnspawnedBotList = new List<GameObject>();
    [SerializeField] public List<GameObject> SpawnedBotList = new List<GameObject>();
    [SerializeField] public static int CurrentBotCount = 0;
    [SerializeField] public Bot Bot;
    [SerializeField] public Player player;
    void Start()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            UnspawnedBotList.Add(botPrefab);
        }
        while(CurrentBotCount <= 20) 
        {
            SpawnBot();
            CurrentBotCount ++;
        }
    }

    void Update() 
    {
        if(SpawnedBotList.Count <= 20)
        {
            SpawnBot();
        }
    }
    public void SpawnBot()
    {
        Vector3 randomPosition;
        bool validPosition;

        do
        {
            validPosition = true;
            randomPosition = UnityEngine.Random.insideUnitSphere * spawnRadius;
            randomPosition += transform.position;
            randomPosition.y = 0;
            foreach (GameObject spawnedBot in SpawnedBotList)
            {
                if (Vector3.Distance(randomPosition, spawnedBot.transform.position) < minDistance)
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

        } while (!validPosition);

        // Spawn bot 
        if(UnspawnedBotList.Count > 0)
        {
            GameObject bot = Instantiate(UnspawnedBotList[0], randomPosition, Quaternion.identity);
            bot.GetComponent<Bot>().BotSpawner = this;
            SpawnedBotList.Add(bot);
            UnspawnedBotList.RemoveAt(0);
        }
        
    }

}
