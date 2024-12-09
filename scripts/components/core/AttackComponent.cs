using Godot;

namespace minions.scripts.components.core;

public abstract partial class AttackComponent : Component
{
    [Export] internal int AttackDamage = ComponentUtils.DefaultAttackDamage;
    [Export] internal float AttackCooldown = ComponentUtils.DefaultAttackCooldown;
    [Export] internal float AttackSpeed = ComponentUtils.DefaultAttackSpeed;

    public abstract void Attack(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);
}
