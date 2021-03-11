using UnityEngine;

[DisallowMultipleComponent]
public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Color hoveringColor = Color.green;
    
    private Material mainMaterial = null;

    private Color originalColor = Color.white;

    private void Awake()
    {
        mainMaterial = GetComponentInChildren<Renderer>().material;
    }

    void Start()
    {
        originalColor = mainMaterial.color;
    }

    public void StartHover()
    {
        mainMaterial.color = hoveringColor;
    }

    public void Interact()
    {
        Debug.Log("I got interacted! O__o");
    }

    public void StopHover()
    {
        mainMaterial.color = originalColor;
    }
}
