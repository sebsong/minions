using System;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class GlideMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.GlideMovement;

    private Vector2 _currentVelocity;

    public override void _Ready()
    {
        base._Ready();
        _currentVelocity = Vector2.Up * Speed;
    }

    protected override Vector2 GetVelocityForInputDirection(Vector2 direction, double delta)
    {
        Vector2 relativeTargetLocation = Vector2.Zero;
        if (direction == Vector2.Left)
        {
            relativeTargetLocation = Vector2.Up;
        }

        if (direction == Vector2.Right)
        {
            relativeTargetLocation = Vector2.Down;
        }

        return GetVelocityForTargetLocation(GetComponentOwner().ToGlobal(relativeTargetLocation), delta);
    }

    protected override Vector2 GetVelocityForTargetLocation(Vector2 targetLocation, double delta)
    {
        Vector2 location = targetLocation;
        if (IsIdleTargetLocation(targetLocation))
        {
            location = GetComponentOwner().ToGlobal(Vector2.Down);
        }

        TurnTowardTargetLocation(location, delta);
        return _currentVelocity;
    }

    private void TurnTowardTargetLocation(Vector2 targetLocation, double delta)
    {
        float angleToDestination = GetComponentOwner().GetAngleTo(targetLocation);
        float amountToTurn = (float)Mathf.Min(
            Mathf.Abs(angleToDestination),
            Mathf.Abs(TurnSpeed / 180 * Mathf.Pi * delta)
        );
        _currentVelocity = _currentVelocity.Rotated(Math.Sign(angleToDestination) * amountToTurn);
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
}
