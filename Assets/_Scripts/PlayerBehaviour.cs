﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystivkVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public bool isJumping;
    public Transform spawnPoint;

    private Rigidbody2D m_rigidBody2d;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        if(isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                m_rigidBody2d.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                m_rigidBody2d.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if(!isJumping)
            {
                m_animator.SetInteger("AnimState", 0);
            }
            
            if (joystick.Vertical > joystivkVerticalSensitivity && !isJumping)
            {
                m_rigidBody2d.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                m_animator.SetInteger("AnimState", 2);
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = spawnPoint.position;
        }
    }
}
