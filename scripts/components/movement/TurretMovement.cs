using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class TurretMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.TurretMovement;
    protected override Vector2 GetVelocityForInputDirection(Vector2 direction, double delta)
    {
        Vector2 relativeTargetLocation = direction.Rotated(Mathf.Pi / 2);

        return GetVelocityForTargetLocation(GetComponentOwner().ToGlobal(relativeTargetLocation), delta);
    }

    protected override Vector2 GetVelocityForTargetLocation(Vector2 targetLocation, double delta)
    {
        RotateTowardsTarget(targetLocation, delta);
        
        return Vector2.Zero;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
}