using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour, IInteractable
{
    [SerializeField] Dialog dialog;

    [SerializeField] private Color hoveringColor = Color.red;
    private Material mainMaterial = null;

    private Color originalColor = Color.white;

    void Awake()
    {
        mainMaterial = GetComponentInChildren<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalColor = mainMaterial.color;
    }

    void IInteractable.Interact()
    {
        //CGDialogs.Instance.BeginDialog();
    }

    void IInteractable.StartHover()
    {
        mainMaterial.color = hoveringColor;
    }

    void IInteractable.StopHover()
    {
        mainMaterial.color = originalColor;
    }
}
