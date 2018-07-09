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
        pos.y = Mathf.Max(pos.y + 2, Source.position.y);
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.9f);

        transform.LookAt(Target.position);
    }

}
