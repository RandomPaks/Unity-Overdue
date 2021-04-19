using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance { get; private set; }
	
	GameSave gameSave;

	void Awake()
	{
		if (Instance != null && this != Instance)
		{
			DestroyImmediate(gameObject);
			return;
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);
	}
	
	[ContextMenu("Save To First Save Slot")]
	public void SaveToFirstSlot() => Save(0, null);
	
	public void Save(int index, Transform saveTransform)
	{
		Vector3 playerPos = Vector3.zero;
		Quaternion playerRot = Quaternion.identity;

		if (saveTransform != null)
		{
			playerPos = saveTransform.position;
			playerRot = saveTransform.rotation;
		}

		List<Item> inventoryItemsToCopy = PhoneManager.Instance.Inventory;
		GameSave newGameSave = new GameSave(playerPos, playerRot, inventoryItemsToCopy);
		
		string filePath = GetGameSaveFullPath(index);
		string jsonGameSave = JsonUtility.ToJson(newGameSave);
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(filePath);
		bf.Serialize(file, jsonGameSave);
		file.Close();
		
		Debug.Log($"Game saved! {jsonGameSave}");
	}

	[ContextMenu("Load From First Save Slot")]
	public void LoadFromFirstSaveSlot() => Load(0);

	public void Load(int index)
	{
		string filePath = GetGameSaveFullPath(index);
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(filePath, FileMode.Open);
		string jsonGameSave = (string)bf.Deserialize(file);
		file.Close();

		GameSave loadedGameSave = JsonUtility.FromJson<GameSave>(jsonGameSave);

		if (loadedGameSave == null)
		{
			Debug.LogError($"Failed to load save! Index: {index}");
			return;
		}

		gameSave = loadedGameSave;
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	public string GetGameSaveFullPath(int index)
	{
		return Application.persistentDataPath + "/save_" + index.ToString().PadLeft(2, '0');
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		GameObject playerGO = GameManager.Instance.Player;
		CharacterController playerController = playerGO.GetComponent<CharacterController>();
		playerController.enabled = false;
		playerGO.transform.SetPositionAndRotation(gameSave.SpawnPos, gameSave.SpawnRot);
		playerController.enabled = true;

		PhoneManager.Instance.ClearInventory();
		foreach (string itemName in gameSave.InventoryItems)
		{
			PhoneManager.Instance.AddItem(itemName);
		}

		SceneManager.sceneLoaded -= OnSceneLoaded;
		
		Debug.Log($"Game loaded! {gameSave}");
	}
}
