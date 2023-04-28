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
    bool spline = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Camera.transform.localPosition;
    }

    public void OnCamMove(InputValue movement)
    {
        if (!spline)
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
        else
        {
            Camera.transform.position = new Vector3(0, 0, 10);
            Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spline")
        {
            spline = true;
        }
    }
}
