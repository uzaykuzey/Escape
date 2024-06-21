using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;
    private short canDoubleJump = 0;
    private bool canChangeGravity = false;
    private bool canJump = false;
    private int health = 3;
    private int bulletNumber = 0;
    private float lastHit = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask gravityController;
    [SerializeField] private PowerUpController powerUpController;
    [SerializeField] private BulletSpawning bulletSpawning;

    void Update()
    {
        if (powerUpController.getCurrentType() == 3)
        {
            rb.gravityScale = 8 * Mathf.Sign(rb.gravityScale);
        }
        else
        {
            rb.gravityScale = 4 * Mathf.Sign(rb.gravityScale);
        }

        if(powerUpController.getCurrentType() == 4 && canDoubleJump==0)
        {
            canDoubleJump = 2;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        float jumpingPower = this.jumpingPower * (powerUpController.getCurrentType() == 3 ? 2 : 1);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower*Mathf.Sign(rb.gravityScale));
            canJump = false;
        }

        if (Input.GetButtonUp("Jump") && Mathf.Abs(rb.velocity.y) > 0f && canDoubleJump==1)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            if (canDoubleJump==1)
            {
                canDoubleJump = 2;
            }
        }

        if(canDoubleJump==2 && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.90f*jumpingPower * Mathf.Sign(rb.gravityScale));
            if(powerUpController.getCurrentType()!=4)
            {
                canDoubleJump = 0;
            }
            
        }

        if(Input.GetKeyDown("s") && Mathf.Abs(rb.velocity.y)>0)
        {
            rb.velocity = rb.velocity + new Vector2(0, -rb.gravityScale*jumpingPower / 10);
        }

        if(canChangeGravity && Physics2D.OverlapCircle(rb.position, 0.5f, gravityController))
        {
            rb.gravityScale *= -1;
            canChangeGravity = false;
            rb.SetRotation((int)((rb.rotation + 180) % 360));
        }

        Flip();
    }

    private void FixedUpdate()
    {
        float speed = this.speed * (powerUpController.getCurrentType() == 3 ? 3 : 1);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    /*private bool IsGrounded()
    {
        if(Physics2D.OverlapCircle(rb.position, 1.02f, groundLayer))
        {
            canDoubleJump = 1;
            canChangeGravity = true;
            return true;
        }
        return false;
    }*/

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f && rb.gravityScale>0) || (!isFacingRight && horizontal > 0f && rb.gravityScale > 0) || (isFacingRight && horizontal > 0f && rb.gravityScale < 0) || (!isFacingRight && horizontal < 0f && rb.gravityScale < 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("PowerUps"))
        {
            powerUpController.teleportAway();
            powerUpController.setUp();
        }
        if (bulletNumber<4 && collision.gameObject.name.Contains("BulletSpawner"))
        {
            bulletSpawning.teleportAway();
            bulletNumber++;
        }
        if (collision.gameObject.name.Contains("Ground"))
        {
            
            canJump = true;
            canDoubleJump = 1;
            canChangeGravity = true;
        }
        if (collision.gameObject.name.Contains("Enemy"))
        {
            if(!isInvulnerable())
            {
                lastHit = Time.timeSinceLevelLoad;
                health--;
            }
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getBullets()
    {
        return bulletNumber;
    }

    public void changeBullets(int value)
    {
        bulletNumber += value;
    }

    public bool isInvulnerable()
    {
        return Time.timeSinceLevelLoad - lastHit < 2;
    }
}
