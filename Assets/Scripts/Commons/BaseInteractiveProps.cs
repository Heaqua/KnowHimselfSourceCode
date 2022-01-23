using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public abstract class BaseInteractiveProp
    {
        protected InteractivePropsType PropType { get; }
        protected InteractivePropConstraints Constraints { get; }
        protected Stage CurrStage { get; set; }
        protected Stage BoundStage { get; set; }

        public BaseInteractiveProp(InteractivePropsType type)
        {
            PropType = type;
            Constraints = InteractivePropConstraintsFactory.create(type);
        }
    }

}