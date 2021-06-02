using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light light;
    [SerializeField] float minTime = 1f;
    [SerializeField] float maxTime = 5f;
    void Awake()
    {
        light = GetComponent<Light>();
    }

    void Start()
    {
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            light.enabled = !light.enabled;
        }
    }
}
