using System;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayers = new LayerMask();
    [SerializeField, Min(0)] private float interactRange = 2f;
    [SerializeField, Min(0)] private float interactRadius = 0.1f;

    private Camera playerCamera = null;

    private IInteractable currentlyHovering = null;

    private bool hasClicked = false;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    public void HandleUpdate()
    {
        IInteractable lookedAtInteractable = GetLookedAtInteractable();
        
        UpdateHoveringInteractable(lookedAtInteractable);

        if (hasClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasClicked = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            hasClicked = true;
            
            if (currentlyHovering == null)
                return;

            currentlyHovering.Interact();
        }
    }

    private void UpdateHoveringInteractable(IInteractable lookedAtInteractable)
    {
        if (lookedAtInteractable != null)
        {
            if (currentlyHovering == lookedAtInteractable)
                return;

            if (currentlyHovering != null)
            {
                StopInteractableHover();
            }

            StartInteractableHover(lookedAtInteractable);
        }
        else if (currentlyHovering != null)
        {
            StopInteractableHover();
        }
    }

    private IInteractable GetLookedAtInteractable()
    {
        var playerCameraTransform = playerCamera.transform;
        Vector3 rayOrigin = playerCameraTransform.position;
        Vector3 rayDir = playerCameraTransform.forward;
        Ray interactRay = new Ray(rayOrigin, rayDir);

        if (Physics.SphereCast(interactRay, interactRadius, out var hit, interactRange, interactableLayers))
        {
            Debug.DrawLine(rayOrigin, hit.point);

            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            
            if (interactable != null)
            {
                return interactable;
            }
        }

        return null;
    }

    private void StartInteractableHover(IInteractable castedInteractable)
    {
        Debug.Assert(currentlyHovering == null);
        currentlyHovering = castedInteractable;
        castedInteractable.StartHover();
    }

    private void StopInteractableHover()
    {
        Debug.Assert(currentlyHovering != null);
        currentlyHovering.StopHover();
        currentlyHovering = null;
    }
}