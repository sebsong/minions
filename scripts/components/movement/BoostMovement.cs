using Godot;
using minions.scripts.components.core;
using Vector2 = Godot.Vector2;

namespace minions.scripts.components.movement;

public partial class BoostMovement : MovementComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.BoostMovement;

    [Export] private Timer _cooldownTimer;
    [Export] private float _boostCooldown;
    [Export] private float _boostVelocityDecay;

    private Vector2 _currentVelocity;
    private bool _canBoost = true;

    public override void _Ready()
    {
        base._Ready();
        _cooldownTimer.WaitTime = _boostCooldown;
        _cooldownTimer.Timeout += OnCooldownTimerTimeout;
    }

    protected override Vector2 GetVelocityForInputDirection(Vector2 direction, double delta)
    {
        Vector2 relativeTargetLocation = Vector2.Zero;
        if (direction == Vector2.Left)
        {
            relativeTargetLocation += Vector2.Up;
        }

        if (direction == Vector2.Right)
        {
            relativeTargetLocation += Vector2.Down;
        }

        if (direction == Vector2.Up)
        {
            relativeTargetLocation += Vector2.Right;
        }

        return GetVelocityForTargetLocation(GetComponentOwner().ToGlobal(relativeTargetLocation), delta);
    }

    protected override Vector2 GetVelocityForTargetLocation(Vector2 targetLocation, double delta)
    {
        RotateTowardsTarget(targetLocation, delta);

        Vector2 relativeDirectionToTarget = GetComponentOwner().ToLocal(targetLocation).Normalized();
        if (relativeDirectionToTarget.Length() > .1 && IsLookingAtTarget(relativeDirectionToTarget) && _canBoost)
        {
            Boost();
        }

        _currentVelocity = GetComponentOwner().GlobalTransform.X * GetDecayedVelocityLength(delta);
        return _currentVelocity;
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }
    
    private void RotateTowardsTarget(Vector2 targetLocation, double delta)
    {
        Vector2 relativeDirection = GetRotatedDirection(Vector2.Right, targetLocation, delta);
        Vector2 lookAtTarget = GetComponentOwner().ToGlobal(relativeDirection);
        GetComponentOwner().LookAt(lookAtTarget);
    }

    private bool IsLookingAtTarget(Vector2 relativeDirectionToTarget)
    {
        return Mathf.Abs(Vector2.Right.AngleTo(relativeDirectionToTarget)) <= .01;
    }

    private float GetDecayedVelocityLength(double delta)
    {
        return Mathf.Max(0, _currentVelocity.Length() - (float)(_boostVelocityDecay * delta));
    }

    private void Boost()
    {
        _canBoost = false;
        _currentVelocity = GetComponentOwner().GlobalTransform.X * Speed;
        _cooldownTimer.Start();
    }

    private void OnCooldownTimerTimeout()
    {
        _canBoost = true;
    }
}
