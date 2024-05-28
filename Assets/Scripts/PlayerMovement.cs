using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    [SerializeField] public FloatingJoystick FloatingJoystick;
    [SerializeField] Collider[] hitColliders;
    [SerializeField] LayerMask layer ;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = CharacterState.Idle;
        setWeapon(Weapon);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkState();
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
        switch (CurrentState)
        {
            case CharacterState.Run:
                Move(MoveDirection);
                break;
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.Attack:
                Attack();
                break;
            case CharacterState.Lose:
                Die();
                break;
            case CharacterState.Win:
                Dance();
                break;
        }
        objectScan();

    }
    void checkState()
    {
    if(FloatingJoystick.Vertical == 0 && FloatingJoystick.Horizontal == 0)
    {
        if(checkTarget(hitColliders) && currentBullet == null) CurrentState = CharacterState.Attack;
        else CurrentState = CharacterState.Idle;
    }
    else CurrentState = CharacterState.Run;
    
    }
    // Attack
    void objectScan()
    {
        hitColliders = Physics.OverlapSphere(transform.position, scanRadius,layer);
    }
    bool checkTarget(Collider[] hitColliders)
    {
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "Enemy")
            {
                target = hitCollider.gameObject;
                return true;
            }
        }
        return false;
    }


}