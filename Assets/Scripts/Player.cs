using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Events
    public UnityEvent HealthChangeEvent;
    public UnityEvent DeathEvent;
    #endregion Events

    #region Variables 
    public Hero hero = Hero.druid;
    private Animator anim;
    private AudioSource death;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private PlayerController controller;
    private PlayerData playerData = new PlayerData();
    #endregion Variables

    #region SerializeField
    [SerializeField] public static readonly int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private int collectedDeathcoins;
    [Header("Hero Settings")]
    [SerializeField] private float stunForce = 5f;
    [SerializeField] private float stunTime = 2f;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Text deathcoinTxt;

    [SerializeField] private AudioSource footstepsSound;
    #endregion SerializeField

    //=======================================================



    //Зробити PlayerManager PlayerSaveSystem



    //=======================================================


    #region Propeties
    public static Player Instance { get; private set; } = null;

    public PlayerState State { get; set; } = PlayerState.idle;
    public int Health
    {
        get { return currentHealth; }
        set
        {
            if (value < 1)
            {
                Death();
            }
            else if (value < currentHealth)
            {
                //Entity becomes red and then white again 
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, 2f);
                Invoke("DamagedAnimationOff", 0.6f);
            }
            currentHealth = value;
            HealthChangeEvent?.Invoke();
        }
    }

    public int CollectedDeathcoins //Collected deathcoins in the current level
    {
        get { return collectedDeathcoins; }
        set
        {
            collectedDeathcoins = value;
            deathcoinTxt.text = collectedDeathcoins.ToString();
        }
    }
    #endregion

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

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<PlayerController>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        Health = maxHealth;
        LoadSave();
        Spawn();
    }

    void Update()
    {
        //if (State != PlayerState.stunned)//Hurt to Stun --- OLD 
        //{
        //    Shootment();
        //}

        AnimationState();
        anim.SetInteger("state", (int)State); // sets animation based on Enumerator state
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            ICollectable collectable = collision.GetComponent<ICollectable>();
            collectable.Colect(this);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Trap"))
        {
            if (other.gameObject.CompareTag("Enemy") && State == PlayerState.falling)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.JumpedOn();
                controller.Jump();
                return;
            }

            if (State != PlayerState.stunned)
            {
                TakeDamage();
                GetStunned();
                GetThrown(other.gameObject.transform.position.x > transform.position.x
                    ? new Vector2(-stunForce, rb.velocity.y)
                    : new Vector2(stunForce, rb.velocity.y));
            }
        }
    }

    public void MakeSave()
    {
        playerData.deathcoins = CollectedDeathcoins;
        SaveSystem.MakePlayerSave(playerData);
    }

    public void LoadSave()
    {
        playerData = SaveSystem.GetPlayerSave();
        hero = playerData.hero;
    }

    //private void Shootment() OLD Func
    //{
    //    if (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0)
    //    {
    //        shoot.MakeShoot(new Vector2(attackJoystick.Horizontal, attackJoystick.Vertical));
    //    }
    //}

    private void AnimationState()
    {
        if (State == PlayerState.jumping)
        {
            if (rb.velocity.y < -0.1f)
            {
                State = PlayerState.falling;
            }
        }
        else if (State == PlayerState.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                SetNormalState();
            }
        }
        else if (State == PlayerState.stunned)
        {
            Invoke(nameof(SetNormalState), stunTime);
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            //Moving
            State = PlayerState.running;
        }
        else
        {
            SetNormalState();
        }
    }

    private void DamagedAnimationOff()
    {
        // Changes color after damage
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, 1f);
    }
    private void SetNormalState()
    {
        State = PlayerState.idle;
    }

    private void FootStep()
    {
        footstepsSound.Play();
    }

    public void TakeDamage()
    {
        Health--;
    }

    public void GetStunned()
    {
        State = PlayerState.stunned;
        AnimationState();
        controller.HorizontalMove = 0;
    }

    public void GetThrown(Vector2 direction)
    {
        rb.AddForce(direction);
    }

    private void Death()
    {
        DeathEvent?.Invoke();
        //print("Play ads"); //Переробити щоб можна було продовжити якщо подивитсь рекламу, но не в цьому методі
        //ShowMenuLose();
    }

    public void Spawn()
    {
        Health = maxHealth;
        transform.position = spawnPoint.position;
    }
}

#region Enums
public enum PlayerState { idle, running, jumping, falling, stunned }

public enum Hero { druid };
#endregion