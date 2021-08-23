using UnityEngine;
using TMPro;

public class DialogEventSequence : AEventSequence
{
    [SerializeField] TMP_Text mapText;
    [SerializeField] string objectiveText;

    [SerializeField] Dialog dialog;

    public override void PlayEvent()
    {
        this.ShowDialog();
    }

    public override void OnFinishEvent()
    {
        if (mapText != null) mapText.text = objectiveText;
        base.OnFinishEvent();
        this.gameObject.SetActive(false);
    }

    void ShowDialog()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(this.dialog, false, this.OnFinishEvent));
    }
}