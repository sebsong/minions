using Godot;
using prototype_minions.scripts.components.core;

namespace prototype_minions.scripts.components.attack;

public partial class ContactAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.ContactAttack;

    private double _timeSinceLastAttack;
    private Enemy _lastEnemyHit;

    public override void Attack(double delta)
    {
        _timeSinceLastAttack += delta;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is Enemy enemy)
        {
            if (enemy == _lastEnemyHit && _timeSinceLastAttack < AttackCooldown)
            {
                return;
            }
            enemy.TakeDamage(Damage);
            _lastEnemyHit = enemy;
            _timeSinceLastAttack = 0;
        }
    }
}
