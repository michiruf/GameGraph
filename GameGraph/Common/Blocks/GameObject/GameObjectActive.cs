using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/GameObject")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GameObjectActive
    {
        // Output
        public event Action wasSet;
        public bool isActive => gameObject.activeSelf;

        // Properties
        public GameObject gameObject;
        public bool setActive { private get; set; }

        public void Set()
        {
            gameObject.SetActive(setActive);
            wasSet?.Invoke();
        }
    }
}
