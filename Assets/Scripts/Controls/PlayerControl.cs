using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerControl : MonoBehaviour
{
    [Header("Player Parameters")]
    [Range(0f, 10f)] public float MoveSpeed = 2f;
    [Range(0f, 5f)] public float RotateSpeed = 1f;

    [Header("Camera Parameters")]
    public GameObject CamTarget;
    [Range(0f, 90f)] public float RotateClampTop = 90f;
    [Range(0f, 90f)] public float RotateClampBottom = 90f;

    public float CurrentSpeed { get; private set; } = 0f;
    float CamPitch;

    PlayerMoveInput Input;
    PlayerMoveAnimator Animator;
    CharacterController Player;

    private void Awake()
    {
        Player = GetComponent<CharacterController>();
        Input = GetComponent<PlayerMoveInput>();
        Animator = GetComponent<PlayerMoveAnimator>();
        CurrentSpeed = 0f;
        CamPitch = 0f;
    }

    private void Update()
    {
        if (Player == null)
            return;

        if (Input == null)
            return;

        if (Input.MoveInput == Vector2.zero)
            CurrentSpeed = 0f;

        Vector3 InputDir = new Vector3(Input.MoveInput.x, 0f, Input.MoveInput.y).normalized;
        if (Input.MoveInput != Vector2.zero)
            InputDir = transform.right * Input.MoveInput.x + transform.forward * Input.MoveInput.y;

        Player.Move(InputDir.normalized * (MoveSpeed * Time.deltaTime));

        if (Animator == null)
            return;

        Animator.UpdateMovement(0f, Input.MoveInput.y * MoveSpeed);
    }

    private void LateUpdate()
    {
        if (Player == null)
            return;

        if (Input == null)
            return;

        if (Input.LookInput.sqrMagnitude >= 0.01f)
        {
            if (CamTarget != null)
            {
                CamPitch += Input.LookInput.y * RotateSpeed * Time.deltaTime;
                CamPitch = ClampAngle(CamPitch, -RotateClampBottom, RotateClampTop);
                CamTarget.transform.localRotation = Quaternion.Euler(CamPitch, 0f, 0f);
            }            
            transform.Rotate(Vector3.up * Input.LookInput.x * RotateSpeed * Time.deltaTime);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
