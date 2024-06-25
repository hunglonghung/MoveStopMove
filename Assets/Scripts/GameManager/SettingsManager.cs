using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject SoundOffButton;
    [SerializeField] private GameObject SoundOnButton;
    [SerializeField] private GameObject VibrationOffButton;
    [SerializeField] private GameObject VibrationOnButton;
    [SerializeField] private GameObject VibrationOffHomeButton;
    [SerializeField] private GameObject VibrationOnButtonButton;
    [SerializeField] public bool isSound = true;
    [SerializeField] public bool isVibrate = true;
    [SerializeField] public int AlivePlayers = 100;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        SettingsPanel.SetActive(state == GameState.Settings);
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void MoveToMainMenu()
    {
        AudioManager.instance.PlayButtonSoundClip();
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void Continue()
    {
        AudioManager.instance.PlayButtonSoundClip();
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
    }
    private void DisplaySettings()
    {
        SoundOnButton.SetActive(isSound);
        SoundOffButton.SetActive(!isSound);
        VibrationOnButton.SetActive(isVibrate);
        VibrationOffButton.SetActive(!isVibrate);
        if(isSound) AudioManager.instance.EnableSound();
        else AudioManager.instance.MuteSound();
    }
    public void adjustVolume()
    {
        AudioManager.instance.PlayButtonSoundClip();
        isSound = !isSound;
        DisplaySettings();
    }
    public void adjustVibration()
    {
        AudioManager.instance.PlayButtonSoundClip();
        isVibrate = !isVibrate;
        DisplaySettings();
    }

}