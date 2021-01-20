using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    #region SerializeField
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _collectedDeathcoins;
    [Header("Hero Settings")]
    [SerializeField] private float _stunForce = 5f;
    [SerializeField] private float _stunTime = 2f;

    [SerializeField] private LayerMask _ground;
    #endregion SerializeField

    #region Variables 
    private Animator _anim;
    private AudioSource _death;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _coll;
    private PlayerMovement _controller;
    private AudioSource _footstepsSound;
    #endregion Variables

    #region Propeties
    public static Player Instance { get; private set; } = null;
    public PlayerState State { get; set; } = PlayerState.idle;

    public int Health
    {
        get => _currentHealth;
        set
        {
            if (value < 1)
            {
                Death();
            }

            _currentHealth = value;
            PlayerEvents.ChangeHealthEvent?.Invoke();
        }
    }

    public int CollectedDeathcoins //Collected deathcoins in the current level
    {
        get => _collectedDeathcoins;
        set
        {
            _collectedDeathcoins = value;
            PlayerEvents.CollectDeathcoinEvent?.Invoke();
        }
    }
    #endregion

    #region Unity functions
    void Awake()
    {
        #region Instance
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        #endregion Instance

        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _death = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<CapsuleCollider2D>();
        _controller = GetComponent<PlayerMovement>();
        _footstepsSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Health = SaveSystem.PlayerSave.MaxHealth;
    }

    private void Update()
    {
        AnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            ICollectable collectable = collision.GetComponent<ICollectable>();
            collectable.Collect(this);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Trap"))
        {
            if (other.gameObject.CompareTag("Enemy") && State == PlayerState.falling)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.JumpedOn();
                _controller.Jump();
                return;
            }

            if (State != PlayerState.stunned)
            {
                ApplyDamage();
                GetStunned();
                GetThrown(other.gameObject.transform.position.x > transform.position.x
                    ? new Vector2(-_stunForce, _rb.velocity.y)
                    : new Vector2(_stunForce, _rb.velocity.y));
            }
        }
    }
    #endregion Unity functions

    public void ApplyDamage(int damage = 1)
    {
        Health -= damage;
        StartCoroutine(nameof(DamageAnimation));
    }

    public void GetStunned()
    {
        State = PlayerState.stunned;
        Invoke(nameof(SetNormalState), _stunTime);
        _controller.HorizontalMove = 0;
    }

    public void GetThrown(Vector2 direction)
    {
        _rb.velocity = direction;
    }

    public void Death()
    {
        PlayerEvents.DeathEvent?.Invoke();
        Time.timeScale = 0;
        //print("Play ads"); //Переробити щоб можна було продовжити якщо подивитсь рекламу, но не в цьому методі
    }

    private void AnimationState()
    {
        if (State != PlayerState.stunned)
        {
            if (!_coll.IsTouchingLayers(_ground))
            {
                if (_rb.velocity.y < -0.1f)
                {
                    State = PlayerState.falling;
                }
                else if (_rb.velocity.y > 10f)
                {
                    State = PlayerState.jumping;
                }
            }
            else if (Mathf.Abs(_rb.velocity.x) > 0.1f)
            {
                //Moving
                State = PlayerState.running;
            }
            else
            {
                SetNormalState();
            }
        }

        _anim.SetInteger("state", (int)State); // sets animation based on Enumerator state
    }

    /// <summary>
    ///Entity becomes red and then normal
    /// </summary>
    private IEnumerator DamageAnimation()
    {
        _spriteRenderer.color = Color.Lerp(Color.white, Color.red, 2f);
        yield return new WaitForSeconds(0.6f);
        _spriteRenderer.color = Color.Lerp(Color.red, Color.white, 1f);
    }

    private void SetNormalState()
    {
        State = PlayerState.idle;
    }

    private void FootStep()
    {
        _footstepsSound.Play();
    }
}

//private void Shootment() OLD Func
//{
//    if (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0)
//    {
//        shoot.MakeShoot(new Vector2(attackJoystick.Horizontal, attackJoystick.Vertical));
//    }
//}