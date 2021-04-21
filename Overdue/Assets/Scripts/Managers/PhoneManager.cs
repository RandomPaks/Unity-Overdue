using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
	public static PhoneManager Instance { get; private set; }

	[Header("UI Prefabs")]
	[SerializeField] InventoryItemButton itemButtonPrefab;

	[Header("UI Instance References")]
	[SerializeField] GameObject phoneUI;
	[SerializeField] GameObject phoneInventoryUI;
	[SerializeField] TMP_Text itemDescriptionText;

	[Header("Item Inspecting")]
	[SerializeField] Transform inspectTransform = null;

	List<Item> inventory = new List<Item>();
	Item currentInspectingItem = null;

	Coroutine switchInspectingItemCoroutine = null;

	bool isPhone = false;
	bool isTransitioningIn = false;
	bool isTransitioningOut = false;
	bool isInspectingItem = false;

	public List<Item> Inventory => inventory;

	public bool isDisabled = false;

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !this.isDisabled)
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
				if (switchInspectingItemCoroutine != null)
				{
					StopCoroutine(switchInspectingItemCoroutine);
					switchInspectingItemCoroutine = null;
				}

				if (isInspectingItem && !isTransitioningOut)
				{
					StopInspectingItem();
				}

				this.phoneUI.SetActive(false);
				GameManager.Instance.toggleCursorLock(true);
				GameManager.Instance.SetState(GameState.GAME);
			}
		}
	}

	public void AddItem(string itemName)
	{
		GameObject itemObjectPrefab = Resources.Load<GameObject>($"Items/{itemName}");

		if (itemObjectPrefab == null)
		{
			Debug.LogError($"Item {itemName} not found in Resources!");
			return;
		}

		// Destroy the item from the world
		foreach (Item itemInstance in FindObjectsOfType<Item>())
		{
			if (itemInstance.ItemName == itemName)
			{
				Destroy(itemInstance.gameObject);
				break;
			}
		}

		Item item = itemObjectPrefab.GetComponent<Item>();
		inventory.Add(item);

		InventoryItemButton inventoryItemButton = Instantiate(itemButtonPrefab, phoneInventoryUI.transform);
		inventoryItemButton.Initialize(item);
	}

	public void RemoveItem(Item item)
	{
		inventory.Remove(item);

		foreach (InventoryItemButton button in phoneInventoryUI.GetComponentsInChildren<InventoryItemButton>())
		{
			if (button.Item == item)
			{
				Destroy(button);
				break;
			}
		}
	}

	public void ClearInventory()
	{
		inventory.Clear();

		foreach (InventoryItemButton button in phoneInventoryUI.GetComponentsInChildren<InventoryItemButton>())
		{
			Destroy(button);
		}
	}


	// Check if player has item
	public bool HasItem(Item item)
	{
		if (item == null)
		{
			Debug.LogError("Tried to check if player has item but argument is null!");
			return false;
		}

		return GetItem(itemInInventory => item == itemInInventory) != null;
	}

	// Get an item using a predicate
	public Item GetItem(Predicate<Item> predicate)
	{
		foreach (Item item in inventory)
		{
			if (predicate.Invoke(item))
				return item;
		}

		return null;
	}

	public void InspectItem(Item itemToInspect)
	{
		if (isTransitioningIn)
			return;

		if (isInspectingItem)
		{
			if (currentInspectingItem == itemToInspect)
			{
				StopInspectingItem();
			}
			else
			{
				SwitchInspectingItem(itemToInspect);
			}
		}
		else
		{
			StartInspectingItem(itemToInspect);
		}
	}

	void StartInspectingItem(Item item)
	{
		StartCoroutine(StartInspectCoroutine(item));
	}

	IEnumerator StartInspectCoroutine(Item item)
	{
		isInspectingItem = true;
		isTransitioningIn = true;

		currentInspectingItem = Instantiate(item, inspectTransform);
		currentInspectingItem.Initialize(inspectTransform);
		currentInspectingItem.transform.localPosition = Vector3.zero;

		itemDescriptionText.gameObject.SetActive(true);
		itemDescriptionText.text = item.ItemDescription;

		yield return StartCoroutine(currentInspectingItem.StartInspectCoroutine());

		isTransitioningIn = false;
	}

	void StopInspectingItem()
	{
		StartCoroutine(StopInspectingItemCoroutine());
	}

	IEnumerator StopInspectingItemCoroutine()
	{
		isTransitioningOut = true;

		itemDescriptionText.gameObject.SetActive(false);

		yield return StartCoroutine(currentInspectingItem.StopInspectCoroutine());

		Destroy(currentInspectingItem.gameObject);

		currentInspectingItem = null;
		isTransitioningOut = false;
		isInspectingItem = false;
	}

	void SwitchInspectingItem(Item itemToInspect)
	{
		if (switchInspectingItemCoroutine != null)
			return;

		switchInspectingItemCoroutine = StartCoroutine(SwitchInspectingItemCoroutine(itemToInspect));
	}

	IEnumerator SwitchInspectingItemCoroutine(Item itemToInspect)
	{
		yield return StartCoroutine(StopInspectingItemCoroutine());
		yield return StartCoroutine(StartInspectCoroutine(itemToInspect));
		switchInspectingItemCoroutine = null;
	}
}
