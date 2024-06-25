using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using TMPro;
using UnityEngine;
using static GameManager;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject HomePanel;
    [SerializeField] private List<GameObject> soundButton;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] public UserData user;
    [SerializeField] public Player player;
    [SerializeField] private GameObject SoundOffHomeButton;
    [SerializeField] private GameObject SoundOnHomeButton;
    [SerializeField] private GameObject VibrationOffHomeButton;
    [SerializeField] private GameObject VibrationOnHomeButton;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private SkinManager skinManager;
    [SerializeField] public List<GameObject> Map;
    [SerializeField] public GameplayManager gameplayManager;
    float time;
    private bool isSound = true; 
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        HomePanel.SetActive(state == GameState.Home);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCoin();
        DisplaySettings();
        for(int i = 0; i < Map.Count; i++)
        {
            if(i == user.levelNumber)
            {
                Map[i].SetActive(true);
            }
            else
            {
                Map[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void DisplaySettings()
    {
        SoundOffHomeButton.SetActive(!settingsManager.isSound);
        SoundOnHomeButton.SetActive(settingsManager.isSound);
        VibrationOffHomeButton.SetActive(!settingsManager.isVibrate);
        VibrationOnHomeButton.SetActive(settingsManager.isVibrate);
    }
    public void ChangeVolume()
    {
        AudioManager.instance.PlayButtonSoundClip();
        settingsManager.isSound = !settingsManager.isSound;
        DisplaySettings();
    }
    public void ChangeVibration()
    {
        AudioManager.instance.PlayButtonSoundClip();
        settingsManager.isVibrate = !settingsManager.isVibrate;
        DisplaySettings();
    }
    public void StartGame()
    {
        gameplayManager.AlivePlayers = 100;
        gameplayManager.SetAliveText(gameplayManager.AlivePlayers);
        player.SetWeapon(player.Weapon);
        player.SetSkin(player.Skin);
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void SetCoin()
    {
        user = GameManager.Instance.userDataManager.userData;
        coinText.text = user.coin.ToString();
    }
    public void MoveToWeaponShop()
    {
        GameManager.Instance.UpdateGameState(GameState.WeaponShop);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void MoveToSkinShop()
    {
        GameManager.Instance.UpdateGameState(GameState.SkinShop);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void ResetData()
    {
        UserDataManager.Instance.ResetUserDataToDefault();
        SetCoin();
        weaponManager.Start();
        skinManager.Start();

    }

}
