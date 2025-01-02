using Godot;
using minions.scripts.components.core;
using Vector2 = Godot.Vector2;

namespace minions.scripts.components.movement;

public partial class PlayerMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.PlayerMovement;

    public override void _Ready()
    {
        base._Ready();
        GetComponentOwner().Velocity = Vector2.Up * Speed;
    }

    public override Vector2 GetVelocity(Vector2 targetLocation, double delta)
    {
        return GetTurnedVelocity(delta);
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private Vector2 GetTurnedVelocity(double delta)
    {
        float turnAmount = 0;
        if (Input.IsActionPressed("left"))
        {
            turnAmount = -TurnSpeed;
        }

        if (Input.IsActionPressed("right"))
        {
            turnAmount = TurnSpeed;
        }

        return GetComponentOwner().Velocity.Rotated((float)(turnAmount / 180 * Mathf.Pi * delta));
    }

}
