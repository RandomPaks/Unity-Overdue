using UnityEngine;

public class DialogEventSequence : AEventSequence
{
    [SerializeField] Dialog dialog; 

    public override void PlayEvent()
    {
        this.ShowDialog();
    }

    public override void OnFinishEvent()
    {
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }

    void ShowDialog()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog, this.OnFinishEvent));
    }
}