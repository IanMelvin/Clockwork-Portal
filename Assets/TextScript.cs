using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    [SerializeField] string[] text;
    [SerializeField] TextMeshProUGUI[] mText;
    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mText[0].text = text[currentIndex];
        if(mText.Length > 1 && text.Length > 1)
        {
            mText[1].text = text[currentIndex + 1];
        }
    }

    //Only made for 1 paper objs
    public void ChangeText()
    {
        currentIndex++;
        if(currentIndex > text.Length) currentIndex = 0;
        mText[0].text = text[currentIndex];
    }
}
