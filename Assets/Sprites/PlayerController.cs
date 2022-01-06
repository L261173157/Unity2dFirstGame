using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Collider2D coll;
    public Collider2D boxColl;
    public LayerMask ground;
    public LayerMask resister;
    public float speed;
    public float jumpforce;
    public Text cherryNumber;

    private int _cherry;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isJumpPressed;
    private float _horizontalMove;
    private float _vertical;
    private float _faceDirection;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _isJumpPressed = Input.GetButtonDown("Jump");
        _horizontalMove = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _faceDirection = Input.GetAxisRaw("Horizontal");

        SwithAnim();
    }

    private void FixedUpdate()
    {
        if (!_anim.GetBool("hurting"))
        {
            Movement();
        }
    }

    private void Movement()
    {
        //角色移动
        if (_horizontalMove != 0)
        {
            _rb.velocity = new Vector2(_horizontalMove * speed*Time.fixedDeltaTime, _rb.velocity.y);
            if (!_anim.GetBool("jumping"))
            {
                _anim.SetBool("running", true);
            }
            else
            {
                _anim.SetBool("running", false);
            }
        }
        else
        {
            _anim.SetBool("running", false);
        }

        if (_faceDirection != 0)
        {
            transform.localScale = new Vector3(_faceDirection, 1, 1);
        }

        //角色跳跃
        if (_isJumpPressed)
        {
            if (coll.IsTouchingLayers(ground))
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpforce*Time.fixedDeltaTime);
                _anim.SetBool("jumping", true);
                _anim.SetBool("running", false);
            }
        }

        //角色下蹲
        if (coll.IsTouchingLayers(ground))
        {
            if (_vertical < 0)
            {
                _anim.SetBool("crouching", true);
                boxColl.enabled = false;
            }
            else if (!boxColl.IsTouchingLayers(resister))
            {
                _anim.SetBool("crouching", false);
                boxColl.enabled = true;
            }
        }
    }

    //切换动画效果
    private void SwithAnim()
    {
        _anim.SetBool("idling", false);
        //跳跃
        if (_anim.GetBool("jumping"))
        {
            if (_rb.velocity.y < 0)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }
        }
        if (_anim.GetBool("falling") && coll.IsTouchingLayers(ground))
        {
            _anim.SetBool("falling", false);
            _anim.SetBool("idling", true);
        }
        if (_rb.velocity.y < 0 && !coll.IsTouchingLayers(ground))
        {
            _anim.SetBool("falling", true);
            _anim.SetBool("jumping", false);
            _anim.SetBool("running", false);
        }
    }

    //获取物品
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Items"))
        {
            //col.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(col.gameObject);
            _cherry++;
            cherryNumber.text = _cherry.ToString();
        }
    }

    //遇到敌人
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //消灭敌人
            if (_anim.GetBool("falling"))
            {
                var enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.JumpOn();
                }
                _rb.velocity = new Vector2(_rb.velocity.x, jumpforce*Time.fixedDeltaTime);
                _anim.SetBool("jumping", true);
                _anim.SetBool("falling", false);
            }
            else
            {
                if (transform.position.x < col.gameObject.transform.position.x)
                {
                    _rb.velocity = new Vector2(-5, _rb.velocity.y);
                }

                if (transform.position.x > col.gameObject.transform.position.x)
                {
                    _rb.velocity = new Vector2(5, _rb.velocity.y);
                }

                Hurt();
            }
        }
    }

    //受伤判断
    private void Hurt()
    {
        _anim.SetBool("hurting", true);

        Invoke("Recover", 0.5f);
    }

    //受伤恢复
    private void Recover()
    {
        _anim.SetBool("hurting", false);
        _anim.SetBool("idling", true);
    }
}