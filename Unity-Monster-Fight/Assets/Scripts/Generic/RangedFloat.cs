using System;

namespace Game.Generic
{
    [Serializable]
    public class RangedFloat
    {
        public float MinValue;
        public float MaxValue;

        public RangedFloat(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
