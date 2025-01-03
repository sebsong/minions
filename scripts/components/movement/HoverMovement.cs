using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class HoverMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.HoverMovement;

    protected override Vector2 GetVelocityForInputDirection(Vector2 direction, double delta)
    {
        return GetVelocityForTargetLocation(GetComponentOwner().GlobalPosition + direction, delta);
    }

    protected override Vector2 GetVelocityForTargetLocation(Vector2 targetLocation, double delta)
    {
        if (IsIdleTargetLocation(targetLocation))
        {
            return Vector2.Zero;
        }

        Vector2 direction = GetComponentOwner().Position.DirectionTo(targetLocation);

        return direction * Speed;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
}
