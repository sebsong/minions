using Godot;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentButton : Button
{
    public ComponentUtils.ComponentType Type;

    public override void _Ready()
    {
        base._Ready();
        Text = Type.ToString();
    }
}
