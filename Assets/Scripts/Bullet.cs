using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    public Character weapon;
    private float maxDistance ;
    private Vector3 startPosition;

    public void OnInit(Vector3 direction, float speed, Character weapon, float maxDistance)
    {
        this.direction = direction;
        this.speed = speed;
        this.weapon = weapon;
        this.maxDistance = maxDistance;
        this.startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
            weapon.OnBulletDestroyed();
        }
    }
    void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.OnBulletDestroyed();
        }
    }
}
