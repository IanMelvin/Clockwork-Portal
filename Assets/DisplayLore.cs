using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mText;
    [SerializeField] List<GameObject> mLoreButtons;
    [SerializeField] GameObject WristUI_BackButton;
    [SerializeField] GameObject LoreUI_BackButton;

    public void DisplayLoreText(int number)
    {
        string text = FindObjectOfType<GameManagerScript>().checkIfLoreIsCollected(number);
        Debug.Log(text);
        if (text != "Error: LoreFailed")
        {
            foreach(var button in mLoreButtons)
            {
                button.SetActive(false);
            }
            mText.text = text;
            WristUI_BackButton.SetActive(false);
            LoreUI_BackButton.SetActive(true);
        }
    }

    public void LoreUIBackButton()
    {
        mText.text = "";
        foreach (var button in mLoreButtons)
        {
            button.SetActive(true);
        }
        WristUI_BackButton.SetActive(true);
        LoreUI_BackButton.SetActive(false);
    }
}
