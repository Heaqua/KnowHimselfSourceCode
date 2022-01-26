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
    public KitchenKnifeProp() : base(InteractivePropsType.KitchenKnife)
    {
        pullDown = new BaseInteraction(false);
        peelApple = new BaseInteraction(false);
        killYourself = new BaseInteraction(true);
    }
    public void KillYourself()
    {
        killYourself.interact();
        int numOfTriggerToStage2 = Constraints.NumOfTriggeredChangeToStage2;
        int numOfTriggerToStage3 = Constraints.NumOfTriggeredChangeToStage3;

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


public class KitchenKnife : MonoBehaviour, IPointerEnterHandler
{
    private KitchenKnifeProp props;

    [Header("Open Sleep Place Laptop Menu")]
    public UnityEvent OnOpenKitchenKnifeMenu;
    void Start()
    {
        props = new KitchenKnifeProp();
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnOpenKitchenKnifeMenu.Invoke");
        OnOpenKitchenKnifeMenu.Invoke();
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
    }

    public void PeelApple()
    {
        Debug.Log("PeelApple");
        props.PeelApple();
    }
}
