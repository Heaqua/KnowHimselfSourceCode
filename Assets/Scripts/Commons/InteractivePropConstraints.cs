using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public class InteractivePropConstraints
    {
        private InteractivePropsType mPropType { get; }
        private int mNumOfTriggeredChangeToStage2 { get; set;}
        private int mNumOfTriggeredChangeToStage3 { get; set;}
        public InteractivePropConstraints(InteractivePropsType propType, int numOfTriggeredChangeToStage2, int numOfTriggeredChangeToStage3)
        {
            mPropType = propType;
            mNumOfTriggeredChangeToStage2 = numOfTriggeredChangeToStage2;
            mNumOfTriggeredChangeToStage3 = numOfTriggeredChangeToStage3;
        }
    }

    public class InteractivePropConstraintsFactory
    {
        private static Dictionary<InteractivePropsType, InteractivePropConstraints>
        mConstraintDict = new Dictionary<InteractivePropsType, InteractivePropConstraints>()
        {
            {InteractivePropsType.BathroomMirror, new InteractivePropConstraints(InteractivePropsType.BathroomMirror, 1, 1)},
        };


        public InteractivePropConstraintsFactory()
        {
        }

        public static InteractivePropConstraints create(InteractivePropsType propType)
        {
            if (mConstraintDict.ContainsKey(propType))
            {
                return mConstraintDict[propType];
            }
            else
            {
                return new InteractivePropConstraints(InteractivePropsType.NULL, int.MaxValue, int.MaxValue);
            }
        }
    }
}
