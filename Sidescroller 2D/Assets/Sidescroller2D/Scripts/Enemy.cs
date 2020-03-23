using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
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
