using System.Collections.Generic;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;
using minions.scripts.entities;
using minions.scripts.globals;

namespace minions.scripts;

public partial class EnemyManager : Node2D
{
    [Signal]
    public delegate void AllEnemiesDefeatedEventHandler();

    [Export] private Timer _spawnTimer;
    [Export] private float _spawnTime;
    [Export] private Array<Node2D> _spawnPoints = new();

    private int _spawnPointIndex;
    private int _numEnemiesToSpawn;
    private int _numSpawnedEnemies;
    private HashSet<Enemy> _enemies = new();

    public override void _Ready()
    {
        base._Ready();
        _numEnemiesToSpawn = CalculateNumberOfEnemiesToSpawn();
        
        _spawnTimer.WaitTime = _spawnTime;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.OneShot = true;
        _spawnTimer.Start();
    }
    
    private int CalculateNumberOfEnemiesToSpawn()
    {
        //TODO: randomize based on room #
        return 3;
    }

    private void OnSpawnTimerTimeout()
    {
        if (_numSpawnedEnemies < _numEnemiesToSpawn)
        {
            SpawnEnemy();
            _spawnTimer.Start();
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = SceneManager.Instance.EnemyScene.Instantiate<Enemy>();
        enemy.Died += () => OnEnemyDied(enemy);
        _enemies.Add(enemy);
        enemy.Position = _spawnPoints[_spawnPointIndex++].Position;
        enemy.SetComponentsFromConfiguration(GetConfiguration());
        GetTree().CurrentScene.AddChild(enemy);
        _numSpawnedEnemies++;
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

    private void OnEnemyDied(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            EmitSignal(SignalName.AllEnemiesDefeated);
        }
    }

}
