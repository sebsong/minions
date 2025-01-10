using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts.globals;

public partial class RunGlobal : Node
{
    public static RunGlobal Instance;

    public int Scrap;

    public int MaxFleetSize = 9; // TODO: get this from save file

    public List<ComponentConfiguration> FleetConfigurations { get; } = new();

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void AddConfiguration(ComponentConfiguration componentConfiguration)
    {
        Debug.Assert(
            FleetConfigurations.Count < MaxFleetSize,
            $"Too many configurations: {FleetConfigurations.Count} for the max fleet size: {MaxFleetSize}"
        );
        FleetConfigurations.Add(componentConfiguration);
    }
}
