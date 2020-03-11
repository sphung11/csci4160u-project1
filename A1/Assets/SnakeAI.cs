using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeAI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float range = 3f;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private LayerMask groundLayers;

    private const int Delay = 5;

    private Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;
    private Vector3 startPos;

    private bool isGoingLeft = false;
    private int delay;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        delay = Delay;
    }
    
    void FixedUpdate()
    {
        if(!isGoingLeft)
        {
            Move(movementSpeed * Time.fixedDeltaTime);
        } else if (isGoingLeft)
        {
            Move(-movementSpeed * Time.fixedDeltaTime);
        }

        delay -= 1;
        SwitchDirections();
    }

    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * movementSpeed, GetComponent<Rigidbody2D>().velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    private void SwitchDirections()
    {
        if (delay < 0)
        {
            if ((transform.position.x > startPos.x + range) || (transform.position.x < startPos.x - range))
            {
                delay = Delay;
                isGoingLeft = !isGoingLeft;

                // flip image on vertical axis
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }


}
