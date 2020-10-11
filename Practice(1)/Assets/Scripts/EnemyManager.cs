using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public enum currentState
    {
        Idle, Trace, Attack, Dead
    }
    public currentState curstate = currentState.Idle;

    private Transform _transform;
    private Transform Playertransform;
    private NavMeshAgent nvagent;
    private Animator ani;

    public float TraceDist = 20.0f;
    public float AttackDist = 3.0f;

    private bool IsDead = false;

    
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        Playertransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvagent = this.gameObject.GetComponent<NavMeshAgent>();
        ani = this.gameObject.GetComponent<Animator>();

        StartCoroutine(checkState());
        StartCoroutine(checkStateAction());
    }

    IEnumerator checkState()
    {
        while(!IsDead)
        {
            yield return new WaitForSeconds(0.2f);

            float Dist = Vector3.Distance(Playertransform.position, -transform.position);
            if(Dist <= AttackDist)
            {
                curstate = currentState.Attack;
            }
            else if(Dist >= AttackDist)
            {
                curstate = currentState.Trace;
            }
            else
            {
                curstate = currentState.Idle;
            }
        }
    }

    
    IEnumerator checkStateAction()
    {
        while(!IsDead)
        {
            switch(curstate)
            {
                case currentState.Idle:
                    nvagent.enabled = false;
                    ani.SetBool("Trace", false);
                    break;
                case currentState.Trace:
                    nvagent.destination= Playertransform.position;
                    nvagent.enabled = true;
                    ani.SetBool("Trace", true);
                    break;
                case currentState.Attack:
                    break;
            }
            yield return null;
        }
    }    
    
    void Update()
    {
        
    }
}
