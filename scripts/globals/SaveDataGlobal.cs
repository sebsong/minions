using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Godot;
using minions.scripts.components.core;
using FileAccess = Godot.FileAccess;

namespace minions.scripts.globals;

[Serializable]
public partial class SaveDataGlobal : Node
{
    private static readonly string SaveFilePath = "user://save_data.save";
    private static readonly JsonSerializerOptions SerializationOptions = new() { IncludeFields = true };

    public static SaveData Instance;

    public class SaveData
    {
        public int Scrap;

        public int MaxFleetSize = 9;

        public List<ComponentConfiguration> FleetConfigurations = new();

        public void AddConfiguration(ComponentConfiguration componentConfiguration)
        {
            Debug.Assert(
                FleetConfigurations.Count < MaxFleetSize,
                $"Too many configurations: {FleetConfigurations.Count} for the max fleet size: {MaxFleetSize}"
            );
            FleetConfigurations.Add(componentConfiguration);
        }
    }

    public override void _Ready()
    {
        base._Ready();
        Instance ??= new();
    }

    public static void SaveToFile()
    {
        using FileAccess saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
        byte[] saveDataBytes = JsonSerializer.SerializeToUtf8Bytes(Instance, SerializationOptions);
        saveFile.Store32((uint)saveDataBytes.Length);
        saveFile.StoreBuffer(saveDataBytes);
    }

    public static void LoadFromFile()
    {
        if (!FileAccess.FileExists(SaveFilePath))
        {
            return;
        }

        using FileAccess saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);
        uint numBytes = saveFile.Get32();
        byte[] saveDataBytes = saveFile.GetBuffer(numBytes);
        Instance = JsonSerializer.Deserialize<SaveData>(saveDataBytes, SerializationOptions);
    }
}
