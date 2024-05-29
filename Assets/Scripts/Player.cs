using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private StateMachine stateMachine;
    [SerializeField] public FloatingJoystick FloatingJoystick;
    [SerializeField] Collider[] hitColliders;
    [SerializeField] LayerMask layer;
    private bool isMoving;
    private bool isAttack;
    private bool isWin;
    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
        if(MoveDirection.normalized.magnitude != 0) isMoving = true;
        else isMoving = false;
    }
    void objectScan()
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
