using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpHeight = 20f;
    Rigidbody2D myRidgidbody;
    Vector2 moveInput;
    Animator myAnimator;
    void Start()
    {
        myRidgidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        Run();
        FlipSprite();
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
        if (value.isPressed)
        {
            myRidgidbody.velocity += new Vector2(0f, jumpHeight);
        }
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
