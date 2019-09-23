General TODOs:
* Add field values (to node views)
  - See https://docs.unity3d.com/ScriptReference/UIElements.EnumField.html
* Dragging and edge should open the search dialog
  - see ShaderGraph: MaterialGraphView.OnDragPerformEvent
* Instance connector - add a script that provides an instance from one graph to another?
  - This could be also achieved using an own script and get and instance and provide it via parameter to another,
    but would be pretty ugly
* Better data handling
  - Unity gives very little space for hooks. Search an useful alternative!
  - Make everything event driven maybe?
  - Data binding?
* Undo
* Validation
* Node instance linking inside a graph (2 node view represent the same node)
* Collapsible nodes
* Reduce only necessary APIs to public
