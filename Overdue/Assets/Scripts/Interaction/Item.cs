using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum ItemTypes { None, Key, Note }
public class Item : MonoBehaviour, IInteractable
{

    [SerializeField] string itemName = String.Empty;
	[SerializeField] string itemDescription = String.Empty;
    [SerializeField] ItemTypes itemType;
    [SerializeField] AudioClip pickupItemSound;

    Transform rotationAnchorTransform;

	bool isBeingInspected = false;
	bool canRotateItem = false;
	
	public string ItemName => itemName;
	public string ItemDescription => itemDescription;
    public ItemTypes ItemType => itemType;

	protected virtual void Awake()
	{
		if (String.IsNullOrWhiteSpace(ItemName))
		{
			itemName = name;
		}

		if (String.IsNullOrWhiteSpace(itemDescription))
		{
			itemDescription = $"No description for item \"{name}\"!";
		}
        if (itemType == ItemTypes.None)
        {
            Debug.LogWarning("Item: " + itemName + " has no itemtype!");
        }

    }
	
	public void Initialize(Transform newRotationAnchorTransform)
	{
		rotationAnchorTransform = newRotationAnchorTransform;
		gameObject.layer = LayerMask.NameToLayer("Inventory");
	}

	public void StartHover()
	{

	}

	public void Interact()
    {
        PhoneManager.Instance.PlaySound(pickupItemSound);
        PhoneManager.Instance.AddItem(itemName);
        if (this.gameObject.GetComponent<ItemInteractEventSequence>() != null)
        {
            this.gameObject.GetComponent<ItemInteractEventSequence>().PlayEvent();
        }
	}

	public void StopHover()
	{

	}
	
	void Update()
    {
        RotateIfBeingInspected();
    }

    void RotateIfBeingInspected()
    {
        if (!canRotateItem)
            return;

        if (!Input.GetKey(KeyCode.Mouse0))
            return;

        float xRotInput = Input.GetAxis("Mouse X");
        float yRotInput = Input.GetAxis("Mouse Y");
        float xRot = xRotInput * 250f;
        float yRot = yRotInput * 250f;

        gameObject.transform.Rotate(rotationAnchorTransform.up, -xRot * Time.deltaTime, Space.World);
        gameObject.transform.Rotate(rotationAnchorTransform.right, yRot * Time.deltaTime, Space.World);
    }

    public void StartInspect()
    {
        if (isBeingInspected)
        {
            Debug.LogError($"Tried to inspect {this} but it is already being inspected!");
            return;
        }

        gameObject.SetActive(true);
        isBeingInspected = true;
        canRotateItem = true;

        transform.rotation = rotationAnchorTransform.rotation;
    }

    public void StopInspect()
    {
        if (!isBeingInspected)
        {
            Debug.LogError($"Tried to stop inspecting {this} but it is already not being inspected!");
            return;
        }

        isBeingInspected = false;
        canRotateItem = false;
        gameObject.SetActive(false);
    }
}
