using Godot;

namespace prototype_minions.scripts.components.core;

public abstract partial class AttackComponent : Component
{
    [Export] internal int Damage = ComponentUtils.DefaultDamage;
    [Export] internal float AttackCooldown = ComponentUtils.DefaultAttackCooldown;

    public abstract void Attack(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);
}
