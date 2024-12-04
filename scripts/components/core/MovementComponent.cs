using Godot;

namespace minions.scripts.components.core;

public abstract partial class MovementComponent : Component
{
    [Export] internal float Speed = ComponentUtils.DefaultMovementSpeed;
    [Export] internal float TurnSpeed = ComponentUtils.DefaultTurnSpeed;

    public abstract Vector2 GetVelocity(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);
}
