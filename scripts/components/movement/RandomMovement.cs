using System;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.components.movement;

public partial class RandomMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.RandomMovement;

    [Export] private Timer _velocityChangeTimer;
    [Export] private float _minTimerDuration;
    [Export] private float _maxTimerDuration;
    [Export] private float _minDestinationDist;
    [Export] private float _maxDestinationDist;

    private RandomNumberGenerator _rng = new();
    private Vector2 _currentVelocity;
    private Vector2 _currentDestination;

    public override void _Ready()
    {
        base._Ready();
        _currentVelocity = Vector2.Up * Speed;
        UpdateDestination();
    }

    public override Vector2 GetVelocity(double delta)
    {
        if (GetComponentOwner().GlobalPosition.DistanceTo(_currentDestination) < 1)
        {
            UpdateDestination();
        }

        TurnTowardDestination(delta);
        return _currentVelocity;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
        UpdateDestination();
    }

    private void TurnTowardDestination(double delta)
    {
        float angleToDestination = GetComponentOwner().GetAngleTo(_currentDestination);
        float amountToTurn = (float)Mathf.Min(
            Mathf.Abs(angleToDestination),
            Mathf.Abs(TurnSpeed / 180 * Mathf.Pi * delta)
        );
        _currentVelocity = _currentVelocity.Rotated(Math.Sign(angleToDestination) * amountToTurn);
    }

    private void UpdateDestination()
    {
        float x = _rng.RandfRange(-1f, 1f);
        float y = _rng.RandfRange(-1f, 1f);
        float distance = _rng.RandfRange(_minDestinationDist, _maxDestinationDist);
        Vector2 offset = new Vector2(x, y).Normalized() * distance;
        Vector2 ownerPosition = GetComponentOwner().GlobalPosition;
        _currentDestination = ownerPosition + offset;
    }

    private void OnVelocityChangeTimerTimeout()
    {
        UpdateDestination();
        _velocityChangeTimer.WaitTime = _rng.RandfRange(_minTimerDuration, _maxTimerDuration);
    }
}
