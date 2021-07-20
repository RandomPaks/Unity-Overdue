using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrierEventSequence : AEventSequence
{
    [SerializeField] List<GameObject> barriers;
    [SerializeField] bool isActive;

    public override void PlayEvent()
    {
        for (int i = 0; i < this.barriers.Count; i++)
        {
            barriers[i].SetActive(isActive);
        }
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
