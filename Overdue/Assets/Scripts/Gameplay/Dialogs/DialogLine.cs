using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [SerializeField] string name; // name of person speaking
    [SerializeField] string line;
    [SerializeField] AudioClip sfx;
    [SerializeField] float sfxVolume = 1f;


    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public string Line
    {
        get
        {
            return this.line;
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
