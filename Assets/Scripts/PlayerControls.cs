using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem;
public class PlayerControls : MonoBehaviour, IPlayerActions
{
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector3 lookInput;
    [SerializeField] private GameObject reticule;
    [SerializeField] private GameObject secondAimer;
    private Camera mainCam; //?
    [SerializeField]
    private controlMode mode;
    private InputSystem conts;
    private bool mouseReset = false;
    [SerializeField]
    private float retDist = 3f;

    private Rigidbody myRB;

    [SerializeField] private float defaultSpeed = 1f, dashSpeed = 3f, currentSpeed = 1f;

    private enum controlMode
    {
        Mouse,
        Gamepad,
        WaitForMode
    }

    private void Awake()
    {
        conts = new InputSystem();
        conts.Player.SetCallbacks(this);
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
        myRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (conts != null)
            conts.Enable();
        else
            Awake();
    }
    private void OnDisable()
    {
        conts.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == controlMode.Mouse)
        {
            reticule.transform.localPosition += lookInput;
            if (reticule.transform.localPosition.magnitude > retDist)
            {
                reticule.transform.localPosition = reticule.transform.localPosition.normalized * retDist;
            }
            if (mouseReset)
            {
                reticule.transform.localPosition = Vector3.zero;
                mouseReset = false;
            }
        }
        else
        {
            reticule.transform.localPosition = lookInput * retDist;
        }
        float dotA;
        float dotB;
        dotA = Vector3.Angle((Vector3.right).normalized, reticule.transform.localPosition.normalized); //Compare ret position with up and right (the default angle of the sprite)
        dotB = Vector3.Dot((Vector3.down).normalized, reticule.transform.localPosition.normalized); //Compare also with down right (90 degrees clockwise)

        if (dotB > 0)
            dotA *= -1;//based on the dotB value, determine the direction of the rotation
        reticule.transform.localRotation = Quaternion.identity;
        reticule.transform.Rotate(Vector3.forward, dotA); //apply the sum rotation
        secondAimer.transform.localRotation = Quaternion.identity;
        secondAimer.transform.Rotate(Vector3.forward, dotA); //apply the sum rotation
    }

    private void FixedUpdate()
    {
        myRB.velocity = (moveInput * currentSpeed);
            //(transform.position + (moveInput * currentSpeed));
        //transform.position += moveInput * currentSpeed;
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        lookInput = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
        if (context.control.device.description.deviceClass == "Mouse")
        {
            mode = controlMode.Mouse;
            lookInput /= 100;
        }
        else
        {
            mode = controlMode.Gamepad;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();//can't fire
    }

    public void OnMouseResetAim(InputAction.CallbackContext context)
    {
        mouseReset = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = dashSpeed;
            Invoke("undash", 0.1f);
        }
    }

    private void undash()
    {
        currentSpeed = defaultSpeed;
    }
}
