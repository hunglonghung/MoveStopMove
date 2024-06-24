using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject HomePanel;
    [SerializeField] private List<GameObject> soundButton;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private UserData userData;
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
        // time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // time += Time.deltaTime;
        // if (time > 10f)
        // {
        //     MoveToWeaponShop();
        // }
    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void SetCoin()
    {
        UserData user = new UserData();
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
    public void ChangeVolume()
    {
        AudioManager.instance.PlayButtonSoundClip();
        isSound = !isSound;  
        AudioListener.volume = isSound ? 1 : 0;  
        for(int i = 0 ; i <= soundButton.Count - 1; i++)
        {
            soundButton[i].SetActive(isSound);
        }
    }

}
