using Godot;

namespace minions.scripts.entities;

public interface IDamageable
{
    public void TakeDamage(int amount, Node2D damageSource);
}
