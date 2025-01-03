using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.behavior;

public partial class PlayerBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.PlayerBehavior;

    public override LocationInput GetLocationInput(double delta)
    {
        Vector2 relativeDirection = Vector2.Zero;
        if (Input.IsActionPressed("left"))
        {
            relativeDirection += Vector2.Left;
        }

        if (Input.IsActionPressed("right"))
        {
            relativeDirection += Vector2.Right;
        }

        if (Input.IsActionPressed("up"))
        {
            relativeDirection += Vector2.Up;
        }

        if (Input.IsActionPressed("down"))
        {
            relativeDirection += Vector2.Down;
        }

        return new LocationInput(true, relativeDirection.Normalized());
    }

    public override bool ShouldAttack(double delta)
    {
        return Input.IsActionPressed("shoot");
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
}
