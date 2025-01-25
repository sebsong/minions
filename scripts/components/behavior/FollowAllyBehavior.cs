using System;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts.components.behavior;

public partial class FollowAllyBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.FollowAllyBehavior;

    [Export] private float _followDistance;

    private Machine _allyTarget;

    public override void _Ready()
    {
        base._Ready();
        UpdateAllyTarget();
    }

    public override LocationInput GetLocationInput(double delta)
    {
        if (!IsInstanceValid(_allyTarget))
        {
            UpdateAllyTarget();
        }

        if (!IsInstanceValid(_allyTarget) || Position.DistanceTo(_allyTarget.Position) <= _followDistance)
        {
            return new LocationInput(false, ComponentUtils.IdleTargetLocation);
        }

        Vector2 targetLocation = _allyTarget.GlobalPosition - (_allyTarget.GlobalTransform.X * _followDistance);

        return new LocationInput(false, targetLocation);
    }

    public override bool ShouldAttack(double delta)
    {
        return false; // TODO
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private void UpdateAllyTarget()
    {
        Array<Node> allies = GetAllies();
        if (allies.Count == 0)
        {
            _allyTarget = null;
        }
        else
        {
            _allyTarget = allies.PickRandom() as Machine;
        }
    }
}
