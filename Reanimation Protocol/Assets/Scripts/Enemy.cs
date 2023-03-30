using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        Debug.Log("Enemy Died!");

        animator.SetBool("IsDead", true);

        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        //FindObjectOfType<AIPath>().enabled = false;
        GetComponentInParent<CircleCollider2D>().enabled = false;
         GetComponentInParent<AIPath>().enabled = false; 
        //FindObjectOfType<A*>().enabled = false;
        
    }
}
