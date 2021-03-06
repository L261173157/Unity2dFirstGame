using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider2D coll;
    public LayerMask ground;
    public LayerMask resister;
    public float speed;
    public float jumpforce;
    public Text cherryNumber;
    public Transform headCheck, footCheck;

    private int _cherry;
    private Rigidbody2D _rb;
    private Animator _anim;

    private Vector2 _collOffset, _collSize;

    [SerializeField]
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
        _collOffset = coll.offset;
        _collSize = coll.size;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isJumpPressed = true;
        }
        //_isJumpPressed = Input.GetButtonDown("Jump");
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
            if (_extraJump > 0)
            {
                _rb.velocity = Vector2.up * jumpforce;
                SoundManagerNew.Instance.Jump();
                _extraJump--;
                _anim.SetBool("jumping", true);
                _anim.SetBool("falling", false);
                _anim.SetBool("running", false);
            }
            _isJumpPressed = false;
        }

        //角色下蹲
        if (_touchGround)
        {
            if (_vertical < 0)
            {
                _anim.SetBool("crouching", true);
                coll.offset = new Vector2(0, -0.6f);
                coll.size = new Vector2(0.8f, 0.8f);
            }
            else if (!Physics2D.OverlapCircle(headCheck.position, 0.2f, resister))
            {
                _anim.SetBool("crouching", false);
                coll.offset = _collOffset;
                coll.size = _collSize;
            }
        }
    }

    //切换动画效果
    private void SwitchAnim()
    {
        //下落动画取消
        if (_anim.GetBool("falling") && _touchGround)
        {
            _anim.SetBool("falling", false);
        }
        //跳跃
        if (_anim.GetBool("jumping"))
        {
            //下落动画
            if (_rb.velocity.y < 0)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }
        }
        //下落动画
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
            SoundManagerNew.Instance.StopAllAudio();
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
                _rb.velocity = new Vector2(_rb.velocity.x, jumpforce);
                _anim.SetBool("jumping", true);
                _anim.SetBool("falling", false);
            }
            else //受伤
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
        SoundManagerNew.Instance.Hurt();
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
        SoundManagerNew.Instance.StartAllAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //状态检查
    private void CheckStatus()
    {
        _touchGround = Physics2D.OverlapCircle(footCheck.position, 0.2f, ground) || Physics2D.OverlapCircle(footCheck.position, 0.2f, resister);
        if (_touchGround)
        {
            _extraJump = 1;
        }
    }
}