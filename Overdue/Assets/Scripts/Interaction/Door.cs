using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Item keyToOpen;
    [SerializeField, Min(0)] float openDuration = 1f;
    [SerializeField, Min(0)] float closeDuration = 3f;
    [SerializeField, Min(0)] float remainOpenDuration = 5f;

    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenSound;
    [SerializeField] AudioClip doorLockedSound;

    Vector3 originalRotation = Vector3.zero;
    
    bool isOpen = false;
    float openTimer = 0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        {
            audioSource.clip = doorLockedSound;
            audioSource.Play();
            return;
        }

        if (isOpen)
        {
            openTimer = 0f;
            return;
        }
        audioSource.clip = doorOpenSound;
        audioSource.Play();
        DOTween.Kill(transform);
        transform.DORotate(originalRotation + new Vector3(0f, -90f, 0), openDuration);
        
        isOpen = true;
    }

    public void StopHover()
    {
        
    }

    bool IsOpenable()
    {
        if (keyToOpen == null)
            return true;
        
        if (PhoneManager.Instance.HasItem(keyToOpen))
            return true;

        return false;
    }
}
