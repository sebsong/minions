using Godot;
using minions.scripts.components.core;
using minions.scripts.globals;

namespace minions.scripts.entities;

public partial class Bullet : Node2D
{
	[Export] private Area2D _hitBox;
	[Export] private Sprite2D _bulletSprite;
	[Export] private GpuParticles2D _bulletExplosion;

	[Export] public float Speed = ComponentUtils.DefaultAttackSpeed;
	[Export] public int Damage = ComponentUtils.DefaultAttackDamage;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hitBox.BodyEntered += OnHitBoxBodyEntered;
		AudioManagerGlobal.Instance.BulletAudio.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Vector2.Right.Rotated(GetGlobalRotation()) * Speed * (float)delta;
	}

	private void OnHitBoxBodyEntered(Node2D body)
	{
		if (body is IDamageable damageable)
		{
			damageable.TakeDamage(Damage);
			Explode();
		}
	}

	private void Explode()
	{
		_bulletSprite.Hide();
		_bulletExplosion.Emitting = true;
	}
}
