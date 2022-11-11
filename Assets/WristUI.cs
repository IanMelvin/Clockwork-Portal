using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActions;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject healthUI;
    [SerializeField] GameObject settingsUI;
    [SerializeField] GameObject KillCountUI;
    [SerializeField] GameObject TimeUI;
    [SerializeField] GameObject LoreUI;
    [SerializeField] GameObject onButton;

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
        onButton.SetActive(false);
        Debug.Log("On Button");
    }

    public void BackButton()
    {
        menu.SetActive(false);
        onButton.SetActive(true);
        Debug.Log("Back Button");
    }

    public void QuitButton()
    {
        Debug.Log("Quit Button");
        Application.Quit();
    }

    public void HealthButton()
    {
        menu.SetActive(false);
        healthUI.SetActive(true);
        Debug.Log("Health Button");
    }

    public void KillCountButton()
    {
        menu.SetActive(false);
        KillCountUI.SetActive(true);
        Debug.Log("KillCount Button");
    }
    public void TimeButton()
    {
        menu.SetActive(false);
        TimeUI.SetActive(true);
        Debug.Log("Time Button");
    }
    public void LoreButton()
    {
        menu.SetActive(false);
        LoreUI.SetActive(true);
        Debug.Log("Lore Button");
    }

    public void SettingsButton()
    {
        menu.SetActive(false);
        settingsUI.SetActive(true);
        Debug.Log("Settings Button");
    }

    public void BackToMain()
    {
        Debug.Log("Back To Main");
        menu.SetActive(true);
        healthUI.SetActive(false);
        settingsUI.SetActive(false);
        KillCountUI.SetActive(false);
        TimeUI.SetActive(false);
        LoreUI.SetActive(false);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
