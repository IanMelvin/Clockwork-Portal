using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    private int mShotCounter = 30;
    private TextMeshProUGUI mText;

    public static event Action OutOfAmmo;
    public static event Action NeedUpdatedAmmoCount;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        UpdateText();
        NeedUpdatedAmmoCount?.Invoke();
    }

    private void OnDestroy()
    {
    }

    private void DecreaseCounter()
    {
        mShotCounter--;
        if(mShotCounter <= 0)
        {
            OutOfAmmo?.Invoke();
        }
        UpdateText();
    }

    private void NewMag(int magSize)
    {
        mShotCounter = magSize;
        UpdateText();
    }

    private void DroppedMag()
    {
        mShotCounter = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        mText.text = mShotCounter.ToString();
    }
}
