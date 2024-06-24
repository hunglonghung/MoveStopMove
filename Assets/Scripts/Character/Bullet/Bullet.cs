using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float distanceTraveled;
    private float maxDistance;
    public Character attacker;
    public Character victim; 
    public WeaponType weaponType;
    [SerializeField] public GameplayManager GameplayManager;
    [SerializeField] private float rotationValue = 10f;

    public void OnInit(Vector3 bulletDirection, float bulletSpeed, Character character, float scanRadius, WeaponType weapon)
    {
        direction = bulletDirection;
        speed = bulletSpeed;
        attacker = character; 
        maxDistance = scanRadius;
        distanceTraveled = 0f;
        weaponType = weapon;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(90,100,0);
    }

    private void Update()
    {
        transform.Rotate(0,0,rotationValue);
        float distanceToTravel = speed * Time.deltaTime;
        transform.Translate(direction * distanceToTravel, Space.World);
        distanceTraveled += distanceToTravel;
        if (distanceTraveled >= maxDistance)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            victim = other.GetComponent<Character>(); 
            // Debug.Log("Attacker: " + attacker.name + " hit Victim: " + victim.name);
            if(!CheckSameCharacter())
            {
                victim.isDead = true;
                AudioManager.instance.PlayWeaponCollisionSoundClip();
                AudioManager.instance.PlayDeathSoundClip();
                OnKill(attacker);
            }
        }
    }

    //On Kill Logic
    public void OnKill(Character attacker)
    {
        attacker.KillCount ++;
        attacker.SizeMultiplier += 0.1f;
        attacker.Range += 1f; 
        attacker.gameObject.transform.localScale = new Vector3(1, 1, 1) * attacker.SizeMultiplier;
        attacker.BulletSpeed += 1f;
        if (attacker is Player player)
        {
            player.IncreaseVirtualCameraRange(3f);
        }
        Debug.Log(GameplayManager.AlivePlayers);
        GameplayManager.AlivePlayers --;
        GameplayManager.SetAliveText();
        if(victim is Player)
        {
            GameplayManager.MoveToLoseUI();
        }
    }

    public bool CheckSameCharacter()
    {
        if(attacker != victim) return false;
        else return true;
    }
}
