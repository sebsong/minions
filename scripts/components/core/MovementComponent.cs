using Godot;

namespace minions.scripts.components.core;

public abstract partial class MovementComponent : Component
{
    [Export] internal float Speed = ComponentUtils.DefaultMovementSpeed;
    [Export] internal float TurnSpeed = ComponentUtils.DefaultTurnSpeed;

    public Vector2 GetVelocity(LocationInput locationInput, double delta)
    {
        if (locationInput.IsRelativeDirection)
        {
            return GetVelocityForInputDirection(locationInput.Location, delta);
        }

        return GetVelocityForTargetLocation(locationInput.Location, delta);
    }

    protected abstract Vector2 GetVelocityForInputDirection(Vector2 direction, double delta);

    protected abstract Vector2 GetVelocityForTargetLocation(Vector2 targetLocation, double delta);


    public abstract void OnCollision(KinematicCollision2D collision);

    protected bool IsIdleTargetLocation(Vector2 targetLocation)
    {
        return targetLocation == ComponentUtils.IdleTargetLocation;
    }
}
