using System.Collections.Generic;
using Types;

namespace Components
{
    public struct PipeRingWeightsComponent
    {
        public float TotalWeight;
        public Dictionary<PipeRingType, float> RingsInLineWeightDictionary;
    }
}