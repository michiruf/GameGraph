using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Event")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class UnityMessageDispatcher
    {
        public Component component;
        public string eventName;

        public void FireEvent()
        {
            component.SendMessage(eventName, SendMessageOptions.RequireReceiver);
        }
    }
}
