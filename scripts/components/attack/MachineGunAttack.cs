using System.Collections.Generic;
using Godot;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts.components.attack;

public partial class MachineGunAttack : AttackComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.MachineGunAttack;

    [Export] private PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/entities/bullet.tscn");
    [Export] private Timer _shootCooldownTimer;

    [Export] private float _maxBulletSpread;

    private RandomNumberGenerator _rng = new();
    private bool _canShoot = true;
    private readonly List<Node2D> _bulletSpawns = new();
    private int _currentBulletSpawnIndex = -1;

    public override void _Ready()
    {
        base._Ready();
        _shootCooldownTimer.Timeout += OnShootCooldownTimerTimeout;
        _shootCooldownTimer.WaitTime = AttackCooldown;
        foreach (var child in GetParent().GetChildren())
        {
            if (child.IsInGroup("bulletSpawns"))
            {
                _bulletSpawns.Add((Node2D)child);
            }
        }

        if (_bulletSpawns.Count > 0)
        {
            _currentBulletSpawnIndex = 0;
        }
    }

    public override void Attack()
    {
        if (_canShoot && _currentBulletSpawnIndex != -1)
        {
            Bullet bullet = _bulletScene.Instantiate<Bullet>();
            bullet.Speed = AttackSpeed;
            bullet.Damage = AttackDamage;
            Node2D bulletSpawn = _bulletSpawns[_currentBulletSpawnIndex];
            bullet.Position = bulletSpawn.GlobalPosition;
            float randomBulletSpread = _rng.RandfRange(-_maxBulletSpread, _maxBulletSpread);
            bullet.Rotation = bulletSpawn.GlobalRotation + randomBulletSpread;
            GetTree().CurrentScene.AddChild(bullet);

            _canShoot = false;
            _shootCooldownTimer.Start();
            _currentBulletSpawnIndex = (_currentBulletSpawnIndex + 1) % _bulletSpawns.Count;
        }
    }

    public override void OnCollision(KinematicCollision2D collision)
    {
    }

    private void OnShootCooldownTimerTimeout()
    {
        _canShoot = true;
    }
}
