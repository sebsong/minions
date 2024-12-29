using minions.scripts.components.core;

namespace minions.scripts.entities;

public partial class Player : ComponentControlledBody
{
    public override void _Ready()
    {
        base._Ready();
        SetComponentsFromSelection(CurrentSelectionGlobal.Instance.CurrentPlayerSelection);
    }
}
