using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class PlayUISounds : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip onClickSound;
    [SerializeField, Range(0, 1)] float clickVolume = 0.5f;
    [SerializeField] AudioClip onHoverSound;
    [SerializeField, Range(0, 1)] float hoverVolume = 1f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        EventTrigger trigger;
        if(!TryGetComponent<EventTrigger>(out trigger))
        {
            trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;
        }

        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerClick;
        enter.callback.AddListener((eventData) => { OnClick(); });
        trigger.triggers.Add(enter);

        EventTrigger.Entry hover = new EventTrigger.Entry();
        hover.eventID = EventTriggerType.PointerEnter;
        hover.callback.AddListener((eventData) => { OnHover(); });
        trigger.triggers.Add(hover);
    }

    void OnClick()
    {
        if(onClickSound != null)
        {
            audioSource.clip = onClickSound;
            audioSource.volume = clickVolume;
            audioSource.Play();
        }
    }

    void OnHover()
    {
        if(onHoverSound != null)
        {
            audioSource.clip = onHoverSound;
            audioSource.volume = hoverVolume;
            audioSource.Play();
        }
    }
}
