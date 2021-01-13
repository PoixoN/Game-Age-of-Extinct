using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    #region Inspector fields
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] public float jumpLength = 2f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private LayerMask ground;
    #endregion

    private Collider2D coll;
    private bool facingLeft = true;

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        //Transition from Jump to Fall 
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Transition from Fall to Idle 
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    #region Frog movement meyhod
    private void Move()
    {

        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                //Make sure sprite is facing right location, and if it is not, then face the right direction 
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //Test to see if I am on the ground, if so jump 
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                //Make sure sprite is facing left location, and if it is not, then face the left direction 
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //Test to see if I am on the ground, if so jump 
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    #endregion
}