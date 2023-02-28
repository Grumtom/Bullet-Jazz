using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem;
public class PlayerControls : MonoBehaviour, IPlayerActions
{
    private Vector2 moveInput;
    private Vector3 lookInput;
    [SerializeField] private GameObject reticule;
    private Camera mainCam; //?
    private controlMode mode;

    private enum controlMode
    {
        Mouse,
        Gamepad,
        WaitForMode
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        reticule.transform.localPosition = Vector3.zero;
        lookInput = Vector3.zero;
        moveInput = Vector3.zero;
        mode = controlMode.WaitForMode;
    }

    // Update is called once per frame
    void Update()
    {

        reticule.transform.localPosition += lookInput;
        if(reticule.transform.localPosition.magnitude > 1f)
        {
            reticule.transform.localPosition.Normalize();
        }
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        lookInput = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseResetAim(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
