using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public class InteractivePropConstraints
    {
        public InteractivePropsType mPropType { get; }
        public int NumOfTriggeredChangeToStage2 { get; set; }
        public int NumOfTriggeredChangeToStage3 { get; set; }
        public InteractivePropConstraints(InteractivePropsType propType, int numOfTriggeredChangeToStage2, int numOfTriggeredChangeToStage3)
        {
            mPropType = propType;
            NumOfTriggeredChangeToStage2 = numOfTriggeredChangeToStage2;
            NumOfTriggeredChangeToStage3 = numOfTriggeredChangeToStage3;
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
