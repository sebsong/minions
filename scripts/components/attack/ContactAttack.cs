using Godot;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts.components.attack;

public partial class ContactAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.ContactAttack;

    private double _timeSinceLastAttack;
    private IDamageable _lastDamageableHit;

    public override void Attack()
    {
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is IDamageable damageable)
        {
            if (damageable == _lastDamageableHit && _timeSinceLastAttack < AttackCooldown)
            {
                return;
            }
            damageable.TakeDamage(AttackDamage, GetComponentOwner());
            _lastDamageableHit = damageable;
            _timeSinceLastAttack = 0;
        }
    }
}
