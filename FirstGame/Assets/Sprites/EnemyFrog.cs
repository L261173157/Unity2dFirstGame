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

    //0=left,1=right
    private bool FaceDirection;

    private float left;
    private float right;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll=GetComponent<Collider2D>();
        transform.DetachChildren();
        left = LeftPoint.position.x;
        right = RightPoint.position.x;

        Destroy(LeftPoint.gameObject);
        Destroy(RightPoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!FaceDirection)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            if (transform.position.x < left)
            {
                FaceDirection = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            if (transform.position.x > right)
            {
                FaceDirection = false;
            }
        }
    }
}