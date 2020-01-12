using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isHurt;

    public Collider2D coll;
    public Collider2D disColl;
    public Transform headCheck;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int Score = 0;
    public Text Number;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        // 角色移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        // 角色跳跃
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            anim.SetBool("jumping", true);
        }

        // 角色下蹲
        if(!Physics2D.OverlapCircle(headCheck.position, 0.2f, ground))
        {
            if (Input.GetButtonDown("Crouch") && coll.IsTouchingLayers(ground))
            {
                anim.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                anim.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }
        
    }

    // 切换动画
    void SwitchAnim()
    {
        anim.SetBool("idle", true);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(isHurt)
        {
            anim.SetBool("hurting", true);
            anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurting", false);
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    // 收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Score += 1;
            Number.text = Score.ToString();
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
