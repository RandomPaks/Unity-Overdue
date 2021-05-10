using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class ResizeButtonText : MonoBehaviour
{
    [SerializeField] int addFontSize = 6;

    TMP_Text text = null;
    float normFontSize;
    void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        normFontSize = text.fontSize;

        EventTrigger trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener((eventData) => { OnEnter(); });
        trigger.triggers.Add(enter);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((eventData) => { OnExit(); });
        trigger.triggers.Add(exit);
    }

    void OnEnter() => text.fontSize = normFontSize + addFontSize;

    void OnExit() => text.fontSize = normFontSize;
}
