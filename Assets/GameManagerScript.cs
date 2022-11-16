using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockObj;
    [SerializeField] TextMeshProUGUI WristUI_Clock;
    [SerializeField] TextMeshProUGUI WristUI_Lore;

    [SerializeField] List<TextScript> lore;

    float timer = 0;
    bool startTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
        }
        
    }

    public void DisplayTime()
    {
        clockObj.text = "";
        WristUI_Clock.text = "";
        float minutes = Mathf.FloorToInt(timer / 60);
        float hour = Mathf.FloorToInt(minutes / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        if (hour < 10)
        {
            clockObj.text += ("0" + hour);
            WristUI_Clock.text += ("0" + hour);
        }
        else
        {
            clockObj.text += hour;
            WristUI_Clock.text += hour;
        }

        clockObj.text += ":";
        WristUI_Clock.text += ":";

        if (minutes < 10)
        {
            clockObj.text += ("0" + minutes);
            WristUI_Clock.text += ("0" + minutes);

        }
        else
        {
            clockObj.text += minutes;
            WristUI_Clock.text += minutes;
        }

        clockObj.text += ":";
        WristUI_Clock.text += ":";

        if (seconds < 10)
        {
            clockObj.text += ("0" + seconds);
            WristUI_Clock.text += ("0" + seconds);
        }
        else
        {
            clockObj.text += seconds;
            WristUI_Clock.text += seconds;
        }
    }

    public void EnableTimer()
    {
        startTimer = true;
    }

    public string checkIfLoreIsCollected(int number)
    {
        if(lore[number].GetWasPickedUp())
        {
            return lore[number].getText(0);
        }
        return "Error: LoreFailed";
    }
}
