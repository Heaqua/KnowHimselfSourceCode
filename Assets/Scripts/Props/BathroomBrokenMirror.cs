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
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public BathroomMirrorProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.BathroomMirror)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
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
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

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


public class BathroomMirror : BaseInteractionComponent
{
    private BathroomMirrorProp props;

    [Header("Open Bathroom Mirror Menu")]
    public UnityEvent OnOpenBathroomMirrorMenu;
    void Start()
    {
        props = new BathroomMirrorProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }
    public override void Interact_Stage1()
    {
        OnOpenBathroomMirrorMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenBathroomMirrorMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenBathroomMirrorMenu.Invoke();
        props.BoundStage = Stage.Stage3;
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
        TriggerCount = TriggerCount + 1;
    }
}
