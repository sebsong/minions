using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace prototype_minions.scripts.components.core;

public static class ComponentUtils
{
    private const string ComponentsPath = "res://scenes/components";
    private const string MovementPath = ComponentsPath + "/movement";
    private const string AttackPath = ComponentsPath + "/attack";

    /* Movement Scenes */
    private static readonly PackedScene RandomMovementScene = LoadScene(MovementPath, "random_movement.tscn");

    private static readonly PackedScene
        TargetEnemyMovementScene = LoadScene(MovementPath, "target_enemy_movement.tscn");

    private static readonly PackedScene FollowALlyMovementScene = LoadScene(MovementPath, "follow_ally_movement.tscn");

    /* Attack Scenes */
    private static readonly PackedScene ContactAttackScene = LoadScene(AttackPath, "contact_attack.tscn");
    private static readonly PackedScene AreaAttackScene = LoadScene(AttackPath, "area_attack.tscn");

    /* Movement Defaults */
    public const float DefaultMovementSpeed = 100f;

    /* Attack Defaults */
    public const int DefaultDamage = 1;
    public const float DefaultAttackCooldown = 1f;

    public enum ComponentType
    {
        /** Movement Component Types **/
        RandomMovement,
        TargetEnemyMovement,
        FollowAllyMovement,

        /** Attack Component Types **/
        ContactAttack,
        AreaAttack,
    }

    public static readonly List<ComponentType> MovementComponentTypes = new()
    {
        ComponentType.RandomMovement,
        ComponentType.TargetEnemyMovement,
        ComponentType.FollowAllyMovement,
    };

    public static readonly List<ComponentType> AttackComponentTypes = new()
    {
        ComponentType.ContactAttack,
        ComponentType.AreaAttack,
    };

    private static readonly Godot.Collections.Dictionary<ComponentType, PackedScene> TypeToScene = new()
    {
        // Movement Component Types
        { ComponentType.RandomMovement, RandomMovementScene },
        { ComponentType.TargetEnemyMovement, TargetEnemyMovementScene },
        { ComponentType.FollowAllyMovement, FollowALlyMovementScene },

        // Attack Component Types
        { ComponentType.ContactAttack, ContactAttackScene },
        { ComponentType.AreaAttack, AreaAttackScene },
    };

    public static T AttachComponent<T>(Node2D owner, ComponentType componentType) where T : Component
    {
        ClearComponents<T>(owner);

        PackedScene componentScene = TypeToScene[componentType];
        T component = componentScene.Instantiate<T>();
        owner.AddChild(component);
        return component;
    }

    private static void ClearComponents<T>(Node2D owner)
    {
        foreach (var node in owner.GetChildren())
        {
            if (node is T)
            {
                owner.RemoveChild(node);
            }
        }
    }

    private static PackedScene LoadScene(string pathPrefix, string sceneFileName)
    {
        return ResourceLoader.Load<PackedScene>($"{pathPrefix}/{sceneFileName}");
    }
}
