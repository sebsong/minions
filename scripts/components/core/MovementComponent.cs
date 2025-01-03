using Godot;

namespace minions.scripts.components.core;

public abstract partial class MovementComponent : Component
{
    [Export] internal float Speed = ComponentUtils.DefaultMovementSpeed;
    [Export] internal float TurnSpeed = ComponentUtils.DefaultTurnSpeed;

    public abstract Vector2 GetVelocity(Vector2 targetLocation, double delta);

    public abstract void OnCollision(KinematicCollision2D collision);

    protected bool IsIdleTargetLocation(Vector2 targetLocation)
    {
        return targetLocation == ComponentUtils.IdleTargetLocation;
    }
}
