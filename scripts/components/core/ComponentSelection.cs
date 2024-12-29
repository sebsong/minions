using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace minions.scripts.components.core;

public class ComponentSelection : Dictionary<ComponentUtils.ComponentCategory, ComponentUtils.ComponentType>
{
    public void ValidateSelection()
    {
        foreach (ComponentUtils.ComponentCategory category in Enum.GetValues(typeof(ComponentUtils.ComponentCategory)))
        {
            Debug.Assert(ContainsKey(category), $"Missing selection for component category: {category}.");
        }

        foreach (var (category, type) in this)
        {
            ICollection<ComponentUtils.ComponentType> categoryTypes = ComponentUtils.ComponentCategoryToType[category];
            Debug.Assert(categoryTypes.Contains(type), $"Component type {type} does not belong to component category {category}.");
        }
    }
}
