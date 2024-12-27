using System;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentSelectorMenu : HBoxContainer
{
    private static readonly PackedScene ComponentSelectorScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/component_selector.tscn");

    public override void _Ready()
    {
        base._Ready();

        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            ComponentSelector selector = ComponentSelectorScene.Instantiate<ComponentSelector>();
            selector.Category = category;
            AddChild(selector);
        }
    }
}
