using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] KeyType keyType = KeyType.None;
    [SerializeField, Min(0)] float openDuration = 1f;
    [SerializeField, Min(0)] float closeDuration = 3f;
    [SerializeField, Min(0)] float remainOpenDuration = 5f;

    Vector3 originalRotation = Vector3.zero;
    
    bool isOpen = false;
    float openTimer = 0f;

    void Start()
    {
        originalRotation = transform.rotation.eulerAngles;
    }
    
    void Update()
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
        if (!IsOpenable())
            return;

        if (isOpen)
        {
            openTimer = 0f;
            return;
        }

        DOTween.Kill(transform);
        transform.DORotate(originalRotation + new Vector3(0f, -90f, 0), openDuration);
        
        isOpen = true;
    }

    public void StopHover()
    {
        
    }

    bool IsOpenable()
    {
        if (keyType == KeyType.None)
            return true;

        if (PhoneManager.Instance.HasKey(keyType))
            return true;

        return false;
    }
}
