using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{

    void Awake()
    {
        player = GameObject.Find("CatKnight");
        animator = player.GetComponent<Animator>();
        particles = player.GetComponent<ParticleSystem>();
    }

 void Start ()
    {
        GameObject.Find("m_body2d");
    }
    public GameObject player;
    public Animator animator;
    public ParticleSystem particles;
    private void OnTriggerEnter2D(Collider2D trap)
    {
        if(trap.CompareTag("Player"))
        {
            particles.Play();
            player.GetComponent<CatKnight>().freezeMove();
            animator.SetTrigger("IsDead");
            Destroy(trap.gameObject, 1f);
            SceneManager.LoadScene("GameOver");
            // m_body2d.gravityScale = 0;
        }
    }

}
