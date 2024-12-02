using Godot;
using prototype_minions.scripts.components.core;

namespace prototype_minions.scripts.components.movement;

public partial class RandomMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.RandomMovement;

    [Export] private Timer _velocityChangeTimer;
    [Export] private float _minTimerDuration;
    [Export] private float _maxTimerDuration;

    private Vector2 _currentVelocity;
    private RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        base._Ready();
        UpdateVelocity();
    }

    public override Vector2 GetVelocity()
    {
        return _currentVelocity;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        _currentVelocity = GenerateRandomVelocity();
    }

    private Vector2 GenerateRandomVelocity()
    {
        float x = _rng.RandfRange(-1f, 1f);
        float y = _rng.RandfRange(-1f, 1f);
        return new Vector2(x, y).Normalized() * Speed;
    }

    private void OnVelocityChangeTimerTimeout()
    {
        UpdateVelocity();
        _velocityChangeTimer.WaitTime = _rng.RandfRange(_minTimerDuration, _maxTimerDuration);
    }

}
