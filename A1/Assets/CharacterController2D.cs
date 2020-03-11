using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float movementSpeed = 10f;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    public float currentSpeed = 0.0f;

    [Header("Jumping")]
    [SerializeField] private bool canAirControl = true;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private Transform groundPosition;
    public bool isGrounded;

    private bool isFacingRight = true;

    const float groundedRadius = 0.2f;
    const float ceilingRadius = 0.2f;

    private Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    public UnityEvent OnLandEvent;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }
    
    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // find any ground layer colliders closer than the ground position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPosition.position, groundedRadius);
        //Debug.Log("Overlapping colliders: " + colliders.Length);
        for (int i = 0; i < colliders.Length; i++)
        {
            // if any of the colliders are not the object itself, it must be the ground
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool jump)
    {
        // only control if player is grounded or canAirControl is on
        if (isGrounded || canAirControl)
        {
            Vector3 targetVelocity = new Vector2(move * movementSpeed, GetComponent<Rigidbody2D>().velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
            currentSpeed = rigidBody.velocity.magnitude;

            if (move > 0 && !isFacingRight)
            {
                Flip();
            } else if (move < 0 && isFacingRight)
            {
                Flip();
            }
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
