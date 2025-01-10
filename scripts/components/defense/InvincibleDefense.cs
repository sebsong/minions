using System;
using minions.scripts.components.core;

namespace minions.scripts.components.defense;

public partial class InvincibleDefense : DefenseComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.InvincibleDefense;
    public override int ResolveDamage(int amount)
    {
        return 0;
    }
}
