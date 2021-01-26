using System;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    public enum InputDetectionType
    {
        Continuous,
        Down,
        Up
    }

    public static class InputDetection
    {
        public static bool GetButton(string button, InputDetectionType inputDetectionType)
        {
            switch (inputDetectionType)
            {
                case InputDetectionType.Up:
                    return Input.GetButtonUp(button);
                case InputDetectionType.Down:
                    return Input.GetButtonDown(button);
                case InputDetectionType.Continuous:
                    return Input.GetButton(button);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
