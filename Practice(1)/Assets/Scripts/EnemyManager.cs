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
    public float AttackDist = 1.5f;
    public float attacktimer = 0f;
    public float attackdelay = 4.0f;
    public float damage = 1.0f;
    
    

    private bool IsDead = false;

    
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        Playertransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvagent = this.gameObject.GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        StartCoroutine(checkState());
        StartCoroutine(checkStateAction());
    }

    IEnumerator checkState()
    {
        while(!IsDead)
        {
            yield return new WaitForSeconds(0.2f);

            float Dist = Vector3.Distance(Playertransform.position, transform.position);
            if(Dist <= AttackDist)
            {
                curstate = currentState.Attack;
                Debug.Log("Attack");
            }
            else if(Dist >= AttackDist)
            {
                curstate = currentState.Trace;
                Debug.Log("trace");
                //nvagent.Warp(newPos);
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
                    nvagent.destination = Playertransform.position;
                    nvagent.enabled = true;
                    ani.SetBool("Trace", true);
                    
                    break;
                case currentState.Attack:
                    nvagent.enabled = false;
                    Attack();
                    
                    break;
            }
            yield return null;
        }
    }    
    
    
    public void Attack()
    {
        if (curstate == currentState.Attack)
        {
            if (attacktimer > attackdelay)
            {
                transform.LookAt(Playertransform.position);
                ani.SetBool("Attack", true);
                
                attacktimer = 0f;
            }

            else
            {
                curstate = currentState.Idle;
                ani.SetBool("Attack", false);
                
            }
            attacktimer += Time.deltaTime;
        }
        else
        {
            curstate = currentState.Trace;
        }
    }

}
