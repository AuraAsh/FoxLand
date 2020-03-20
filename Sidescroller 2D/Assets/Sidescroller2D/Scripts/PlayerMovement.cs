using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;


    public float runSpeed = 40f;
    public float Speedjump = 15f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Rigidbody2D rbsd;
    void Update()

    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") &&!jump)
        {
            jump = true;
            animator.SetBool("IsJumping", true);

            if (jump)
            {
                rbsd.AddForce(Vector2.up * Speedjump, ForceMode2D.Impulse);
                jump = false;
                print("the player jump");
            }
           
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }
        CallMovementPlayer();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }


    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void CallMovementPlayer()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, crouch);
        jump = false; 

        }
}
