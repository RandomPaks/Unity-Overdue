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
    [SerializeField] GameObject visualCue = null;
    Vector3 originalCuePosition;
    [SerializeField] float doorRotationAngle = -90f;

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

        if(visualCue != null)
        {
            originalCuePosition = visualCue.transform.position;
        }
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
            if (visualCue != null)
            {
                visualCue.transform.DOMove(originalCuePosition, openDuration);
            }
                

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
        transform.DORotate(originalRotation + new Vector3(0f, doorRotationAngle, 0), openDuration);
        if(visualCue != null)
        {
            visualCue.transform.DOMove(new Vector3(originalCuePosition.x + 2f, originalCuePosition.y, originalCuePosition.z + 2f), openDuration);
        }
        
        isOpen = true;

        if (this.gameObject.GetComponent<AEventSequence>() != null)
        {
            this.gameObject.GetComponent<AEventSequence>().PlayEvent();
        }
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
