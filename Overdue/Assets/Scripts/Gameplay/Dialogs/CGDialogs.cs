using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGDialogs : MonoBehaviour
{
    [SerializeField] List<Dialog> dialogs;
    int currentIndex = 0; // index in the dialog list 

    public static CGDialogs Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.BeginDialog();
    }

    public void BeginDialog()
    {
        if (this.currentIndex < this.dialogs.Count)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(this.dialogs[this.currentIndex], () => { this.currentIndex++; }));
        }
        
    }
}
