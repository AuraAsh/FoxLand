using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator Anim;

    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;
    [SerializeField]private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;

    

    private bool facingLeft = true;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Anim.GetBool("Jump"))
        {
            if(rb.velocity.y < .1)
            {
                Anim.SetBool("Fall", true);
                Anim.SetBool("Jump", false);
            }
        }
        if (coll.IsTouchingLayers() && Anim.GetBool("Fall"))
        {
            Anim.SetBool("Fall", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    Anim.SetBool("Jump", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    Anim.SetBool("Jump", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    public void JumpedOn()
    {
        Anim.SetTrigger("Death");
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
