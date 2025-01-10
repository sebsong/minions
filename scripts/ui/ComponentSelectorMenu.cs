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
    public Button RemoveMachineButton;

    private Dictionary<ComponentUtils.ComponentCategory, ComponentSelector> _componentSelectors = new();

    public override void _Ready()
    {
        base._Ready();

        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            ComponentSelector selector = ComponentSelectorScene.Instantiate<ComponentSelector>();
            selector.Category = category;
            _componentSelectors[category] = selector;
            _componentSelectorContainer.AddChild(selector);
        }
    }


    public ComponentConfiguration GetConfiguration()
    {
        Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> configurationDict = new();
        foreach (var (category, selector) in _componentSelectors)
        {
            if (selector.SelectedComponentButton != null)
            {
                configurationDict[category] = selector.SelectedComponentButton.Type;
            }
        }

        return new ComponentConfiguration(configurationDict);
    }
}
