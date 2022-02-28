using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;

    private int _currentHealth;
    private AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(_horizontal, _vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
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

        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(_rigidbody.position + Vector2.up * 0.5f, _lookDirection, 1.5f,
                LayerMask.GetMask("NPC"));
            if (hit2D.collider != null)
            {
                NonPlayerCharacter character = hit2D.collider.gameObject.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
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
        UIHealthBar.instance.SetValue(_currentHealth / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject =
            Instantiate(projectilePrefab, _rigidbody.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        _animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}