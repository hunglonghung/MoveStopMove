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
    [Header("Target")]
    [SerializeField] public GameObject TargetPrefab;
    private GameObject targetObject;

    public override void Start()
    {
        base.Start();
        SetTarget();
        CameraDistanceInit(25f);
    }
    public override void Update()
    {
        base.Update();
        objectScan();
        SetTarget();
        UpdateTargetPosition();
        if(isDead) ChangeState(new PlayerLoseState());
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
    //SetTarget
    public void SetTarget()
    {
        if (target != null)
        {
            targetObject = TargetPool.Instance.GetTarget();
            targetObject.transform.position = target.transform.position;
            targetObject.transform.rotation = Quaternion.Euler(90, 0, 0);
            targetObject.transform.localScale = target.transform.localScale; 
        }
    }

    public void DestroyTarget()
    {
        if (targetObject != null)
        {
            TargetPool.Instance.ReturnTarget();
        }
    }

    private void UpdateTargetPosition()
    {
        if (target != null && targetObject != null )
        {
            targetObject.transform.position = target.transform.position;
        }
        else TargetPool.Instance.ReturnTarget();
    }



    //UI
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
