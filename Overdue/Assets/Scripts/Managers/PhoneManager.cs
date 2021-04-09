using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
	public static PhoneManager Instance { get; private set; }

	List<InventoryItem> inventory;

	bool isPhone = false;

	[Header("UI Prefabs")]
	[SerializeField] InventoryItemButton itemButtonPrefab;
	
	[Header("UI Instance References")]
	[SerializeField] GameObject phoneUI;
	[SerializeField] GameObject phoneInventoryUI;
	[SerializeField] TMP_Text itemDescriptionText;

	[Header("Item Inspecting")]
	[SerializeField] Transform inspectTransform = null;

	InventoryItem currentInspectingItem = null;

	Coroutine switchInspectingItemCoroutine = null;
	
	bool isTransitioningIn = false;
	bool isTransitioningOut = false;
	bool isInspectingItem = false;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		inventory = new List<InventoryItem>();
	}

	public void AddItem(InventoryItem inventoryItem)
	{
		inventoryItem.gameObject.SetActive(false);
		inventoryItem.transform.position = new Vector3(0, -100, 0);
		inventoryItem.Initialize(inspectTransform);
		inventory.Add(inventoryItem);

		InventoryItemButton inventoryItemButton = Instantiate(itemButtonPrefab, phoneInventoryUI.transform);
		inventoryItemButton.Initialize(inventoryItem);
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

	// Check if player has key of matching KeyType
	public bool HasKey(KeyType keyType)
	{
		bool IsItemOfKeyType(InventoryItem inventoryItem)
		{
			if (!inventoryItem.TryGetComponent(out Key key))
				return false;

			return key.KeyType == keyType;
		}

		return GetItem(IsItemOfKeyType) != null;
	}

	// Get an item using a predicate
	public InventoryItem GetItem(Predicate<InventoryItem> predicate)
	{
		foreach (InventoryItem inventoryItem in inventory)
		{
			if (predicate.Invoke(inventoryItem))
				return inventoryItem;
		}

		return null;
	}

	public void InspectItem(InventoryItem itemToInspect)
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

	void StartInspectingItem(InventoryItem inventoryItem)
	{
		StartCoroutine(StartInspectCoroutine(inventoryItem));
	}

	IEnumerator StartInspectCoroutine(InventoryItem inventoryItem)
	{
		isInspectingItem = true;
		isTransitioningIn = true;
		
		currentInspectingItem = inventoryItem;
		currentInspectingItem.transform.position = inspectTransform.position;
		
		itemDescriptionText.gameObject.SetActive(true);
		itemDescriptionText.text = inventoryItem.ItemDescription;
			
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

		currentInspectingItem = null;
		isTransitioningOut = false;
		isInspectingItem = false;
	}

	void SwitchInspectingItem(InventoryItem itemToInspect)
	{
		if (switchInspectingItemCoroutine != null)
			return;
		
		switchInspectingItemCoroutine = StartCoroutine(SwitchInspectingItemCoroutine(itemToInspect));
	}

	IEnumerator SwitchInspectingItemCoroutine(InventoryItem itemToInspect)
	{
		yield return StartCoroutine(StopInspectingItemCoroutine());
		yield return StartCoroutine(StartInspectCoroutine(itemToInspect));
		switchInspectingItemCoroutine = null;
	}
}
