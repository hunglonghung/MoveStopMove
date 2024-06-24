using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    [SerializeField] public List<SkinItem> skinList;

    public Skin GetSkin(int index)
    {
        if(index >= 0 && index < skinList.Count)
        {
            return skinList[index].skin;
        }
        return null;
    }
    
    public int GetPrice(int index)
    {
        if(index >= 0 && index < skinList.Count)
        {
            return skinList[index].price;
        }
        return 0;
    }
    
    public GameObject GetHat(int index)
    {
        if(index >= 0 && index < skinList.Count)
        {
            return skinList[index].hat;
        }
        return null;
    }
    
    public Color GetColor(int index)
    {
        if(index >= 0 && index < skinList.Count)
        {
            return skinList[index].color;
        }
        return Color.black;
    }
    
    public Material GetPantsMaterial(int index)
    {
        if(index >= 0 && index < skinList.Count)
        {
            return skinList[index].pants;
        }
        return null;
    }
}

[System.Serializable]
public class SkinItem
{
    public Skin skin;
    public int price;
    public GameObject hat;
    public Color color;
    public Material pants;
}

<<<<<<< HEAD
[System.Serializable]
public class Hat
{
    public GameObject HatGameObject;
    public Sprite HatSprite;
    public int HatPrice;
    public string status;
    public GameObject HatObject;

    public GameObject GetHatGameObject()
    {
        return HatGameObject;
    }

    public Sprite GetHatSprite()
    {
        return HatSprite;
    }

    public int GetHatPrice()
    {
        return HatPrice;
    }

    public string GetStatus()
    {
        return status;
    }
}
[System.Serializable]
public class Skin
{
    public Sprite SkinSprite;
    public int SkinPrice;
    public string status;
    public GameObject SkinObject;

    public Sprite GetSprite()
    {
        return SkinSprite;
    }


    public int GetSkinPrice()
    {
        return SkinPrice;
    }

    public string GetStatus()
    {
        return status;
    }
}
[System.Serializable]
public class Pants
{
    public Material pantsMaterial;
    public Sprite pantsSprite;
    public int pantsPrice;
    public string status;
    public GameObject PantsObject;

    public Material GetPantsMaterial()
    {
        return pantsMaterial;
    }

    public Sprite GetPantsSprite()
    {
        return pantsSprite;
    }

    public int GetPantsPrice()
    {
        return pantsPrice;
    }

    public string GetStatus()
    {
        return status;
    }
}
[System.Serializable]
public class Shield
{
    public GameObject ShieldGameObject;
    public Sprite ShieldSprite;
    public int ShieldPrice;
    public string status;
    public GameObject ShieldObject;

    public GameObject GetShieldGameObject()
    {
        return ShieldGameObject;
    }

    public Sprite GetShieldSprite()
    {
        return ShieldSprite;
    }

    public int GetShieldPrice()
    {
        return ShieldPrice;
    }

    public string GetStatus()
    {
        return status;
    }
=======
public class Skin
{
    public string skinName;
    public bool isUnlocked;
>>>>>>> parent of 9a266e1 (Adding skin shop)
}
