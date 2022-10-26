using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class FireBullet : MonoBehaviour
{
    [SerializeField] float mSpeed = 50f;
    [SerializeField] GameObject mBullet;
    [SerializeField] Transform mFirePoint;

    [SerializeField] InputActionAsset inputActions;
    [SerializeField] GameObject droppedClipObj;
    private InputAction mDropMag1, mDropMag2;
    bool droppedClip = false, outOfAmmo = false;

    Transform magSpot;
    GameObject newClip;
    int totalAmmo, currentAmmo = 30;

    public static event Action GunFired;
    public static event Action<int> NewMag;
    public static event Action DroppedMag;

    // Start is called before the first frame update
    void Start()
    {
        mDropMag1 = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("DropMag");
        mDropMag1.Enable();
        mDropMag1.performed += DropMag;
        mDropMag2 = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("DropMag");
        mDropMag2.Enable();
        mDropMag2.performed += DropMag;

        magSpot = transform.GetChild(6);
        AmmoCounter.OutOfAmmo += OutOfAmmo;
        AmmoCounter.NeedUpdatedAmmoCount += SendingUpdatedAmmo;
    }

    private void OnDestroy()
    {
        mDropMag1.performed -= DropMag;
        mDropMag2.performed -= DropMag;
        AmmoCounter.OutOfAmmo -= OutOfAmmo;
        AmmoCounter.NeedUpdatedAmmoCount -= SendingUpdatedAmmo;
    }

    private void FixedUpdate()
    { 
        if(droppedClip && checkCollisons())
        {
            totalAmmo = newClip.GetComponent<TotalAmmo>().ammo;
            currentAmmo = totalAmmo;
            Destroy(newClip);
            droppedClip = false;
            outOfAmmo = false;
            Transform clip = transform.GetChild(3);
            clip.gameObject.SetActive(true);
            if(NewMag != null) NewMag(totalAmmo);
        }
    }

    bool checkCollisons()
    {
        Collider[] hitColliders = Physics.OverlapSphere(magSpot.position, 0.1f);
        foreach(var hitCollider in hitColliders)
        {
            if(hitCollider.CompareTag("clip"))
            {
                newClip = hitCollider.gameObject;
                return true;
            }
        }
        return false;
    }

    void OutOfAmmo()
    {
        outOfAmmo = true;
    }

    void SendingUpdatedAmmo()
    {
        NewMag(currentAmmo);
    }

    public void Fire()
    {
        if(!droppedClip && !outOfAmmo)
        {
            GetComponent<AudioSource>().Play();
            GameObject spawnedBullet = Instantiate(mBullet, mFirePoint.position, mFirePoint.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = mSpeed * mFirePoint.forward;
            Destroy(spawnedBullet, 5f);
            GunFired?.Invoke();
            currentAmmo--;
        }
    }

    public void DropMag(InputAction.CallbackContext context)
    {
        if (!droppedClip)
        {
            Transform clip = transform.GetChild(3);
            clip.gameObject.SetActive(false);
            GameObject droppedClip = Instantiate(droppedClipObj, clip.position, clip.rotation);
            droppedClip.SetActive(true);
            Destroy(droppedClip, 5f);
            DroppedMag?.Invoke();
        }
        droppedClip = true;
        outOfAmmo = true;
    }
}
