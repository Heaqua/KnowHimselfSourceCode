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
    private BaseInteraction playGame;
    private BaseInteraction turnOff;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public SleepPlaceLaptopProp(int TriggerCountTargetStage2, int TriggerCountTargetStage3) : base(InteractivePropsType.SleepPlaceLaptop)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        checkNews = new BaseInteraction(false);
        checkTime = new BaseInteraction(true);
        playGame = new BaseInteraction(true);
        turnOff = new BaseInteraction(false);
    }
    public void CheckTheTime()
    {
        checkTime.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

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

    public void PlayTheGame()
    {
        playGame.interact();
        int numOfTriggerToStage2 = NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = NumOfTriggeredChangeToStage3;

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

    public void TurnOff()
    {
        turnOff.interact();
    }
}


public class SleepPlaceLaptop : BaseInteractionComponent
{
    private SleepPlaceLaptopProp props;

    [Header("Open Sleep Place Laptop Menu")]
    public UnityEvent OnOpenSleepPlaceLaptopMenu;
    void Start()
    {
        props = new SleepPlaceLaptopProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
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
        TriggerCount = TriggerCount + 1;
    }

    public void PlayTheGame()
    {
        Debug.Log("PlayTheGame");
        props.PlayTheGame();
        TriggerCount = TriggerCount + 1;
    }

    public void TurnOff()
    {
        Debug.Log("TurnOff");
        props.TurnOff();
    }

}
