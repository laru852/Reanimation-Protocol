using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rigidbody;
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Ouch");

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.enabled = false;
        animator.SetTrigger("Dead");
        GetComponent<BoxCollider2D>().enabled = false;
        Rigidbody.gravityScale = 0;
        GetComponent<EnemyBehavior>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponentInChildren<CircleCollider2D>().enabled = false;
    }
    
}
