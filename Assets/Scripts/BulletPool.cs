using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private int poolSize = 5;
    private Dictionary<WeaponType, List<GameObject>> characterBulletPools;
    private Dictionary<WeaponType, Vector3> bulletOriginalScales;

    private void Awake()
    {
        Instance = this;
        characterBulletPools = new Dictionary<WeaponType, List<GameObject>>();
        bulletOriginalScales = new Dictionary<WeaponType, Vector3>();
    }

    //pool for each weapon type + original size
    public void CreatePool(WeaponType wpType, GameObject bulletPrefab)
    {
        // Debug.Log("creating");
        if (!characterBulletPools.ContainsKey(wpType))
        {
            List<GameObject> bulletPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }
            characterBulletPools.Add(wpType, bulletPool);
        }
        if (!bulletOriginalScales.ContainsKey(wpType))
        {
            bulletOriginalScales[wpType] = bulletPrefab.transform.localScale;
        }

    }

    // Set bullet
    public GameObject GetBullet(WeaponType weaponType)
    {
        if (characterBulletPools.ContainsKey(weaponType))
        {
            // Debug.Log("wptype: " + weaponType + "list: " + characterBulletPools[weaponType] );
            foreach (GameObject bullet in characterBulletPools[weaponType])
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    // Debug.Log("Wptype:" + weaponType + "bullet:" + bullet);
                    return bullet;
                }
            }
        }
        return null;
    }


    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    //check if bullet still there
    public bool IsBulletActive(WeaponType weaponType, Character character)
    {
        if (characterBulletPools.ContainsKey(weaponType))
        {
            foreach (GameObject bullet in characterBulletPools[weaponType])
            {
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bullet.activeInHierarchy && 
                bulletComponent.weaponType == weaponType
                && bulletComponent.attacker == character)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Get original scale of prefab
    public Vector3 GetOriginalScale(WeaponType weaponType)
    {
        if (bulletOriginalScales.ContainsKey(weaponType))
        {
            return bulletOriginalScales[weaponType];
        }
        return Vector3.one * 20;
    }
    // public void LogDictionary()
    // {
    //     if (characterBulletPools == null || characterBulletPools.Count == 0)
    //     {
    //         Debug.Log("BulletPool is empty.");
    //         return;
    //     }

    //     foreach (var kvp in characterBulletPools)
    //     {
    //         foreach (var bullet in kvp.Value)
    //         {
    //             Debug.Log($"WeaponType: {kvp.Key}, Bullet Count: {kvp.Value.Count} Bullet: {bullet.name}, Active: {bullet.activeInHierarchy}");
    //         }
    //     }
    // }

}
