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
    private GameObject targetObject;
    [SerializeField] public GameObject TargetPrefab;
    [Header("Skin")]
    [SerializeField] List<GameObject> playerHat = new List<GameObject>();
    [SerializeField] List<GameObject> playerShield = new List<GameObject>();
    [Header("Gun")]
    [SerializeField] List<GameObject> playerWeapon = new List<GameObject>();


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
        PantsMaterial = skinData.GetPants(user.currentPantIndex).GetPantsMaterial();
        Pants.GetComponent<Renderer>().material = PantsMaterial;
        Hat = skinData.GetHat(user.currentHatIndex).GetHatGameObject();
        if (Hat != null)
        {
            if(playerHat.Count == 0)
            {
                playerHat.Add(Instantiate(Hat, Head.transform.position + Vector3.up * 0.6f, Quaternion.identity, Head.transform));
            }
            else
            {
                for(int i = 0; i < playerHat.Count; i++)
                {
                    Destroy(playerHat[i]);
                } 
                playerHat.Add(Instantiate(Hat, Head.transform.position + Vector3.up * 0.6f, Quaternion.identity, Head.transform));
            }
        }
        Shield = skinData.GetShield(user.currentShieldIndex).GetShieldGameObject();
        if(Shield != null)
        {
            if(playerShield.Count == 0)
            {
                playerShield.Add(Instantiate(Shield, LeftHand.transform.position, Quaternion.Euler(70,-22,-243), LeftHand.transform));
            }
            else
            {
                for(int i = 0; i < playerShield.Count; i++)
                {
                    Destroy(playerShield[i]);
                } 
                playerShield.Add(Instantiate(Shield, LeftHand.transform.position, Quaternion.Euler(70,-22,-243), LeftHand.transform));
            }
        }

    }
    public override void SetWeapon(WeaponData weaponData)
    {
        UserData user = GameManager.Instance.userDataManager.userData;
        //Debug.Log("Current wp index:" + user.currentWeaponIndex + weaponData.GetWeaponType(user.currentWeaponIndex));
        WeaponType = weaponData.GetWeaponType(user.currentWeaponIndex);
        Gun = weaponData.GetGun(user.currentWeaponIndex);   
        if(Gun != null)
        {   
            GameObject characterWeapon = Instantiate(Gun, RightHand.transform.position, Quaternion.Euler(180, 90, 0), RightHand.transform);
            if(playerWeapon.Count == 0)
            {
                playerWeapon.Add(characterWeapon);
            }
            else
            {
                for(int i = 0; i < playerWeapon.Count; i++)
                {
                    Destroy(playerWeapon[i]);
                } 
                playerWeapon.Add(characterWeapon);
            }
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
