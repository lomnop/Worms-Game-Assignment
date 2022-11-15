using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent=null;
    private GameObject target;
    private GameObject turnControllerObject;
    private TurnController turnController;
    private float _moveSpeed;

    [SerializeField]public Animator anim;

    
    void Start()
    {
        GetReferences();
        _moveSpeed=_navMeshAgent.speed;
        target = GameObject.Find("Player");
        turnControllerObject = GameObject.Find("TurnController");
        turnController=turnControllerObject.GetComponent<TurnController>();
    }
    
    private void GetReferences()
    {
        _navMeshAgent=GetComponent<NavMeshAgent>();
    }

    private void MoveToTarget()
    {
        _navMeshAgent.SetDestination(target.transform.position);
        anim.Play("ZombieWalk");
    }

    private void FixedUpdate()
    {
        if(turnController.TurnOver==false)
        {
            _navMeshAgent.speed=_moveSpeed;
        }
        else
        {
            _navMeshAgent.speed=0;
        }
        MoveToTarget();
    }
}
