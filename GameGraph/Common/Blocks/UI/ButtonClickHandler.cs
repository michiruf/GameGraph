using System;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/UI")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ButtonClickHandler : IStartHook, IDestroyHook
    {
        // Output
        public event Action onClick;

        // Properties
        public Button button { private get; set; }

        [ExcludeFromGraph]
        public void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        [ExcludeFromGraph]
        public void Destroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            onClick?.Invoke();
        }
    }
}
