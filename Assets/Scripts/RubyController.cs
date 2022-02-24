using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    private int _currentHealth;

    public int Health
    {
        get { return _currentHealth; }
    }

    private Rigidbody2D _rigidbody;

    private bool _isInvincible;
    private float _invincibleTimer;

    private float _horizontal;
    private float _vertical;
    private Animator _animator;
    private Vector2 _lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;

        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(_horizontal, _vertical);
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }
        
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);


        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
                _isInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * _horizontal * Time.deltaTime;
        position.y = position.y + speed * _vertical * Time.deltaTime;
        _rigidbody.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible)
                return;

            _isInvincible = true;
            _invincibleTimer = timeInvincible;
            _animator.SetTrigger("Hit");
        }

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
    }
}