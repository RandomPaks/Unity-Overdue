using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvaroEventSequence : AEventSequence
{
    [SerializeField] GameObject alvaroModel;
    [SerializeField] GameObject alvaroLight;
    [SerializeField] GameObject spirit;
    [SerializeField] GameObject eventBarriers;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    public override void PlayEvent()
    {
        this.alvaroModel.SetActive(true);
        this.alvaroLight.SetActive(false);
        this.spirit.SetActive(false);
        this.eventBarriers.SetActive(false);

        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        CharacterController characterController = this.player.GetComponent<CharacterController>();
        characterController.enabled = false;
        //this.player.transform.rotation = Quaternion.LookRotation(this.alvaroModel.transform.position, this.transform.up);
        this.player.m_MouseLook.Init(this.player.transform, Camera.main.transform);
        //this.player.enabled = true;
        characterController.enabled = true;

        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
