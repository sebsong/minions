using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.attack;

public partial class ContactAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.ContactAttack;

    private double _timeSinceLastAttack;
    private minions.scripts.Enemy _lastEnemyHit;

    public override void Attack(double delta)
    {
        _timeSinceLastAttack += delta;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is minions.scripts.Enemy enemy)
        {
            if (enemy == _lastEnemyHit && _timeSinceLastAttack < AttackCooldown)
            {
                return;
            }
            enemy.TakeDamage(AttackDamage);
            _lastEnemyHit = enemy;
            _timeSinceLastAttack = 0;
        }
    }
}
