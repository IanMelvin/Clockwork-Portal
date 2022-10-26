using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotCounter : MonoBehaviour
{
    private int mShotCounter = 0;
    private TextMeshProUGUI mText;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        UpdateText();
        Enemy_Script.Died += IncreaseCounter;
    }

    private void OnDestroy()
    {
        Enemy_Script.Died -= IncreaseCounter;
    }

    public void ResetCounter()
    {
        mShotCounter = 0;
        UpdateText();
    }

    private void IncreaseCounter()
    {
        mShotCounter++;
        UpdateText();
    }

    private void UpdateText()
    {
        mText.text = mShotCounter.ToString();
    }
}
