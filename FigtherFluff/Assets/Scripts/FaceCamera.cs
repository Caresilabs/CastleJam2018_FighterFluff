using Assets.Scripts;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void Start()
    {
        CameraHolder.onPreCull += (x) => {
            Vector3 fwd = x.transform.forward;
            fwd.y = 0;
            transform.rotation = Quaternion.LookRotation(fwd);
        };
    }

}
