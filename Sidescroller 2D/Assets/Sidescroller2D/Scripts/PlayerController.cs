using UnityEngine;
public enum State
{
    idle, running, jumping, falling
};
public class PlayerController : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator Anim;

    [SerializeField] private State state = State.idle;
    [SerializeField] private LayerMask ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void Update()
    {
        #region SETTINGS MOVEMENT
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        VelocityState();

        #endregion
       
        #region SETTINGS JUMP PLAYER
        if (Input.GetButtonDown("Jump") && !coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;

            Anim.SetBool("isJump", true);
        }
        else
        {
            Anim.SetBool("isJump", false);
        } 
        #endregion

    }
    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }


    }


}
  