using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("State")]
    [SerializeField] public IState<Character> currentState;
    [Header("Animation")]
    [SerializeField] public Animator anim;
    public string currentAnimName;
    public bool isWin;
    public bool isDead;
    [SerializeField] public Collider[] hitColliders;
    [SerializeField] LayerMask layer;
    [SerializeField] public BotList bots;
    public float MoveSpeed;
    public Vector3 MoveDirection;
    public float scanRadius = 5.0f;
    public GameObject target;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject Weapon;
    [SerializeField] public float bulletSpeed = 5f;
    public GameObject CurrentBullet;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        if (currentState == null) return;
        Debug.Log(currentState);
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

    public void OnInit()
    {
        ChangeState(new PlayerIdleState());
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
                Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
                CurrentBullet.GetComponent<Bullet>().OnInit(bulletDirection, bulletSpeed, this, scanRadius); // Truyền thông tin attacker
            }
        }
    }

    //Set Weapon
    public void setWeapon(GameObject Weapon)
    {
        GameObject characterWeapon = Instantiate(Weapon,Hand.transform.position,Quaternion.identity,Hand.transform);
        characterWeapon.transform.rotation = Quaternion.Euler(180,90,0);
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
        if(hitColliders.Count() > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                targetCount ++;
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
}
