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

    private Minion _allyTarget;

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

        return new LocationInput(false, _allyTarget.Position); // TODO: follow distance
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
        Array<Node> allMinions = GetTree().GetNodesInGroup("minions");
        allMinions.Remove(GetComponentOwner());
        if (allMinions.Count == 0)
        {
            _allyTarget = null;
        }
        else
        {
            _allyTarget = allMinions.PickRandom() as Minion;
        }
    }

}
