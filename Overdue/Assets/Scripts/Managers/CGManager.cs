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
        Debug.Log("CG");
        this.OnShowCG?.Invoke();

        this.IsShowing = true;
        this.cg = cg;
        this.delay = delay; 
        this.cgCanvas.gameObject.SetActive(true);
        this.onCGFinished = onFinished;
        this.UpdateCGFrame(this.cg.Frames[this.currentIndex].Texture); 
        if (this.cg.Frames[this.currentIndex].SFX != null)
        {
            this.cgSFX.volume = this.cg.Frames[this.currentIndex].SFXVolume;
            this.cgSFX.clip = this.cg.Frames[this.currentIndex].SFX;
            this.cgSFX.Play();
        }
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        this.ticks += Time.deltaTime; 
        if (this.ticks >= this.delay)
        {
            this.ticks = 0; 
            this.currentIndex++;
            if (this.currentIndex < this.cg.Frames.Count)
            {
                this.UpdateCGFrame(this.cg.Frames[this.currentIndex].Texture);
                if (this.cg.Frames[this.currentIndex].SFX != null)
                {
                    this.cgSFX.clip = this.cg.Frames[this.currentIndex].SFX;
                    this.cgSFX.Play();
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

    public void UpdateCGFrame(Texture2D texture)
    {
        this.cgCanvas.texture = texture; 
    }

    public void ShowCanvas(bool flag)
    {
        this.cgCanvas.gameObject.SetActive(flag);
    }
}
