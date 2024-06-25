using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject ResultPanel;
    [SerializeField] private UserDataManager userDataManager;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI rankingText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private int prize;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        ResultPanel.SetActive(state == GameState.Result);
    }

    // Start is called before the first frame update
    void Start()
    {
        AddCoin();
        DisplayResult();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveToHomePanel()
    {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void DisplayResult()
    {
        DisplayText();
        DisplayRanking();
        DisplayCoin();
    }
    public void DisplayText()
    {
        if(gameplayManager.AlivePlayers + 1 == 1)
        {
            resultText.SetText("YOU WIN!!!");
        }
        else
        {
            resultText.SetText("YOUR RANK:");
        }
    }
    public void DisplayRanking()
    {
        rankingText.SetText((gameplayManager.AlivePlayers + 1).ToString());
    }
    public void DisplayCoin()
    {
        prize = (100 - gameplayManager.AlivePlayers + 1 ) * 50;
        coinText.SetText(prize.ToString());
    }
    public void Continue()
    {
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
    }
    public void BackToHome()
    {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void AddCoin()
    {
        UserData user = Instance.userDataManager.userData;
        user.coin += prize;
        user.levelNumber ++;
    }


}
