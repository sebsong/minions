using Godot;

namespace prototype_minions.scripts;

public partial class HealthBar : Node2D
{
    [Export]
    private TextureProgressBar _progressBar;

    public void SetMaxHealth(int maxHealth)
    {
        _progressBar.MaxValue = maxHealth;
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
