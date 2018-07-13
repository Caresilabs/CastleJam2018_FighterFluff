using Assets.Scripts;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform Target { get; set; }

    public Transform Source { get; set; }

    [SerializeField]
    private Vector3 Offset;

    private CameraHolder CameraHolder;

    private void Start()
    {
        this.CameraHolder = GetComponentInChildren<CameraHolder>();
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(CameraHolder.ShaketCoroutine(duration, magnitude));
    }

    private void LateUpdate()
    {
        var targetDir = (Target.position - Source.position).normalized;

        var pos = Source.position - targetDir * Offset.z;
        pos.y = Mathf.Max(pos.y + Offset.y, Source.position.y);
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.95f);

        transform.LookAt(Target.position);
    }

}
