using System;
using System.Diagnostics;
using Godot.Collections;

namespace minions.scripts.components.core;

[Serializable]
public class ComponentConfiguration
{
    public ComponentUtils.ComponentType BehaviorComponentType;
    public ComponentUtils.ComponentType MovementComponentType;
    public ComponentUtils.ComponentType AttackComponentType;
    public ComponentUtils.ComponentType DefenseComponentType;

    public ComponentConfiguration()
    {
    }

    public ComponentConfiguration(
        ComponentUtils.ComponentType behaviorComponentType,
        ComponentUtils.ComponentType movementComponentType,
        ComponentUtils.ComponentType attackComponentType,
        ComponentUtils.ComponentType defenseComponentType
    )
    {
        ValidateConfiguration(behaviorComponentType, movementComponentType, attackComponentType, defenseComponentType);
        BehaviorComponentType = behaviorComponentType;
        MovementComponentType = movementComponentType;
        AttackComponentType = attackComponentType;
        DefenseComponentType = defenseComponentType;
    }

    public ComponentConfiguration(
        Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> configurationDict)
    {
        ValidateConfigurationDict(configurationDict);
        BehaviorComponentType = configurationDict[ComponentUtils.ComponentCategory.Behavior];
        MovementComponentType = configurationDict[ComponentUtils.ComponentCategory.Movement];
        AttackComponentType = configurationDict[ComponentUtils.ComponentCategory.Attack];
        DefenseComponentType = configurationDict[ComponentUtils.ComponentCategory.Defense];
    }

    public Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> ToDict()
    {
        return new()
        {
            { ComponentUtils.ComponentCategory.Behavior, BehaviorComponentType },
            { ComponentUtils.ComponentCategory.Movement, MovementComponentType },
            { ComponentUtils.ComponentCategory.Attack, AttackComponentType },
            { ComponentUtils.ComponentCategory.Defense, DefenseComponentType },
        };
    }

    private static void ValidateConfiguration(
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

    private static void ValidateConfigurationDict(
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
