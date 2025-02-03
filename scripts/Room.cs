using Godot;
using minions.scripts.entities;
using minions.scripts.globals;

namespace minions.scripts;

public partial class Room : Node2D
{
    private PackedScene _machineScene =
        ResourceLoader.Load<PackedScene>("res://scenes/entities/minion.tscn");
    [Export] private Sprite2D _background;
    [Export] private Texture2D _dayTexture;
    [Export] private Texture2D _duskTexture;
    [Export] private Texture2D _nightTexture;

    [Export] private GpuParticles2D _cloudParticles;
    

    [Export] private EnemyManager _enemyManager;
    [Export] private Node2D _machineSpawnPoint;
    [Export] private float _spawnTime;

    [Export] private Label _roomNumberLabel;

    public override void _Ready()
    {
        base._Ready();
        
        SetupBackground();
        SetupClouds();

        _roomNumberLabel.Text = CurrentRunDataGlobal.Instance.RoomNumber.ToString();

        _enemyManager.AllEnemiesDefeated += OnEnemyManagerAllEnemiesDefeated;

        SpawnBodies();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
    }

    private void SetupClouds()
    {
        _cloudParticles.SpeedScale = 100;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_cloudParticles, "speed_scale", 1, 5f);
    }

    private void SetupBackground()
    {
        int roomNumber = CurrentRunDataGlobal.Instance.RoomNumber;
        _background.Texture = roomNumber switch
        {
            <= 3 => _dayTexture,
            <= 6 => _duskTexture,
            _ => _nightTexture
        };
    }

    private async void SpawnBodies()
    {
        for (int i = 0; i < CurrentRunDataGlobal.Instance.FleetConfigurations.Count; i++)
        {
            SpawnBodyFromConfiguration(i);
            await ToSignal(GetTree().CreateTimer(_spawnTime), SceneTreeTimer.SignalName.Timeout);
        }
    }

    private void SpawnBodyFromConfiguration(int index)
    {
        //TODO: move to minion spawner class
        Minion body = _machineScene.Instantiate<Minion>();
        body.Position = _machineSpawnPoint.Position;
        body.FleetIndex = index;
        AddChild(body);
    }

    private void OnEnemyManagerAllEnemiesDefeated()
    {
        AdvanceToNextRoom();
    }

    private void AdvanceToNextRoom()
    {
        CurrentRunDataGlobal.Instance.RoomNumber++;
        SceneManager.Instance.ChangeScene(SceneManager.Instance.RoomScene);
    }
}