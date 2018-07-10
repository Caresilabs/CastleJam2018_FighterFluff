using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Transform Source;

    [SerializeField]
    private Vector3 Offset;

    public delegate void PreCullEvent(Transform camera);
    public static PreCullEvent onPreCull;

    void OnPreCull()
    {
        if (onPreCull != null)
        {
            onPreCull(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        var targetDir = (Target.position - Source.position).normalized;

        var pos = Source.position - targetDir * Offset.z;
        pos.y = Mathf.Max(pos.y + Offset.y, Source.position.y);
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.9f);

        transform.LookAt(Target.position);
    }

}
