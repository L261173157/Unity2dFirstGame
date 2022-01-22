using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bird : MonoBehaviour
{
    public float Speed, JumpForce, rotationSpeed;

    public int Score;

    public GameObject enemy, line, backGround;

    private Rigidbody2D _rb;
    private bool _isJumpPressed;

    [SerializeField]
    private int _scoreTimes;

    private float EnemyInitialPos, BackInitialPos;

    private Quaternion birdUPTo, birdDownTo;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        EnemyInitialPos = 6;
        BackInitialPos = 0;
        CreateBackGround(10);
        CreateEnemy(10);
        birdUPTo = Quaternion.Euler(0, 0, 30f);
        birdDownTo = Quaternion.Euler(0, 0, -30f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isJumpPressed = true;
        }
        SwitchAnimation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //运动
    private void Movement()
    {
        //x速度
        _rb.velocity = new Vector2(Speed * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJumpPressed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
            _isJumpPressed = false;
        }
    }

    //遇到物体
    private void OnCollisionEnter2D(Collision2D other)
    {
        //接触管子
        if (other.gameObject.CompareTag("Enemy"))
        {
            Invoke(nameof(Dead), 0.5f);
        }
    }

    //得分及生成后续
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit pass");
        if (other.gameObject.CompareTag("Score"))
        {
            //加分
            Score++;
            Debug.Log("score:"+Score);
            //生成前方物体
            _scoreTimes++;
            if (_scoreTimes == 4)
            {
                CreateBackGround(3);
                CreateEnemy(4);
                _scoreTimes = 0;
            }
        }
    }

    //死亡
    private void Dead()
    {
        Score = 0;
        _scoreTimes = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //切换姿态动画
    private void SwitchAnimation()
    {
        //身体方向 -30~30
        if (_rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, birdUPTo, Time.deltaTime * rotationSpeed);
        }
        if (_rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, birdDownTo, Time.deltaTime * rotationSpeed);
        }
    }

    #region 生成物体

    //生成障碍
    private void CreateEnemy(int times)
    {
        float currentPos = 0;
        for (int i = 0; i < times; i++)
        {
            CreateEnemyBase(EnemyInitialPos + 4 * i, Random.Range(-1.5f, 2.5f));
            currentPos = EnemyInitialPos + 4 * i;
        }
        EnemyInitialPos = currentPos + 4;
    }

    //生成背景
    private void CreateBackGround(int times)
    {
        float currentPos = 0;
        for (int i = 0; i < times; i++)
        {
            CreateBackGroundBase(BackInitialPos + 6 * i, 0);
            CreateLineBase(BackInitialPos + 6 * i, -0.5f);
            currentPos = BackInitialPos + 6 * i;
        }
        BackInitialPos = currentPos + 6;
    }

    //创建敌人基础方法
    private void CreateEnemyBase(float x, float y)
    {
        Instantiate(enemy, new Vector3(x, y, 0f), Quaternion.identity);
    }

    //创建基准线基础方法
    private void CreateLineBase(float x, float y)
    {
        Instantiate(line, new Vector3(x, y, 0f), Quaternion.identity);
    }

    //创建背景基础方法
    private void CreateBackGroundBase(float x, float y)
    {
        Instantiate(backGround, new Vector3(x, y, 0f), Quaternion.identity);
    }

    #endregion 生成物体
}