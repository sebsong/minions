using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.behavior;

public partial class RandomBehavior : BehaviorComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.RandomBehavior;

    [ExportGroup("Target Location")]
    [Export] private Timer _targetLocationUpdateTimer;
    [Export] private float _minTargetLocationUpdateTimerDuration;
    [Export] private float _maxTargetLocationUpdateTimerDuration;
    [Export] private float _minTargetLocationDist;
    [Export] private float _maxTargetLocationDist;

    [ExportGroup("Attack")]
    [Export] private Timer _attackTimer;
    [Export] private float _minAttackTimerDuration;
    [Export] private float _maxAttackTimerDuration;

    private RandomNumberGenerator _rng = new();
    private Vector2 _currentTargetLocation;
    private bool _shouldAttack;

    public override void _Ready()
    {
        base._Ready();
        _targetLocationUpdateTimer.Timeout += OnTargetLocationUpdateTimerTimeout;
        _attackTimer.Timeout += OnAttackTimerTimeout;
        UpdateTargetLocation();
    }

    public override LocationInput GetLocationInput(double delta)
    {
        if (GetComponentOwner().GlobalPosition.DistanceTo(_currentTargetLocation) < 0.1)
        {
            UpdateTargetLocation();
        }

        return new LocationInput(false, _currentTargetLocation);
    }

    public override bool ShouldAttack(double delta)
    {
        return _shouldAttack;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        UpdateTargetLocation();
    }

    private void UpdateTargetLocation()
    {
        float x = _rng.RandfRange(-1f, 1f);
        float y = _rng.RandfRange(-1f, 1f);
        float distance = _rng.RandfRange(_minTargetLocationDist, _maxTargetLocationDist);
        Vector2 offset = new Vector2(x, y).Normalized() * distance;
        Vector2 ownerPosition = GetComponentOwner().GlobalPosition;
        _currentTargetLocation = ownerPosition + offset;
    }

    private void OnTargetLocationUpdateTimerTimeout()
    {
        UpdateTargetLocation();
        _targetLocationUpdateTimer.WaitTime = _rng.RandfRange(_minTargetLocationUpdateTimerDuration, _maxTargetLocationUpdateTimerDuration);
    }

    private void OnAttackTimerTimeout()
    {
        _shouldAttack = !_shouldAttack;
        _attackTimer.WaitTime = _rng.RandfRange(_minAttackTimerDuration, _maxAttackTimerDuration);
    }
}
