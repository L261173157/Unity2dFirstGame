using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public ParticleSystem hitEffect;

    private Rigidbody2D _rigidbody2D;
    private float _timer;
    private int _direction = 1;
    private Animator _animator;
    private bool broken = true;

    // 在第一次帧更新之前调用 Start
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _direction = -_direction;
            _timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = _rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * _direction;
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", _direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * _direction;
            _animator.SetFloat("Move X", _direction);
            _animator.SetFloat("Move Y", 0);
        }

        _rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger("Fixed");
        Instantiate(hitEffect,_rigidbody2D.position,Quaternion.identity);
        smokeEffect.Stop();
    }
}