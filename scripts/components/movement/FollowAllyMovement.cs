using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class FollowAllyMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.FollowAllyMovement;

    [Export] private float _followDistance;

    private Minion _allyTarget;

    public override void _Ready()
    {
        base._Ready();
        UpdateAllyTarget();
    }

    public override Vector2 GetVelocity(double delta)
    {
        if (!IsInstanceValid(_allyTarget))
        {
            UpdateAllyTarget();
        }

        if (_allyTarget == null || Position.DistanceTo(_allyTarget.Position) <= _followDistance)
        {
            return Vector2.Zero;
        }

        // TODO: have some sort of follow distance
        return (_allyTarget.Position - GlobalPosition).Normalized() * Speed;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private void UpdateAllyTarget()
    {
        // TODO: filter out this minion so it doesn't follow itself
        Array<Node> allMinions = GetTree().GetNodesInGroup("minions");
        if (allMinions.Count == 0)
        {
            _allyTarget = null;
        }
        else
        {
            _allyTarget = allMinions.PickRandom() as minions.scripts.Minion;
        }
    }
}
