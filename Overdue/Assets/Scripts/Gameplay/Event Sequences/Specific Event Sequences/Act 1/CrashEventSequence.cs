using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrashEventSequence : AEventSequence 
{
    [SerializeField] Texture2D blackTexture; 
    [SerializeField] Dialog dialog;
    [SerializeField] GameObject professorModel;
    [SerializeField] GameObject chaseEventBarriers;
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    public override void PlayEvent()
    {
        CGManager.Instance.UpdateCGFrame(this.blackTexture);
        CGManager.Instance.ShowCanvas(true);
        StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog, this.OnFinishEvent));
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.professorModel.SetActive(true);
        this.chaseEventBarriers.SetActive(false);
        CGManager.Instance.ShowCanvas(false);

        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        CharacterController characterController = this.player.GetComponent<CharacterController>();
        characterController.enabled = false;
        this.player.transform.rotation = Quaternion.LookRotation(this.professorModel.transform.position, this.transform.up);
        this.player.m_MouseLook.Init(this.player.transform, Camera.main.transform);
        this.player.enabled = true;
        characterController.enabled = true;

        this.gameObject.SetActive(false);
    }
}
