using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public Transform LeftPoint;
    public Transform RightPoint;
    public LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    //0=left,1=right
    private bool FaceDirection;

    private float left;
    private float right;

    private bool frogIdleFinished;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        transform.DetachChildren();
        left = LeftPoint.position.x;
        right = RightPoint.position.x;

        Destroy(LeftPoint.gameObject);
        Destroy(RightPoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        SwithAnim();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!FaceDirection)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (coll.IsTouchingLayers(ground) && frogIdleFinished)
            {
                rb.velocity = new Vector2(-speed, jumpforce);

                anim.SetBool("jumping", true);
                frogIdleFinished = false;
            }
            if (!coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            if (transform.position.x < left)
            {
                FaceDirection = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);

            if (coll.IsTouchingLayers(ground) && frogIdleFinished)
            {
                rb.velocity = new Vector2(speed, jumpforce);

                anim.SetBool("jumping", true);
                frogIdleFinished = false;
            }
            if (!coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            if (transform.position.x > right)
            {
                FaceDirection = false;
            }
        }
    }

    private void SwithAnim()
    {
        if (anim.GetBool("jumping") && rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("falling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }

    public void FrogIdle()
    {
        frogIdleFinished = true;
    }
}