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
    private float brakeTime = 2.55f;
    bool brake = false;
    [SerializeField] private Renderer barrelModel;
    Color color;
    Color startColor;
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

        startColor = barrelModel.material.color;

        color = barrelModel.material.color;
        color.r = 255;
        color.g = 0;
        color.b = 0;
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

        if (brake)
        {
            if (brakeTime > 0)
            {
                brakeTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && platformType == type.move)
        {
            collision.gameObject.transform.SetParent(transform);
        }
        else if (collision.gameObject.tag == "Player" && platformType == type.brake)
        {
            brake = true;
            color.g = 0;
            color.b = 0;
            barrelModel.material.color = color;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && platformType == type.move)
        {
            collision.gameObject.transform.SetParent(null);
        }
        else if (collision.gameObject.tag == "Player" && platformType == type.brake)
        {
            brake = false;
            color = startColor;
            barrelModel.material.color = color;
        }
    }
}
