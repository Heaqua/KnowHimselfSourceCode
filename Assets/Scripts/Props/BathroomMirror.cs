using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BathroomMirrorProp : BaseInteractiveProp
{
    private BaseInteraction clearAway;
    private BaseInteraction killYourself;
    public BathroomMirrorProp() : base(InteractivePropsType.BathroomMirror)
    {
        clearAway = new BaseInteraction(false);
        killYourself = new BaseInteraction(true);
    }

    public void ClearAway()
    {
        clearAway.interact();
    }

    public void KillYourself()
    {
        killYourself.interact();
        int numOfTriggerToStage2 = Constraints.NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = Constraints.NumOfTriggeredChangeToStage3;

        if (killYourself.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (killYourself.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }
}


public class BathroomMirror : MonoBehaviour
{
    private BathroomMirrorProp props;

    [Header("Open Bathroom Mirror Menu")]
    public UnityEvent OnOpenBathroomMirrorMenu;
    void Start()
    {
        props = new BathroomMirrorProp();
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnOpenBathroomMirrorMenu.Invoke");
        OnOpenBathroomMirrorMenu.Invoke();
    }

    public void ClearAway()
    {
        Debug.Log("ClearAway");
        props.ClearAway();
    }

    public void KillYourself()
    {
        Debug.Log("KillYourself");
        props.KillYourself();
    }
}
