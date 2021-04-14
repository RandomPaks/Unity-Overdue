using UnityEngine;

public enum KeyType
{
    None,
    Bronze,
    Silver,
    Gold,
}

[DisallowMultipleComponent]
[RequireComponent(typeof(InventoryItem))]
public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] KeyType keyType = KeyType.None;

    public KeyType KeyType => keyType;

    InventoryItem inventoryItem = null;

    void Awake()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }

    public void Start()
    {
        if (keyType == KeyType.None)
        {
            Debug.LogError($"Invalid KeyType on {name}! Keys should not be KeyType.None!");
        }
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
