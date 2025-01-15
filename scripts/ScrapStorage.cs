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
        if (amount >= _scrap)
        {
            EmitSignal(_scrap <= 0 ? SignalName.OnScrapFinalBlow : SignalName.OnScrapDepleted);
        }

        int adjustedAmount = Mathf.Min(amount, _scrap);
        _scrap -= adjustedAmount;

        if (causer is Machine machine)
        {
            SpawnScrap(machine.ScrapStorage, adjustedAmount);
        }

        UpdateScrapText();
    }

    private void SpawnScrap(ScrapStorage target, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnScrap(target);
        }
    }

    private void SpawnScrap(ScrapStorage target)
    {
        Scrap scrap = ScrapScene.Instantiate<Scrap>();
        scrap.GlobalPosition = GlobalPosition;
        //TODO: launch scrap
        scrap.TargetStorage = target;
        GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, scrap);
    }


    private void UpdateScrapText()
    {
        _scrapText.Text = _scrap.ToString();
    }
}
