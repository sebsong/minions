using Godot;

namespace minions.scripts.globals;

public partial class AudioManagerGlobal : Node2D
{
    [Export] public AudioStreamPlayer2D BulletAudio;
    [Export] public AudioStreamPlayer2D HitAudio;
    [Export] public AudioStreamPlayer2D ExplodeAudio;

    public static AudioManagerGlobal Instance;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }
}
