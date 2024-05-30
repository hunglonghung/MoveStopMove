using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float distanceTraveled;
    private float maxDistance;
    private Player player;

    public void OnInit(Vector3 bulletDirection, float bulletSpeed, Player playerRef, float scanRadius)
    {
        direction = bulletDirection;
        speed = bulletSpeed;
        player = playerRef;
        maxDistance = scanRadius;
        distanceTraveled = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(0,100,0);
    }

    private void Update()
    {
        transform.Rotate(0,0,20f);
        float distanceToTravel = speed * Time.deltaTime;
        transform.Translate(direction * distanceToTravel, Space.World);
        distanceTraveled += distanceToTravel;
        if (distanceTraveled >= maxDistance)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        
    }
}
