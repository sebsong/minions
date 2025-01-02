using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class CurrentSelectionGlobal : Node
{
    public static CurrentSelectionGlobal Instance;

    public ComponentSelection CurrentPlayerSelection = new(
        ComponentUtils.ComponentType.PlayerBehavior,
        ComponentUtils.ComponentType.GlideMovement,
        ComponentUtils.ComponentType.MachineGunAttack,
        ComponentUtils.ComponentType.BasicDefense
    );

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }
}
