using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHealth : MonoBehaviour
{
    //Objects for hands
    [SerializeField] Material baseMaterial;
    [SerializeField] Material wireMaterial;
    [SerializeField] GameObject leftHandPrefab;
    [SerializeField] GameObject rightHandPrefab;

    //Health
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth = 100;

    //Hand Value
    int corruptedHands = 0;

    //Get MeshRenders, connect to event, set health
    private void Start()
    {
        Enemy_Script.hitPlayer += UpdateHealth;
        HealScript.healPlayer += UpdateHealth;

        health = maxHealth;
    }

    //Disconnect from event
    private void OnDestroy()
    {
        Enemy_Script.hitPlayer -= UpdateHealth;
        HealScript.healPlayer -= UpdateHealth;
    }

    //Neg values do damage, pos values heal
    void UpdateHealth(int change)
    {
        if(health > 0)
        {
            Debug.Log(change);
            health += change;
            UpdateHands();
            UpdateText();
        }
        else
        {
            Restart.restartScene();
        }
    }

    //Update Material for hands
    void UpdateHands()
    {
        if(health <= (maxHealth / 4) && corruptedHands != 2)
        {
            Debug.Log("Update 2 Hands");
            corruptedHands = 2;
            
        }
        else if (health <= (maxHealth / 2) && corruptedHands != 1)
        {
            Debug.Log("Update Hands");
            corruptedHands = 1;
            if (Random.Range(1, 3) % 2 == 0)
            {
                
            }
            else
            {
                
            }
        }
        else if(health > (maxHealth / 2) && corruptedHands != 0)
        {
            Debug.Log("Reset Hands");
            corruptedHands = 0;
            
        }
    }

    //Update UI Text
    void UpdateText()
    {
        Debug.Log("Health UI Update: " + health);
    }
}
