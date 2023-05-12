using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    Rigidbody2D EnemyRigidBody;
    [SerializeField] Rigidbody2D Rigidbody;
    
    #region Public Variables
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        EnemyRigidBody = GetComponent<Rigidbody2D>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Invoke(nameof(pause), 1f);   
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Dead");
        Rigidbody.gravityScale = 0;
        Invoke(nameof(wait), 0.825f);
    }
    
    void pause()
        {
            Debug.Log("Freeze foo");
            EnemyRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

    void wait()
    {
        Object.Destroy(this.gameObject);
        CancelInvoke();
    }
}
