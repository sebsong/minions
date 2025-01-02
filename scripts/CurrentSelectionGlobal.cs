using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class CurrentSelectionGlobal : Node
{
    public static CurrentSelectionGlobal Instance;

    public ComponentSelection CurrentPlayerSelection = new(
        ComponentUtils.ComponentType.PlayerMovement,
        ComponentUtils.ComponentType.PlayerAttack,
        ComponentUtils.ComponentType.BasicDefense
    );

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }
}
