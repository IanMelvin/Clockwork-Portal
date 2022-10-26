using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] float timeBetween, startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time - startTime >= timeBetween)
        {
            startTime = Time.time;
            SpawnAlot();
        }
        
    }

    void SpawnAlot()
    {
        for(int i = 0; i <= 3; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(skeleton, transform.position, transform.rotation);
    }
}
