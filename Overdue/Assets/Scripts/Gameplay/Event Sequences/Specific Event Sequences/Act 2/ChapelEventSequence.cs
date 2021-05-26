using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapelEventSequence : AEventSequence
{
    [SerializeField] GameObject spirit;
    [SerializeField] Vector3 spiritPosition;

    [SerializeField] GameObject robert;
    [SerializeField] GameObject alvaro;
    [SerializeField] Vector3 alvaroPosition;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    public override void PlayEvent()
    {
        this.spirit.SetActive(false);
        this.spirit.transform.position = this.spiritPosition;
        this.spirit.SetActive(true);

        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        CharacterController characterController = this.player.GetComponent<CharacterController>();
        characterController.enabled = false;
        //this.player.transform.rotation = Quaternion.LookRotation(this.spirit.transform.position, this.transform.up);
        this.player.m_MouseLook.Init(this.player.transform, Camera.main.transform);
        characterController.enabled = true;

        this.robert.transform.localPosition = Vector3.zero;
        this.alvaro.transform.localPosition = this.alvaroPosition;

        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
