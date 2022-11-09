using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class FireBullet_Shotgun : MonoBehaviour
{
    //Bullet Stats
    [SerializeField] float mSpeed = 50f;
    [SerializeField] int numBullets = 6;
    [SerializeField] GameObject mBullet;
    [SerializeField] Transform mFirePoint;

    //Reloading
    [SerializeField] GameObject droppedClipObj;
    bool outOfAmmo = false;
    Transform reloadSpot;
    GameObject newClip;

    //Spread
    [SerializeField] float spread = 5.0f;

    //Ammo
    int totalAmmo = 6, currentAmmo = 6;

    // Setting Up reloading Input and locating the location for reloading
    void Start()
    {
        reloadSpot = transform.GetChild(4);
    }

    private void FixedUpdate()
    {
        checkCollisons();
    }

    bool checkCollisons()
    {
        Collider[] hitColliders = Physics.OverlapSphere(reloadSpot.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("shotgunRound") && currentAmmo < totalAmmo)
            {
                newClip = hitCollider.gameObject;
                //Get new ammo data
                currentAmmo += newClip.GetComponent<TotalAmmo>().ammo;
                Destroy(newClip);

                //Update values
                outOfAmmo = false;

                return true;
            }
        }
        return false;
    }

    public void Fire()
    {
        if (!outOfAmmo)
        {
            //Play Audio
            GetComponent<AudioSource>().Play();

            for(int i = 0; i < numBullets; i++)
            {
                float x = UnityEngine.Random.Range(-spread, spread);
                float y = UnityEngine.Random.Range(-spread, spread);

                Vector3 direction = -mFirePoint.right + new Vector3 (x, y, 0);

                //Spawn bullet
                GameObject spawnedBullet = Instantiate(mBullet, mFirePoint.position, mFirePoint.rotation);
                spawnedBullet.GetComponent<Rigidbody>().velocity = mSpeed * direction;
                Destroy(spawnedBullet, 5f);
            }

            //Update Ammo
            currentAmmo--;
            if (currentAmmo <= 0) outOfAmmo = true;
        }
    }
}
