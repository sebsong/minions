using Godot;

namespace prototype_minions.scripts.components.core;

public abstract partial class Component : Node2D
{
    public abstract ComponentUtils.ComponentType ComponentType { get; }
}
