using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamController : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] GameObject Camera;
    public float camSpeed = 5f;
    float camDirection = 0;
    PlayerController playerref;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Camera.transform.localPosition;
        playerref = GetComponent<PlayerController>();
        startPos = Camera.transform.localPosition;
    }

    private void Update()
    {
        if (!playerref.spline)
        {
            Camera.transform.localPosition = startPos;
            Camera.transform.RotateAround(transform.position, Vector3.up, 1f * camSpeed * camDirection);
            Camera.transform.LookAt(transform.position);
        }
        else
        {
            Camera.transform.localPosition = new Vector3(15, 1.25f, 0);
            Camera.transform.LookAt(transform.position);
        }
        
    }
    public void OnCamMove(InputValue movement)
    {
        Vector2 movementVector = movement.Get<Vector2>();

        if (movementVector.x > 0)
        {
            camDirection = 1;
        }
        else if (movementVector.x < 0)
        {
            camDirection = -1;
        }
        else
        {
            camDirection = 0;
        }
    }
}
