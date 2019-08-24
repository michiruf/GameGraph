using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameGraph.Editor
{
    [Obsolete]
    public class DragDropHandler : MouseManipulator
    {
        private bool dragActive;

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(OnStartDrag);
            target.RegisterCallback<MouseMoveEvent>(OnDrag);
            target.RegisterCallback<MouseUpEvent>(OnStopDrag);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnStartDrag);
            target.UnregisterCallback<MouseMoveEvent>(OnDrag);
            target.UnregisterCallback<MouseUpEvent>(OnStopDrag);
        }

        private void OnStartDrag(MouseDownEvent e)
        {
            if (dragActive)
            {
                e.StopImmediatePropagation();
                return;
            }

            if (!CanStartManipulation(e))
                return;

            Debug.Log("on mouse down and can start manipulation");
            dragActive = true;
            target.CaptureMouse();
            e.StopPropagation();
        }

        private void OnDrag(MouseMoveEvent e)
        {
            if (!dragActive || !target.HasMouseCapture())
                return;

            Debug.Log("on mouse drag");

            DragAndDrop.PrepareStartDrag();
            DragAndDrop.StartDrag(target.name);
//            DragAndDrop.objectReferences = new Object[] {historyItem};
            e.StopPropagation();
        }

        private void OnStopDrag(MouseUpEvent e)
        {
            if (!dragActive || !target.HasMouseCapture() || !CanStopManipulation(e))
                return;

            dragActive = false;
            target.ReleaseMouse();
            e.StopPropagation();
        }
    }
}
