using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject Zombie;
    public int xPos;
    public int yPos;
    public int enemyMaxAmount;
    public int currentEnemyAmount;
    public bool spawnZombies;
    private int _activeSpawner;
    
    void Start()
    {
       StartCoroutine(ZombieSpawn());
    }

    public IEnumerator ZombieSpawn()
    {
        while(spawnZombies)
        {
            if(currentEnemyAmount<enemyMaxAmount)
            {
            xPos=Random.Range(1,11);
            yPos=Random.Range(1,11);
            NavMeshHit closestHit;
            _activeSpawner=Random.Range(0,SpawnPoints.Length);
            if (NavMesh.SamplePosition(new Vector3(xPos+SpawnPoints[_activeSpawner].transform.position.x,20,yPos+SpawnPoints[_activeSpawner].transform.position.z), out closestHit, 500f, NavMesh.AllAreas))
            Instantiate(Zombie, closestHit.position,Quaternion.identity);
            currentEnemyAmount++;
            }
            yield return new WaitForSeconds(0.2F);
        }
    }
}
