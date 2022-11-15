using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroyObject : MonoBehaviour
{
   public float Damage;

   private GameObject _zombieSpawnerObject;
   private ZombieSpawner _zombieSpawnerScript;
   private GameObject _TurnControllerObject;
   private TurnController _TurnControllerScript;


   void Awake()
   {
      _zombieSpawnerObject = GameObject.Find("Zombie Spawner");
      _zombieSpawnerScript=_zombieSpawnerObject.GetComponent<ZombieSpawner>();

      _TurnControllerObject = GameObject.Find("TurnController");
      _TurnControllerScript=_TurnControllerObject.GetComponent<TurnController>();
   }


    void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "DestructableTerrain")
     {
        Object.Destroy(collision.gameObject);
        Object.Destroy(gameObject);
     }
     else if(collision.gameObject.tag == "Zombie")
     {
        Object.Destroy(collision.gameObject);
        Object.Destroy(gameObject);
        _zombieSpawnerScript.currentEnemyAmount--;

     }
     else if(collision.gameObject.name == "Terrain")
     {
        Object.Destroy(gameObject);
     }
     else if(collision.gameObject.tag == "PlayerEmpty")
     {
        Object.Destroy(gameObject);
        _TurnControllerScript.DamageInactivePlayer(Damage);
     }
      
    }
}
