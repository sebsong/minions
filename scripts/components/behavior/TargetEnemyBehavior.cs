using System;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class TargetEnemyBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.TargetEnemyBehavior;

    private entities.Enemy _enemyTarget;

    public override Vector2 GetTargetLocation(double delta)
    {
        if (!IsInstanceValid(_enemyTarget))
        {
            UpdateEnemyTarget();
        }

        if (!IsInstanceValid(_enemyTarget))
        {
            return ComponentUtils.IdleTargetLocation;
        }

        return _enemyTarget.Position;
    }

    public override bool ShouldAttack(double delta)
    {
        return false; // TODO
    }


    public override void _Ready()
    {
        base._Ready();
        UpdateEnemyTarget();
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
