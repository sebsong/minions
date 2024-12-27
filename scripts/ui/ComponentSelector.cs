using Godot;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentSelector : Control
{
    private static readonly PackedScene ComponentButtonScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/component_button.tscn");

    [Signal]
    public delegate void ComponentSelectedEventHandler(ComponentUtils.ComponentType type);

    [Export] private Label _categoryTitle;
    [Export] private VBoxContainer _componentOptions;

    public ComponentUtils.ComponentCategory Category;

    public ComponentButton SelectedComponentButton { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        _categoryTitle.Text = Category.ToString();
        foreach (ComponentUtils.ComponentType type in ComponentUtils.ComponentCategoryToType[Category])
        {
            ComponentButton button = ComponentButtonScene.Instantiate<ComponentButton>();
            button.Type = type;
            button.Pressed += () => OnComponentButtonPressed(button);
            _componentOptions.AddChild(button);
        }
    }

    private void OnComponentButtonPressed(ComponentButton button)
    {
        SelectedComponentButton?.SetPressed(false);
        SelectedComponentButton = button;
    }
}
