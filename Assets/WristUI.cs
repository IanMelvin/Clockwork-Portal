using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActions;
    [SerializeField] GameObject menu;

    private Canvas mWristUICanvas;
    private InputAction mMenu;

    // Start is called before the first frame update
    void Start()
    {
        mWristUICanvas = GetComponent<Canvas>();
        mMenu = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("Menu");
        mMenu.Enable();
        mMenu.performed += ToggleMenu;
    }

    private void OnDestroy()
    {
        mMenu.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        mWristUICanvas.enabled = !mWristUICanvas.enabled;
    }

    public void OnButton()
    {
        menu.SetActive(true);
        Debug.Log("On Button");
    }

    public void BackButton()
    {
        menu.SetActive(false);
        Debug.Log("Back Button");
    }

    public void QuitButton()
    {
        Debug.Log("Quit Button");
        Application.Quit();
    }

    public void HealthButton()
    {
        Debug.Log("Health Button");
    }

    public void KillCountButton()
    {
        Debug.Log("KillCount Button");
    }
    public void TimeButton()
    {
        Debug.Log("Time Button");
    }
    public void LoreButton()
    {
        Debug.Log("Lore Button");
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button");
    }
}
