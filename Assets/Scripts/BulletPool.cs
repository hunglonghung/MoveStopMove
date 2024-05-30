using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 3;
    private List<GameObject> bullets;

    private void Awake()
    {
        Instance = this;
        bullets = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
    public bool IsBulletActive()
    {
        foreach (GameObject bullet in bullets)
        {
            if (bullet.activeInHierarchy)
            {
                Debug.Log("true");
                return true;
            }
        }
        return false;
    }
}
