using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    private Rigidbody2D rb;
    private bool faceLeft = true;
    private float leftx, rightx;
    // private Animator anim;
    private Collider2D coll;

    public LayerMask ground;
    public Transform leftPoint, rightPoint;
    public float speed,jumpForce;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        SwitchAnim();
    }

    void Movement()
    {
        if(faceLeft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                anim.SetBool("jumping", true);
            }
            if(transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speed, jumpForce);
                anim.SetBool("jumping", true);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }
}
