using minions.scripts.components.core;

namespace minions.scripts.components.defense;

public partial class BasicDefense : DefenseComponent
{
    public override ComponentUtils.ComponentType ComponentType => ComponentUtils.ComponentType.BasicDefense;

    public override int ResolveDamage(int amount)
    {
        return amount;
    }


}
