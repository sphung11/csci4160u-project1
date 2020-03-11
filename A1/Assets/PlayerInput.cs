using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerInput : MonoBehaviour
{
    public float runSpeed = 40f;

    private CharacterController2D controller;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private AudioSource fall;
    private HP hp;

    private const int Delay = 50;
    private int delay;

    private float horizontalMove = 0f;
    private bool jumping = false;

    private Vector3 startPos;
    private float floor = 5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fall = GetComponent<AudioSource>();
        hp = GetComponent<HP>();

        startPos = transform.position;
        delay = Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (hp.hitPoints > 0)
        {
            horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        } else
        {
            horizontalMove = 0;
        }

        if (Input.GetButtonDown("Jump")) {
            jumping = true;
        }

        animator.SetFloat("Speed", controller.currentSpeed);
        animator.SetBool("Jumping", !controller.isGrounded);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jumping);

        jumping = false;

        delay -= 1;

        if (delay < 0)
        {
            if (rigidBody.velocity.y < -0.01f && transform.position.y < startPos.y - 0.1f)
            {
                delay = Delay;
                animator.SetBool("Damaged", true);
                fall.Play();
            }
        }

        // if past bottom, respawn character
        if (transform.position.y < startPos.y - floor)
        {
            hp.TakeDamage();
            transform.position = startPos;
            animator.SetBool("Damaged", false);
        }
    }
}
