using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryItemButton : MonoBehaviour
{
    [SerializeField] TMP_Text itemNameText = null;
    
    Item item = null;

    public Item Item => item;
    
    public void Initialize(Item newItem)
    {
        item = newItem;
        itemNameText.text = newItem.ItemName;
    }
    
    public void OnButtonClicked()
    {
        PhoneManager.Instance.InspectItem(item);
    }
}
