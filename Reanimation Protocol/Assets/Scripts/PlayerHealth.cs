using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour 
{
    public int currentHealth;
    public int maxHealth = 18;
    public HealthBar healthbar;
    void start ()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    void update()
    {
         if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthbar.SetHealth(currentHealth);
       
    } 

}

   