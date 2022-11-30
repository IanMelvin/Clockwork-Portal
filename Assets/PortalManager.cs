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

    private void OnEnable()
    {
        GameManagerScript.startGame += StartGame;
    }

    private void OnDisable()
    {
        GameManagerScript.startGame -= StartGame;
    }


    private void StartGame()
    {
        StartCoroutine("waitTillPortalOnTime");
    }

    //Set 1 portal to be active and start coroutine to turn them off
    void ActivatePortal()
    {
        Portals[0].SetActive(true);
        portalActive?.Invoke(Portals[0].transform, true);
        StartCoroutine("waitTillPortalOffTime");
    }

    //Turn off portals and call coroutine to turn them back on
    void TurnOffPortals()
    {
        Portals[0].SetActive(false);
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
