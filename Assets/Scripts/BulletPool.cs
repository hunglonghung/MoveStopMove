using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
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
    
    // Kiểm tra xem có đạn nào đang hoạt động bởi attacker này hay không
    public bool IsBulletActive(Character attacker)
    {
        foreach (GameObject bullet in bullets)
        {
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bullet.activeInHierarchy && bulletComponent.attacker == attacker)
            {
                return true;
            }
        }
        return false;
    }
}
