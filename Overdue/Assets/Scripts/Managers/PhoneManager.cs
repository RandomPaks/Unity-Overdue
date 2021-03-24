using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance { get; private set; }

    private List<GameObject> inventory;

    bool isPhone = false;
    [SerializeField] GameObject phoneUI;
    [SerializeField] private GameObject itemButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventory = new List<GameObject>();
    }

    public void AddItem(GameObject item)
    {
        item.SetActive(false);
        item.transform.position = new Vector3(0, -100, 0);
        inventory.Add(item);

        GameObject button = Instantiate(itemButton, phoneUI.transform);
        button.GetComponentInChildren<Text>().text = item.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPhone = !isPhone;
            if (isPhone)
            {
                this.phoneUI.SetActive(true);
                GameManager.Instance.toggleCursorLock(false);
                GameManager.Instance.SetState(GameState.PAUSED);
            }
            else
            {
                this.phoneUI.SetActive(false);
                GameManager.Instance.toggleCursorLock(true);
                GameManager.Instance.SetState(GameState.GAME);
            }
        }
    }
}
