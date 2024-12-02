using Godot;

namespace minions.scripts.components.core;

public abstract partial class AttackComponent : minions.scripts.components.core.Component
{
    [Export] internal int Damage = ComponentUtils.DefaultDamage;
    [Export] internal float AttackCooldown = ComponentUtils.DefaultAttackCooldown;

    public abstract void Attack(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);
}
