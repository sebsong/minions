using System;
using System.Diagnostics;
using Godot.Collections;

namespace minions.scripts.components.core;

public class ComponentSelection
{
    public readonly ComponentUtils.ComponentType BehaviorComponentType;
    public readonly ComponentUtils.ComponentType MovementComponentType;
    public readonly ComponentUtils.ComponentType AttackComponentType;
    public readonly ComponentUtils.ComponentType DefenseComponentType;

    public ComponentSelection(
        ComponentUtils.ComponentType behaviorComponentType,
        ComponentUtils.ComponentType movementComponentType,
        ComponentUtils.ComponentType attackComponentType,
        ComponentUtils.ComponentType defenseComponentType
    )
    {
        ValidateSelection(behaviorComponentType, movementComponentType, attackComponentType, defenseComponentType);
        BehaviorComponentType = behaviorComponentType;
        MovementComponentType = movementComponentType;
        AttackComponentType = attackComponentType;
        DefenseComponentType = defenseComponentType;
    }

    public ComponentSelection(Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> selectionDict)
    {
        ValidateSelectionDict(selectionDict);
        MovementComponentType = selectionDict[ComponentUtils.ComponentCategory.Movement];
        AttackComponentType = selectionDict[ComponentUtils.ComponentCategory.Attack];
        DefenseComponentType = selectionDict[ComponentUtils.ComponentCategory.Defense];
    }

    private static void ValidateSelection(
        ComponentUtils.ComponentType behaviorComponentType,
        ComponentUtils.ComponentType movementComponentType,
        ComponentUtils.ComponentType attackComponentType,
        ComponentUtils.ComponentType defenseComponentType
    )
    {
        ValidateTypeBelongsToCategory(behaviorComponentType, ComponentUtils.ComponentCategory.Behavior);
        ValidateTypeBelongsToCategory(movementComponentType, ComponentUtils.ComponentCategory.Movement);
        ValidateTypeBelongsToCategory(attackComponentType, ComponentUtils.ComponentCategory.Attack);
        ValidateTypeBelongsToCategory(defenseComponentType, ComponentUtils.ComponentCategory.Defense);
    }

    private static void ValidateSelectionDict(
        Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> selectionDict)
    {
        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            Debug.Assert(selectionDict.ContainsKey(category), $"Missing selection for component category: {category}.");
        }

        foreach (var (category, type) in selectionDict)
        {
            ValidateTypeBelongsToCategory(type, category);
        }
    }

    private static void ValidateTypeBelongsToCategory(
        ComponentUtils.ComponentType componentType,
        ComponentUtils.ComponentCategory category
    )
    {
        System.Collections.Generic.ICollection<ComponentUtils.ComponentType> validComponentTypes =
            ComponentUtils.ComponentCategoryToType[category];
        Debug.Assert(validComponentTypes.Contains(componentType),
            $"Component type {componentType} does not belong to component category {category}.");
    }
}
