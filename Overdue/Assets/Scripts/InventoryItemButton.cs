using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryItemButton : MonoBehaviour
{
    [SerializeField] TMP_Text itemNameText = null;
    
    InventoryItem inventoryItem = null;
    
    public void Initialize(InventoryItem newInventoryItem)
    {
        inventoryItem = newInventoryItem;
        itemNameText.text = newInventoryItem.ItemName;
    }
    
    public void OnButtonClicked()
    {
        PhoneManager.Instance.InspectItem(inventoryItem);
    }
}
