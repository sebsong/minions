using System.Collections.Generic;
using Godot;
using minions.scripts.components.core;
using minions.scripts.globals;

namespace minions.scripts.ui;

public partial class FleetManagementMenu : Control
{
    private static readonly PackedScene ComponentSelectorMenuScene =
        ResourceLoader.Load<PackedScene>("res://scenes/ui/component_selector_menu.tscn");

    [Export] private Control _selectorMenusContainer;

    [Export] private Button _addMachineButton;
    [Export] private Button _confirmButton;
    [Export] private Button _saveButton;

    private readonly HashSet<ComponentSelectorMenu> _componentSelectorMenus = new();

    public override void _Ready()
    {
        base._Ready();
        _addMachineButton.Pressed += OnAddMachineButtonPressed;
        _confirmButton.Pressed += OnConfirmButtonPressed;
        _saveButton.Pressed += OnSaveButtonPressed;
        PopulateFromSaveFile();
    }

    private void OnAddMachineButtonPressed()
    {
        AddMenu();
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
            CurrentRunDataGlobal.Instance.AddConfiguration(menu.GetConfiguration());
        }

        GetTree().ChangeSceneToFile("res://scenes/room.tscn");
    }

    private void OnSaveButtonPressed()
    {
        List<ComponentConfiguration> configurations = new();
        foreach (ComponentSelectorMenu menu in _componentSelectorMenus)
        {
            configurations.Add(menu.GetConfiguration());
        }

        SaveDataGlobal.Instance.FleetConfigurations = configurations;
        SaveDataGlobal.SaveToFile();
    }

    private ComponentSelectorMenu AddMenu()
    {
        ComponentSelectorMenu menu = ComponentSelectorMenuScene.Instantiate<ComponentSelectorMenu>();
        menu.RemoveMachineButton.Pressed += () => OnRemoveMachineButtonPressed(menu);
        _componentSelectorMenus.Add(menu);
        _selectorMenusContainer.AddChild(menu);
        return menu;
    }

    private void PopulateFromSaveFile()
    {
        SaveDataGlobal.LoadFromFile();

        foreach (ComponentConfiguration config in SaveDataGlobal.Instance.FleetConfigurations)
        {
            ComponentSelectorMenu menu = AddMenu();
            menu.ApplyConfiguration(config);
        }
    }
}
