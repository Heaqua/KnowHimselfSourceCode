using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

using System.Collections;


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

    [Header("Laptop Parameters")]
    // jane's code
    public GameObject laptopPrefab;
    public GameObject laptopModel;
    public bool LaptopCamEnabled { get; private set; }

    [Header("Audio Parameters")]
    [SerializeField] BGMManager bgm;

    [Header("Events")]
    public UnityEvent OnNextStage;

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

    private void OnEnable()
    {
        if (bgm == null)
            bgm = BGMManager.instance;
        if (bgm == null)
            bgm = FindObjectOfType<BGMManager>();
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

        // Change BGM
        if (bgm != null)
            bgm.PlayMusic(CurrentStage);


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
            StartCoroutine(instantiateLaptop());
            
        }
    }

    IEnumerator instantiateLaptop()
    {
        yield return new WaitForSeconds(0.8f);
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
