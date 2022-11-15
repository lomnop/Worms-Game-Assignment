using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float HorizontalSensitivity;
    public float VerticalSensitivity;
    public float RotationX;
    public float RotationY;

    public void CameraRotate(float rotateX,float rotateY)
    {
        RotationX = HorizontalSensitivity * rotateX * Time.deltaTime;
        RotationY = VerticalSensitivity * rotateY * Time.deltaTime;

        Vector3 CameraRotation = transform.rotation.eulerAngles;
 
        CameraRotation.x -= RotationY;
        CameraRotation.y += RotationX;


        transform.rotation = Quaternion.Euler(CameraRotation);
    }
}
