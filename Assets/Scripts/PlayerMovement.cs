using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpHeight = 20f;
    [SerializeField] float climbSpeed = 8f;
    Rigidbody2D myRidgidbody;
    Vector2 moveInput;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    float gravityScaleAtStart;

    void Start()
    {
        myRidgidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRidgidbody.gravityScale;
    }


    void Update()
    {
        FlipSprite();
        Run();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRidgidbody.velocity.y);
        myRidgidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRidgidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRidgidbody.velocity += new Vector2(0f, jumpHeight);
        }
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRidgidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRidgidbody.velocity.x, moveInput.y * climbSpeed);
        myRidgidbody.velocity = climbVelocity;
        myRidgidbody.gravityScale = 0f;

        // I didnt like how this looked
        // bool playerHasVerticalSpeed = Mathf.Abs(myRidgidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", true);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRidgidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidbody.velocity.x), 1f);
        }

    }
}
