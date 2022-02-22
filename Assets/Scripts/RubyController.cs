using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 5;
    private int currentHealth;

    public int health
    {
        get { return currentHealth; }
    }

    private Rigidbody2D _rigidbody;

    private float horizontal;

    private float vertical;

    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;

        _rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // Debug.Log(horizontal);
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        _rigidbody.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}