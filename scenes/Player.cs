using Godot;
using System;
using minions.scripts.components.core;

public partial class Player : CharacterBody2D
{
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
}
