using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;

    public static event Action Died;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Play-er");
        player = obj[0];
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        chasePlayer();
        checkDistance();
        //idle();
        //lyingDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            Died?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void chasePlayer()
    {
        agent.SetDestination(player.transform.position);
        animator.SetFloat("Vertical", .2f);
    }

    private void idle()
    {
        animator.SetFloat("Vertical", 0f);
    }

    private void lyingDown()
    {
        animator.SetTrigger("lying");
    }


    //Change to Coroutine
    private void checkDistance()
    {
        if(Mathf.Abs(Vector3.Distance(transform.position,player.transform.position)) < 2.0f)
        {
            animator.SetTrigger("punch_L");
            Debug.Log("Dead");
            Application.Quit();
        }
        
    }
}
