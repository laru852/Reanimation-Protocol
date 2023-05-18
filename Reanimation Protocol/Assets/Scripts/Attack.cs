using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public CatKnight playerScript;
    public HealthBar health;
    public Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trig) 
    {   
     if (trig.gameObject.tag == "Player" && trig.GetComponent<CatKnight>())
            {
                if (trig.GetComponentInChildren<Collider2D>()!=null)
                {
                    playerScript.currentHealth -= enemyScript.damage;
                    playerScript.animator.SetTrigger("Hurt");
                    health.FillImage.fillAmount = playerScript.currentHealth / 100;
                }
            }
    }
}
