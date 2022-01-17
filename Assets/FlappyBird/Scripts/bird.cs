using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bird : MonoBehaviour
{

    public float Speed, JumpForce;

    public int Score;

    public GameObject enemy;

    private Rigidbody2D _rb;
    private bool _isJumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        CreateAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
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
    //得分
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            Score++;
        }
    }
    //死亡
    void Dead()
    {
        Score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //生成管子
    void CreateAll()
    {
        for (int i = 12; i < 200; i += 4)
        {
            CreateEnemy(i, Random.Range(-3f, 3f));
        }
    }
    void CreateEnemy(float x, float y)
    {
        Instantiate(enemy, new Vector3(x, y, 0f), Quaternion.identity);
    }


}
