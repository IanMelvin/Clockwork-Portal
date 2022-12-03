using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealScript : MonoBehaviour
{
    [SerializeField] int healing;

    public static event Action<int> healPlayer;
    
    public void Heal()
    {
        healPlayer?.Invoke(healing);
        Destroy(gameObject);
    }
}
