using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class OpenMenuButton : MonoBehaviour
{
    [SerializeField] Menu menuToClose = null;
    [SerializeField] Menu menuToOpen = null;
    
    Button button = null;
    
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        menuToClose.Hide();
        menuToOpen.Show();
    }
}
