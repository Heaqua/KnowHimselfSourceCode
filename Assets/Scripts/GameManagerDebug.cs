using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GameManager))]

public class GameManagerDebug : MonoBehaviour
{
    [Header("Camera Debug - Switch to Laptop")]
    public InputAction CamLaptop;
    [Header("Camera Debug - Toggle Stages")]
    public InputAction StageToggle;

    private void Awake()
    {
        if (!Application.isEditor)
            this.enabled = false;
    }

    private void Start()
    {
        StageToggle.performed += a => GameManager.instance.GotoNextStage();
        CamLaptop.performed += a => GameManager.instance.ToggleLaptopCam();
    }

    private void OnEnable()
    {
        StageToggle.Enable();
        CamLaptop.Enable();
    }

    private void OnDisable()
    {
        StageToggle.Disable();
        CamLaptop.Disable();
    }
}
