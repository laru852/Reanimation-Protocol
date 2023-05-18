using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZone : MonoBehaviour
{
    private EnemyBehavior enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() 
    {
        {
            enemyParent = GetComponentInParent<EnemyBehavior>();
            anim = GetComponentInParent<Animator>();
        }
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("S_attack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        inRange = false;
        gameObject.SetActive(false);
        enemyParent.triggerArea.SetActive(true);
        enemyParent.inRange = false;
        enemyParent.SelectTarget();
    }

}
