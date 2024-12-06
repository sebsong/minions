using Godot;
using System;
using minions.scripts.components.core;

public partial class Player : CharacterBody2D
{
    [Export] private PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/Bullet.tscn");
    [Export] private Node2D _bulletSpawnLeft;
    [Export] private Node2D _bulletSpawnRight;
    [Export] private Timer _shootCooldownTimer;

    [Export] private float _speed = ComponentUtils.DefaultMovementSpeed;
    [Export] private float _turnSpeed = ComponentUtils.DefaultTurnSpeed;

    private bool _canShoot = true;
    private Node2D _currentBulletSpawn;

    public override void _Ready()
    {
        base._Ready();
        _currentBulletSpawn = _bulletSpawnLeft;
        _shootCooldownTimer.Timeout += OnShootCooldownTimerTimeout;
        Velocity = Vector2.Up * _speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        LookAt(GlobalPosition + Velocity);
        HandleTurning(delta);
        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        HandleShooting();
    }

    private void HandleTurning(double delta)
    {
        float turnAmount = 0;
        if (Input.IsActionPressed("left"))
        {
            turnAmount = -_turnSpeed;
        }

        if (Input.IsActionPressed("right"))
        {
            turnAmount = _turnSpeed;
        }

        Velocity = Velocity.Rotated((float)(turnAmount / 180 * Mathf.Pi * delta));
    }

    private void HandleShooting()
    {
        if (Input.IsActionPressed("shoot") && _canShoot)
        {
            Bullet bullet = _bulletScene.Instantiate<Bullet>();
            bullet.Position = _currentBulletSpawn.GlobalPosition;
            bullet.Rotation = _currentBulletSpawn.GlobalRotation;
            GetTree().CurrentScene.AddChild(bullet);
            _canShoot = false;
            _currentBulletSpawn = _currentBulletSpawn == _bulletSpawnLeft ? _bulletSpawnRight : _bulletSpawnLeft;
            _shootCooldownTimer.Start();
        }
    }

    private void OnShootCooldownTimerTimeout()
    {
        _canShoot = true;
    }
}
