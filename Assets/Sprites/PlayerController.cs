using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Collider2D coll;
    public LayerMask ground;
    public LayerMask resister;
    public float speed;
    public float jumpforce;
    public Text cherryNumber;
    public Transform resisterCheck, jumpCheck;

    private int _cherry;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isJumpPressed;
    private float _horizontalMove;
    private float _vertical;
    private float _faceDirection;
    [SerializeField]
    private bool _touchGround;
    [SerializeField]
    private int _extraJump;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _isJumpPressed = Input.GetButton("Jump");
        _horizontalMove = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _faceDirection = Input.GetAxisRaw("Horizontal");

        SwitchAnim();
    }

    private void FixedUpdate()
    {
        CheckStatus();

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
            _rb.velocity = new Vector2(_horizontalMove * speed * Time.fixedDeltaTime, _rb.velocity.y);
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
            if (_touchGround || _extraJump > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpforce * Time.fixedDeltaTime);
                _extraJump--;
                _anim.SetBool("jumping", true);
                _anim.SetBool("running", false);
            }
        }

        //角色下蹲
        if (_touchGround)
        {
            if (_vertical < 0)
            {
                _anim.SetBool("crouching", true);
                //boxColl.enabled = false;
            }
            else if (!Physics2D.OverlapCircle(resisterCheck.position, 0.2f, resister))
            {
                _anim.SetBool("crouching", false);
                //boxColl.enabled = true;
            }
        }
    }

    //切换动画效果
    private void SwitchAnim()
    {
        //跳跃
        if (_anim.GetBool("jumping"))
        {
            if (_rb.velocity.y < 0)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }
        }
        if (_anim.GetBool("falling") && _touchGround)
        {
            _anim.SetBool("falling", false);
        }
        if (_rb.velocity.y < 0 && !_touchGround)
        {
            _anim.SetBool("falling", true);
            _anim.SetBool("jumping", false);
            _anim.SetBool("running", false);
        }
    }

    //触碰触发
    private void OnTriggerEnter2D(Collider2D col)
    {
        //获取物品
        if (col.CompareTag("Items"))
        {
            //col.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(col.gameObject);
            _cherry++;
            cherryNumber.text = _cherry.ToString();
        }
        //死亡
        if (col.CompareTag("DeadLine"))
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 1f);
        }
    }

    //遇到物体
    private void OnCollisionEnter2D(Collision2D col)
    {
        //遇到敌人
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
                _rb.velocity = new Vector2(_rb.velocity.x, jumpforce * Time.fixedDeltaTime);
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
    }

    //重启当前场景
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //状态检查
    private void CheckStatus()
    {
        _touchGround = Physics2D.OverlapCircle(jumpCheck.position, 0.2f, ground);
        if (_touchGround)
        {
            _extraJump = 2;
        }
    }
}