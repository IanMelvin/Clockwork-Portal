using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject[] Portals;
    [SerializeField] int timeBetweenPortals;
    [SerializeField] int portalDuration;

    public static event Action<Transform, bool> portalActive;

    // Call coroutine to activate a portal
    void Start()
    {
        StartCoroutine("waitTillPortalOnTime");
    }

    //Set 1 portal to be active and start coroutine to turn them off
    void ActivatePortal()
    {
        switch (UnityEngine.Random.Range(0,3))
        {
            case 0:
                Portals[0].SetActive(true);
                portalActive?.Invoke(Portals[0].transform, true);
                break;
            case 1:
                Portals[1].SetActive(true);
                portalActive?.Invoke(Portals[1].transform, true);
                break;
            case 2:
                Portals[2].SetActive(true);
                portalActive?.Invoke(Portals[2].transform, true);
                break;
        }
        StartCoroutine("waitTillPortalOffTime");
    }

    //Turn off portals and call coroutine to turn them back on
    void TurnOffPortals()
    {
        Portals[0].SetActive(false);
        Portals[1].SetActive(false);
        Portals[2].SetActive(false);
        portalActive?.Invoke(Portals[0].transform, false);
        StartCoroutine("waitTillPortalOnTime");
    }

    // Coroutine to activate portal
    IEnumerator waitTillPortalOnTime()
    {
        yield return new WaitForSeconds(timeBetweenPortals);
        ActivatePortal();
    }

    //Coroutine to turn off portals
    IEnumerator waitTillPortalOffTime()
    {
        yield return new WaitForSeconds(portalDuration);
        TurnOffPortals();
    }
}
