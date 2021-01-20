using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for all enemies
/// </summary>
abstract public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth;

    public readonly int MaxHealth = 3;

    protected Animator _anim;
    protected Rigidbody2D _rb;
    protected AudioSource _death;
    protected SpriteRenderer _spriteRenderer;

    public virtual int Health
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
                StartCoroutine("DamageAnimation");
            }
            currentHealth = value;
        }
    }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _death = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = MaxHealth;
    }

    public void JumpedOn()
    {
        Death();
    }

    public virtual void ApplyDamage(int damage = 1)
    {
        Health -= damage;
    }

    public void DestroyAfterDeath() //Function for animation event
    {
        Destroy(gameObject);
    }

    protected virtual void Death()
    {
        _anim.SetTrigger("Death");
        _death.Play();
        _rb.velocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator DamageAnimation()
    {
        //Entity becomes red and then white again 
        _spriteRenderer.color = Color.Lerp(Color.white, Color.red, 2f);
        yield return new WaitForSeconds(0.6f);
        _spriteRenderer.color = Color.Lerp(Color.red, Color.white, 1f);
    }
}
