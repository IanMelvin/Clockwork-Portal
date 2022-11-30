using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] float timeBetween, startTime;

    GameObject player;

    static int numEnemiesDead = 0;

    bool startedSpawner = false;

    public static int getNumEnemiesDead()
    {
        return numEnemiesDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Enemy_Script.Died += UpdateKillCount;
        numEnemiesDead = 0;
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Play-er");
        player = obj[0];
    }

    private void OnDestroy()
    {
        Enemy_Script.Died -= UpdateKillCount;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= 50.0 && !startedSpawner)
        {
            startedSpawner = true;
            StartCoroutine("spawnCoolDown");
        }
    }

    void SpawnAlot()
    {
        for(int i = 0; i <= 5; i++)
        {
            Spawn();
        }
        StartCoroutine("spawnCoolDown");
    }

    void Spawn()
    {
        Instantiate(skeleton, transform.position, transform.rotation);
    }

    void UpdateKillCount()
    {
        numEnemiesDead++;
    }

    IEnumerator spawnCoolDown()
    {
        yield return new WaitForSeconds(timeBetween);
        SpawnAlot();
    }
}
