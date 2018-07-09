using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Transform Source;

    void Awake()
    {
    }

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

        var pos = Source.position - targetDir * 5;
        pos.y += 2;
        transform.position = pos;
        transform.LookAt(Target.position);
    }

}
