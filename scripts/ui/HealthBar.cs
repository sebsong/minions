using Godot;

namespace minions.scripts.ui;

public partial class HealthBar : Node2D
{
    [Export]
    private TextureProgressBar _progressBar;

    public void SetMaxHealth(int maxHealth)
    {
        _progressBar.MaxValue = maxHealth;
        _progressBar.Value = maxHealth;
    }

    public void Lower(int amount)
    {
        _progressBar.Value -= amount;
    }

    public void Raise(int amount)
    {
        _progressBar.Value += amount;
    }
}
