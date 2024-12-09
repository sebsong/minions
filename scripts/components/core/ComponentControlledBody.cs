using Godot;

namespace minions.scripts.components.core;

public partial class ComponentControlledBody : CharacterBody2D, IDamageable
{
    private MovementComponent _movementComponent;
    private AttackComponent _attackComponent;
    private DefenseComponent _defenseComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        SetMovementComponent(ComponentUtils.ComponentType.RandomMovement);
        SetAttackComponent(ComponentUtils.ComponentType.ContactAttack);
        SetDefenseComponent(ComponentUtils.ComponentType.BasicDefense);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Velocity = _movementComponent.GetVelocity(delta);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        LookAt(GlobalPosition + Velocity);
        _attackComponent.Attack(delta);
        if (MoveAndSlide())
        {
            KinematicCollision2D collision = GetLastSlideCollision();
            _movementComponent.OnCollision(collision);
            _attackComponent.OnCollision(collision);
        }
    }

    public void TakeDamage(int amount)
    {
        _defenseComponent.TakeDamage(amount);
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

    public void SetDefenseComponent(ComponentUtils.ComponentType componentType)
    {
        _defenseComponent = ComponentUtils.AttachComponent<DefenseComponent>(
            this,
            componentType
        );
    }
}
