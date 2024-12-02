using Godot;
using Godot.Collections;
using prototype_minions.scripts.ui;

namespace prototype_minions.scripts;

public partial class Main : Node2D
{
    [Export] private PackedScene _componentSelectorScene;
    [Export] private VBoxContainer _vBoxContainer;

    public override void _Ready()
    {
        base._Ready();

        Array<Node> minionNodes = GetTree().GetNodesInGroup("minions");
        foreach (var minionNode in minionNodes)
        {
            Minion minion = (Minion)minionNode;
            ComponentSelector selector = _componentSelectorScene.Instantiate<ComponentSelector>();
            _vBoxContainer.AddChild(selector);
            selector.RegisterMinion(minion);
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
    }
}
