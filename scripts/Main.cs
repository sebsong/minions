using Godot;
using Godot.Collections;
using minions.scripts.ui;

namespace minions.scripts;

public partial class Main : Node2D
{
    [Export] private PackedScene _componentSelectorScene;
    [Export] private VBoxContainer _vBoxContainer;

    [Export] private GpuParticles2D _cloudParticles;

    public override void _Ready()
    {
        base._Ready();

        Array<Node> minionNodes = GetTree().GetNodesInGroup("minions");
        foreach (var minionNode in minionNodes)
        {
            entities.Minion minion = (entities.Minion)minionNode;
            ComponentSelectorOld selector = _componentSelectorScene.Instantiate<ComponentSelectorOld>();
            _vBoxContainer.AddChild(selector);
            selector.RegisterMinion(minion);
        }

        _cloudParticles.SpeedScale = 100;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_cloudParticles, "speed_scale", 1, 5f);
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
