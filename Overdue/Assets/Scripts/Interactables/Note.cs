using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(InventoryItem))]
public class Note : MonoBehaviour, IInteractable
{
    InventoryItem inventoryItem = null;

    void Awake()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }
    
    public void StartHover()
    {
        
    }

    public void Interact()
    {
        PhoneManager.Instance.AddItem(inventoryItem);
    }

    public void StopHover()
    {
        
    }
}
