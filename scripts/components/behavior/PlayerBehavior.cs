using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.behavior;

public partial class PlayerBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.PlayerBehavior;

    public override Vector2 GetTargetLocation(double delta)
    {
        Vector2 targetLocation = Vector2.Zero;
        if (Input.IsActionPressed("left"))
        {
            targetLocation = Vector2.Up;
        }

        if (Input.IsActionPressed("right"))
        {
            targetLocation = Vector2.Down;
        }

        return GetComponentOwner().ToGlobal(targetLocation);
    }

    public override bool ShouldAttack(double delta)
    {
        return Input.IsActionPressed("shoot");
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
}
