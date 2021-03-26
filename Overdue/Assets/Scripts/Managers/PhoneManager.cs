using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance { get; private set; }

    private List<GameObject> inventory;

    bool isPhone = false;
    [SerializeField] GameObject phoneUI;
    [SerializeField] GameObject itemButton;
    [SerializeField] Transform inspectTransform = null;

    bool isInspectingItem = false;

    InventoryItem currentInspectingItem = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventory = new List<GameObject>();
    }

    public void AddItem(GameObject item)
    {
        item.SetActive(false);
        item.transform.position = new Vector3(0, -100, 0);
        inventory.Add(item);

        GameObject button = Instantiate(itemButton, phoneUI.transform);
        button.GetComponentInChildren<Text>().text = item.name;

        // Have to initialize the inventory item
        InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
        inventoryItem.Initialize(inspectTransform);
        item.layer = LayerMask.NameToLayer("Inventory");
        
        // Link button to inventory item
        button.GetComponent<InventoryItemButton>().Initialize(inventoryItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPhone = !isPhone;

            if (isPhone)
            {
                this.phoneUI.SetActive(true);
                GameManager.Instance.toggleCursorLock(false);
                GameManager.Instance.SetState(GameState.PAUSED);
            }
            else
            {
                this.phoneUI.SetActive(false);
                GameManager.Instance.toggleCursorLock(true);
                GameManager.Instance.SetState(GameState.GAME);
            }
        }
    }

    public void InspectItem(InventoryItem inventoryItem)
    {
        if (isInspectingItem)
        {
            StopInspectingItem();

            if (currentInspectingItem != inventoryItem)
            {
                StartInspectingItem(inventoryItem);
            }
        }
        else
        {
            StartInspectingItem(inventoryItem);
        }
    }

    void StartInspectingItem(InventoryItem inventoryItem)
    {
        isInspectingItem = true;
        currentInspectingItem = inventoryItem;
        currentInspectingItem.transform.position = inspectTransform.position;
        currentInspectingItem.StartInspect();
    }

    void StopInspectingItem()
    {
        isInspectingItem = false;
        currentInspectingItem.StopInspect();
    }
}
