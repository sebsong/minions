using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class Enemy : ComponentControlledBody
{
    public override void _Ready()
    {
        base._Ready();
        SetMovementComponent(ComponentUtils.ComponentType.RandomMovement);
        SetAttackComponent(ComponentUtils.ComponentType.ContactAttack);
        SetDefenseComponent(ComponentUtils.ComponentType.InvincibleDefense);
    }
}
