using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<DialogLine> lines;

    public List<DialogLine> Lines
    {
        get
        {
            return this.lines;
        }
    }
}
