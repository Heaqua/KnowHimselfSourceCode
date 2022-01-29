using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Parameters")]
    public Commons.Stage CurrentStage;

    [Header("Camera Parameters")]
    public Camera MainCamera;
    public Animator CinemachineStateController;
    public string AnimatorClipFirstPerson = "FirstPerson";
    int AnimatorHashFirstPerson;
    public string AnimatorClipThirdPerson = "ThirdPerson";
    int AnimatorHashThirdPerson;
    public string AnimatorClipLaptop = "Laptop";
    int AnimatorHashLaptop;

    [Header("Events")]
    public UnityEvent OnNextStage;

    public bool LaptopCamEnabled { get; private set; }
    // jane's code
    public GameObject laptopPrefab;
    public GameObject laptopModel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        AnimatorHashFirstPerson = Animator.StringToHash(AnimatorClipFirstPerson);
        AnimatorHashThirdPerson = Animator.StringToHash(AnimatorClipThirdPerson);
        AnimatorHashLaptop = Animator.StringToHash(AnimatorClipLaptop);
    }

    public void GotoNextStage()
    {
        // Change Global Stage
        if (CurrentStage == Commons.Stage.Stage3)
            CurrentStage = Commons.Stage.Stage1;
        else
            CurrentStage++;

        // Change Camera
        if (CinemachineStateController != null)
        {
            if (CurrentStage == Commons.Stage.Stage3)
                CinemachineStateController.Play(AnimatorHashThirdPerson);
            else
                CinemachineStateController.Play(AnimatorHashFirstPerson);
        }


        OnNextStage.Invoke();
    }

    public void EnableLaptopCam()
    {
        if (CinemachineStateController != null)
        {
            CinemachineStateController.Play(AnimatorHashLaptop);
        }
        LaptopCamEnabled = true;
    }
    
    public void DisableLaptopCam()
    {
        if (CinemachineStateController != null)
        {
            if (CurrentStage == Commons.Stage.Stage3)
                CinemachineStateController.Play(AnimatorHashThirdPerson);
            else
                CinemachineStateController.Play(AnimatorHashFirstPerson);
        }
        LaptopCamEnabled = false;
    }

    public void ToggleLaptopCam()
    {
        if (LaptopCamEnabled)
        {
            DisableLaptopCam();
            Destroy(laptopPrefab);
        }
        
        else
        {
            // Jane's code
            EnableLaptopCam();
            instantiateLaptop();
            
        }
    }

    void instantiateLaptop()
    {
        new WaitForSeconds(1);
        laptopPrefab=Instantiate(laptopPrefab, laptopModel.transform.position, quaternion.identity);
    }
    public void ToggleLaptopCam(bool NewCamState)
    {
        if (NewCamState)
            EnableLaptopCam();
        else
            DisableLaptopCam();
    }
}
