// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Bullet : MonoBehaviour
// {
//     private Vector3 direction;
//     private float speed;
//     public Character weapon;
//     private float maxDistance ;
//     private Vector3 startPosition;
//     public Character Attacker;
//     public Character Victim;

//     public void OnInit(Vector3 direction, float speed, Character weapon, float maxDistance, Character Attacker)
//     {
//         this.direction = direction;
//         this.speed = speed;
//         this.weapon = weapon;
//         this.maxDistance = maxDistance;
//         this.startPosition = transform.position;
//         this.Attacker = Attacker;
//     }

//     void Update()
//     {
//         transform.Translate(direction * speed * Time.deltaTime, Space.World);
//         if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
//         {
//             Destroy(gameObject);
//             weapon.OnBulletDestroyed();
//         }
//         transform.Rotate(0,0,20f);
//     }
//     void OnDestroy()
//     {
//         if (weapon != null)
//         {
//             weapon.OnBulletDestroyed();
//         }
//     }
//     private void OnTriggerEnter(Collider other) {
//         Victim = other.gameObject.GetComponent<Character>();
//         if(Attacker != Victim)
//         {
//             Victim.ChangeState(Character.CharacterState.Lose);
//         }
//     }
// }
