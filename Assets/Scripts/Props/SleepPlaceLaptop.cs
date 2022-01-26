using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SleepPlaceLaptopProp : BaseInteractiveProp
{
    private BaseInteraction checkTime;
    private BaseInteraction checkNews;
    public SleepPlaceLaptopProp() : base(InteractivePropsType.SleepPlaceLaptop)
    {
        checkNews = new BaseInteraction(false);
        checkTime = new BaseInteraction(true);
    }
    public void CheckTheTime()
    {
        checkTime.interact();
        int numOfTriggerToStage2 = Constraints.NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = Constraints.NumOfTriggeredChangeToStage3;

        if (checkTime.getNumberOfTriggered() == numOfTriggerToStage2 &&
            (BoundStage == Stage.Stage2 || BoundStage == Stage.Stage3))
        {
            CurrStage = Stage.Stage2;
        }
        else if (checkTime.getNumberOfTriggered() == numOfTriggerToStage3 &&
            BoundStage == Stage.Stage3)
        {
            CurrStage = Stage.Stage3;
        }
    }

    public void CheckNews()
    {
        checkNews.interact();
    }
}


public class SleepPlaceLaptop : MonoBehaviour, IPointerEnterHandler
{
    private SleepPlaceLaptopProp props;

    [Header("Open Sleep Place Laptop Menu")]
    public UnityEvent OnOpenSleepPlaceLaptopMenu;
    void Start()
    {
        props = new SleepPlaceLaptopProp();
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnOpenSleepPlaceLaptopMenu.Invoke");
        OnOpenSleepPlaceLaptopMenu.Invoke();
    }

    public void CheckNews()
    {
        Debug.Log("CheckNews");
        props.CheckNews();
    }

    public void CheckTheTime()
    {
        Debug.Log("CheckTheTime");
        props.CheckTheTime();
    }
}
