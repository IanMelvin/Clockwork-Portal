using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Play-er"))
        {
            Debug.Log("You Win");
            Application.Quit();
        }
    }
}
