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
		StartCoroutine(SavePromptCoroutine());
	}

	IEnumerator SavePromptCoroutine()
	{
		savePromptUI.Show();
		yield return new WaitUntil(() => savePromptUI.HasPlayerChosen);
		savePromptUI.Hide();

		if (savePromptUI.WillSave)
		{
			savePoint.Save();
		}
	}
	
	public void StopHover()
	{
		
	}
}
