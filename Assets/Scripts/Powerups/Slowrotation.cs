using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowrotation : MonoBehaviour
{
    public float RoatationSpeedX=100;
    public float RoatationSpeedY=0;
    public float SmoothVal=1;
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation*Quaternion.Euler(0+RoatationSpeedY, 0+RoatationSpeedX, 0),  Time.deltaTime * SmoothVal);
    }
}
