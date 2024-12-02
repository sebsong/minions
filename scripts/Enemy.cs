using Godot;

namespace minions.scripts;

public partial class Enemy : CharacterBody2D
{
    [Export]
    private HealthBar _healthBar;

    [Export]
    private int _initialHealth = 3;
    private int _currentHealth;

    public override void _Ready()
    {
        base._Ready();
        _healthBar.SetMaxHealth(_initialHealth);
        _currentHealth = _initialHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _healthBar.Lower(amount);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        QueueFree();
    }
}
