using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SleepPlaceBedProp : BaseInteractiveProp
{
    private BaseInteraction getUp;
    private BaseInteraction layDown;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public SleepPlaceBedProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.SleepPlaceBed)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        getUp = new BaseInteraction(true);
        layDown = new BaseInteraction(true);
    }
    public void GetUp()
    {
        getUp.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

        if (getUp.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (getUp.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }

    public void LayDown()
    {
        layDown.interact();
    }
}


public class SleepPlaceBed : BaseInteractionComponent
{
    private SleepPlaceBedProp props;

    [Header("Open Sleep Place Bed Menu")]
    public UnityEvent OnOpenSleepPlaceBedMenu;
    void Start()
    {
        props = new SleepPlaceBedProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }

    public override void Interact_Stage1()
    {
        OnOpenSleepPlaceBedMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenSleepPlaceBedMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenSleepPlaceBedMenu.Invoke();
        props.BoundStage = Stage.Stage3;
    }



    public void LayDown()
    {
        Debug.Log("LayDown");
        props.LayDown();
    }

    public void GetUp()
    {
        Debug.Log("GetUp");
        props.GetUp();
        TriggerCount = TriggerCount + 1;
    }
}
