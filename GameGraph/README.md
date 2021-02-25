# GameGraph

Graphical dependency injection / flow control plugin for Unity3d.


## Usage

### Install

To embed GameGraph in your project use the git url variant. Put this in your `Packages\manifest.json`:
```json
{
  "dependencies": {
    // ...
    "de.michiruf.gamegraph": "https://github.com/michiruf/GameGraph.git"
    // , ...
  }
}
```


### Writing nodes

Annotate classes with `[GameGraph]` to have them included in the GameGraph windows node creation dialog.

```c#
[GameGraph("MyNodes/Rotator")]
public class Rotator
{
    public float deltaTime { private get; set; } = Time.fixedDeltaTime;
    public float rotationSpeed { private get; set; }

    public GameObject gameObject { private get; set; }

    public void Rotate()
    {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }
}
```


### Using the editor

1. Create an asset of type GameGraph in your asset explorer
2. Add a Game Graph Behaviour to your game object
3. Drag the created asset to the behaviour
4. Start editing the asset and hit play to test


## Changelog

Nothing here yet.


## Further TODOs

* Link unitys metadata id to class names -> refactoring of classes is then allowed
* Add field values (to node views)
    - See https://docs.unity3d.com/ScriptReference/UIElements.EnumField.html
* Dragging and edge should open the search dialog
    - see ShaderGraph: MaterialGraphView.OnDragPerformEvent
* Instance connector - add a script that provides an instance from one graph to another?
    - This could be also achieved using an own script and get and instance and provide it via parameter to another,
      but would be pretty ugly
* Node ids could reference UUIDs from unity meta file. If a class gets renamed, the graph will still work.
* Better data handling
    - Unity gives very little space for hooks. Search an useful alternative!
    - Make everything event driven maybe?
    - Data binding?
* Undo
* Validation
* Node instance linking inside a graph (2 node view represent the same node)
* Collapsible nodes
* Reduce only necessary APIs to public
* Allow static methods to be used as blocks (e.g. use-case: get the transform of a game object)
* Allow custom editor view for some classes (via GameGraph annotation)
* Aliases for node search (e.g. float should be found with Vector1)
* Notice nodes / Notices anyhow
* To provide primitive fields to the graph, may serialize the CommonBlocks Float, Boolean, ... in the inspector of the GameGraphBehaviour and construct those on start
* Lists by nodes "CreateList" chaining into multiple "AddItem" nodes with list to modify and object to put in as input
