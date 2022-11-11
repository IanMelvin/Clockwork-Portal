using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBullet_Pistol : MonoBehaviour
{
    //Bullet Stats
    [SerializeField] float mSpeed = 50f;
    [SerializeField] GameObject mBullet;
    [SerializeField] Transform mFirePoint;

    //Input
    [SerializeField] InputActionAsset inputActions;
    private InputAction mDropMag1, mDropMag2;

    //Reloading
    [SerializeField] GameObject droppedClipObj;
    [SerializeField] Transform bar;
    bool droppedClip = false, outOfAmmo = false;
    Transform magSpot, defaultBarPosition;
    float barOffset = -0.05f;
    GameObject newClip;

    //Ammo
    int totalAmmo, currentAmmo = 15;

    // Setting Up reloading Input and locating the location for reloading
    void Start()
    {
        mDropMag1 = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("DropMag");
        mDropMag1.Enable();
        mDropMag1.performed += DropMag;
        mDropMag2 = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("DropMag");
        mDropMag2.Enable();
        mDropMag2.performed += DropMag;

        magSpot = transform.GetChild(3);
    }

    //Disconnecting from Input System
    private void OnDestroy()
    {
        mDropMag1.performed -= DropMag;
        mDropMag2.performed -= DropMag;
    }

    private void FixedUpdate()
    {
        if (droppedClip && checkCollisons())
        {
            //Get new ammo data
            totalAmmo = newClip.GetComponent<TotalAmmo>().ammo;
            currentAmmo = totalAmmo;
            Destroy(newClip);

            //Update values
            droppedClip = false;
            outOfAmmo = false;
        }
    }

    bool checkCollisons()
    {
        Collider[] hitColliders = Physics.OverlapSphere(magSpot.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Pistolclip"))
            {
                newClip = hitCollider.gameObject;
                bar.localPosition = new Vector3(bar.localPosition.x - barOffset, bar.localPosition.y, bar.localPosition.z );
                return true;
            }
        }
        return false;
    }

    public void Fire()
    {
        if (!droppedClip && !outOfAmmo)
        {
            //Play Audio
            GetComponent<AudioSource>().Play();

            //Spawn bullet
            GameObject spawnedBullet = Instantiate(mBullet, mFirePoint.position, mFirePoint.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = mSpeed * mFirePoint.forward;
            Destroy(spawnedBullet, 5f);

            //Update Ammo
            currentAmmo--;
            if (currentAmmo <= 0) outOfAmmo = true;
        }
    }

    public void DropMag(InputAction.CallbackContext context)
    {
        if (!droppedClip && GetComponent<XRGrabInteractable>().interactorsSelecting.Count > 0)
        {
            //Spawn dropped clip
            GameObject spawnedDropClip = Instantiate(droppedClipObj, magSpot.position, magSpot.rotation);
            spawnedDropClip.SetActive(true);
            Destroy(spawnedDropClip, 5f);

            //update variables
            droppedClip = true;
            outOfAmmo = true;

            //Update Ammo
            currentAmmo = 0;
            bar.localPosition = new Vector3(bar.localPosition.x + barOffset, bar.localPosition.y, bar.localPosition.z);
        }
    }
}
