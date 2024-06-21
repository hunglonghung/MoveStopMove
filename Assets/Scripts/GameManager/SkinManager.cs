using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class SkinManager : MonoBehaviour
{
    [Header("Skin Shop")]
    [SerializeField] public GameObject SkinShopPanel;
    [SerializeField] public SkinData SkinData;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private int CurrentItemTypeIndex;
    [SerializeField] private int CurrentItemIndex;
    [Header("Buttons")]
    [SerializeField] public List<Item> ItemButton;
    [SerializeField] public List<ItemType> ItemTypeButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equipped;
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject coinPurchaseButton;
    [SerializeField] private TextMeshProUGUI coinPurchaseButtonText;
    [SerializeField] private GameObject videoButton;
    [Header("User Data")]
    [SerializeField] private UserData user;
    void Awake()
    {
        Debug.Log("this is weapon");
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        SkinShopPanel.SetActive(state == GameState.SkinShop);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCoin();
        LoadSkin();
        CurrentItemTypeIndex = 0;
        CurrentItemIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCoin()
    {
        user = Instance.userDataManager.userData;
        Debug.Log(user.coin.ToString());
        coinText.text = user.coin.ToString();
    }
    public void LoadSkin()
    {
        switch (CurrentItemTypeIndex)
        {
            case 0:
            {
                DisplayHat();
                break;
            }
            case 1:
            {
                DisplayPants();
                break;
            }
            case 2:
            {
                DisplayShield();
                break;
            }
            case 3:
            {
                DisplayFullSet();
                break;
            }

        }
    }

    private void DisplayFullSet()
    {
        // Debug.Log(SkinData.SkinList.Count);
        // Debug.Log(user.comboSkinState.Count);

        for (int i = 0; i < ItemButton.Count; i++) 
        {
            ItemButton[i].Button.GetComponent<Image>().color =  new Color32(255,255,255,255);
            if (i < SkinData.SkinList.Count)
            {
                ItemButton[i].Image.enabled = true;
                ItemButton[i].Image.sprite = SkinData.SkinList[i].GetSprite();
                if (user.comboSkinState[i] == 0)
                {
                    ItemButton[i].LockIcon.SetActive(true);

                }
                else
                {
                    ItemButton[i].LockIcon.SetActive(false);
                }
            }
            else
            {
                ItemButton[i].Image.enabled = false;
                ItemButton[i].LockIcon.SetActive(false);
            }
        }
        ItemButton[CurrentItemIndex].Button.GetComponent<Image>().color =  new Color32(0, 255, 1, 255);
        if(user.comboSkinState[CurrentItemIndex] == 0)
        {
            Debug.Log("here");
            coinPurchaseButton.SetActive(true);
            videoButton.SetActive(true);
            equipButton.SetActive(false);
            coinPurchaseButtonText.text = SkinData.SkinList[CurrentItemIndex].GetSkinPrice().ToString();
        }
        else
        {
            coinPurchaseButton.SetActive(false);
            videoButton.SetActive(false);
            equipButton.SetActive(true);
            ItemButton[CurrentItemIndex].LockIcon.SetActive(false);
        }
        
    }


    private void DisplayShield()
    {
        for(int i = 0; i < 6; i++)
        {
            ItemButton[i].Button.GetComponent<Image>().color =  new Color32(255,255,255,255);
            if(i < SkinData.ShieldList.Count)
            {
                ItemButton[i].Image.enabled = true;
                ItemButton[i].Image.sprite = SkinData.ShieldList[i].GetShieldSprite();
                if(user.shieldState[i] == 0)
                {
                    ItemButton[i].LockIcon.SetActive(true);
                }
                else
                {
                    ItemButton[i].LockIcon.SetActive(false);
                }
            }
            else
            {
                ItemButton[i].Image.enabled = false;
                ItemButton[i].LockIcon.SetActive(false);
            }
            

        }
        ItemButton[CurrentItemIndex].Button.GetComponent<Image>().color =  new Color32(0, 255, 1, 255);
        if(user.shieldState[CurrentItemIndex] == 0)
        {
            coinPurchaseButton.SetActive(true);
            videoButton.SetActive(true);
            equipButton.SetActive(false);
            coinPurchaseButtonText.text = SkinData.ShieldList[CurrentItemIndex].GetShieldPrice().ToString();
        }
        else
        {
            coinPurchaseButton.SetActive(false);
            videoButton.SetActive(false);
            equipButton.SetActive(true);
            ItemButton[CurrentItemIndex].LockIcon.SetActive(false);
        }
    }

    private void DisplayPants()
    {
        
        for(int i = 0; i < 6; i++)
        {
            ItemButton[i].Button.GetComponent<Image>().color =  new Color32(255,255,255,255);
            if(i < SkinData.PantsList.Count)
            {
                ItemButton[i].Image.enabled = true;
                ItemButton[i].Image.sprite = SkinData.PantsList[i].GetPantsSprite();
                if(user.pantState[i] == 0)
                {
                    ItemButton[i].LockIcon.SetActive(true);
                }
                else
                {
                    ItemButton[i].LockIcon.SetActive(false);
                }
            }
            else
            {
                ItemButton[i].Image.enabled = false;
                ItemButton[i].LockIcon.SetActive(false);
            }
        }
         ItemButton[CurrentItemIndex].Button.GetComponent<Image>().color =  new Color32(0, 255, 1, 255);
        if(user.pantState[CurrentItemIndex] == 0)
        {
            coinPurchaseButton.SetActive(true);
            videoButton.SetActive(true);
            equipButton.SetActive(false);
            coinPurchaseButtonText.text = SkinData.PantsList[CurrentItemIndex].GetPantsPrice().ToString();
        }
        else
        {
            coinPurchaseButton.SetActive(false);
            videoButton.SetActive(false);
            equipButton.SetActive(true);
            ItemButton[CurrentItemIndex].LockIcon.SetActive(false);
        }
    }

    private void DisplayHat()
    {
        for(int i = 0; i < 6; i++)
        {
            ItemButton[i].Button.GetComponent<Image>().color =  new Color32(255,255,255,255);
            if(i < SkinData.HatList.Count)
            {
                ItemButton[i].Image.enabled = true;
                ItemButton[i].Image.sprite = SkinData.HatList[i].GetHatSprite();
                if(user.hatState[i] == 0)
                {
                    ItemButton[i].LockIcon.SetActive(true);
                }
                else
                {
                    ItemButton[i].LockIcon.SetActive(false);
                }
            }
            else
            {
                ItemButton[i].Image.enabled = false;
                ItemButton[i].LockIcon.SetActive(false);
            }
        }
         ItemButton[CurrentItemIndex].Button.GetComponent<Image>().color =  new Color32(0, 255, 1, 255);
        if(user.hatState[CurrentItemIndex] == 0)
        {
            coinPurchaseButton.SetActive(true);
            videoButton.SetActive(true);
            equipButton.SetActive(false);
            coinPurchaseButtonText.text = SkinData.HatList[CurrentItemIndex].GetHatPrice().ToString();
        }
        else
        {
            coinPurchaseButton.SetActive(false);
            videoButton.SetActive(false);
            equipButton.SetActive(true);
            ItemButton[CurrentItemIndex].LockIcon.SetActive(false);
        }
    }

    public void ItemTypeChange(GameObject clickedButton)
    {
        ItemTypeButton[CurrentItemTypeIndex].Button.GetComponent<Image>().color = new Color32(0, 0, 0, 73);
        clickedButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        for (int i = 0; i < ItemTypeButton.Count; i++)
        {
            if (ItemTypeButton[i].Button == clickedButton)
            {
                CurrentItemTypeIndex = i;
                break;
            }
        }
        LoadSkin();
    }
    public void ItemChange(GameObject clickedButton)
    {
        for (int i = 0; i < ItemButton.Count; i++)
        {
            if (ItemButton[i].Button == clickedButton)
            {
                CurrentItemIndex = i;
                break;
            }
        }
        LoadSkin();
    }
    public void BackToHome()
    {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void ItemPurchase()
    {
        int itemPrice = int.Parse(coinPurchaseButtonText.text);
        if(user.coin >= itemPrice)
        {
            user.coin -= itemPrice;
            switch (CurrentItemTypeIndex)
            {
                case 0:
                {
                    user.hatState[CurrentItemIndex] = 1;
                    break;
                }
                case 1:
                {   
                    user.pantState[CurrentItemIndex] = 1;
                    break;
                }
                case 2:
                {   
                    user.shieldState[CurrentItemIndex] = 1;
                    break;
                }
                case 3:
                {   
                    user.comboSkinState[CurrentItemIndex] = 1;
                    break;
                }
            }
            SetCoin();
            LoadSkin();

        }
        GameManager.Instance.userDataManager.SaveUserData();
    }
    public void EquipItem()
    {
        switch (CurrentItemTypeIndex)
        {
            case 0:
            {
                user.currentHatIndex = CurrentItemIndex;
                break;
            }
            case 1:
            {   
                user.currentPantIndex = CurrentItemIndex;
                break;
            }
            case 2:
            {   
                user.currentShieldIndex = CurrentItemIndex;
                break;
            }
            case 3:
            {   
                user.currentComboSkinIndex = CurrentItemIndex;
                break;
            }
        }
        LoadSkin();
    }


}
[System.Serializable]
public class Item
{
    public Image Image;
    public GameObject Button;
    public GameObject LockIcon;
}
[System.Serializable]
public class ItemType
{
    public GameObject Button;
}
