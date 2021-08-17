using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogName;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] RawImage dialogSprite;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    int currentLine = 0;
    Dialog dialog;
    AudioSource dialogSFX;
    Action onDialogFinished;

    bool withCG = false; // dialog is displaying on top of CG
    public bool IsShowing { get; private set; }

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        this.dialogSFX = GetComponent<AudioSource>();
    }

    public IEnumerator ShowDialog(Dialog dialog, bool withCG, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Dialog");
        this.OnShowDialog?.Invoke();

        this.IsShowing = true;
        this.dialog = dialog;
        this.dialogBox.SetActive(true);
        this.dialogSprite.gameObject.SetActive(true);
        this.onDialogFinished = onFinished;
        this.UpdateDialogText(this.dialog.Lines[this.currentLine].Line);
        this.UpdateDialogName(this.dialog.Lines[this.currentLine].Name);
        this.withCG = withCG;
        if (this.dialog.Lines[this.currentLine].SFX != null)
        {
            this.dialogSFX.volume = this.dialog.Lines[this.currentLine].SFXVolume;
            this.dialogSFX.clip = this.dialog.Lines[this.currentLine].SFX;
            this.dialogSFX.Play();
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetMouseButtonDown(0) &&  this.IsShowing)
        {
            this.currentLine++;
            if (this.currentLine < this.dialog.Lines.Count)
            {
                this.UpdateDialogText(this.dialog.Lines[this.currentLine].Line);
                this.UpdateDialogName(this.dialog.Lines[this.currentLine].Name);
                this.UpdateDialogSprite(this.dialog.Lines[this.currentLine].Sprite);
                if (this.dialog.Lines[this.currentLine].SFX != null)
                {
                    this.dialogSFX.clip = this.dialog.Lines[this.currentLine].SFX;
                    this.dialogSFX.Play();
                }
            }
            else
            {
                this.currentLine = 0;
                this.IsShowing = false;
                this.dialogBox.SetActive(false);
                this.dialogSprite.gameObject.SetActive(false);
                this.onDialogFinished?.Invoke();
                if (!this.withCG)
                {
                    this.OnCloseDialog?.Invoke();
                }
                //Debug.Log("Dialog finished");
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

    public void UpdateDialogSprite(Texture sprite)
    {
        this.dialogSprite.texture = sprite;
    }
}
