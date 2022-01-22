using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public abstract class InteractivePropsBase
    {
        private InteractivePropsType mPropType { get; }
        private InteractivePropConstraints mConstraints { get; }
        private int mNumOfTriggered { get; set; }
        private Stage mCurrStage { get; set; }

        InteractivePropsBase(InteractivePropsType type)
        {
            mPropType = type;
            mConstraints = InteractivePropConstraintsFactory.create(type);
        }

    }

}