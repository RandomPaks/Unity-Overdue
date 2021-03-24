using UnityEngine;

[DisallowMultipleComponent]
public class Key : MonoBehaviour, IInteractable
{
    public void StartHover()
    {

    }

    public void Interact()
    {
        PhoneManager.Instance.AddItem(gameObject);
    }

    public void StopHover()
    {

    }
}
