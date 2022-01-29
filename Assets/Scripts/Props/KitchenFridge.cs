using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KitchenFridgeProp : BaseInteractiveProp
{
    private BaseInteraction eat;
    private BaseInteraction drink;
    private BaseInteraction closeTheFridge;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public KitchenFridgeProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.KitchenFridge)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        drink = new BaseInteraction(false);
        closeTheFridge = new BaseInteraction(false);
        eat = new BaseInteraction(false);
    }
    public void Eat()
    {
        eat.interact();
    }

    public void Drink()
    {
        drink.interact();
    }

    public void CloseTheFridge()
    {
        closeTheFridge.interact();
    }
}


public class KitchenFridge : BaseInteractionComponent
{
    private KitchenFridgeProp props;

    [Header("Open Kitchen Fridge Menu")]
    public UnityEvent OnOpenKitchenFridgeMenu;
    void Start()
    {
        props = new KitchenFridgeProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }

    public override void Interact_Stage1()
    {
        OnOpenKitchenFridgeMenu.Invoke();
        props.BoundStage = Stage.Stage1;
    }

    public override void Interact_Stage2()
    {
        OnOpenKitchenFridgeMenu.Invoke();
        props.BoundStage = Stage.Stage2;
    }

    public override void Interact_Stage3()
    {
        OnOpenKitchenFridgeMenu.Invoke();
        props.BoundStage = Stage.Stage3;
    }



    public void Eat()
    {
        Debug.Log("Eat");
        props.Eat();
    }

    public void Drink()
    {
        Debug.Log("Drink");
        props.Drink();
    }

    public void CloseTheFridge()
    {
        Debug.Log("CloseTheFridge");
        props.CloseTheFridge();
    }
}
