using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //MeshRenderers for material changing
    Renderer leftHandRenderer;
    Renderer rightHandRenderer;

    //Hand Value
    int corruptedHands = 0;

    //Get MeshRenders, connect to event, set health
    private void Start()
    {
        leftHandRenderer = leftHandPrefab.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>();
        rightHandRenderer = rightHandPrefab.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>();
        leftHandRenderer.material = baseMaterial;
        rightHandRenderer.material = baseMaterial;

        Enemy_Script.hitPlayer += UpdateHealth;
        HealScript.healPlayer += UpdateHealth;

        health = maxHealth;
    }

    //Disconnect from event
    private void OnDestroy()
    {
        Enemy_Script.hitPlayer -= UpdateHealth;
        HealScript.healPlayer -= UpdateHealth;

        leftHandRenderer.material = baseMaterial;
        rightHandRenderer.material = baseMaterial;
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
            corruptedHands = 2;
            leftHandRenderer.material = wireMaterial;
            rightHandRenderer.material = wireMaterial;
        }
        else if (health <= (maxHealth / 2) && corruptedHands != 1)
        {
            corruptedHands = 1;
            if (Random.Range(1, 3) % 2 == 0)
            {
                leftHandRenderer.material = wireMaterial;
                rightHandRenderer.material = baseMaterial;
            }
            else
            {
                leftHandRenderer.material = baseMaterial;
                rightHandRenderer.material = wireMaterial;
            }
        }
        else if(health > (maxHealth / 2) && corruptedHands != 0)
        {
            corruptedHands = 0;
            leftHandRenderer.material = baseMaterial;
            rightHandRenderer.material = baseMaterial;
        }
    }

    //Update UI Text
    void UpdateText()
    {
        Debug.Log("Health UI Update: " + health);
    }
}
