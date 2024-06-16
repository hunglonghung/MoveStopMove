using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static int Coins = 500;
    public static event Action<GameState> OnGameStateChanged;
    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Đảm bảo đối tượng không bị hủy khi tải cảnh mới
    }
    else
    {
        Destroy(gameObject); // Hủy đối tượng nếu đã có một thể hiện khác tồn tại
    }
}

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Loading);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateGameState(GameState newState)
    {
        Debug.Log("old State:" + State);
        State = newState;
        Debug.Log("new State:" + State);
        switch (newState)
        {
            case GameState.Loading:
                break;
            case GameState.Home:
                break;
            case GameState.WeaponShop:
                break;
            case GameState.SkinShop:
                break;
            case GameState.GamePlay:
                Time.timeScale = 0;
                break;
            case GameState.Settings:
                Time.timeScale = 0;
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState{
        Loading,
        Home, 
        WeaponShop,
        SkinShop,
        GamePlay,
        Settings,
        Win,
        Lose,

        

    }
}
