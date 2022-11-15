using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject[] Items;
    public int MaxAmount;
    public int CurrentAmount;
    public bool SpawnActive;

    private int _xPos;
    private int _yPos;
    private int _activeSpawner;
    private int _activeItem;
    
    void Start()
    {
       StartCoroutine(ZombieSpawn());
    }

    public IEnumerator ZombieSpawn()
    {
        while(SpawnActive)
        {
            if(CurrentAmount<MaxAmount)
            {
            _xPos=Random.Range(1,11);
            _yPos=Random.Range(1,11);
            NavMeshHit closestHit;
            _activeSpawner=Random.Range(0,SpawnPoints.Length);
            _activeItem=Random.Range(0,Items.Length);
            if (NavMesh.SamplePosition(new Vector3(_xPos+SpawnPoints[_activeSpawner].transform.position.x,20,_yPos+SpawnPoints[_activeSpawner].transform.position.z), out closestHit, 500f, NavMesh.AllAreas))
            Instantiate(Items[_activeItem], closestHit.position+new Vector3(0,0.5f,0),Quaternion.identity);
            CurrentAmount++;
            }
            yield return new WaitForSeconds(0.2F);
        }
    }
}
