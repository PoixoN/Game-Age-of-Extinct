using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for all game members 
/// </summary>

abstract public class Entity : MonoBehaviour
{
    #region Variables 
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource death;
    protected SpriteRenderer spriteRenderer;
    [SerializeField]  public readonly int maxHealth = 3;
    #endregion

    [SerializeField] private int currentHealth;

    #region Health property
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
                //Entity becomes red and then white again 
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, 2f);
                Invoke("DamagedAnimationOff", 0.6f);
            }
            currentHealth = value;
        }
    }
    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void JumpedOn()
    {
        Death();
    }

    private void DamagedAnimationOff()
    {
        // Changes color after damage
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, 1f);
    }

    public virtual void ReceiveDamage()
    {
        Health--;
    }

    protected virtual void Death()
    {
        anim.SetTrigger("Death");
        death.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyAfterDeath()
    {
        Destroy(this.gameObject);
    }
}
