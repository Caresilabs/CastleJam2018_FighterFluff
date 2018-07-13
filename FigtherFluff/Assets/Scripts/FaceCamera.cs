using Assets.Scripts;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void Start()
    {
        CameraHolder.onPreCull += OnCameraRender;
    }

    private void OnCameraRender(Transform x)
    {
        Vector3 fwd = x.forward;
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);
    }

    private void OnDestroy()
    {
        CameraHolder.onPreCull -= OnCameraRender;
    }

}
