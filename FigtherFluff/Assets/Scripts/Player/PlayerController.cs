using Assets.Scripts.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public PlayerType PlayerType;

    [SerializeField]
    public float MaxHealth = 100;

    public float Health { get; private set; }

    public Rigidbody RigidBody { get; private set; }
    public MovementController Movement { get; private set; }

    public CameraController PlayerCamera { get; set; }

    private string inputPrefix;

    private float cooldown;

    protected virtual void Start()
    {
        this.Health = MaxHealth;
        this.RigidBody = GetComponent<Rigidbody>();
        this.Movement = GetComponent<MovementController>();
        this.inputPrefix = GetComponent<PlayerController>().PlayerType == PlayerType.PLAYER1 ? "P1_" : "P2_";
    }

    protected virtual void Update()
    {
        Vector3 fwd = (GameManager.Instance.Player1 == this ? GameManager.Instance.Player2.transform.position : GameManager.Instance.Player1.transform.position) - transform.position;
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);

        cooldown -= Time.deltaTime;
    }

    public void Damage(Transform source, float damage, float stunTime, float knockback, float knockbackHeight = 0)
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

    public bool IsKeys(params string[] keys)
    {
        if (keys == null) return false;

        foreach (var item in keys)
        {
            if (!Input.GetButton(GetKeyName(item)))
                return false;
        }
        return true;
    }

    public bool IsAxis(string axis, bool inverted)
    {
        var val = Input.GetAxis(GetKeyName(axis));
        return inverted ? val < -0.2f : val > 0.2f;
    }

    private string GetKeyName(string key)
    {
        return inputPrefix + key;
    }
}
