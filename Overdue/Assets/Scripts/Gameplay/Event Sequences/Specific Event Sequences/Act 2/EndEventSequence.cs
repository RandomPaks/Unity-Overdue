using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEventSequence : AEventSequence
{
    [SerializeField] GameObject spirit;
    [SerializeField] GameObject alvaro; 

    public override void PlayEvent()
    {
        this.spirit.SetActive(false);
        this.alvaro.transform.position = Vector3.zero;

        GameManager.Instance.toggleCursorLock(false);
        SceneManager.UnloadSceneAsync("Game Scene");
        SceneManager.LoadSceneAsync("End Game Scene");

        this.OnFinishEvent();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }
}
