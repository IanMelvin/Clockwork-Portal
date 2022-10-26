using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputActions;

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
}
