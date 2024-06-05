using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField] public List<WeaponItem> weaponList;

    public GameObject GetGun(int index)
    {
        return weaponList[index].weapon.gun;
    }
    public GameObject GetBullet(int index)
    {
        return weaponList[index].weapon.bullet;
    }
}

[System.Serializable]
public class WeaponItem
{
    public WeaponType weaponType;
    public Weapon weapon;
    public int price;
}

public enum WeaponType
{
    vertical,
    horizontal,
    travelBack,
    straight,

}

[System.Serializable]
public class Weapon
{
    public GameObject gun;
    public GameObject bullet;
}
