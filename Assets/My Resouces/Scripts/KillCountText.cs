using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCountText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mText;

    // Start is called before the first frame update
    void Start()
    {
        mText.text = "Kills: 0";
    }

    private void OnEnable()
    {
        UpdateText(Spawner.getNumEnemiesDead());
    }

    private void FixedUpdate()
    {
        UpdateText(Spawner.getNumEnemiesDead());
    }

    void UpdateText(int value)
    {
        mText.text = "Kills: " + value.ToString();
    }
}
