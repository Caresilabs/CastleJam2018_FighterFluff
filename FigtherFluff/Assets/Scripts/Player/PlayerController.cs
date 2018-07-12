using Assets.Scripts;
using Assets.Scripts.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //public PlayerType PlayerType;

    [SerializeField]
    public float MaxHealth = 100;

    [SerializeField]
    public ParticleSystem SweatParticles;

    public float Health { get; private set; }

    public Rigidbody RigidBody { get; private set; }
    public MovementController Movement { get; private set; }

    public CameraController PlayerCamera { get; set; }

    public InputLayout Input { get; internal set; }

    private float cooldown;

    protected virtual void Start()
    {
        this.Health = MaxHealth;
        this.RigidBody = GetComponent<Rigidbody>();
        this.Movement = GetComponent<MovementController>();
    }

    protected virtual void Update()
    {
        Vector3 fwd = (GameManager.Instance.Player1 == this ? GameManager.Instance.Player2.transform.position : GameManager.Instance.Player1.transform.position) - transform.position;
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);

        cooldown -= Time.deltaTime;

        #pragma warning disable CS0618 // Type or member is obsolete
        SweatParticles.emissionRate = (1 - Movement.SpeedScale) * 6;
        #pragma warning restore CS0618 // Type or member is obsolete
    }

    public virtual void Damage(Transform source, float damage, float stunTime, float knockback, float knockbackHeight = 0)
    {
        Health -= damage;

        var dir = (transform.position - source.position);
        dir.y = knockbackHeight;
        dir.Normalize();

        if (IsDead())
        {
            Invoke("Kill", 1.2f);

            RigidBody.AddForce(dir * knockback * 50, ForceMode.Impulse);
            GameManager.Instance.Hitstun(1);

            GameManager.Instance.Player1.PlayerCamera.Shake(1, 0.7f);
            GameManager.Instance.Player2.PlayerCamera.Shake(1, 0.7f);
            return;
        }

        if (stunTime > 0)
            GameManager.Instance.Hitstun(stunTime);

        RigidBody.AddForce(dir * knockback * 25, ForceMode.Impulse);
    }

    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public bool HasCooldown() { return cooldown > 0; }

    public void SetCooldown(float time)
    {
        this.cooldown = time;
    }

    public bool IsKeys(params InputLayout.ActionType[] keys)
    {
        if (keys == null) return false;

        foreach (var item in keys)
        {
            if (!Input.IsButtonDown(item))
                return false;
            // if (!Input.GetButton(GetKeyName(item)))
            //     return false;
        }
        return true;
    }

    public bool IsAxis(InputLayout.ActionType axis, bool inverted)
    {
        var val = Input.GetAxis(axis);
        return inverted ? val < -0.2f : val > 0.2f;

        // var val = Input.GetAxis(GetKeyName(axis));
        // return inverted ? val < -0.2f : val > 0.2f;
    }

}
