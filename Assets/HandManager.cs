using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandManager : MonoBehaviour
{
    [SerializeField] XRDirectInteractor leftHandGrab;
    [SerializeField] XRDirectInteractor RightHandGrab;
    [SerializeField] XRRayInteractor leftHandRay;
    [SerializeField] XRRayInteractor RightHandRay;

    private void FixedUpdate()
    {
        compareLeftHands();
        compareRightHands();
    }

    void compareLeftHands()
    {
        if(leftHandRay.hasSelection)
        {
            leftHandGrab.gameObject.SetActive(false);
        }
        else
        {
            leftHandGrab.gameObject.SetActive(true);
        }
    }

    void compareRightHands()
    {
        if (RightHandRay.hasSelection)
        {
            RightHandGrab.gameObject.SetActive(false);
        }
        else
        {
            RightHandGrab.gameObject.SetActive(true);
        }
    }
}
