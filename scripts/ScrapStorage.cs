using Godot;

namespace minions.scripts;

public partial class ScrapStorage : Node2D
{
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

    public void ModifyScrap(int scrapUpdate)
    {
        int updatedScrap = _scrap + scrapUpdate;
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

        UpdateScrapText();
    }

    private void UpdateScrapText()
    {
        _scrapText.Text = _scrap.ToString();
    }
}
