using Godot;
using System;
using minions.scripts.components.core;

public partial class Player : CharacterBody2D
{
    [Export] private PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/Bullet.tscn");
    [Export] private Node2D _bulletSpawn;

    [Export] private float _speed = ComponentUtils.DefaultMovementSpeed;
    [Export] private float _turnSpeed = ComponentUtils.DefaultTurnSpeed;

    public override void _Ready()
    {
        base._Ready();
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
        if (Input.IsActionJustPressed("shoot"))
        {
            Bullet bullet = _bulletScene.Instantiate<Bullet>();
            bullet.Position = _bulletSpawn.GlobalPosition;
            bullet.Rotation = _bulletSpawn.GlobalRotation;
            GetTree().CurrentScene.AddChild(bullet);
        }
    }
}
