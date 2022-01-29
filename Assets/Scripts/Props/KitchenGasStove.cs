using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KitchenGasStoveProp : BaseInteractiveProp
{
    private BaseInteraction turnOn;
    private BaseInteraction turnOff;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public KitchenGasStoveProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.KitchenGasStove)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        turnOn = new BaseInteraction(true);
        turnOff = new BaseInteraction(true);
    }
    public void TurnOn()
    {
        turnOn.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

        if (turnOn.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (turnOn.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }

    public void TurnOff()
    {
        turnOff.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

        if (turnOff.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (turnOff.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }
}


public class KitchenGasStove : BaseInteractionComponent
{
    private KitchenGasStoveProp props;

    [Header("Open Kitchen Gas Stove Menu")]
    public UnityEvent OnOpenKitchenGasStoveMenu;
    void Start()
    {
        props = new KitchenGasStoveProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }

    public override void Interact_Stage1()
    {
        OnOpenKitchenGasStoveMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenKitchenGasStoveMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenKitchenGasStoveMenu.Invoke();
        props.BoundStage = Stage.Stage3;
    }



    public void TurnOff()
    {
        Debug.Log("TurnOff");
        props.TurnOff();
        TriggerCount = TriggerCount + 1;
    }

    public void TurnOn()
    {
        Debug.Log("TurnOn");
        props.TurnOn();
        TriggerCount = TriggerCount + 1;
    }
}
