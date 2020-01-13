using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private float topy, bottomy;
    private bool isUp;

    public Transform topPoint, bottomPoint;
    public float speed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        topy = topPoint.position.y;
        bottomy = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > topy)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < bottomy)
            {
                isUp = true;
            }
        }
    }
}
