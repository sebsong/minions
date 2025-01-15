using Godot;

namespace minions.scripts.entities;

public partial class Scrap : Node2D
{
    [Export] private Area2D _area;
    [Export] private float _speed;

    public ScrapStorage TargetStorage;

    public override void _Ready()
    {
        base._Ready();
        _area.AreaEntered += OnAreaEntered;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (IsInstanceValid(TargetStorage))
        {
            Vector2 direction = GlobalPosition.DirectionTo(TargetStorage.GlobalPosition);
            GlobalPosition += direction * (float)(_speed * delta);
        }
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.GetOwner() is ScrapStorage storage && storage == TargetStorage)
        {
            storage.AddScrap(1);
            QueueFree();
        }
    }
}
