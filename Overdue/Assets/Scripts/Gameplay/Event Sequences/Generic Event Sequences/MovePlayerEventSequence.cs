using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayerEventSequence : AEventSequence
{
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    [SerializeField] Vector3 newPosition;
    [SerializeField] Quaternion newRotation;
    [SerializeField] Texture2D blackTexture;

    public override void PlayEvent()
    {
        StartCoroutine(this.BlackoutScreen());
    }

    IEnumerator BlackoutScreen()
    {
        CGManager.Instance.UpdateCGFrame(this.blackTexture);
        CGManager.Instance.ShowCanvas(true);

        GameManager.Instance.SetState(GameState.OTHER);

        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        CharacterController characterController = this.player.gameObject.GetComponent<CharacterController>();
        characterController.enabled = false; 

        yield return new WaitForSeconds(2.0f);

        this.player.gameObject.transform.position = this.newPosition;
        this.player.gameObject.transform.rotation = this.newRotation;
        this.player.m_MouseLook.Init(this.player.transform, Camera.main.transform);
        characterController.enabled = true;

        CGManager.Instance.ShowCanvas(false);
        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        GameManager.Instance.SetState(GameState.GAME);
        this.gameObject.SetActive(false);
    }
}
