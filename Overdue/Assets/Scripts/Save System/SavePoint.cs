using UnityEngine;

[DisallowMultipleComponent]
public class SavePoint : MonoBehaviour
{
	[SerializeField] Transform spawnTransform;

	void Start()
	{
		if (spawnTransform == null)
		{
			spawnTransform = transform;
		}
	}

	public void Save()
	{
		SaveManager.Instance.Save(0, spawnTransform);
	}
}
