using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponItem> weaponList;

    public Weapon GetWeapon(WeaponType wpType)
    {
        return weaponList[(int)wpType].weapon;
    }
}

[System.Serializable]
public class WeaponItem
{
    public WeaponType weaponType;
    public Weapon weapon;
    public string name;
    public int price;
}

public enum WeaponType
{
    normal,
    travelBack
}

public class Weapon
{
    public string weaponName;
    public int ammo;
    public float reloadTime;
}
