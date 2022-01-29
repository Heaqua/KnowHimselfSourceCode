using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BathroomBrokenMirrorProp : BaseInteractiveProp
{
    private BaseInteraction clearAway;
    private BaseInteraction killYourself;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public BathroomBrokenMirrorProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.BathroomMirrorBrokenPieces)
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


public class BathroomBrokenMirror : BaseInteractionComponent
{
    private BathroomBrokenMirrorProp props;

    [Header("Open Bathroom Broken Mirror Menu")]
    public UnityEvent OnOpenBathroomBrokenMirrorMenu;
    void Start()
    {
        props = new BathroomBrokenMirrorProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }
    public override void Interact_Stage1()
    {
        OnOpenBathroomBrokenMirrorMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenBathroomBrokenMirrorMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenBathroomBrokenMirrorMenu.Invoke();
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
