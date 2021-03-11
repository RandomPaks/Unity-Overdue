using System;
using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField, Min(0)] private float openDuration = 1f;
    [SerializeField, Min(0)] private float closeDuration = 3f;
    [SerializeField, Min(0)] private float remainOpenDuration = 5f;

    private Vector3 originalRotation = Vector3.zero;
    
    private bool isOpen = false;
    private float openTimer = 0f;

    private void Start()
    {
        originalRotation = transform.rotation.eulerAngles;
    }
    
    private void Update()
    {
        if (!isOpen)
            return;

        openTimer += Time.deltaTime;

        if (openTimer >= remainOpenDuration)
        {
            DOTween.Kill(transform);
            transform.DORotate(originalRotation, closeDuration);
            
            isOpen = false;
            openTimer = 0f;
        }
    }

    public void StartHover()
    {
        
    }

    public void Interact()
    {
        if (isOpen)
            return;

        DOTween.Kill(transform);
        transform.DORotate(originalRotation + new Vector3(0f, -90f, 0), openDuration);
        
        isOpen = true;
    }

    public void StopHover()
    {
        
    }
}
