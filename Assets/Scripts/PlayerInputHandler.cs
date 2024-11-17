using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public InputActionAsset PlayerControls;

    [SerializeField] private string actionMapName = "Player";

    private string movement = "Movement";
    private string look = "Look";
    private string interact = "Interact";

    private InputAction moveAction;
    private InputAction lookAction;
    public InputAction interactAction;

    public Vector2 MoveInput {  get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractInput { get ; private set; }

    public static PlayerInputHandler Instance { get; private set; } 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = PlayerControls.FindActionMap(actionMapName).FindAction(movement);
        lookAction = PlayerControls.FindActionMap(actionMapName).FindAction(look);
        interactAction = PlayerControls.FindActionMap(actionMapName).FindAction(interact);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
    }
}
