using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow1 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed=0.125f;
    public Vector3 offset;

    public float  HorizontalSensitivity  = 30.0f;
    public float  VerticalSensitivity  = 30.0f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition=Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position=smoothedPosition;

        transform.LookAt(target);

        //camera rotation

        
    }

    
    


}
