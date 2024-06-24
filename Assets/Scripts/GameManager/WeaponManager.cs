using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Shop")]
    [SerializeField] public GameObject WeaponShopPanel;
    [SerializeField] public WeaponData WeaponData;
    [SerializeField] public Image WeaponImage;
    [SerializeField] public TextMeshProUGUI WeaponText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] public int CurrentWeaponIndex = 0;
    [SerializeField] private TextMeshProUGUI statusText ;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equipped;
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject coinPurchaseButton;
    [SerializeField] private TextMeshProUGUI coinPurchaseButtonText;
    [SerializeField] private GameObject videoButton;
    [Header("User Data")]
    [SerializeField] private UserDataManager userDataManager;
    [SerializeField] private UserData user;
    void Awake()
    {
        Debug.Log("this is weapon");
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        WeaponShopPanel.SetActive(state == GameState.WeaponShop);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCoin();
        LoadWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCoin()
    {
        user = userDataManager.userData;
        Debug.Log(user.coin.ToString());
        coinText.text = user.coin.ToString();
    }
    public void LoadWeapon()
    {
        WeaponImage.sprite = WeaponData.GetSprite(CurrentWeaponIndex);
        WeaponText.text = WeaponData.GetWeaponName(CurrentWeaponIndex);
        statusText.text = WeaponData.GetStatusText(CurrentWeaponIndex);
        coinPurchaseButtonText.text = WeaponData.GetPrice(CurrentWeaponIndex);
        if(user.weaponState[CurrentWeaponIndex] == 1)
        {
            equipButton.SetActive(true);
            lockedText.SetActive(false);
            buttonGroup.SetActive(true);
            if(user.currentWeaponIndex == CurrentWeaponIndex)
            {
                equipped.SetActive(true);
                select.SetActive(false);
                equipButton.GetComponent<Image>().color = new Color(0, 0, 0, 0); 
            }
            else
            {
                equipped.SetActive(false);
                select.SetActive(true);
                equipButton.GetComponent<Image>().color = new Color(255, 255, 255, 255); 
            }
            coinPurchaseButton.SetActive(false);
            videoButton.SetActive(false);
        }
        else
        {
            coinPurchaseButton.SetActive(true);
            videoButton.SetActive(true);
            equipButton.SetActive(false);
            lockedText.SetActive(true);
            buttonGroup.SetActive(false);
        }
        

    }   
    public void MoveToNextWeapon()
    {
        if (CurrentWeaponIndex < WeaponData.weaponList.Count - 1)
        {
            CurrentWeaponIndex ++;
        }
        LoadWeapon();
    }
    public void MoveToPreviousWeapon()
    {
        if (CurrentWeaponIndex > 0)
        {
            CurrentWeaponIndex --;
        }
        LoadWeapon();
    }
    public void PurchaseItem()
    {
        int weaponPrice = int.Parse(WeaponData.GetPrice(CurrentWeaponIndex));
        if(user.coin >= weaponPrice)
        {
            user.coin -= weaponPrice;
            user.weaponState[CurrentWeaponIndex] = 1;
        }
<<<<<<< HEAD
        GameManager.Instance.userDataManager.SaveUserData();
        SetCoin();
        LoadWeapon();
=======
        userDataManager.SaveUserData();
>>>>>>> parent of 9a266e1 (Adding skin shop)
    }
    public void EquipWeapon()
    {
        user.currentWeaponIndex = CurrentWeaponIndex;
        userDataManager.SaveUserData();
        LoadWeapon();

    }
    public void BackToHome()
    {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }


}
