using System;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class TargetEnemyBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.TargetEnemyBehavior;

    private Machine _enemyTarget;

    public override LocationInput GetLocationInput(double delta)
    {
        if (!IsInstanceValid(_enemyTarget))
        {
            UpdateEnemyTarget();
        }

        if (!IsInstanceValid(_enemyTarget))
        {
            return new LocationInput(false, ComponentUtils.IdleTargetLocation);
        }

        return new LocationInput(false, _enemyTarget.Position);
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
        Array<Node> enemies = GetEnemies();
        if (enemies.Count == 0)
        {
            _enemyTarget = null;
        }
        else
        {
            _enemyTarget = enemies.PickRandom() as Machine;
        }
    }
}
