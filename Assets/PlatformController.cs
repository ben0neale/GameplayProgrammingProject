using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float distance;
    public Vector3 direction;
    Vector3 startPos;
    Vector3 endPos;
    public float speed = .1f;
    bool move = true;

    public enum type 
    {
        move,
        brake,
        idle
    }

    public type platformType = type.idle;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + (direction * distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (platformType == type.move)
        {
            if (Vector3.Distance(transform.position, endPos) < .3f)
                move = false;
            else if (Vector3.Distance(transform.position, startPos) < .3f)
                move = true;

            if (move)
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed);
            else
                transform.position = Vector3.MoveTowards(transform.position, transform.position - direction, speed);
        }
    }
}
