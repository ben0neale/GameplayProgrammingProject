using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamController : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] GameObject Camera;
    public float camSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Camera.transform.localPosition;
    }
    public void OnCamMoveRight(InputValue movement)
    {
        Camera.transform.RotateAround(transform.position, Vector3.up, 1f * camSpeed);
    }
    public void OnCamMoveLeft(InputValue movement)
    {
        Camera.transform.RotateAround(transform.position, Vector3.up, -1f * camSpeed);
    }
    public void OnMove()
    {
        //Camera.transform.localPosition = startPos;
        //Camera.transform.LookAt(transform.position + new Vector3(0,3,0));
    }
}
