using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    [SerializeField] public List<Skin> SkinList;
    [SerializeField] public List<Hat> HatList;
    [SerializeField] public List<Shield> ShieldList;
    [SerializeField] public List<Pants> PantsList;
    [SerializeField] public List<Color> ColorList;

    public Skin GetSkin(int index)
    {
        if(index >= 0 && index < SkinList.Count)
        {
            return SkinList[index];
        }
        return null;
    }
    
    
    public Hat GetHat(int index)
    {
        if(index >= 0 && index < HatList.Count)
        {
            return HatList[index];
        }
        return null;
    }
    
    public Color GetColor(int index)
    {
        if(index >= 0 && index < ColorList.Count)
        {
            return ColorList[index];
        }
        return Color.white;
    }
    
    public Pants GetPants(int index)
    {
        if(index >= 0 && index < PantsList.Count)
        {
            return PantsList[index];
        }
        return null;
    }
    public Shield GetShield(int index)
    {
        if(index >= 0 && index < ShieldList.Count)
        {
            return ShieldList[index];
        }
        return null;
    }
}
[System.Serializable]
public class Colors
{
    public Color color;
}

[System.Serializable]
public class Hat
{
    public GameObject HatGameObject;
    public Sprite HatSprite;
    public int HatPrice;
    public string status;

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
}
