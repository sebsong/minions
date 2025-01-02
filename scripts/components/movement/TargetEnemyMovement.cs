using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class TargetEnemyMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.TargetEnemyMovement;

    private entities.Enemy _enemyTarget;

    public override void _Ready()
    {
        base._Ready();
        UpdateEnemyTarget();
    }

    public override Vector2 GetVelocity(Vector2 targetLocation, double delta)
    {
        if (!IsInstanceValid(_enemyTarget))
        {
            UpdateEnemyTarget();
        }

        if (_enemyTarget == null)
        {
            return Vector2.Zero;
        }

        return (_enemyTarget.Position - GlobalPosition).Normalized() * Speed;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        if (collision.GetCollider() is entities.Enemy)
        {
            UpdateEnemyTarget();
        }
    }

    private void UpdateEnemyTarget()
    {
        Array<Node> allEnemies = GetTree().GetNodesInGroup("enemies");
        if (allEnemies.Count == 0)
        {
            _enemyTarget = null;
        }
        else
        {
            _enemyTarget = allEnemies.PickRandom() as entities.Enemy;
        }
    }
}
