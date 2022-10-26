using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    [SerializeField] GameObject weaponUI;
    bool isActive = false;

    public void ToggleUI()
    {
        isActive = !isActive;
        weaponUI.SetActive(isActive);
    }
}
