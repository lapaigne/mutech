using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _onInteract;
    UnityEvent IInteractable.OnInteract
    {
        get => _onInteract;
        set => _onInteract = value;
    }

    public void Interact()
    {
        _onInteract.Invoke();
    }
}
