using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    private float _obstacleDestructionTime;
    public float obstacleDestructionTime;

    public float collitionDamage;
    public float extendedCollitionDamage;

    private GameObject _TurnControllerObject;
   private TurnController _TurnControllerScript;

   void Awake()
   {
      _TurnControllerObject = GameObject.Find("TurnController");
      _TurnControllerScript=_TurnControllerObject.GetComponent<TurnController>();
   }
 
 void OnCollisionEnter(Collision other)
 {
    _obstacleDestructionTime = 0;
    if(other.gameObject.tag == "PlayerObject")
    {
        _TurnControllerScript.DamagePlayer(collitionDamage);
    }
 }
 
 void OnCollisionStay(Collision other)
 {
        _obstacleDestructionTime += Time.deltaTime;
        if(obstacleDestructionTime<=_obstacleDestructionTime && other.gameObject.tag == "DestructableTerrain")
        {
            Object.Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "PlayerObject")
        {
            _TurnControllerScript.DamagePlayer(extendedCollitionDamage*Time.deltaTime);
        }
 }
}
