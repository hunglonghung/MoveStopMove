using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private Dictionary<Character, List<GameObject>> characterBulletPools;
    public Vector3 scale;

    private void Awake()
    {
        Instance = this;
        characterBulletPools = new Dictionary<Character, List<GameObject>>();

    }

    //pool for each character
    public void CreatePool(Character character, GameObject bulletPrefab)
    {
        Debug.Log("creating");
        if (!characterBulletPools.ContainsKey(character))
        {
            List<GameObject> bulletPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }
            characterBulletPools.Add(character, bulletPool);
        }
        scale = bulletPrefab.transform.localScale;
    }

    public GameObject GetBullet(Character character)
    {
        if (characterBulletPools.ContainsKey(character))
        {
            foreach (GameObject bullet in characterBulletPools[character])
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
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

    public bool IsBulletActive(Character attacker)
    {
        if (characterBulletPools.ContainsKey(attacker))
        {
            foreach (GameObject bullet in characterBulletPools[attacker])
            {
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bullet.activeInHierarchy && bulletComponent.attacker == attacker)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
