using System.Collections.Generic;
using Godot;

namespace minions.scripts.components.core;

public static class ComponentUtils
{
    private const string ComponentsPath = "res://scenes/components";
    private const string MovementPath = ComponentsPath + "/movement";
    private const string AttackPath = ComponentsPath + "/attack";
    private const string DefensePath = ComponentsPath + "/defense";

    /* Movement Scenes */
    private static readonly PackedScene PlayerMovementScene = LoadScene(MovementPath, "player_movement.tscn");
    private static readonly PackedScene RandomMovementScene = LoadScene(MovementPath, "random_movement.tscn");
    private static readonly PackedScene
        TargetEnemyMovementScene = LoadScene(MovementPath, "target_enemy_movement.tscn");
    private static readonly PackedScene FollowALlyMovementScene = LoadScene(MovementPath, "follow_ally_movement.tscn");

    /* Attack Scenes */
    private static readonly PackedScene PlayerAttackScene = LoadScene(AttackPath, "player_attack.tscn");
    private static readonly PackedScene ContactAttackScene = LoadScene(AttackPath, "contact_attack.tscn");
    private static readonly PackedScene AreaAttackScene = LoadScene(AttackPath, "area_attack.tscn");

    /* Defense Scenes */
    private static readonly PackedScene BasicDefenseScene = LoadScene(DefensePath, "basic_defense.tscn");
    private static readonly PackedScene InvincibleDefenseScene = LoadScene(DefensePath, "invincible_defense.tscn");

    /* Movement Defaults */
    public const float DefaultMovementSpeed = 100f;
    public const float DefaultTurnSpeed = 120f;

    /* Attack Defaults */
    public const int DefaultAttackDamage = 1;
    public const float DefaultAttackCooldown = 1f;
    public const float DefaultAttackSpeed = 750f;

    /* Defense Defaults */
    public const int DefaultMaxHealth = 10;

    public enum ComponentType
    {
        /** Movement Component Types **/
        PlayerMovement,
        RandomMovement,
        TargetEnemyMovement,
        FollowAllyMovement,

        /** Attack Component Types **/
        PlayerAttack,
        ContactAttack,
        AreaAttack,

        /** Defense Component Types **/
        BasicDefense,
        InvincibleDefense,
    }

    public static readonly List<ComponentType> MovementComponentTypes = new()
    {
        ComponentType.PlayerMovement,
        ComponentType.RandomMovement,
        ComponentType.TargetEnemyMovement,
        ComponentType.FollowAllyMovement,
    };

    public static readonly List<ComponentType> AttackComponentTypes = new()
    {
        ComponentType.PlayerAttack,
        ComponentType.ContactAttack,
        ComponentType.AreaAttack,
    };

    public static readonly List<ComponentType> DefenseComponentTypes = new()
    {
        ComponentType.BasicDefense,
        ComponentType.InvincibleDefense
    };

    private static readonly Dictionary<ComponentType, PackedScene> TypeToScene = new()
    {
        // Movement Component Types
        { ComponentType.PlayerMovement, PlayerMovementScene },
        { ComponentType.RandomMovement, RandomMovementScene },
        { ComponentType.TargetEnemyMovement, TargetEnemyMovementScene },
        { ComponentType.FollowAllyMovement, FollowALlyMovementScene },

        // Attack Component Types
        { ComponentType.PlayerAttack, PlayerAttackScene },
        { ComponentType.ContactAttack, ContactAttackScene },
        { ComponentType.AreaAttack, AreaAttackScene },

        // Defense Component Types
        { ComponentType.BasicDefense, BasicDefenseScene },
        { ComponentType.InvincibleDefense, InvincibleDefenseScene },
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
