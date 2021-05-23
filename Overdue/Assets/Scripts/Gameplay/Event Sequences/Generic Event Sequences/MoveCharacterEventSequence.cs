using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCharacterEventSequence : AEventSequence
{
    [SerializeField] GameObject characterToMove;
    [SerializeField] Vector3 newPosition;
    [SerializeField] Quaternion newRotation;

    public override void PlayEvent()
    {
        this.characterToMove.transform.localPosition = this.newPosition;
        this.characterToMove.transform.rotation = this.newRotation;
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
