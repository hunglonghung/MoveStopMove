using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int coin; 
    public int levelNumber;
    public string name;

    public int currentWeaponIndex;
    public int currentHatIndex; 
    public int currentPantIndex; 
    public int currentShieldIndex; 
    public int currentComboSkinIndex; 

    public List<int> weaponState; 
    public List<int> hatState; 
    public List<int> pantState; 
    public List<int> shieldState; 
    public List<int> comboSkinState; 

    public UserData()
    {
        coin = 10000; 
        levelNumber = 1; 
        name = "YOU"; 

        currentWeaponIndex = 0;
        currentHatIndex = 0;
        currentPantIndex = 0;
        currentShieldIndex = 0;
        currentComboSkinIndex = 0;

        weaponState = new List<int>() { 1, 0, 0, 0, 0, 0, 0, 0 };
        hatState = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        pantState = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        shieldState = new List<int>() { 0, 0, 0 };
        comboSkinState = new List<int>() { 0, 0, 0 };
    }
}
