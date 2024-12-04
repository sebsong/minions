using Godot;

namespace minions.scripts.components.core;

public abstract partial class Component : Node2D
{
    public abstract ComponentUtils.ComponentType ComponentType { get; }

    public CharacterBody2D GetComponentOwner()
    {
        return GetParent<CharacterBody2D>();
    }
}
