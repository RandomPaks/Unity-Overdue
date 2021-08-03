using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PhoneManager : MonoBehaviour
{
	public static PhoneManager Instance { get; private set; }

	[Header("UI Prefabs")]
	[SerializeField] InventoryItemButton itemButtonPrefab;

	[Header("UI Instance References")]
	[SerializeField] GameObject phoneUI;
	[SerializeField] GameObject keyInventoryUI;
	[SerializeField] Toggle keyToggle;

	[SerializeField] GameObject notesInventoryUI;
	[SerializeField] Toggle notesToggle;

	[SerializeField] GameObject mapUI;
	[SerializeField] Toggle mapToggle;

	[SerializeField] Toggle flashlightToggle;
	[SerializeField] Light flashlight;

	[SerializeField] TMP_Text itemDescriptionText;

	[Header("Item Inspecting")]
	[SerializeField] Transform inspectTransform = null;

	List<Item> inventory = new List<Item>();
	Item currentInspectingItem = null;

	bool isPhone = false;
	bool isInspectingItem = false;

	public List<Item> Inventory => inventory;

	public bool isDisabled = false;

	AudioSource audioSource;
	[SerializeField] AudioClip onPhoneSound;
	[SerializeField] AudioClip offPhoneSound;

	void Awake()
	{
		Instance = this;

		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !this.isDisabled && (GameManager.Instance.GetState() == GameState.GAME || GameManager.Instance.GetState() == GameState.PHONE))
		{
			isPhone = !isPhone;
			flashlightToggle.isOn = flashlight.enabled;

			audioSource.volume = 0.2f;
			audioSource.clip = onPhoneSound;
			audioSource.Play();

			if (isPhone)
			{
				this.phoneUI.SetActive(true);
				GameManager.Instance.toggleCursorLock(false);
				GameManager.Instance.SetState(GameState.PHONE);
			}
			else
			{
				audioSource.volume = 0.2f;
				audioSource.clip = offPhoneSound;
				audioSource.Play();

				if (isInspectingItem)
				{
					StopInspectingItem();
				}

				this.phoneUI.SetActive(false);
				GameManager.Instance.toggleCursorLock(true);
				GameManager.Instance.SetState(GameState.GAME);
			}
		}

		if (Input.GetKeyDown(KeyCode.M) && !this.isDisabled && (GameManager.Instance.GetState() == GameState.GAME || GameManager.Instance.GetState() == GameState.PHONE))
		{
			isPhone = !isPhone;
			flashlightToggle.isOn = flashlight.enabled;
			keyToggle.isOn = false;
			keyInventoryUI.SetActive(false);

			notesToggle.isOn = false;
			notesInventoryUI.SetActive(false);

			mapToggle.isOn = true;
			mapUI.SetActive(true);

			audioSource.volume = 0.2f;
			audioSource.clip = onPhoneSound;
			audioSource.Play();

			if (isPhone)
			{
				this.phoneUI.SetActive(true);
				GameManager.Instance.toggleCursorLock(false);
				GameManager.Instance.SetState(GameState.PHONE);
			}
			else
			{
				audioSource.volume = 0.2f;
				audioSource.clip = offPhoneSound;
				audioSource.Play();

				if (isInspectingItem)
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
				Destroy(itemInstance.gameObject, 2);
				break;
			}
		}

		Item item = itemObjectPrefab.GetComponent<Item>();
		inventory.Add(item);
		InventoryItemButton inventoryItemButton = null;
		if (item.ItemType == ItemTypes.Key)
        {
			inventoryItemButton = Instantiate(itemButtonPrefab, keyInventoryUI.transform);
		}
		else if (item.ItemType == ItemTypes.Note)
        {
			inventoryItemButton = Instantiate(itemButtonPrefab, notesInventoryUI.transform);
		}

		inventoryItemButton.Initialize(item);
	}

	public void RemoveItem(Item item)
	{
		inventory.Remove(item);

		foreach (InventoryItemButton button in keyInventoryUI.GetComponentsInChildren<InventoryItemButton>())
		{
			if (button.Item == item)
			{
				Destroy(button);
				break;
			}
		}
		foreach (InventoryItemButton button in notesInventoryUI.GetComponentsInChildren<InventoryItemButton>())
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

		foreach (InventoryItemButton button in keyInventoryUI.GetComponentsInChildren<InventoryItemButton>())
		{
			Destroy(button);
		}
		foreach (InventoryItemButton button in notesInventoryUI.GetComponentsInChildren<InventoryItemButton>())
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
		isInspectingItem = true;

		currentInspectingItem = Instantiate(item, inspectTransform);
		currentInspectingItem.Initialize(inspectTransform);
		currentInspectingItem.transform.localPosition = Vector3.zero;

		itemDescriptionText.gameObject.SetActive(true);
		itemDescriptionText.text = item.ItemDescription;

		currentInspectingItem.StartInspect();
	}

	void StopInspectingItem()
	{
		itemDescriptionText.gameObject.SetActive(false);

		currentInspectingItem.StopInspect();

		Destroy(currentInspectingItem.gameObject);

		currentInspectingItem = null;
		isInspectingItem = false;
	}

	void SwitchInspectingItem(Item itemToInspect)
	{
		StopInspectingItem();
		StartInspectingItem(itemToInspect);
	}

	public void KeyToggler()
    {
		if(currentInspectingItem != null)
        {
			StopInspectingItem();
		}
        if (keyInventoryUI.activeSelf)
        {
			keyInventoryUI.SetActive(false);
        }
        else
        {
			keyInventoryUI.SetActive(true);
			notesInventoryUI.SetActive(false);
			mapUI.SetActive(false);
		}
	}

	public void NotesToggler()
	{
		if (currentInspectingItem != null)
		{
			StopInspectingItem();
		}
		if (notesInventoryUI.activeSelf)
		{
			notesInventoryUI.SetActive(false);
		}
		else
		{
			notesInventoryUI.SetActive(true);
			keyInventoryUI.SetActive(false);
			mapUI.SetActive(false);
		}
	}

	public void MapToggler()
    {
		if (currentInspectingItem != null)
		{
			StopInspectingItem();
		}
		if (mapUI.activeSelf)
		{
			mapUI.SetActive(false);
		}
		else
		{
			mapUI.SetActive(true);
			notesInventoryUI.SetActive(false);
			keyInventoryUI.SetActive(false);
		}
	}

	public void PlaySound(AudioClip sound)
    {
		audioSource.clip = sound;
		audioSource.Play();
    }
}
