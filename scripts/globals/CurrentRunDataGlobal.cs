using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.globals;

public partial class CurrentRunDataGlobal : Node
{
    public static CurrentRunDataGlobal Instance;

    public int Scrap;

    public List<ComponentConfiguration> FleetConfigurations { get; } = new();

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void AddConfiguration(ComponentConfiguration componentConfiguration)
    {
        Debug.Assert(
            FleetConfigurations.Count < SaveDataGlobal.Instance.MaxFleetSize,
            $"Too many configurations: {FleetConfigurations.Count} for the max fleet size: {SaveDataGlobal.Instance.MaxFleetSize}"
        );
        FleetConfigurations.Add(componentConfiguration);
    }
}
