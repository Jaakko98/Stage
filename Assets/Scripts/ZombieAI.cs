using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class ZombieAI : MonoBehaviour
{
    //REFERENCES
    private Animator anim;
    private NavMeshAgent na;
    private Transform target;
    private Health health;

    //VARIABLES
    private float targetDistance;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        na = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        health = GetComponent<Health>();
        anim.SetBool("IsAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        anim.SetFloat("DistToTarget", targetDistance);

        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);

        //if animation state is walking, walk zombie towards target. Otherwise stay put
        if (asi.IsName("Z_Run"))
        {
            na.SetDestination(target.position);
            na.isStopped = false;
        }
        if(asi.IsName("Z_Idle") || asi.IsName("Z_Attack"))
        {
            na.isStopped = true;
        }
        if (!health.IsAlive())
        {
            anim.SetBool("IsAlive", false);
            Destroy(gameObject, 1.4f);
        }
    }
}
