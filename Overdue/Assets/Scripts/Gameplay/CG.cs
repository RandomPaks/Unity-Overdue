using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CG
{
    [SerializeField] List<CGFrame> frames;
    [SerializeField] float delay;

    public List<CGFrame> Frames
    {
        get
        {
            return this.frames;
        }
    }

    public float Delay
    {
        get
        {
            return this.delay; 
        }
    }
}
