using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;

    [Header("Game Parameters")]
    public Commons.Stage CurrentStage;

    [Header("Events")]
    public UnityEvent OnNextStage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void GotoNextStage()
    {
        if (CurrentStage == Commons.Stage.Stage3)
            CurrentStage = Commons.Stage.Stage1;
        else
            CurrentStage++;

        OnNextStage.Invoke();
    }
}
