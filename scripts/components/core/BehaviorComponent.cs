using Godot;

namespace minions.scripts.components.core;

public abstract partial class BehaviorComponent : Component
{
    public abstract Vector2 GetTargetLocation(double delta);

    public abstract bool ShouldAttack(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);
}
