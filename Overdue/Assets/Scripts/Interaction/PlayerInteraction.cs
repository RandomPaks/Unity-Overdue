using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayers = new LayerMask();
    [SerializeField, Min(0)] float interactRange = 2f;
    [SerializeField, Min(0)] float interactRadius = 0.1f;

    Camera playerCamera = null;

    IInteractable currentlyHovering = null;

    bool hasClicked = false;

    void Awake()
    {
        playerCamera = Camera.main;
    }

    public void HandleUpdate()
    {
        IInteractable lookedAtInteractable = GetLookedAtInteractable();
        
        UpdateHoveringInteractable(lookedAtInteractable);

        if (hasClicked)
        {
            if (Input.GetMouseButtonUp(0))
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

    void UpdateHoveringInteractable(IInteractable lookedAtInteractable)
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

    IInteractable GetLookedAtInteractable()
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
                Debug.DrawLine(rayOrigin, hit.point);
                return interactable;
            }
        }

        return null;
    }

    void StartInteractableHover(IInteractable castedInteractable)
    {
        if (currentlyHovering != null)
        {
            Debug.LogError("Tried to overwrite currently hovered interactable!");
            return;
        }

        currentlyHovering = castedInteractable;
        castedInteractable.StartHover();
    }

    void StopInteractableHover()
    {
        if (currentlyHovering == null)
        {
            Debug.LogError("Tried to stop hovering interactable when there is none!");
            return;
        }
        
        currentlyHovering.StopHover();
        currentlyHovering = null;
    }
}