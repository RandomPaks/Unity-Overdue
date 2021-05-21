using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGEventSequence : AEventSequence
{
    [SerializeField] CG cg;

    public override void PlayEvent()
    {
        this.ShowCG();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }

    void ShowCG()
    {
        StartCoroutine(CGManager.Instance.ShowCG(this.cg, this.cg.Delay, this.OnFinishEvent));
    }
}
