using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogName;
    [SerializeField] TextMeshProUGUI dialogText;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    int currentLine = 0;
    Dialog dialog;
    Action onDialogFinished;

    public bool IsShowing { get; private set; }

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog dialog, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();
        this.OnShowDialog?.Invoke();

        this.IsShowing = true;
        this.dialog = dialog;
        this.dialogBox.SetActive(true);
        this.onDialogFinished = onFinished;
        this.UpdateDialogText(this.dialog.Lines[this.currentLine]);
    }

    public void HandleUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.currentLine++;
            if (this.currentLine < this.dialog.Lines.Count)
            {
                this.UpdateDialogText(this.dialog.Lines[this.currentLine]);
            }
            else
            {
                this.currentLine = 0;
                this.IsShowing = false;
                this.dialogBox.SetActive(false);
                this.onDialogFinished?.Invoke();
                this.OnCloseDialog?.Invoke();
                Debug.Log("Dialog finished");
            }
        }
    }

    public void UpdateDialogName(string name)
    {
        this.dialogName.text = name;
    }

    public void UpdateDialogText(string line)
    {
        this.dialogText.text = line; 
    }
}
