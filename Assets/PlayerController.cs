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
    float attackTime = .5f;
    bool attacking = false;
    bool buttonCollide = false;

    public GameController controllerRef;
    public GameObject respawnPoint;

    public bool spline = false;
    public GameObject SplineObj;
    bool trigger1 = false;
    bool trigger2 = false;
    bool forward1 = false;
    bool forward2 = false;

    public int PlayerHealth = 5;

    public GameObject hitbox;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void TakeDamage()
    {
        PlayerHealth -= 1;
    }

    public void OnMove(InputValue movementValue)
    {
        if (controllerRef.state == GameController.Gamestate.play)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    public void OnJump()
    {
        if (controllerRef.state == GameController.Gamestate.play)
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
    }

    public void OnAttack()
    {
        if (controllerRef.state == GameController.Gamestate.play)
        {
            StartCoroutine(attackTimer());
            anim.SetBool("ButtonPress", true);
            attacking = true;
            if (buttonCollide)
            {
                controllerRef.state = GameController.Gamestate.cutscene;
                transform.position = new Vector3(3.5f, .5f, 20);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    IEnumerator attackTimer()
    {
        hitbox.SetActive(true);

        yield return new WaitForSeconds(1);

        hitbox.SetActive(false);
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
        if (trigger1 && forward1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), .2f);
        }           
        else if(trigger1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), .2f);
        }
        if (trigger2 && forward2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .2f);
        }
        else if (trigger2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), .2f);
        }


        if (!spline)
        {
            Vector3 movement = new Vector3(0, 0, movementY * speed);
            if (RB.velocity.z < maxSpeed)
            {
                RB.AddRelativeForce(movement * speed);
            }

            if (controllerRef.state == GameController.Gamestate.play)
            {
                transform.Rotate(0, movementX * rotationSpeed, 0);
                RB.AddForce(0, -1 * gravityScale, 0);
                anim.SetFloat("Speed", movementY);
            }
        }
        else
        {
            RB.AddRelativeForce(new Vector3(0, 0, movementX * speed * 10));
            anim.SetFloat("Speed", movementX);
        }

        if (attacking)
        {
            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
            }
            else
            {
                attackTime = .5f;
                attacking = false;
                anim.SetBool("ButtonPress", false);
            }
        }

        Powerup();
    }
    private void OnCollisionEnter(Collision collision)
    {
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
        if (collision.gameObject.tag == "Finish")
        {
            spline = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "button")
        {
            buttonCollide = true;
        }
        if (other.gameObject.tag == "respawn")
        {
            transform.position = respawnPoint.transform.position;
        }
        if (other.gameObject.tag == "spline")
        {
            spline = true;
            transform.position = SplineObj.transform.GetChild(0).position;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (other.gameObject.tag == "trigger1" && spline && !trigger1)
        {
            RB.velocity = new Vector3(0,0,0);
            forward1 = !forward1;
            trigger1 = true;
            
        }
        if (other.gameObject.tag == "trigger2" && spline && !trigger2)
        {
            RB.velocity = new Vector3(0, 0, 0);
            forward2 = !forward2;
            trigger2 = true;
            
        }
        anim.SetBool("Jump", false);
        grounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "button")
        {
            buttonCollide = false;
        }
        if (other.gameObject.tag == "trigger1" && spline)
            trigger1 = false;
        if (other.gameObject.tag == "trigger2" && spline)
            trigger2 = false;
    }
}
