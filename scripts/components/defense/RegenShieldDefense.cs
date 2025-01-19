using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.defense;

public partial class RegenShieldDefense : DefenseComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.RegenShieldDefense;

    [Export] private Timer _regenTimer;
    [Export] private float _regenTime;
    [Export] private Sprite2D _shieldSprite;

    private bool _shieldActive = true;

    public override void _Ready()
    {
        base._Ready();
        _regenTimer.WaitTime = _regenTime;
        _regenTimer.Timeout += OnRegenTimerTimeout;
    }

    public override int ResolveDamage(int amount)
    {
        if (_shieldActive)
        {
            ConsumeShield();
            return 0;
        }

        return amount;
    }

    private void ConsumeShield()
    {
        _shieldActive = false;
        _shieldSprite.Visible = false;
        _regenTimer.Start();
    }

    private void OnRegenTimerTimeout()
    {
        _shieldActive = true;
        _shieldSprite.Visible = true;
    }
}
