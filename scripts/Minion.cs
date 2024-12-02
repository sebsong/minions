using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class Minion : CharacterBody2D
{
    private MovementComponent _movementComponent;
    private AttackComponent _attackComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetMovementComponent(ComponentUtils.ComponentType.RandomMovement);
        SetAttackComponent(ComponentUtils.ComponentType.ContactAttack);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Velocity = _movementComponent.GetVelocity();
        LookAt(GlobalPosition + Velocity);
        _attackComponent.Attack(delta);
        if (MoveAndSlide())
        {
            KinematicCollision2D collision = GetLastSlideCollision();
            _movementComponent.OnCollision(collision);
            _attackComponent.OnCollision(collision);
        }
    }

    public void SetMovementComponent(ComponentUtils.ComponentType componentType)
    {
        _movementComponent = ComponentUtils.AttachComponent<MovementComponent>(
            this,
            componentType
        );
    }

    public void SetAttackComponent(ComponentUtils.ComponentType componentType)
    {
        _attackComponent = ComponentUtils.AttachComponent<AttackComponent>(
            this,
            componentType
        );
    }
}
