using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class SkinManager : MonoBehaviour
{
    [Header("Weapon Shop")]
    [SerializeField] public GameObject SkinShopPanel;
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
        SkinShopPanel.SetActive(state == GameState.SkinShop);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCoin();
        LoadSkin();
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
    public void LoadSkin()
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
<<<<<<< HEAD
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
                    if (i == user.currentComboSkinIndex && CurrentItemTypeIndex == 3)
                    {
                        DisplayEquipped();
                    }
                    else
                    {
                        DisplaySelect();
                    }
                }
=======
                equipped.SetActive(true);
                select.SetActive(false);
                equipButton.GetComponent<Image>().color = new Color(0, 0, 0, 0); 
>>>>>>> parent of 9a266e1 (Adding skin shop)
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
<<<<<<< HEAD
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
                    if (i == user.currentShieldIndex && CurrentItemTypeIndex == 2)
                    {
                        DisplayEquipped();
                    }
                    else
                    {
                        DisplaySelect();
                    }
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
                if(user.pantState[i] == 0 && CurrentItemTypeIndex == 1)
                {
                    ItemButton[i].LockIcon.SetActive(true);
                }
                else
                {
                    ItemButton[i].LockIcon.SetActive(false);
                    if (i == user.currentPantIndex)
                    {
                        DisplayEquipped();
                    }
                    else
                    {
                        DisplaySelect();
                    }
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
                    if (i == user.currentHatIndex && CurrentItemTypeIndex == 0)
                    {
                        DisplayEquipped();
                    }
                    else
                    {
                        DisplaySelect();
                    }
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
        AudioManager.instance.PlayButtonSoundClip();
        ItemTypeButton[CurrentItemTypeIndex].Button.GetComponent<Image>().color = new Color32(0, 0, 0, 73);
        clickedButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        for (int i = 0; i < ItemTypeButton.Count; i++)
        {
            if (ItemTypeButton[i].Button == clickedButton)
            {
                CurrentItemTypeIndex = i;
                break;
            }
=======
            CurrentWeaponIndex ++;
>>>>>>> parent of 9a266e1 (Adding skin shop)
        }
        LoadSkin();
    }
    public void MoveToPreviousWeapon()
    {
<<<<<<< HEAD
        AudioManager.instance.PlayButtonSoundClip();
        for (int i = 0; i < ItemButton.Count; i++)
=======
        if (CurrentWeaponIndex > 0)
>>>>>>> parent of 9a266e1 (Adding skin shop)
        {
            CurrentWeaponIndex --;
        }
        LoadSkin();
    }
    public void PurchaseItem()
    {
<<<<<<< HEAD
        AudioManager.instance.PlayButtonSoundClip();
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void ItemPurchase()
    {
        int itemPrice = int.Parse(coinPurchaseButtonText.text);
        if(user.coin >= itemPrice)
        {
            AudioManager.instance.PlayButtonSoundClip();
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

        }
        else AudioManager.instance.PlayButtonRejectSoundClip();
        GameManager.Instance.userDataManager.SaveUserData();
        LoadSkin();
        SetCoin();
        
=======
        int weaponPrice = int.Parse(WeaponData.GetPrice(CurrentWeaponIndex));
        if(user.coin >= weaponPrice)
        {
            user.coin -= weaponPrice;
            user.weaponState[CurrentWeaponIndex] = 1;
            SetCoin();
            LoadSkin();
        }
        userDataManager.SaveUserData();
>>>>>>> parent of 9a266e1 (Adding skin shop)
    }
    public void EquipWeapon()
    {
        user.currentWeaponIndex = CurrentWeaponIndex;
        userDataManager.SaveUserData();
        LoadSkin();

    }
    public void DisplayEquipped()
    {
        AudioManager.instance.PlayButtonSoundClip();
        equipped.SetActive(true);
        select.SetActive(false);
        equipButton.GetComponent<Button>().image.color = new Color32(255,255,255,0);
    }
    public void DisplaySelect()
    {
        AudioManager.instance.PlayButtonSoundClip();
        equipped.SetActive(false);
        select.SetActive(true);
        equipButton.GetComponent<Button>().image.color = new Color32(255,255,255,255);
    }


}
