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
    public BathroomToiletProp() : base(InteractivePropsType.BathroomToilet)
    {
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
        int numOfTriggerToStage2 = Constraints.NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = Constraints.NumOfTriggeredChangeToStage3;

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


public class BathroomToilet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BathroomToiletProp props;

    [Header("Open Bathroom Toilet Menu")]
    public UnityEvent OnOpenBathroomToiletMenu;

    [Header("Close Bathroom Toilet Menu")]
    public UnityEvent OnCloseBathroomToiletMenu;

    void Start()
    {
        props = new BathroomToiletProp();
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnOpenBathroomToiletMenu.Invoke");
        OnOpenBathroomToiletMenu.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnCloseBathroomToiletMenu.Invoke");
        OnCloseBathroomToiletMenu.Invoke();
    }

    public void UseToilet()
    {
        Debug.Log("UseToilet");
        props.UseToilet();
    }

    public void DrinkToiletWater()
    {
        Debug.Log("UseToilet");
        props.DrinkToiletWater();
    }
}
