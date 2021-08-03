using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGManager : MonoBehaviour
{
    [SerializeField] RawImage cgCanvas;

    public event Action OnShowCG;
    public event Action OnCloseCG;

    int currentIndex = 0;
    CG cg;
    CGFrame currentFrame; 
    AudioSource cgSFX;
    Action onCGFinished;

    float delay; // delay time before switching to next frame 
    float ticks; 

    public bool IsShowing { get; private set; }
    public static CGManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        this.cgSFX = GetComponent<AudioSource>();
    }

    public IEnumerator ShowCG(CG cg, float delay, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();
        //Debug.Log("CG");
        this.OnShowCG?.Invoke();

        this.IsShowing = true;
        this.cg = cg;
        this.currentFrame = cg.Frames[this.currentIndex]; 
        this.delay = delay; 
        this.cgCanvas.gameObject.SetActive(true);
        this.onCGFinished = onFinished;
        this.UpdateCGFrame(this.cg.Frames[this.currentIndex].Texture); 
        if (this.currentFrame.SFX != null)
        {
            this.cgSFX.volume = this.currentFrame.SFXVolume;
            this.cgSFX.clip = this.currentFrame.SFX;
            this.cgSFX.Play();
        }
        if (this.currentFrame.DialogLines.Lines.Count != 0)
        {
            StartCoroutine(this.DisplayDialogCG());
        }
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        Debug.Log("Current frame line count: " + this.currentFrame.DialogLines.Lines.Count);
        if (this.currentFrame.DialogLines.Lines.Count == 0 && this.IsShowing)
        {
            this.DisplayCG();
            Debug.Log("No dialog");
        }
    }

    public void UpdateCGFrame(Texture2D texture)
    {
        this.cgCanvas.texture = texture; 
    }

    public void ShowCanvas(bool flag)
    {
        this.cgCanvas.gameObject.SetActive(flag);
    }

    void DisplayCG()
    {
        this.ticks += Time.deltaTime;
        if (this.ticks >= this.delay)
        {
            this.ticks = 0;
            this.ShowNextCG();
        }
    }

    // if the CG has dialog
    IEnumerator DisplayDialogCG()
    {
        //Debug.Log("Showing Dialog CG");
        yield return new WaitForSeconds(this.delay);
        StartCoroutine(DialogManager.Instance.ShowDialog(this.currentFrame.DialogLines, true, this.ShowNextCG)); 
    }

    // cg dialog finished show next frame
    void ShowNextCG()
    {
        this.currentIndex++;
        
        if (this.currentIndex < this.cg.Frames.Count)
        {
            this.currentFrame = this.cg.Frames[this.currentIndex];
            Debug.Log("Current frame line count: " + this.currentFrame.DialogLines.Lines.Count);
            this.UpdateCGFrame(this.currentFrame.Texture);
            if (this.currentFrame.SFX != null)
            {
                this.cgSFX.clip = this.currentFrame.SFX;
                this.cgSFX.Play();
            }

            if (this.currentFrame.DialogLines.Lines.Count != 0)
            {
                StartCoroutine(this.DisplayDialogCG());
            }
        }
        else
        {
            this.currentIndex = 0;
            this.IsShowing = false;
            this.cgCanvas.gameObject.SetActive(false);
            this.onCGFinished?.Invoke();
            this.OnCloseCG?.Invoke();
            Debug.Log("CG finished");
        }
    }
}
