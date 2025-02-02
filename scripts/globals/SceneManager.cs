using Godot;

namespace minions.scripts.globals;

public partial class SceneManager : Node2D
{
    [ExportCategory("Levels")] [Export] public PackedScene RoomScene;

    [ExportCategory("Entities")] [Export] public PackedScene EnemyScene;
    [Export] public PackedScene BulletScene;

    public static SceneManager Instance;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void ChangeScene(PackedScene scene)
    {
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToPacked, scene);
    }
}
