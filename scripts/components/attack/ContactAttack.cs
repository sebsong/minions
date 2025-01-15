using Godot;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts.components.attack;

public partial class ContactAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.ContactAttack;

    [Export] private Timer _cooldownTimer;

    private IDamageable _lastDamageableHit;
    private bool _canAttackLastDamageable = true;

    public override void _Ready()
    {
        base._Ready();
        _cooldownTimer.Timeout += ResetCanAttack;
    }

    public override void Attack()
    {
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is IDamageable damageable)
        {
            if (damageable == _lastDamageableHit && !_canAttackLastDamageable)
            {
                return;
            }

            damageable.TakeDamage(AttackDamage, GetComponentOwner());

            _lastDamageableHit = damageable;
            _canAttackLastDamageable = false;
            _cooldownTimer.Start(AttackCooldown);
        }
    }

    private void ResetCanAttack()
    {
        _canAttackLastDamageable = true;
    }
}
