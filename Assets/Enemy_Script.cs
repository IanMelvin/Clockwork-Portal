using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    //Attacking properties
    [SerializeField] int damageToPlayer = 10;
    [SerializeField] float timeBetweenAttacks = 2.0f;
    bool hasAttacked;

    //Enemy properties
    NavMeshAgent agent;
    GameObject player;
    Animator animator;

    //Portal Properties
    Transform activePortal;
    bool isPortalActive;

    //Events
    public static event Action Died;
    public static event Action<int> hitPlayer;

    // Set values and connect to event
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Play-er");
        player = obj[0];
        animator = GetComponent<Animator>();

        PortalManager.portalActive += portalInfo;
    }

    //Disconnect from event
    private void OnDestroy()
    {
        PortalManager.portalActive -= portalInfo;
    }

    // Check if enemy should go towards player or portal
    void FixedUpdate()
    {
        if(!isPortalActive)
        {
            chasePlayer();
            checkDistance();
        }
        else
        {
            gotToPortal();
        }

        //idle();
        //lyingDown();
    }

    //Check bullet collisons
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            Died?.Invoke();
            Destroy(this.gameObject);
        }
    }

    //Go after the player
    private void chasePlayer()
    {
        agent.SetDestination(player.transform.position);
        animator.SetFloat("Vertical", .2f);
    }

    //Go to active portal
    private void gotToPortal()
    {
        agent.SetDestination(activePortal.position);
        animator.SetFloat("Vertical", .2f);
    }

    //Animation: Idle
    private void idle()
    {
        animator.SetFloat("Vertical", 0f);
    }

    //Animation: lying down
    private void lyingDown()
    {
        animator.SetTrigger("lying");
    }

    //Update Portal information
    void portalInfo(Transform portal, bool isActive)
    {
        activePortal = portal;
        isPortalActive = isActive;
    }

    //Check distance from player, if close enough attack, then call attack cooldown coroutine
    private void checkDistance()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= 2.5f && !hasAttacked)
        {
            if(UnityEngine.Random.Range(1, 3) % 2 == 0)
            {
                animator.SetTrigger("punch_L");
            }
            else
            {
                animator.SetTrigger("punch_R");
            }
            
            hitPlayer?.Invoke(-damageToPlayer);
            hasAttacked = true;
            StartCoroutine("attackCooldown");
        }
    }

    //Coroutine to end attack cooldown
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        hasAttacked = false;
    }
}
