using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace minions.scripts.components.core;

public struct ComponentSelection
{
    public Godot.Collections.Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> Selection;

    public ComponentSelection(Godot.Collections.Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> selection)
    {
        _validateSelection(selection);
        Selection = selection;
    }

    private static void _validateSelection(Godot.Collections.Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType> selection)
    {
        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            Debug.Assert(selection.ContainsKey(category), $"Missing selection for component category: {category}.");
        }

        foreach (var (category, type) in selection)
        {
            ICollection<ComponentUtils.ComponentType> categoryTypes = ComponentUtils.ComponentCategoryToType[category];
            Debug.Assert(categoryTypes.Contains(type), $"Component type {type} does not belong to component category {category}.");
        }
    }
}
