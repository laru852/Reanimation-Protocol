using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpikeTrap : MonoBehaviour
{

    void Awake()
    {
        player = GetComponent<CatKnight>();
    }

 void Start ()
    {
        GameObject.Find("m_body2d")
    }

    public CatKnight player;

    private void OnTriggerEnter2D(Collider2D trap)
    {
        if(trap.CompareTag("Player"))
        {
            Debug.Log("One");
            Destroy(trap.gameObject, 1f);
            animator.SetTrigger("IsDead");
            m_body2d.gravityScale = 0;
        }
    }

}
