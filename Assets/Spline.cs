using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    Vector3 splineOffset = new Vector3(0, 0, -10);
    Vector3[] splinePoint;
    int splineCount;
    int x = 0;
    public GameObject SplineObj;
    // Start is called before the first frame update
    void Start()
    {
        splineCount = SplineObj.transform.childCount;
        splinePoint = new Vector3[splineCount];
        for (int i = 0; i < splineCount; i++)
        {
            splinePoint[i] = transform.GetChild(i).position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
