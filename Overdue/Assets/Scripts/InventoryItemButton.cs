using UnityEngine;

public class InventoryItemButton : MonoBehaviour
{
    InventoryItem inventoryItem = null;
    
    public void Initialize(InventoryItem newInventoryItem)
    {
        inventoryItem = newInventoryItem;
    }
    
    public void OnButtonClicked()
    {
        PhoneManager.Instance.InspectItem(inventoryItem);
    }
}
