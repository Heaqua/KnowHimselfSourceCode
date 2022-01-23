using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public class BaseInteraction
    {
        private int numOfTriggered;
        private bool IsNegativeInteraction { get; }

        public BaseInteraction(bool isNegativeInteration)
        {
            IsNegativeInteraction = isNegativeInteration;
        }

        public int getNumberOfTriggered()
        {
            return numOfTriggered;
        }

        public void interact()
        {
            numOfTriggered++;
        }
    }
}

