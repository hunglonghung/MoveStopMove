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

public class Skin
{
    public string skinName;
    public bool isUnlocked;
}
