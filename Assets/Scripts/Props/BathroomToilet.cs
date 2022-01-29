using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BathroomToiletProp : BaseInteractiveProp
{
    private BaseInteraction goToToilet;
    private BaseInteraction drinkToiletWater;

    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }

    public BathroomToiletProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.BathroomToilet)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        goToToilet = new BaseInteraction(false);
        drinkToiletWater = new BaseInteraction(true);
    }

    public void UseToilet()
    {
        goToToilet.interact();
    }

    public void DrinkToiletWater()
    {
        drinkToiletWater.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

        if (drinkToiletWater.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (drinkToiletWater.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }
}


public class BathroomToilet : BaseInteractionComponent
{
    private BathroomToiletProp props;

    [Header("Open Bathroom Toilet Menu")]
    public UnityEvent OnOpenBathroomToiletMenu;

    [Header("Use Toilet")]
    public UnityEvent OnUseToilet;

    [Header("Drink Toilet water")]
    public UnityEvent OnDrinkToiletWater;
    void Start()
    {
        props = new BathroomToiletProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }
    public override void Interact_Stage1()
    {
        OnOpenBathroomToiletMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenBathroomToiletMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenBathroomToiletMenu.Invoke();
        props.BoundStage = Stage.Stage3;
    }

    public void UseToilet()
    {
        Debug.Log("UseToilet");
        props.UseToilet();
        OnUseToilet.Invoke();
    }

    public void DrinkToiletWater()
    {
        Debug.Log("DrinkToiletWater");
        props.DrinkToiletWater();
        TriggerCount = TriggerCount + 1;
        OnDrinkToiletWater.Invoke();
    }
}
