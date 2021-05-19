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
		PhoneManager.Instance.AddItem(itemName);
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

    public IEnumerator StartInspectCoroutine()
    {
        if (isBeingInspected)
        {
            Debug.LogError($"Tried to inspect {this} but it is already being inspected!");
            yield break;
        }

        gameObject.SetActive(true);
        isBeingInspected = true;
        canRotateItem = true;

        transform.localScale = Vector3.zero;
        transform.rotation = rotationAnchorTransform.rotation;
        
        DOTween.Kill(transform);
        yield return transform.DOScale(Vector3.one, 0.5f).WaitForCompletion();
    }

    public IEnumerator StopInspectCoroutine()
    {
        if (!isBeingInspected)
        {
            Debug.LogError($"Tried to stop inspecting {this} but it is already not being inspected!");
            yield break;
        }

        isBeingInspected = false;
        canRotateItem = false;

        float tweenDuration = 0.5f;
        DOTween.Kill(transform);
        yield return DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, tweenDuration))
            .Join(transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 180f, 0), tweenDuration))
            .WaitForCompletion();
        
        gameObject.SetActive(false);
    }
}
