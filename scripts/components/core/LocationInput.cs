using Godot;

namespace minions.scripts.components.core;

public struct LocationInput
{
    public bool IsRelativeDirection;

    public bool IsGlobalLocation => !IsRelativeDirection;

    public Vector2 Location;

    public LocationInput(bool isRelativeDirection, Vector2 location)
    {
        IsRelativeDirection = isRelativeDirection;
        Location = location;
    }
}
