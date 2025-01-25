using Godot;
using Godot.Collections;

namespace minions.scripts.components.core;

public abstract partial class BehaviorComponent : Component
{
    public abstract LocationInput GetLocationInput(double delta);

    public abstract bool ShouldAttack(double delta);

    public abstract void OnCollision(KinematicCollision2D collision);

    protected Array<Node> GetAllies()
    {
        string groupName = GetComponentOwner().IsInGroup("minions") ? "minions" : "enemies";
        var allies = GetTree().GetNodesInGroup(groupName);
        allies.Remove(GetComponentOwner());
        return allies;
    }
    
    protected Array<Node> GetEnemies()
    {
        string groupName = GetComponentOwner().IsInGroup("minions") ? "enemies" : "minions";
        return GetTree().GetNodesInGroup(groupName);
    }
}