using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

//This is based on code provided by: https://www.youtube.com/watch?v=q1HuVUdq9ps&list=PLX8u1QKl_yPDn0-_fMYjJ_5hIA397L-z6&index=8
public class TeleportationController : MonoBehaviour
{
    static private bool mTeleportIsActive = false;

    public enum ControllerType
    {
        RightHand,
        LeftHand
    }

    public ControllerType targetController;

    public InputActionAsset inputAction;

    public XRRayInteractor rayInteractor;

    public TeleportationProvider teleportationProvider;

    private InputAction mThumbstickInputAction;

    private InputAction mTeleportActivate;

    private InputAction mTeleportCancel;

    [SerializeField] XRDirectInteractor HandGrab;
    [SerializeField] XRRayInteractor HandRay;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        Debug.Log("XRI " + targetController.ToString() + " Locomotion");
        mTeleportActivate = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Activate");
        mTeleportActivate.Enable();
        mTeleportActivate.performed += OnTeleportActivate;

        mTeleportCancel = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Cancel");
        mTeleportCancel.Enable();
        mTeleportCancel.performed += OnTeleportCancel;

        mThumbstickInputAction = inputAction.FindActionMap("XRI " + targetController.ToString() + " Interaction").FindAction("Select");
        mTeleportCancel.Enable();
        mTeleportCancel.performed += OnTeleportCancel;
    }

    private void OnDestroy()
    {
        mTeleportActivate.performed -= OnTeleportActivate;
        mTeleportCancel.performed -= OnTeleportCancel;
    }

    private void Update()
    {
        if(HandGrab.hasSelection || HandRay.hasSelection)
        {
            rayInteractor.enabled = false;
            mTeleportIsActive = false;
        }
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if(!mTeleportIsActive)
        {
            rayInteractor.enabled = true;
            mTeleportIsActive = true;
        }    
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (mTeleportIsActive && rayInteractor.enabled == true)
        {
            rayInteractor.enabled = false;
            mTeleportIsActive = false;
        }
    }

    private void Move()
    {
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
        {
            Debug.Log(raycastHit);
            rayInteractor.enabled = false;
            mTeleportIsActive = false;
            return;
        }

        Debug.Log(raycastHit);
        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = raycastHit.point,
        };

        teleportationProvider.QueueTeleportRequest(teleportRequest);

        rayInteractor.enabled = false;
        mTeleportIsActive = false;
    }
}
