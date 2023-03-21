using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody RB;
    private float movementX;
    private float movementY;
    float speed = 6f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 2f;
    public float gravityScale = 2f;
    [SerializeField] private Animator anim;
    bool grounded = true;
    bool speedPowerup = false;
    bool jumpPowerup = false;
    bool doubleJump = false;
    float speedTimer = 10f;
    float jumpTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump()
    {
        if (grounded || doubleJump)
        {
            RB.AddForce(0, 600, 0);
            anim.SetBool("Jump", true);
            
            if (!grounded)
            {
                doubleJump = false;
            }
            grounded = false;
        }
    }
    void Powerup()
    {
        if (speedPowerup)
        {
            if (speedTimer > 0)
            {
                speedTimer -= Time.deltaTime;
            }
            else
            {
                speed /= 1.5f;
                speedTimer = 5f;
                speedPowerup = false;
            }
        }
        if (jumpPowerup)
        {           
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                jumpTimer = 5f;
                doubleJump = false;
                jumpPowerup = false;
            }
        }
    }

    // Update is called once per frameS
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, 0, movementY * speed);
        if (RB.velocity.z < maxSpeed)
        {
            RB.AddRelativeForce(movement * speed);
        }

        transform.Rotate(0, movementX * rotationSpeed, 0);
        RB.AddForce(0, -1 * gravityScale, 0);
        anim.SetFloat("Speed", movementY);

        Powerup();
    }
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Jump", false);
        grounded = true;

        if (jumpPowerup)
            doubleJump = true;
       
        if (collision.gameObject.tag == "speed+")
        {          
            Destroy(collision.gameObject);
            speed *= 1.5f;
            speedPowerup = true;
        }
        if (collision.gameObject.tag == "jump+")
        {
            Destroy(collision.gameObject);
            doubleJump = true;
            jumpPowerup = true;
        }
    }
}
