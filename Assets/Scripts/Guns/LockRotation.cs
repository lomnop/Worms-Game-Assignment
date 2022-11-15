using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion _iniRot;
 
 void Start(){
     _iniRot = transform.rotation;
 }
 
 void LateUpdate(){
     transform.rotation = _iniRot;
 }
}
