using System;
using Godot;
using Godot.Collections;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentSelectorMenu : Control
{
    private static readonly PackedScene ComponentSelectorScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/component_selector.tscn");

    [Export]
    private HBoxContainer _componentSelectorContainer;
    [Export]
    private Button _confirmButton;

    private Dictionary<ComponentUtils.ComponentCategory, ComponentSelector> _componentSelectors = new();

    public override void _Ready()
    {
        base._Ready();

        _confirmButton.Pressed += OnConfirmButtonPressed;

        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            ComponentSelector selector = ComponentSelectorScene.Instantiate<ComponentSelector>();
            selector.Category = category;
            _componentSelectors[category] = selector;
            _componentSelectorContainer.AddChild(selector);
        }
    }

    private void OnConfirmButtonPressed()
    {
        foreach (var (category, type) in GetSelection().Selection)
        {
            GD.Print($"SELECTION: {category} - {type}");
        }
    }

    private ComponentSelection GetSelection()
    {
        Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> selection = new();
        foreach (var (category, selector) in _componentSelectors)
        {
            selection[category] = selector.SelectedComponentButton.Type;
        }

        return new ComponentSelection(selection);
    }
}
