using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    private TouchControls touchControl;

    public UnityEvent<InputAction, float> OnTouchStarted;
    public UnityEvent<Vector2, float> OnTouchEnded;

    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("[InputManager] There's more than one instance of InputManager on the scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        touchControl = new TouchControls();
    }

    private void OnEnable() 
    {
        touchControl.Enable();
    }

    private void OnDisable() 
    {
        touchControl.Disable();
    }

    private void Start() 
    {
        touchControl.Player.TouchPress.started += ctx => StartTouch(ctx);
        touchControl.Player.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        OnTouchStarted.Invoke(touchControl.Player.TouchPosition, (float)context.startTime);
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        OnTouchEnded.Invoke(touchControl.Player.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }
}
