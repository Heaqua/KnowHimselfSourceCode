using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Commons
{
    public abstract class BaseInteractionComponent : MonoBehaviour
    {
        [Header("Prop Parameters")]
        public InteractivePropsType PropType;
        public int TriggerCount { get; protected set; } = 0;
        [Range(1, 32)] public int TriggerCountTargetStage2 = 1;
        [Range(1, 32)] public int TriggerCountTargetStage3 = 1;

        [Header("Events")]
        public UnityEvent OnInteractStage1;
        public UnityEvent OnInteractStage2;
        public UnityEvent OnInteractStage3;

        GameManager Manager;

        private void OnEnable()
        {
            Manager = GameManager.instance;
            if (Manager == null)
                Manager = FindObjectOfType<GameManager>();
        }

        public virtual void Interact()
        {
            if (Manager != null)
            {
                switch (Manager.CurrentStage)
                {
                    case Stage.Stage3:
                        if (TriggerCount >= TriggerCountTargetStage3 + TriggerCountTargetStage2)
                        {
                            Interact_Stage3();
                            OnInteractStage3.Invoke();
                        }
                        else
                            goto S2Case;
                        break;
                    case Stage.Stage2:
                    S2Case:
                        if (TriggerCount >= TriggerCountTargetStage2)
                        {
                            Interact_Stage2();
                            OnInteractStage2.Invoke();
                        }
                        else
                            goto S1Case;
                        break;
                    case Stage.Stage1:
                    default:
                    S1Case:
                        Interact_Stage1();
                        OnInteractStage1.Invoke();
                        break;
                }
            }
            else
                OnInteractStage1.Invoke();
        }

        public void ResetTrigger() => TriggerCount = 0;

        public virtual void Interact_Stage1()
        {

        }

        public virtual void Interact_Stage2()
        {

        }

        public virtual void Interact_Stage3()
        {

        }
    }

}