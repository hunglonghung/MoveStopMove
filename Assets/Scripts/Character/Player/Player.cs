using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : Character
{
    [Header("Joystick")]
    [SerializeField] public FloatingJoystick FloatingJoystick;
    [Header("Camera")]
    [SerializeField] public CinemachineVirtualCamera PlayerCamera;

    public override void Start()
    {
        base.Start();
        CameraDistanceInit(25f);
    }
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
    public void IncreaseVirtualCameraRange(float value)
    {
        CinemachineFramingTransposer framingTransposer = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance += value;
        }
    }
    public void CameraDistanceInit(float value)
    {
        CinemachineFramingTransposer framingTransposer = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance = value;
        }
    }

    

}
