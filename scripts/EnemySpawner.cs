using System.Collections.Generic;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts;

public partial class EnemySpawner : Node2D
{
    private PackedScene _enemyScene = ResourceLoader.Load<PackedScene>("res://scenes/entities/enemy.tscn");

    [Export] private Timer _spawnTimer;
    [Export] private float _spawnTime;
    [Export] private Array<Node2D> _spawnPoints = new();

    private int _spawnPointIndex;
    private int _totalNumEnemies;
    private HashSet<Enemy> _enemies = new();

    public override void _Ready()
    {
        base._Ready();
        _totalNumEnemies = GetNumberOfEnemies();
        _spawnTimer.WaitTime = _spawnTime;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.OneShot = true;
        _spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        if (_enemies.Count < _totalNumEnemies)
        {
            SpawnEnemy();
            _spawnTimer.Start();
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyScene.Instantiate<Enemy>();
        _enemies.Add(enemy);
        enemy.Position = _spawnPoints[_spawnPointIndex++].Position;
        enemy.SetComponentsFromConfiguration(GetConfiguration());
        GetTree().CurrentScene.AddChild(enemy);
    }

    private ComponentConfiguration GetConfiguration()
    {
        //TODO: use weights based on current room # and other factors
        return new ComponentConfiguration(
            ComponentUtils.ComponentCategoryToType[ComponentUtils.ComponentCategory.Behavior].PickRandom(),
            ComponentUtils.ComponentCategoryToType[ComponentUtils.ComponentCategory.Movement].PickRandom(),
            ComponentUtils.ComponentCategoryToType[ComponentUtils.ComponentCategory.Attack].PickRandom(),
            ComponentUtils.ComponentCategoryToType[ComponentUtils.ComponentCategory.Defense].PickRandom()
        );
    }

    private int GetNumberOfEnemies()
    {
        //TODO: randomize based on room #
        return 3;
    }
}