using minions.scripts.components.core;
using minions.scripts.globals;

namespace minions.scripts.entities;

public partial class Minion : Machine
{
    public override void _Ready()
    {
        base._Ready();
        AddToGroup("minions");
        SetComponentsFromConfiguration(
            CurrentRunDataGlobal.Instance.FleetConfigurations[FleetIndex]
        );
    }
}
