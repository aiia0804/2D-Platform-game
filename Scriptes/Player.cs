using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float MovingSpeed = 1f;
    [SerializeField] float JumpSpeed = 10f;
    [SerializeField] float ClimbingSpeed = 1f;

    float defaultGravity;


    //State
    bool isAlive = true;
    Vector2 deathKick = new Vector2(0f, 80f);


    //Cached Compnent references
    Animator animator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyColider;
    BoxCollider2D myFeet;


    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyColider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        defaultGravity = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Move();
        Jump();
        FlipSPrite();
        laddleClimbing();
        death();
    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myFeet.IsTouchingLayers(LayerMask.GetMask("Laddle")))
        {
            animator.SetBool("Jumping", false); return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelecityToAdd = new Vector2(myRigidBody.velocity.x, JumpSpeed);
            myRigidBody.velocity += jumpVelecityToAdd;
            animator.SetBool("Jumping", true);
        }
    }

    private void Move()
    {
        float controlThrow = Input.GetAxis("Horizontal");//value is betwween -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * MovingSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool HorizontalActived = Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon;

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Running", HorizontalActived);
        }
    }

    private void FlipSPrite()
    {
        bool playerHasHorizontal = Mathf.Abs(myRigidBody.velocity.x) > 0;
        var x = Mathf.Abs(transform.localScale.x);
        var y = transform.localScale.y;
        if (playerHasHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * x, y);
        }
    }


    public void laddleClimbing()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Laddle")))
        {
            myRigidBody.gravityScale = defaultGravity;
            return;
        }

        float climbThrow = Input.GetAxis("Vertical");//value is betwween -1 to +1
        Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, climbThrow * ClimbingSpeed);
        myRigidBody.velocity = playerVelocity;
        myRigidBody.gravityScale = 0;
    }

    private void death()
    {
        string[] deatTrigger = { "Enemy", "Hazard" };
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask(deatTrigger)))

        {
            isAlive = false;
            GetComponent<Rigidbody2D>().velocity = deathKick;
            animator.SetTrigger("Die");
            FindObjectOfType<GameSession>().playerDie();
        }
    }
}
