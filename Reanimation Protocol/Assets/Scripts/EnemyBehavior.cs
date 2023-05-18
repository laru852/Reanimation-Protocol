using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; 
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    public Animator animator;
    public int currentHealth;
    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask playerMask;
    public int enemyDamage = 15;
    public new Collider2D collider;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    public CatKnight playerScript;
    public Enemy enemyScript;


    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private float distance; 
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion
    
    void Start()
    {
        GetComponentInChildren<BoxCollider2D>();
        GetComponent<GameObject>();
        enemyScript = FindObjectOfType<Enemy>();
        playerScript = FindObjectOfType<CatKnight>();
    }

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
            {
                Move();
            }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTarget();
        }

        if(inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();            
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

     
    void Attack()
    {   
        timer = intTimer; //Reset timer when player enter attack range 
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
        StopAttack();
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        } 
        else
        {
            target = rightLimit;
        }
        Flip();
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

       transform.eulerAngles = rotation;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;     
        animator.SetTrigger("Hurt");


        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 2);
        this.enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false; 
        Destroy(GetComponentInChildren<BoxCollider2D>());
    }
    
}
