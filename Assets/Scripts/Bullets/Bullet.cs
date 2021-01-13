using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for bullet types and making damage 
/// </summary>

public class Bullet : MonoBehaviour, IPooledObject
{
    [HideInInspector] public string shooterTag;

    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType type;
    [SerializeField] private float lifeTime = 1.5f;
    [SerializeField] private float speed = 10;
    [SerializeField] private LayerMask ground;

    private float currentLifeTime;

    public ObjectPooler.ObjectInfo.ObjectType Type => type;

    private Collider2D coll;

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (coll.IsTouchingLayers(ground))
        {
            ObjectPooler.Instance.DestroyObject(gameObject);
        }

        if ((currentLifeTime -= Time.deltaTime) < 0)
        {
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != shooterTag && (collision.tag == "Enemy" || collision.tag == "Player"))
        {
            Entity entity = collision.GetComponent<Entity>();
            MakeDamage(entity);
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }

    protected virtual void MakeDamage(Entity entity)
    {
        entity.ReceiveDamage();
    }

    public void OnCreate(string _shooterTag, Vector3 position, Vector2 direction)
    {
        shooterTag = _shooterTag;
        transform.position = position;
        currentLifeTime = lifeTime;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}

