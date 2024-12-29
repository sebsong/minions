using Godot;

namespace minions.scripts;

public partial class AudioManager : Node2D
{
    [Export] public AudioStreamPlayer2D BulletAudio;
    [Export] public AudioStreamPlayer2D HitAudio;
    [Export] public AudioStreamPlayer2D ExplodeAudio;

    public static AudioManager Instance;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }
}
