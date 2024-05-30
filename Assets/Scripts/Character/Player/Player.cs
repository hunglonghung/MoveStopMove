using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] public FloatingJoystick FloatingJoystick;
    [SerializeField] public Collider[] hitColliders;
    [SerializeField] LayerMask layer;
    private bool isMoving;
    private bool isAttack;
    private bool isWin;
    private bool isDead;
    public float MoveSpeed;
    public Vector3 MoveDirection;
    public float scanRadius = 5.0f;
    public GameObject target;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject Weapon;
    [SerializeField] public float bulletSpeed = 5f;
    public GameObject CurrentBullet;
    //Move Direction
    public void GetMoveDirection()
    {
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
    }
    public bool GetInput()
   {
      if (Mathf.Abs(FloatingJoystick.Vertical) < 0.01f &&
          Mathf.Abs(FloatingJoystick.Horizontal) < 0.01f)
      {
         return false;
      }
      return true;
   }

    
    // Run
    public void Move(Vector3 direction)
    {
        transform.Translate(direction * MoveSpeed * Time.deltaTime, Space.World);
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    public void Fire()
    {
        CurrentBullet = BulletPool.Instance.GetBullet();
        CurrentBullet.transform.position = Hand.transform.position;
        Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
        CurrentBullet.GetComponent<Bullet>().OnInit(bulletDirection, bulletSpeed, this, scanRadius);
        transform.rotation = Quaternion.LookRotation(bulletDirection);
    }

    //Weapon
    public void setWeapon(GameObject Weapon)
    {
        GameObject characterWeapon = Instantiate(Weapon,Hand.transform.position,Quaternion.identity,Hand.transform);
        characterWeapon.transform.rotation = Quaternion.Euler(180,90,0);
    }
    //Dead
    public void Die()
    {
        ChangeAnim("die");

    }
    //Dance
    public void Dance()
    {
        ChangeAnim("dance");
    }

    public void objectScan()
    {
        hitColliders = Physics.OverlapSphere(transform.position, scanRadius, layer);
    }

    public bool CheckTarget(Collider[] hitColliders)
    {
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                target = hitCollider.gameObject;
                return true;
            }
        }
        return false;
    }

}
