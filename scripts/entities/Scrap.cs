using Godot;

namespace minions.scripts.entities;

public partial class Scrap : Node2D
{
    [Export] private Area2D _area;
    [Export] private float _speed;

    public ScrapStorage TargetStorage;

    public override void _Ready()
    {
        GD.Print("Scrap Ready");
        base._Ready();
        _area.AreaEntered += OnAreaEntered;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Vector2 direction = Position.DirectionTo(TargetStorage.Position);
        Position += direction * (float)(_speed * delta);
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.GetOwner() is ScrapStorage storage)
        {
            storage.AddScrap(1);
            QueueFree();
        }
    }
}
