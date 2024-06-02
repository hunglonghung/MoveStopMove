using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        if (currentState == null) return;
        Debug.Log(currentState);
        currentState.OnExecute(this);
    }
    
    //Change State
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


    // Initiallize
    public void OnInit()
    {
        ChangeState(new IdleState());
    }
    
    // Change Animation 
    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void LookAtEnemy()
    {
        Vector3 lookDirection = (target.transform.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    public void Fire()
    {
        if(!BulletPool.Instance.IsBulletActive())
        {
            CurrentBullet = BulletPool.Instance.GetBullet();
            CurrentBullet.transform.position = Hand.transform.position;
            Vector3 bulletDirection = (target.transform.position - gameObject.transform.position).normalized;
            CurrentBullet.GetComponent<Bullet>().OnInit(bulletDirection, bulletSpeed, this, scanRadius);
        }
        
        
    }

    //Weapon
    public void setWeapon(GameObject Weapon)
    {
        GameObject characterWeapon = Instantiate(Weapon,Hand.transform.position,Quaternion.identity,Hand.transform);
        characterWeapon.transform.rotation = Quaternion.Euler(180,90,0);
    }
    //Dead
    public void Die()
    {
        ChangeAnim("die");

    }
    //Dance
    public void Dance()
    {
        ChangeAnim("dance");
    }

    public void objectScan()
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
    // public void CheckWin(BotList bots)
    // {
    //     if(bots.bots.Count == 0)
    //     {
    //         GetComponent<Character>().isWin = true;
    //     }
        
    // }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "bullet")
        {
            GetComponent<Character>().isDead = true;
        }
    }
    


}