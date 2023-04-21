using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatKnight : MonoBehaviour {

    #region Serialized Feilds
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource rollSFX;
    [SerializeField] private AudioSource attack1SFX;
    [SerializeField] private AudioSource attack2SFX;
    [SerializeField] private AudioSource attack3SFX;
    [SerializeField] private AudioSource landSFX;
    [SerializeField] private AudioSource deathSFX;
    #endregion

    #region Public Values
    public Animator             m_animator;
    #endregion

    #region Private Values
    private Rigidbody2D         m_body2d;
    private Sensor_CatKnight    m_groundSensor;
    private bool                m_grounded = false;
    private int                 _jumpsLeft;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
   #endregion


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_CatKnight>();
        _jumpsLeft = maxJumps;
    }

    // Update is called once per frame
        void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Reset number of jumps
        if(m_grounded && m_body2d.velocity.y <= 0)
        {
            _jumpsLeft = maxJumps;
        }

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            landSFX.Play();
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            transform.localScale = new Vector2(1,1);
        }
            
        else if (inputX < 0)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            transform.localScale = new Vector2(-1, 1);
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Attack
        if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.3f)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;


            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"    
            m_animator.SetTrigger("Attack" + m_currentAttack);
            
            if(m_currentAttack == 1)
            {
            attack1SFX.Play();
            }
            else if(m_currentAttack == 2)
            {
                attack2SFX.Play();
            }
            else if(m_currentAttack == 3)
            {
                attack3SFX.Play();
            }
            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
        

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
            

        //Jump
        else if (Input.GetKeyDown("space") && _jumpsLeft > 0)
        {
            jumpSFX.Play();
            _jumpsLeft -= 1;
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);

        }
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }
    // Take damage
   
    void freezeMove()
    {
        m_body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
    } 

    void canMove()
    {
        m_body2d.constraints = RigidbodyConstraints2D.None;
        m_body2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
