﻿using System;
using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float MAX_JUMP_TIME_DELAY = 0.2f;

    [SerializeField]
    private float Speed;

    private Rigidbody RigidBody;
    private CapsuleCollider capsuleCollider;
    private PlayerController controller;

    public bool Grounded { get; private set; }
    public bool CanMove { get; private set; }

    private string inputPrefix;
    private bool canJump;
    private float allowJumpDelay;

    private Vector3 groundNormal;
    private float fallMultiplier = 1.5f;
    private float lowJumpMultiplier = 1.5f;

    public delegate void OnJump();
    public OnJump onJump;

    // Use this for initialization
    void Start()
    {
        this.controller = GetComponent<PlayerController>();
        this.inputPrefix = controller.PlayerType == PlayerType.PLAYER1 ? "P1_" : "P2_";
        this.RigidBody = GetComponent<Rigidbody>();
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.Grounded = true;
        this.canJump = true;
        this.CanMove = true;
    }

    void FixedUpdate()
    {
        UpdateMovement();
        Grounded = false;
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit)) //, 1.3f); 
        {
            if (hit.distance <= capsuleCollider.height / 2f + 0.2f)
            {
                if (hit.collider != null && hit.collider.GetComponent<PlayerController>() == null)
                {
                    groundNormal = hit.normal;
                    if (groundNormal.y < 0.15f) // Disable slope jumping
                        return;

                    Grounded = true;
                    allowJumpDelay = 0;
                }
            }
            //// Update shadow distance
            //if (Projector != null)
            //{
            //    Projector.farClipPlane = Projector.nearClipPlane + hit.distance - 0.5f;
            //    var mr = MeshRenderer.transform.position;
            //    Projector.transform.position = new Vector3(mr.x, Projector.transform.position.y, mr.z);
            //}
        }
        else
        {

        }
    }

    private void UpdateMovement()
    {
        Vector3 velocity = RigidBody.velocity;

        var isJumpPressed = Input.GetButton(GetKeyName("Jump"));

        if (RigidBody.useGravity)
        {
            if (velocity.y < 0)
            {
                RigidBody.velocity += Physics.gravity * (fallMultiplier) * Time.fixedDeltaTime;
            }
            else if (!isJumpPressed && velocity.y > 0)
            {
                RigidBody.velocity += Physics.gravity * (lowJumpMultiplier) * Time.fixedDeltaTime;
            }
        }

        if (!CanMove)
            return;

        Vector3 targetVelocity = new Vector3(Input.GetAxis(GetKeyName("Horizontal")), 0, Input.GetAxis(GetKeyName("Vertical")));
        if (targetVelocity.magnitude >= 1)
            targetVelocity.Normalize();

        targetVelocity = controller.PlayerCamera.transform.TransformDirection(targetVelocity);
        targetVelocity.y = 0;
        if (targetVelocity.magnitude > 0.05f)//(PlayerCamera.rotation.eulerAngles.x > 75)
            targetVelocity.Normalize();

        targetVelocity *= Speed;

        // Jump
        allowJumpDelay += Time.fixedDeltaTime;
        if (canJump && (Grounded || allowJumpDelay < MAX_JUMP_TIME_DELAY))
        {
            if (isJumpPressed)
            {
                if (onJump != null)
                {
                    Grounded = false;
                    canJump = false;
                    onJump();
                }
            }
               // canJump = false;
               // RigidBody.velocity = new Vector3(velocity.x, Mathf.Sqrt(2 * JumpHeight * Gravity), velocity.z);
        }

        //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKey(kcode))
        //        Debug.Log("KeyCode down: " + kcode);
        //}

        float maxVelocityChange = 1.0f;
        if (Grounded)
        {
            // Apply a force that attempts to reach our target velocity
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            RigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 velocityChange;
            if (targetVelocity.magnitude <= Mathf.Epsilon)
            {
                velocityChange = Vector3.zero;
            }
            else
            {
                velocityChange = (targetVelocity - velocity);
            }

            float mx = groundNormal.y * 1.7f * 0.3f;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -mx, mx);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -mx, mx);
            velocityChange.y = 0;

            RigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.name == name) // Nasty bug causes oncollsion enter on itself
            return;

        canJump = true;
    }

    void OnCollisionStay(Collision hit)
    {
        if (hit.gameObject == GameManager.Instance.Player1.gameObject || hit.gameObject == GameManager.Instance.Player2.gameObject) // Nasty bug causes oncollsion enter on itself
            return;

        canJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }

    public void LockMovement(float duration)
    {
        StartCoroutine(LockMovementCoroutine(duration));
    }

    private IEnumerator LockMovementCoroutine(float duration)
    {
        CanMove = false;
        yield return new WaitForSeconds(duration);
        CanMove = true;
    }

    public void LockGravity(float duration)
    {
        StartCoroutine(LockGravityCoroutine(duration));
    }

    private IEnumerator LockGravityCoroutine(float duration)
    {
        RigidBody.useGravity = false;
        yield return new WaitForSeconds(duration);
        RigidBody.useGravity = true;
    }

    private string GetKeyName(string key)
    {
        return inputPrefix + key;
    }
}
