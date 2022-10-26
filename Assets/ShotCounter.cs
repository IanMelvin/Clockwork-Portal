using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotCounter : MonoBehaviour
{
    private int mShotCounter = 0;
    private TextMeshProUGUI mText;

    // Sets values and connects to function
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        UpdateText();
        Enemy_Script.Died += IncreaseCounter;
    }

    //Disconnects from function
    private void OnDestroy()
    {
        Enemy_Script.Died -= IncreaseCounter;
    }

    //Reset counter
    public void ResetCounter()
    {
        mShotCounter = 0;
        UpdateText();
    }

    //Increment counter
    private void IncreaseCounter()
    {
        mShotCounter++;
        UpdateText();
    }

    //Update UI text
    private void UpdateText()
    {
        mText.text = mShotCounter.ToString();
    }
}
