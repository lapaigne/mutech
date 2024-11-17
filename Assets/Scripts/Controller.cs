using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2.5f;

    [SerializeField] private float gravity = 9.81f;
    
    [SerializeField] private float mouseSensitivity = 0.3f;
    [SerializeField] private float verticalRange = 80f;

    private CharacterController characterController;
    private Camera mainCamera;

    [SerializeField] private PlayerInputHandler inputHandler;

    private Transform targetObject;

    private Vector3 currentMovement;
    private float verticalRotation;

    [SerializeField] private float interactionDistance = 2.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
    }

    private void OnEnable()
    {
        inputHandler.interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputHandler.interactAction.performed -= OnInteract;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        HandleRotation(); 
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hitInfo, interactionDistance) && !hitInfo.collider.gameObject.isStatic)
        {
            targetObject = hitInfo.transform;

            if (targetObject.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log("Interacted");
                interactable.Interact();
            }
        }
    }

    public void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection  = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * walkSpeed;
        currentMovement.z = worldDirection.z * walkSpeed;

        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }

        characterController.Move(currentMovement * Time.deltaTime);
    }

    public void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);
        
        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
