using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    [SerializeField] string name; // name of person speaking
    [SerializeField] string line; 

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
}
