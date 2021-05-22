using UnityEngine;
using TMPro;

public class VisualCue : MonoBehaviour
{
    //adjust the z position of the image for axis rotation
    //make sure that the parent is at uniform scale (1) or skewing will occur

    [SerializeField, Min(0)] float visualCueScale = 0.05f;
    [SerializeField, Min(0)] float maxDistance = 2.5f;
    SpriteRenderer image;
    TMP_Text text;

    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.SetText(gameObject.transform.parent.name);
        image = GetComponentInChildren<SpriteRenderer>();

        gameObject.transform.localScale = new Vector3(
            visualCueScale / gameObject.transform.parent.localScale.x,
            visualCueScale / gameObject.transform.parent.localScale.y,
            visualCueScale / gameObject.transform.parent.localScale.z);
    }

    void Update()
    {
        if(GameManager.Instance.Player.GetComponentInChildren<Camera>() != null)
        {
            float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.GetComponentInChildren<Camera>().gameObject.transform.position);
            Color color = image.color;

            if (distance > maxDistance)
            {
                color.a = 0f;
            }
            else
            {
                Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3.0f);

                color.a = 1f;
            }
            image.color = color;
            text.color = color;
        }
    }
}
