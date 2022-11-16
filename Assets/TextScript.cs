using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TextScript : MonoBehaviour
{
    [SerializeField] string[] text;
    [SerializeField] TextMeshProUGUI[] mText;
    [SerializeField] bool isPickUpAble = false;
    int currentIndex = 0;
    bool wasPickedUp = false;

    XRGrabInteractable grab;

    // Start is called before the first frame update
    void Start()
    {
        mText[0].text = text[currentIndex];
        if(mText.Length > 1 && text.Length > 1)
        {
            mText[1].text = text[currentIndex + 1];
        }

        if(isPickUpAble)
        {
            grab = GetComponent<XRGrabInteractable>();
        }
    }

    private void FixedUpdate()
    {
        if(isPickUpAble && !wasPickedUp)
        {
            if (grab.interactorsSelecting.Count > 0)
            {
                wasPickedUp = true;
            }
        }
    }

    //Only made for 1 paper objs
    public void ChangeText()
    {
        currentIndex++;
        if(currentIndex > text.Length) currentIndex = 0;
        mText[0].text = text[currentIndex];
    }

    public bool GetWasPickedUp()
    {
        return wasPickedUp;
    }

    public string getText(int index)
    {
        return text[index];
    }
}
