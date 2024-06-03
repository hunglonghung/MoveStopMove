using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Joystick")]
    [SerializeField] public FloatingJoystick FloatingJoystick;


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
    public void DisableJoystick()
    {
        FloatingJoystick.gameObject.SetActive(false);
    }

    

}
