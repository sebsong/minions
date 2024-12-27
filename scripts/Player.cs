using minions.scripts.components.core;

namespace minions.scenes;

public partial class Player : ComponentControlledBody
{
    public override void _Ready()
    {
        base._Ready();
        SetMovementComponent(ComponentUtils.ComponentType.PlayerMovement);
        SetAttackComponent(ComponentUtils.ComponentType.PlayerAttack);
        SetDefenseComponent(ComponentUtils.ComponentType.BasicDefense);
    }
}
