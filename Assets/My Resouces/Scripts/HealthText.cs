using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.healthUpdate += UpdateText;
    }

    private void OnEnable()
    {
        Debug.Log(PlayerHealth.getHealth());
        UpdateText(PlayerHealth.getHealth());
    }

    private void OnDestroy()
    {
        PlayerHealth.healthUpdate -= UpdateText;
    }

    void UpdateText(int health)
    {
        mText.text = "HP: " + health.ToString();
    }
}
