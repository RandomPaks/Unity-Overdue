using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGFrame
{
    [SerializeField] Texture2D texture;
    [SerializeField] AudioClip sfx;
    [SerializeField] float sfxVolume = 1f;
    
    public Texture2D Texture
    {
        get
        {
            return this.texture;
        }
    }

    public AudioClip SFX
    {
        get
        {
            return this.sfx;
        }
    }

    public float SFXVolume
    {
        get
        {
            return this.sfxVolume;
        }
    }
}
