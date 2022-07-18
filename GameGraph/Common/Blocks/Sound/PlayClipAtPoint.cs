using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Sound")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayClipAtPoint
    {
        // Output
        public event Action invoked;

        // Properties
        public UnityEngine.Vector3 position { private get; set; }
        public AudioClip clip { private get; set; }
        public float volume = 1f;

        public void Invoke()
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
            invoked?.Invoke();
        }
    }
}
