using System;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.ui;

public partial class ComponentSelectorOld : Control
{
    [Export] private OptionButton _movementOptions;
    [Export] private OptionButton _attackOptions;

    private Minion _minion;

    public override void _Ready()
    {
        base._Ready();

        _registerOptions();
    }

    public void RegisterMinion(Minion minion)
    {
        _minion = minion;
        UpdateMinionMovementComponent(_movementOptions.Selected);
        UpdateMinionAttackComponent(_attackOptions.Selected);
    }

    public void UpdateMinionMovementComponent(int index)
    {
        _minion.SetMovementComponent((ComponentUtils.ComponentType)_movementOptions.GetSelectedId());
    }

    public void UpdateMinionAttackComponent(int index)
    {
        _minion.SetAttackComponent((ComponentUtils.ComponentType)_attackOptions.GetSelectedId());
    }

    private void _registerOptions()
    {
        foreach (var componentType in ComponentUtils.MovementComponentTypes)
        {
            _movementOptions.AddItem(Enum.GetName(componentType), (int)componentType);
        }
        foreach (var componentType in ComponentUtils.AttackComponentTypes)
        {
            _attackOptions.AddItem(Enum.GetName(componentType), (int)componentType);
        }

        _movementOptions.Selected = 1;
        _attackOptions.Selected = 1;
    }
}
