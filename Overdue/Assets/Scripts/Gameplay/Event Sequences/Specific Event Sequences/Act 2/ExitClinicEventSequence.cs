using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitClinicEventSequence : AEventSequence
{
    [SerializeField] GameObject spirit;
    [SerializeField] Vector3 spiritPosition;

    [SerializeField] GameObject alvaroLight;
    [SerializeField] GameObject alvaro; 
    [SerializeField] GameObject eventBarriers;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    

    public override void PlayEvent()
    {
        this.spirit.transform.position = this.spiritPosition;
        this.spirit.SetActive(true);

        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        CharacterController characterController = this.player.GetComponent<CharacterController>();
        characterController.enabled = false;
        this.player.transform.rotation = Quaternion.LookRotation(this.spirit.transform.position, this.transform.up); 
        this.player.m_MouseLook.Init(this.player.transform, Camera.main.transform);
        //this.player.enabled = true;
        characterController.enabled = true;

        this.alvaroLight.SetActive(true);
        this.alvaro.SetActive(true);
        this.eventBarriers.SetActive(true);

        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
