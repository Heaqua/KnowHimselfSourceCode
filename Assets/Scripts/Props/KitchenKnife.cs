using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KitchenKnifeProp : BaseInteractiveProp
{
    private BaseInteraction killYourself;
    private BaseInteraction pullDown;
    private BaseInteraction peelApple;
    public int NumOfTriggeredChangeToStage2 { get; private set; }
    public int NumOfTriggeredChangeToStage3 { get; private set; }
    public KitchenKnifeProp(int NumOfTriggeredChangeToStage2, int NumOfTriggeredChangeToStage3) : base(InteractivePropsType.KitchenKnife)
    {
        this.NumOfTriggeredChangeToStage2 = NumOfTriggeredChangeToStage2;
        this.NumOfTriggeredChangeToStage3 = NumOfTriggeredChangeToStage3;
        pullDown = new BaseInteraction(false);
        peelApple = new BaseInteraction(false);
        killYourself = new BaseInteraction(true);
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

    public void PullDown()
    {
        pullDown.interact();
    }

    public void PeelApple()
    {
        peelApple.interact();
    }
}


public class KitchenKnife : BaseInteractionComponent
{
    private KitchenKnifeProp props;

    [Header("Open Sleep Place Laptop Menu")]
    public UnityEvent OnOpenKitchenKnifeMenu;
    void Start()
    {
        props = new KitchenKnifeProp(TriggerCountTargetStage2, TriggerCountTargetStage3);
    }

    void Update()
    {
    }

    public void PullDown()
    {
        Debug.Log("PullDown");
        props.PullDown();
    }

    public void KillYourself()
    {
        Debug.Log("KillYourself");
        props.KillYourself();
        TriggerCount = TriggerCount + 1;
    }

    public void PeelApple()
    {
        Debug.Log("PeelApple");
        props.PeelApple();
    }
}
