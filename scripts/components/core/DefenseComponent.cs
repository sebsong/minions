using Godot;

namespace minions.scripts.components.core;

public abstract partial class DefenseComponent: Component
{
    public abstract void TakeDamage(int amount);
}
