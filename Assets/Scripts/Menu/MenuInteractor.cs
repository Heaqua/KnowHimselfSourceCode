using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInteractor : MonoBehaviour
{
    public LayerMask InteractLayers;

    public Camera cam;

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            if (cam == null)
                cam = GameManager.instance.MainCamera;
        }
        if (cam == null)
            cam = Camera.main;
        if (cam == null)
            cam = GetComponent<Camera>();
        if (cam == null)
            cam = FindObjectOfType<Camera>();
    }

    public void OnInteract(InputValue value)
    {
        if (cam == null)
        {
            Debug.LogWarning("No camera has been assigned, skipping!");
            return;
        }

        RaycastHit RayHit;
        Ray CamRay = cam.ScreenPointToRay(Pointer.current.position.ReadValue());

        if (Physics.Raycast(CamRay, out RayHit))
        {
            if (((1 << RayHit.collider.gameObject.layer) & InteractLayers) != 0)
            {
                Debug.LogFormat("\"{0}\" is being interacted!", RayHit.collider.gameObject.name);
            }
        }
    }

}
