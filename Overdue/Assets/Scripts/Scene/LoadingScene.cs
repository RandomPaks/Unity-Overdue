using UnityEngine;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    private float ticks;
    [SerializeField] TMP_Text loadingText;

    void Update()
    {
        ticks += Time.deltaTime;
        if (ticks > 1.33f)
        {
            loadingText.text = "Loading...";
            ticks = 0.0f;
        }
        else if (ticks > 1.0f)
            loadingText.text = "Loading..";
        else if (ticks > 0.66f)
            loadingText.text = "Loading.";
        else if (ticks > 0.33f)
            loadingText.text = "Loading";
    }
}
