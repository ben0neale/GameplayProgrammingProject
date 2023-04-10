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

    // Start is called before the first frame update
    void Start()
    {
        startPos = Camera.transform.localPosition;
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

        Camera.transform.RotateAround(transform.position, Vector3.up, 1f * camSpeed * camDirection);
    }
    //public void OnCamMoveLeft(InputValue movement)
    //{
    //    Camera.transform.RotateAround(transform.position, Vector3.up, -1f * camSpeed);
    //}
    public void OnMove()
    {
        //Camera.transform.localPosition = startPos;
        //Camera.transform.LookAt(transform.position + new Vector3(0,3,0));
    }
}
