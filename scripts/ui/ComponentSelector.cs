using Godot;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentSelector : Control
{
    private static readonly PackedScene ComponentButtonScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/component_button.tscn");
    [Export] private Label _categoryTitle;
    [Export] private VBoxContainer _componentOptions;

    public ComponentUtils.ComponentCategory Category;

    public override void _Ready()
    {
        base._Ready();
        _categoryTitle.Text = Category.ToString();
        foreach (ComponentUtils.ComponentType type in ComponentUtils.ComponentCategoryToType[Category])
        {
            ComponentButton button = ComponentButtonScene.Instantiate<ComponentButton>();
            button.Type = type;
            _componentOptions.AddChild(button);
        }
    }
}
