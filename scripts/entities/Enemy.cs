using minions.scripts.components.core;

namespace minions.scripts.entities;

public partial class Enemy : ComponentControlledBody
{
    public override void _Ready()
    {
        // base._Ready();
        SetComponentsFromConfiguration(new ComponentConfiguration(
            ComponentUtils.ComponentType.RandomBehavior,
            ComponentUtils.ComponentType.GlideMovement,
            ComponentUtils.ComponentType.ContactAttack,
            ComponentUtils.ComponentType.BasicDefense
        ));
    }
}
