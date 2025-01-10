using System.Collections.Generic;
using Godot;

namespace minions.scripts.ui;

public partial class FleetManagementMenu : Control
{
    private static readonly PackedScene ComponentSelectorMenuScene =
        ResourceLoader.Load<PackedScene>("res://scenes/ui/component_selector_menu.tscn");

    [Export] private Control _selectorMenusContainer;

    [Export] private Button _addMachineButton;
    [Export] private Button _confirmButton;

    private readonly HashSet<ComponentSelectorMenu> _componentSelectorMenus = new();

    public override void _Ready()
    {
        base._Ready();
        _addMachineButton.Pressed += OnAddMachineButtonPressed;
        _confirmButton.Pressed += OnConfirmButtonPressed;
        OnAddMachineButtonPressed();
    }

    private void OnAddMachineButtonPressed()
    {
        ComponentSelectorMenu menu = ComponentSelectorMenuScene.Instantiate<ComponentSelectorMenu>();
        menu.RemoveMachineButton.Pressed += () => OnRemoveMachineButtonPressed(menu);
        _componentSelectorMenus.Add(menu);
        _selectorMenusContainer.AddChild(menu);
    }

    private void OnRemoveMachineButtonPressed(ComponentSelectorMenu menu)
    {
        _componentSelectorMenus.Remove(menu);
        _selectorMenusContainer.RemoveChild(menu);
    }

    private void OnConfirmButtonPressed()
    {
        foreach (ComponentSelectorMenu menu in _componentSelectorMenus)
        {
            RunGlobal.Instance.AddConfiguration(menu.GetConfiguration());
        }
        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }
}
