using UnityEngine;
using UnityEngine.UI;
public enum State
{
    idle, running, jumping, falling, hurt
};
public class PlayerController : MonoBehaviour
{
    //Start() Variables
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator Anim;
   
    //Inspector Variables
    [SerializeField] private State state = State.idle;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private float hurtforce = 10f;
    [SerializeField] private int Score = 0;
    [SerializeField] private Text scoreText;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        VelocityState();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            scoreText.text = Score.ToString();
            Score += 1;
            Destroy(collision.gameObject); 
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.falling)
            {
                Jump();
                enemy.JumpedOn();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                }
            }
        }
    }

    private void Movement()
    {
        #region SETTINGS MOVEMENT
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            state = State.running;
            transform.localScale = new Vector2(-1, 1);

            Anim.SetBool("isRun", true);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            state = State.running;
            transform.localScale = new Vector2(1, 1);

            Anim.SetBool("isRun", true);
        }
        else
        {
            Anim.SetBool("isRun", false);
        }
        VelocityState();

        #endregion

        #region SETTINGS JUMP PLAYER
        if (Input.GetButtonDown("Jump") && !coll.IsTouchingLayers())
        {
            Jump();
            Anim.SetBool("isJump", true);
        }
        else
        {
            Anim.SetBool("isJump", false);
        }
        #endregion

        #region SETTINGS HURT PLAYER
        if (state == State.hurt)
        {
            rb.velocity = new Vector2(rb.velocity.x, hurtforce);
            Anim.SetBool("isHurt", true);
            state = State.hurt;
        }
        else
        {
            Anim.SetBool("isHurt", false);
        }
        #endregion

    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
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
            if (coll.IsTouchingLayers())
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
            else
            {
                Anim.SetBool("isHurt", true);
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
  