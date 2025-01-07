using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using minions.scripts.components.core;

namespace minions.scripts;

public partial class FleetManagementGlobal : Node
{
    public static FleetManagementGlobal Instance;

    public int FleetSize { get; set; }

    public List<ComponentConfiguration> Configurations { get; } = new();

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void AddConfiguration(ComponentConfiguration componentConfiguration)
    {
        Debug.Assert(
            Configurations.Count < FleetSize,
            $"Too many configurations: {Configurations.Count} for the given fleet size: {FleetSize}"
        );
        Configurations.Add(componentConfiguration);
    }
}
