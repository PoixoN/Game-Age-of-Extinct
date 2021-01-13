using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 15f;
    public float HorizontalMove { get; set; } = 0f;

    [Space]
    [SerializeField] private Player player;
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private JumpButton jumpButton;
    [Space]
    [SerializeField] private float m_JumpForce = 15f;                          // Amount of force added when the player jumps
    [Range(0, .3f)] 
    [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.


    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private CapsuleCollider2D coll;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (player.State != PlayerState.stunned)
        {


            if (m_Rigidbody2D.velocity.y < -0.1f)
            {
                player.State = PlayerState.falling;
            }
            else if (!m_Grounded && m_Rigidbody2D.velocity.y > 10f)
            {
                player.State = PlayerState.jumping;
            }
            //HorizontalMove = moveJoystick.Horizontal * speed * Time.fixedDeltaTime;
            HorizontalMove = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(HorizontalMove * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (HorizontalMove > 0 && !m_FacingRight)
                {
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (HorizontalMove < 0 && m_FacingRight)
                {
                    Flip();
                }
            }

            // If the player should jump...
            if ((Input.GetButtonDown("Jump") || jumpButton.Pressed) && m_Grounded)
            {
                m_Grounded = false;
                Jump();
            }
            m_Grounded = coll.IsTouchingLayers(m_WhatIsGround);
        }
    }

    public void Jump()
    {
        player.State = PlayerState.jumping;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}