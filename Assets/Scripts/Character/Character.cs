using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("State")]
    [SerializeField] public IState<Character> currentState;
    [Header("Animation")]
    [SerializeField] public Animator anim;
    public string currentAnimName;
    public bool isWin;
    public bool isDead;
    [Header("Target")]
    [SerializeField] public Collider[] hitColliders;
    [SerializeField] LayerMask layer;
    public GameObject target;
    [Header("Movement")]
    public float MoveSpeed;
    public Vector3 MoveDirection;
    [Header("Size and Range")]
    [SerializeField] public int KillCount = 0;
    [SerializeField] public float Range = 10.0f;
    [SerializeField] public float SizeMultiplier = 1f;
    [Header("Skin and Weapon")]
    [SerializeField] public SkinData Skin;
    [SerializeField] public WeaponData Weapon;
    [SerializeField] public WeaponType WeaponType;
    [SerializeField] public GameObject CharacterSkin;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject Gun;
    [SerializeField] public GameObject Pants;
    [SerializeField] public Material PantsMaterial;
    [SerializeField] public GameObject Head;
    [SerializeField] public GameObject Hat;
    [SerializeField] public Color Color;

    [Header("Bullet")]
    [SerializeField] public float BulletSpeed = 5f;
    public GameObject CurrentBullet;
    [Header("Enemy List")]
    [SerializeField] public BotSpawner BotSpawner;


    public virtual void Start()
    {
        OnInit();
    }

    public virtual void Update()
    {
        if (currentState == null) return;
        if (isWin) ChangeState(new PlayerWinState());
        currentState.OnExecute(this);
        // BulletPool.Instance.LogDictionary();
    }
    
    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public virtual void OnInit()
    {
        ChangeState(new PlayerIdleState());
        SetWeapon(Weapon);
        SetSkin(Skin);
    }
    
    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    //Attack
    public void LookAtEnemy()
    {
        Vector3 lookDirection = (target.transform.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void Fire()
    {
        if (!BulletPool.Instance.IsBulletActive(WeaponType,this))
        {
            BulletSpawn();
        }
    }
    public void BulletSpawn()
    {
        CurrentBullet = BulletPool.Instance.GetBullet(WeaponType);
        if (CurrentBullet != null)
        {
            CurrentBullet.transform.position = gameObject.transform.position;
            Vector3 originalScale = BulletPool.Instance.GetOriginalScale(WeaponType);
            CurrentBullet.transform.localScale = originalScale * SizeMultiplier;
            CurrentBullet.transform.position += Vector3.up * SizeMultiplier;
            Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
            CurrentBullet.GetComponent<Bullet>().OnInit(bulletDirection, BulletSpeed, this, Range, WeaponType); 
        }
    }

    //Set Weapon
    public void SetWeapon(WeaponData weaponData)
    {
        int randomIndex = Random.Range(0, weaponData.weaponList.Count);
        Gun = weaponData.GetGun(randomIndex);
        WeaponType = weaponData.GetWeaponType(randomIndex);
        if(Gun != null)
        {   
            GameObject characterWeapon = Instantiate(Gun, Hand.transform.position, Quaternion.identity, Hand.transform);
            characterWeapon.transform.rotation = Quaternion.Euler(180, 90, 0);
        }
        // Debug.Log(WeaponType + "and" + Weapon.GetBulletByWeaponType(WeaponType));
        BulletPool.Instance.CreateBulletPool(WeaponType, Weapon.GetBulletByWeaponType(WeaponType));
    }

    //Set Skin
    public void SetSkin(SkinData skinData)
    {
        int randomIndex = Random.Range(0, skinData.skinList.Count);

        // Set hat
        Hat = skinData.GetHat(randomIndex);
        if (Hat != null)
        {
            GameObject botHat = Instantiate(Hat, Head.transform.position, Quaternion.identity, Head.transform);
            botHat.transform.position += Vector3.up * 0.4f;
        }

        // Set color
        Color = skinData.GetColor(randomIndex);
        CharacterSkin.GetComponent<Renderer>().material.color = Color;

        // Set pants material
        PantsMaterial = skinData.GetPantsMaterial(randomIndex);
        if (PantsMaterial != null)
        {
            Pants.GetComponent<Renderer>().material = PantsMaterial;
        }
    }

    //Target 
    public void objectScan()
    {
        hitColliders = Physics.OverlapSphere(transform.position, Range, layer);
        hitColliders = FilterSelf(hitColliders);
        if (hitColliders.Length > 0) target = hitColliders[hitColliders.Count()-1].gameObject;
        else target = null;
    }

    private Collider[] FilterSelf(Collider[] colliders)
    {
        List<Collider> filteredColliders = new List<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != this.gameObject && !collider.GetComponent<Character>().isDead)
            {
                filteredColliders.Add(collider);
            }
        }
        return filteredColliders.ToArray();
    }

    public int CheckTarget(Collider[] hitColliders)
    {
        int targetCount = 0;
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                targetCount++;
            }
        }
        return targetCount;
    }

    //Destroy
    public void OnDeactive()
    {
        BotSpawner.Instance.ReturnBot(this.gameObject);
    }




    

}
