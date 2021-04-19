using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
	[SerializeField] List<string> inventoryItems = new List<string>();
	[SerializeField] Vector3 spawnPos = Vector3.zero;
	[SerializeField] Quaternion spawnRot = Quaternion.identity;

	public Vector3 SpawnPos => spawnPos;
	public Quaternion SpawnRot => spawnRot;
	public List<string> InventoryItems => inventoryItems;

	public GameSave(Vector3 newSpawnPos, Quaternion newSpawnRot, List<Item> newInventoryItems)
	{
		spawnPos = newSpawnPos;
		spawnRot = newSpawnRot;
		
		foreach (Item item in newInventoryItems)
		{
			inventoryItems.Add(item.ItemName);
		}
	}
}
