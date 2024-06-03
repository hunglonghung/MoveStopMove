using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float scanRadius = 5.0f;
    [Header("Movement")]
    public float MoveSpeed;
    public Vector3 MoveDirection;
    [Header("Skin and Weapon")]
    [SerializeField] public SkinData Skin;
    [SerializeField] public GameObject CharacterSkin;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject Weapon;
    [SerializeField] public GameObject Pants;
    [SerializeField] public Material PantsMaterial;
    [SerializeField] public GameObject Head;
    [SerializeField] public GameObject Hat;
    [SerializeField] public Color Color;

    [Header("Bullet")]
    [SerializeField] public float bulletSpeed = 5f;
    public GameObject CurrentBullet;
    [Header("Enemy List")]
    [SerializeField] public BotSpawner BotSpawner;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        isWin = CheckWin();
        if (currentState == null) return;
        if (isWin) ChangeState(new PlayerWinState());
        currentState.OnExecute(this);
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
        if (!BulletPool.Instance.IsBulletActive(this))
        {
            CurrentBullet = BulletPool.Instance.GetBullet();
            if (CurrentBullet != null)
            {
                CurrentBullet.transform.position = gameObject.transform.position;
                CurrentBullet.transform.position += Vector3.up;
                Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
                CurrentBullet.GetComponent<Bullet>().OnInit(bulletDirection, bulletSpeed, this, scanRadius); // Truyền thông tin attacker
            }
        }
    }

    //Set Weapon
    public void SetWeapon(GameObject Weapon)
    {
        GameObject characterWeapon = Instantiate(Weapon, Hand.transform.position, Quaternion.identity, Hand.transform);
        characterWeapon.transform.rotation = Quaternion.Euler(180, 90, 0);
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
        hitColliders = Physics.OverlapSphere(transform.position, scanRadius, layer);
        hitColliders = FilterSelf(hitColliders);
    }

    private Collider[] FilterSelf(Collider[] colliders)
    {
        List<Collider> filteredColliders = new List<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != this.gameObject)
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
            target = hitColliders[0].gameObject;
        }
        return targetCount;
    }

    //Destroy
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    //Win condition
    public bool CheckWin()
    {
        int enemyRemaining = BotSpawner.UnspawnedBotList.Count + BotSpawner.SpawnedBotList.Count ;
        Debug.Log(enemyRemaining);
        if(enemyRemaining == 0) return true;
        else return false;
    }

    

}
