using UnityEngine;
using UnityEngine.InputSystem;

public class UnityGameInput : MonoBehaviour, IGameInput
{
    private GameInputActions actions;
    private Camera cam;

    public Vector2 Move => actions.Gameplay.Move.ReadValue<Vector2>();

    public Vector2 MouseScreenPosition => actions.Gameplay.AimMouse.ReadValue<Vector2>();

    public bool JumpPressed => actions.Gameplay.Jump.WasPressedThisFrame();
    public bool JumpHeld => actions.Gameplay.Jump.IsPressed();

    public bool InteractPressed => actions.Gameplay.Interact.WasPressedThisFrame();
    public bool InteractHeld => actions.Gameplay.Interact.IsPressed();


    public bool CancelPressed => actions.Global.Cancel.WasPressedThisFrame();
    public bool CancelHeld => actions.Global.Cancel.IsPressed();

    public bool UsingController { get; private set; }

    void Awake()
    {
        actions = new GameInputActions();
        cam = Camera.main;
    }

    void OnEnable()
    {
        actions.Enable();

    }

    void OnDisable()
    {
        actions.Disable();
    }



    //private void UpdateAim()
    //{
    //    Vector2 stick = actions.Gameplay.AimStick.ReadValue<Vector2>();

    //    // Controller aim
    //    if (stick.sqrMagnitude > 0.01f)
    //    {
    //        AimStick = stick.normalized;
    //        UsingController = true;
    //        return;
    //    }
    //    else
    //    {
    //        UsingController = false;

    //        // Mouse aim (via Input Actions, not Mouse.current)
    //        Vector2 mouseScreen = actions.Gameplay.AimMouse.ReadValue<Vector2>();

    //        Vector3 world = cam.ScreenToWorldPoint(new Vector3(
    //            mouseScreen.x,
    //            mouseScreen.y,
    //            Mathf.Abs(cam.transform.position.z) // correct depth
    //        ));

    //        Vector2 dir = (Vector2)(world - transform.position);
    //        AimStick = dir.normalized;

    //    }


    //}
}