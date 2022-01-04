using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    public float speed;
    public float jumpforce;
    public Transform LeftPoint;
    public Transform RightPoint;
    public LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D coll;

    //0=left,1=right
    private bool FaceDirection;

    private float left;
    private float right;

    private bool frogIdleFinished;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

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
        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            Movement();
        }
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

                m_Animator.SetBool("jumping", true);
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

                m_Animator.SetBool("jumping", true);
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
        if (m_Animator.GetBool("jumping") && rb.velocity.y < 0)
        {
            m_Animator.SetBool("jumping", false);
            m_Animator.SetBool("falling", true);
        }
        if (m_Animator.GetBool("falling") && coll.IsTouchingLayers(ground))
        {
            m_Animator.SetBool("falling", false);
        }
    }

    public void FrogIdle()
    {
        frogIdleFinished = true;
    }
}