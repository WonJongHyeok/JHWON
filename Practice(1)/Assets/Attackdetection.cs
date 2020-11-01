using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackdetection : MonoBehaviour
{
    public EnemyManager enemy;


    public SphereCollider attCollider;

    private void Awake()
    {
        attCollider.radius = enemy.AttackDist;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemy.curstate = EnemyManager.currentState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.curstate = EnemyManager.currentState.Trace;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            enemy.GetComponent<EnemyManager>().Attack();
        }
    }
}
