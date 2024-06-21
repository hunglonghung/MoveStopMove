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
    [SerializeField] public UserData user;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
    }
    public void SetCoin()
    {
        user = GameManager.Instance.userDataManager.userData;
        coinText.text = user.coin.ToString();
    }
    public void MoveToWeaponShop()
    {
        GameManager.Instance.UpdateGameState(GameState.WeaponShop);
    }
    public void MoveToSkinShop()
    {
        GameManager.Instance.UpdateGameState(GameState.SkinShop);
    }
    public void ChangeVolume()
    {
        isSound = !isSound;  
        AudioListener.volume = isSound ? 1 : 0;  
        for(int i = 0 ; i <= soundButton.Count - 1; i++)
        {
            soundButton[i].SetActive(isSound);
        }
    }
    public void ResetData()
    {
        UserDataManager.Instance.ResetUserDataToDefault();
        SetCoin();
    }

}
