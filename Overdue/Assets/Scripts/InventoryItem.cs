using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryItem : MonoBehaviour
{
    Transform rotationAnchorTransform = null;
    
    bool isBeingInspected = false;
    bool canRotateItem = false;
    
    void Update()
    {
        RotateIfBeingInspected();
    }

    void RotateIfBeingInspected()
    {
        if (!canRotateItem)
            return;

        if (!Input.GetKey(KeyCode.Mouse0))
            return;
        
        float xRotInput = Input.GetAxis("Mouse X");
        float yRotInput = Input.GetAxis("Mouse Y");
        float xRot = xRotInput * 250f;
        float yRot = yRotInput * 250f;

        gameObject.transform.Rotate(rotationAnchorTransform.up, -xRot * Time.deltaTime, Space.World);
        gameObject.transform.Rotate(rotationAnchorTransform.right, yRot * Time.deltaTime, Space.World);
    }

    public void Initialize(Transform newRotationAnchorTransform)
    {
        rotationAnchorTransform = newRotationAnchorTransform;
    }
    
    public void StartInspect()
    {
        if (isBeingInspected)
        {
            Debug.LogError($"Tried to inspect {this} but it is already being inspected!");
            return;
        }
        
        isBeingInspected = true;
        canRotateItem = true;

        DOTween.Kill(transform);
        transform.localScale = Vector3.zero;
        var tween = transform.DOScale(Vector3.one, 0.5f);

        gameObject.SetActive(true);
    }
    

    public void StopInspect()
    {
        if (!isBeingInspected)
        {
            Debug.LogError($"Tried to stop inspecting {this} but it is already not being inspected!");
            return;
        }
        
        isBeingInspected = false;
        canRotateItem = false;

        DOTween.Kill(transform);
        transform.localScale = Vector3.one;
        var tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.onComplete += onStopInspectAnimationComplete;
    }

    void onStopInspectAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
