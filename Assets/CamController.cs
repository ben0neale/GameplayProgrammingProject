using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamController : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }
    public void OnCamMoveRight()
    {
        print("CamMove");
        transform.RotateAround(player.transform.position, Vector3.up, 1000f);
    }
    public void OnCamMoveLeft()
    {
        print("Cam Move L");
        transform.RotateAround(player.transform.position, Vector3.up, -1000f);
    }
}
