using UnityEngine;

[DisallowMultipleComponent]
public class SavePoint : MonoBehaviour, IInteractable
{
	[SerializeField] Transform spawnTransform;

	void Start()
	{
		if (spawnTransform == null)
		{
			spawnTransform = transform;
		}
	}
	
	public void StartHover()
	{
		
	}
	
	public void Interact()
	{
		SaveManager.Instance.Save(0, spawnTransform);
	}
	
	public void StopHover()
	{
		
	}
}
