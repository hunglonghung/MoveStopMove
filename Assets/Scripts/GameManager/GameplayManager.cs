using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject GameplayPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private TextMeshProUGUI aliveText;
    [SerializeField] public UserData user;
    [SerializeField] public int AlivePlayers = 100;
    [SerializeField] public Player player;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        GameplayPanel.SetActive(state == GameState.GamePlay);
    }
    void Start()
    {
        SetAliveText();
        AlivePlayers = 100;
        
    }
    void Update()
    {

    }
    public void MoveToSettings()
    {
        GameManager.Instance.UpdateGameState(GameState.Settings);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void SetAliveText()
    {
        Debug.Log("Before updating text: " + AlivePlayers); // Debug here
        aliveText.text = "Alive: " + AlivePlayers;
        Debug.Log("After updating text: " + AlivePlayers); // Debug here


    }
    public void MoveToLoseUI()
    {
        GameManager.Instance.UpdateGameState(GameState.Lose);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void MoveToWinUI()
    {
        GameManager.Instance.UpdateGameState(GameState.Lose);
        AudioManager.instance.PlayButtonSoundClip();
    }

}