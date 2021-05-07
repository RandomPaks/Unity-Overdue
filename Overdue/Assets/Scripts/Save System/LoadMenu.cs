using UnityEngine;

[DisallowMultipleComponent]
public class LoadMenu : Menu
{
    [SerializeField] GameSavePanel[] gameSavePanels = null;

    public void Start()
    {
        for (int i = 0; i < gameSavePanels.Length; i++)
        {
            GameSave gameSave = SaveManager.Instance.Load(i);
            
            if (gameSave != null)
            {
                gameSavePanels[i].Initialize(gameSave);
            }
            else
            {
                gameSavePanels[i].InitializeNoGameSave();
            }
        }
    }
}
