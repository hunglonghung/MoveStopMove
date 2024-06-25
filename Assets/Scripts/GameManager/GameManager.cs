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
    public GameObject coinDisplay;
    public GameObject targetIndicator;
    public static event Action<GameState> OnGameStateChanged;
    [Header("User Data")]
    [SerializeField] public UserDataManager userDataManager;

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
        State = newState;
        switch (newState)
        {
            case GameState.Loading:
                Time.timeScale = 1f;
                coinDisplay.SetActive(false);
                targetIndicator.SetActive(false);
                break;
            case GameState.Home:
                Time.timeScale = 0f;
                coinDisplay.SetActive(true);
                targetIndicator.SetActive(false);
                break;
            case GameState.WeaponShop:
                Time.timeScale = 0f;
                coinDisplay.SetActive(true);
                targetIndicator.SetActive(false);
                break;
            case GameState.SkinShop:
                Time.timeScale = 0f;
                coinDisplay.SetActive(true);
                targetIndicator.SetActive(false);
                break;
            case GameState.GamePlay:
                Time.timeScale = 1f;
                coinDisplay.SetActive(false);
                targetIndicator.SetActive(true);
                break;
            case GameState.Settings:
                Time.timeScale = 0f;
                coinDisplay.SetActive(false);
                targetIndicator.SetActive(false);
                break;
            case GameState.Result:
                Time.timeScale = 0f;
                coinDisplay.SetActive(true);
                targetIndicator.SetActive(false);
                break;
            case GameState.Lose:
                Time.timeScale = 1f;
                coinDisplay.SetActive(true);
                targetIndicator.SetActive(false);
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
        Result,
        Lose,

        

    }
}
