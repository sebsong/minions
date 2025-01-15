using Godot;
using minions.scripts.components.core;
using minions.scripts.entities;

namespace minions.scripts;

public partial class ScrapStorage : Node2D
{
    private static readonly PackedScene ScrapScene =
        ResourceLoader.Load<PackedScene>("res://scenes/entities/scrap.tscn");

    [Signal]
    public delegate void OnScrapDepletedEventHandler();

    [Signal]
    public delegate void OnScrapFinalBlowEventHandler();

    [Export] private Label _scrapText;

    private int _scrap = 10; // TODO: default this somewhere else

    public override void _Ready()
    {
        base._Ready();
        UpdateScrapText();
    }

    public void AddScrap(int amount)
    {
        _scrap += amount;
        UpdateScrapText();
    }

    public void RemoveScrap(int amount, Node2D causer)
    {
        int updatedScrap = _scrap - amount;
        if (updatedScrap <= 0)
        {
            if (_scrap <= 0)
            {
                EmitSignal(SignalName.OnScrapFinalBlow);
            }
            else
            {
                _scrap = 0;
                EmitSignal(SignalName.OnScrapDepleted);
            }
        }
        else
        {
            _scrap = updatedScrap;
        }

        if (causer is Machine machine)
        {
            SpawnScrap(machine.ScrapStorage);
        }
        UpdateScrapText();
    }

    private void SpawnScrap(ScrapStorage target)
    {
        Scrap scrap = ScrapScene.Instantiate<Scrap>();
        scrap.Position = Position;
        //TODO: launch scrap
        scrap.TargetStorage = target;
        GetTree().GetRoot().CallDeferred(Node.MethodName.AddChild, scrap);
    }


    private void UpdateScrapText()
    {
        _scrapText.Text = _scrap.ToString();
    }
}
