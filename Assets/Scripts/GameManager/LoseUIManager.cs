using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using static GameManager;

public class LoseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] float time = 6f;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject image;
    [SerializeField] Player player;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        LosePanel.SetActive(state == GameState.Lose);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            time -= Time.deltaTime;
            DisplayLoseImage();
        }
        if(time <= 0) MoveToMainMenu();
    }

    private void DisplayLoseImage()
    {
        image.transform.rotation = Quaternion.Euler(0, 0, -360 * ((int)time + 1 - time));
        timeText.text = ((int)time).ToString();
    }

    public void PlayAgain()
    {
        ResetPlayer();
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        AudioManager.instance.PlayButtonSoundClip();
    }
    public void MoveToMainMenu()
    {
        if(time > -1f)
        {
            ResetPlayer();
            if(GameManager.Instance.State == GameState.Lose)
            {
                GameManager.Instance.UpdateGameState(GameState.Home);
                AudioManager.instance.PlayButtonSoundClip();
            }
            time = 6f;
        }

    }

    private void ResetPlayer()
    {
        player.isDead = false;
        player.ChangeState(new PlayerIdleState());
        player.FloatingJoystick.enabled = true;
    }
}
