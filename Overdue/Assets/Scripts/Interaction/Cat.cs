using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SavePoint))]
public class Cat : MonoBehaviour, IInteractable
{
	[Header("UI")]
	[SerializeField] SavePromptUI savePromptUI;
	
	[Header("Gameplay")]
	[SerializeField] Dialog saveDialog;
	
	SavePoint savePoint;

	void Awake()
	{
		savePoint = GetComponent<SavePoint>();
	}
	
	public void StartHover()
	{
		
	}
	
	public void Interact()
	{
		DialogManager.Instance.UpdateDialogName("Cat");
		StartCoroutine(DialogManager.Instance.ShowDialog(saveDialog, OnDialogComplete));
	}

	void OnDialogComplete()
	{
		savePromptUI.Show(OnPlayerChosen);
	}
	
	void OnPlayerChosen(bool answer)
	{
		if (answer)
		{
			savePoint.Save();
		}
	}
	
	public void StopHover()
	{
		
	}
}
