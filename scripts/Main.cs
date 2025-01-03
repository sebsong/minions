using Godot;

namespace minions.scripts;

public partial class Main : Node2D
{
    [Export] private GpuParticles2D _cloudParticles;

    public override void _Ready()
    {
        base._Ready();

        _cloudParticles.SpeedScale = 100;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(_cloudParticles, "speed_scale", 1, 5f);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
    }
}
