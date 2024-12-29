using Godot;
using minions.scripts.components.core;
using minions.scripts.components.movement;

namespace minions.scripts;

public partial class CurrentSelectionGlobal : Node
{
    public static CurrentSelectionGlobal Instance;

    private ComponentSelection _currentPlayerSelection = new()
    {
        { ComponentUtils.ComponentCategory.Movement, ComponentUtils.ComponentType.PlayerMovement },
        { ComponentUtils.ComponentCategory.Attack, ComponentUtils.ComponentType.PlayerAttack },
        { ComponentUtils.ComponentCategory.Defense, ComponentUtils.ComponentType.BasicDefense },
    };

    public ComponentSelection CurrentPlayerSelection
    {
        get => _currentPlayerSelection;
        set
        {
            value.ValidateSelection();
            _currentPlayerSelection = value;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }
}
