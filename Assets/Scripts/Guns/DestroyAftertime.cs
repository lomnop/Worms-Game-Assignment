using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAftertime : MonoBehaviour
{
public float TimeUntillDelete;
private float _time=0;
    void FixedUpdate()
    {
        _time+=Time.deltaTime;
        if(_time>=TimeUntillDelete)
        {
            Destroy(gameObject);
        }
    }
}
