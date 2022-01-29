using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    [RequireComponent(typeof(Collider))]
    public class TriggerInteractor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            BaseInteractionComponent interact = other.gameObject.GetComponent<BaseInteractionComponent>();
            Renderer renderer = other.gameObject.GetComponent<Renderer>();

            Debug.LogWarningFormat("TriggerInteractor is now colliding with {0}", other.name);

            if (renderer != null && renderer.isVisible)
            {
                Debug.LogFormat("TriggerInteractor is now interacting with {0}", other.name);

                if (interact != null)
                    interact.Interact();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}
