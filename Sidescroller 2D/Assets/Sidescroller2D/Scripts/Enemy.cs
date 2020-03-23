using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected Rigidbody2D rb;
    protected virtual void Start()

    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void JumpedOn()
    {
        Anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
