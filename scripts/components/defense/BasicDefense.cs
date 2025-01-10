using Godot;
using minions.scripts.components.core;
using minions.scripts.ui;

namespace minions.scripts.components.defense;

public partial class BasicDefense : DefenseComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.BasicDefense;

    [Export]
    private HealthBar _healthBar;

    [Export]
    private int _initialHealth = ComponentUtils.DefaultMaxHealth;
    private int _currentHealth;

    public override void _Ready()
    {
        base._Ready();
        _healthBar.SetMaxHealth(_initialHealth);
        _currentHealth = _initialHealth;
    }

    public override void TakeDamage(int amount)
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
        AudioManager.Instance.ExplodeAudio.Play();
        GetComponentOwner().QueueFree();
    }

}
