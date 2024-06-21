using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadingPanel;
    float time = 0;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        LoadingPanel.SetActive(state == GameState.Loading);
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 3f)
        {
            time += Time.deltaTime;
        }
        else
        {
            if(GameManager.Instance.State == GameState.Loading)
            {
                MoveToHomePanel();
            }
        }
    }
    public void MoveToHomePanel()
    {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }

}
