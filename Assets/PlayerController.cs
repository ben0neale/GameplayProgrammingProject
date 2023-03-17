using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody RB;
    private float movementX;
    private float movementY;
    public float speed = 1f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 2f;
    [SerializeField] private Animator anim;

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
        RB.AddForce(0, 350, 0);
        anim.SetBool("Jump", true);
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
        anim.SetFloat("Speed", movementY);
    }
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Jump", false);
    }
}
