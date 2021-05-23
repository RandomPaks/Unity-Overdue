using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneEventSequence : AEventSequence
{
    [SerializeField] Camera cutsceneCamera;
    private Animator cameraAnimator;

    [SerializeField] Camera playerCamera;

    [SerializeField] string triggerName;
    [SerializeField] float animationTime; 

    // Start is called before the first frame update
    void Start()
    {
        this.cameraAnimator = this.cutsceneCamera.GetComponent<Animator>();
    }

    public override void PlayEvent()
    {
        this.cutsceneCamera.gameObject.SetActive(true);
        this.cameraAnimator.SetTrigger(this.triggerName);

        GameManager.Instance.SetState(GameState.CUTSCENE);
        //this.cutsceneCamera.gameObject.SetActive(true);
        this.playerCamera.gameObject.SetActive(false);

        StartCoroutine(this.FinishCutscene());
    }

    IEnumerator FinishCutscene()
    {
        yield return new WaitForSeconds(this.animationTime);
        this.cameraAnimator.ResetTrigger(this.triggerName);
        this.playerCamera.gameObject.SetActive(true);
        
        base.OnFinishEvent();

        this.cutsceneCamera.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        GameManager.Instance.SetState(GameState.GAME);
    }
}
