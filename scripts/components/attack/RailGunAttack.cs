using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.components.attack;

public partial class RailGunAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.RailGunAttack;

    private PackedScene _railScene = ResourceLoader.Load<PackedScene>("res://scenes/entities/rail.tscn");

    [Export] private ShapeCast2D _shapeCast;
    [Export] private Timer _shootCooldownTimer;

    [Export] private Timer _railDurationTimer;
    [Export] private float _railDuration;

    private bool _canShoot = true;

    public override void _Ready()
    {
        base._Ready();

        _shapeCast.AddException(GetComponentOwner());

        _shootCooldownTimer.Timeout += OnShootCooldownTimerTimeout;
        _shootCooldownTimer.WaitTime = AttackCooldown;
        _railDurationTimer.WaitTime = _railDuration;
    }

    public override void Attack()
    {
        if (_canShoot)
        {
            PlayRailAnimation();
            Array collisionResult = _shapeCast.CollisionResult;
            foreach (Dictionary collision in collisionResult)
            {
                var collider = collision["collider"].Obj;
                if (collider is Machine machine)
                {
                    machine.TakeDamage(AttackDamage, GetComponentOwner());
                }
            }

            _canShoot = false;
            _shootCooldownTimer.Start();
        }
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private void OnShootCooldownTimerTimeout()
    {
        _canShoot = true;
    }

    private void PlayRailAnimation()
    {
        Node2D railNode = _railScene.Instantiate<Node2D>();
        railNode.GlobalPosition = GlobalPosition;
        railNode.GlobalRotation = GlobalRotation;
        GetTree().CurrentScene.AddChild(railNode);

        SceneTreeTimer railDurationTimer = GetTree().CreateTimer(_railDuration);
        railDurationTimer.Timeout += () => OnRailDurationTimerTimeout(railNode);
    }

    private void OnRailDurationTimerTimeout(Node railNode)
    {
        if (IsInstanceValid(railNode))
        {
            railNode.QueueFree();
        }
    }
}
