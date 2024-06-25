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
        SetSkin(Skin);
        SetWeapon(Weapon);
        SetTarget();
        CameraDistanceInit(25f);
    }
    public override void Update()
    {
        base.Update();
        ObjectScan();
        SetTarget();
        UpdateTargetPosition();
        if(isDead) ChangeState(new PlayerLoseState());
    }
    //Skin + Wp Override
    public override void SetSkin(SkinData skinData)
    {
        UserData user = GameManager.Instance.userDataManager.userData;
        Hat = skinData.GetHat(user.currentHatIndex).GetHatGameObject();
        if (Hat != null)
        {
            GameObject botHat = Instantiate(Hat, Head.transform.position, Quaternion.identity, Head.transform);
            botHat.transform.position += Vector3.up * 0.4f;
        }
        PantsMaterial = skinData.GetPants(user.currentPantIndex).GetPantsMaterial();
        Pants.GetComponent<Renderer>().material = PantsMaterial;
    }
    public override void SetWeapon(WeaponData weaponData)
    {
        UserData user = GameManager.Instance.userDataManager.userData;
        //Debug.Log("Current wp index:" + user.currentWeaponIndex + weaponData.GetWeaponType(user.currentWeaponIndex));
        WeaponType = weaponData.GetWeaponType(user.currentWeaponIndex);
        Gun = weaponData.GetGun(user.currentWeaponIndex);   
        if(Gun != null)
        {   
            GameObject characterWeapon = Instantiate(Gun, Hand.transform.position, Quaternion.identity, Hand.transform);
            characterWeapon.SetActive(true);
            characterWeapon.transform.rotation = Quaternion.Euler(180, 90, 0);
        }
        // Debug.Log(WeaponType + "and" + Weapon.GetBulletByWeaponType(WeaponType));
        BulletPool.Instance.CreateBulletPool(WeaponType, Weapon.GetBulletByWeaponType(WeaponType));
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
