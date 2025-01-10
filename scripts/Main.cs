using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class Main : Node2D
{
    private PackedScene _componentControlledBodyScene =
        ResourceLoader.Load<PackedScene>("res://scenes/entities/player.tscn");

    [Export] private GpuParticles2D _cloudParticles;

    [Export] private Node2D _machineSpawnPoint;
    [Export] private float _spawnTime;

    public override void _Ready()
    {
        base._Ready();

        _cloudParticles.SpeedScale = 100;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_cloudParticles, "speed_scale", 1, 5f);
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

    private async void SpawnBodies()
    {
        for (int i = 0; i < RunGlobal.Instance.FleetConfigurations.Count; i++)
        {
            SpawnBodyFromConfiguration(i);
            await ToSignal(GetTree().CreateTimer(_spawnTime), SceneTreeTimer.SignalName.Timeout);
        }
    }

    private void SpawnBodyFromConfiguration(int index)
    {
        ComponentControlledBody body = _componentControlledBodyScene.Instantiate<ComponentControlledBody>();
        body.Position = _machineSpawnPoint.Position;
        body.Index = index;
        AddChild(body);
    }
}
