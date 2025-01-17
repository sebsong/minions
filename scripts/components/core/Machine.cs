using Godot;
using minions.scripts.entities;
using minions.scripts.globals;

namespace minions.scripts.components.core;

public partial class Machine : CharacterBody2D, IDamageable
{
    [Export] public ScrapStorage ScrapStorage;

    public int FleetIndex;
    private BehaviorComponent _behaviorComponent;
    private MovementComponent _movementComponent;
    private AttackComponent _attackComponent;
    private DefenseComponent _defenseComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        ScrapStorage.OnScrapDepleted += OnScrapDepleted;
        ScrapStorage.OnScrapFinalBlow += Die;
        SetComponentsFromConfiguration(
            CurrentRunDataGlobal.Instance.FleetConfigurations[FleetIndex]
        );
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        LocationInput locationInput = _behaviorComponent?.GetLocationInput(delta) ??
                                      new LocationInput(false, ComponentUtils.IdleTargetLocation);
        Velocity = _movementComponent.GetVelocity(locationInput, delta);
        if (_behaviorComponent != null && _behaviorComponent.ShouldAttack(delta))
        {
            _attackComponent.Attack();
        }

        if (MoveAndSlide())
        {
            KinematicCollision2D collision = GetLastSlideCollision();
            _movementComponent.OnCollision(collision);
            _attackComponent.OnCollision(collision);
        }
    }

    public void TakeDamage(int amount, Node2D damageSource)
    {
        int scrapToRemove = _defenseComponent.ResolveDamage(amount);
        ScrapStorage.RemoveScrap(scrapToRemove, damageSource);
        if (this is not Player)
        {
            AudioManagerGlobal.Instance.HitAudio.Play();
        }
    }

    private void OnScrapDepleted()
    {
        GD.Print("SCRAP CRITICAL");
    }

    private void Die()
    {
        AudioManagerGlobal.Instance.ExplodeAudio.Play();
        QueueFree();
    }

    protected void SetComponentsFromConfiguration(ComponentConfiguration configuration)
    {
        SetBehaviorComponent(configuration.BehaviorComponentType);
        SetMovementComponent(configuration.MovementComponentType);
        SetAttackComponent(configuration.AttackComponentType);
        SetDefenseComponent(configuration.DefenseComponentType);
    }

    public void SetBehaviorComponent(ComponentUtils.ComponentType componentType)
    {
        _behaviorComponent = ComponentUtils.AttachComponent<BehaviorComponent>(
            this,
            componentType
        );
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
