using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected Rigidbody2D rb;
    protected AudioSource death;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
    }
    public void JumpedOn()
    {
        rb.velocity = Vector2.zero;
        Anim.SetTrigger("Death");
        death.Play();
        GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
