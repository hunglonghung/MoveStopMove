    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public List<WeaponItem> weaponList;
        public WeaponType GetWeaponType(int index)
        {
            return weaponList[index].weaponType;
        }
        public string GetWeaponName(int index)
        {
            return weaponList[index].weaponType.ToString();
        }
        public GameObject GetGun(int index)
        {
            return weaponList[index].weapon.gun;
        }
        public GameObject GetGunByWeaponType(WeaponType wptype)
        {
            return weaponList[(int)wptype].weapon.gun;
        }
        public GameObject GetBullet(int index)
        {
            return weaponList[index].weapon.bullet;
        }
        public GameObject GetBulletByWeaponType(WeaponType wptype)
        {
            // Debug.Log(wptype + " and " + (int)wptype + " and " + weaponList[(int)wptype].weapon.bullet);
            return weaponList[(int)wptype].weapon.bullet;
        }
        public Sprite GetSprite(int index)
        {
            return weaponList[index].sprite;
        }
        public string GetPrice(int index)
        {
            return weaponList[index].price.ToString();
        }
        public string GetStatusText(int index)
        {
            return weaponList[index].statusText;
        }
        
    }

    [System.Serializable]
    public class WeaponItem
    {
        public WeaponType weaponType;
        public Weapon weapon;
        public int price;
        public Sprite sprite;
        public string statusText;
        public GameObject WeaponObject;
    }

    public enum ProjectileType
    {
        vertical,
        horizontal,
        travelBack,
        straight,
    }
    public enum WeaponType
    {
        Z = 0,
        Knife = 1,
        Hammer = 2,
        FlatLolipop = 3,
        Icecream = 4,
        Candycane = 5,
        SphereLolipop = 6,
        Boomerang = 7,
        Axe = 8,
        Pickaxe = 9,
        Arrow = 10,


    }

    [System.Serializable]
    public class Weapon
    {
        public GameObject gun;
        public GameObject bullet;
    }
