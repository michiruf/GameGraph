using System;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace GameGraph.Common.Blocks
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SliderSetValue
    {
        // Output
        public event Action valueSet;

        // Properties
        public Slider slider { private get; set; }
        public float value;

        public void SetValue()
        {
            slider.value = value;
            valueSet?.Invoke();
        }
    }
}
