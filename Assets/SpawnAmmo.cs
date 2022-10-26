using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    [SerializeField] GameObject clip;
    [SerializeField] Transform spawnPoint;

    public void Spawn()
    {
        Debug.Log("spawning");
        Instantiate(clip, spawnPoint.position, transform.rotation);
    }
}
