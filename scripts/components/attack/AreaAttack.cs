using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.attack;

public partial class AreaAttack : AttackComponent
{
    [Export] private Timer _attackTimer;
    [Export] private Timer _attackActiveTimer;
    [Export] private CollisionShape2D _hitBoxCollisionShape;

    [Export] private float _hitBoxActiveSeconds;
    [Export] private float _hitBoxRadius;

    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.AreaAttack;

    public override void _Ready()
    {
        base._Ready();
        ((CircleShape2D)_hitBoxCollisionShape.Shape).Radius = _hitBoxRadius;
        _attackTimer.Start(AttackCooldown);
    }

    public override void Attack()
    {
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private void OnHitBoxEntered(Node2D body)
    {
        if (body is IDamageable damageable)
        {
            damageable.TakeDamage(AttackDamage);
        }
    }

    private void OnAttackTimerTimeout()
    {
        EnableAttackHitBox();
        _attackActiveTimer.Start(_hitBoxActiveSeconds);
    }

    private void OnAttackHitBoxActiveTimerTimeout()
    {
        DisableAttackHitBox();
    }

    private void EnableAttackHitBox()
    {
        _hitBoxCollisionShape.Disabled = false;
    }

    private void DisableAttackHitBox()
    {
        _hitBoxCollisionShape.Disabled = true;
    }
}
