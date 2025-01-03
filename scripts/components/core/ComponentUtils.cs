using System.Collections.Generic;
using Godot;

namespace minions.scripts.components.core;

public static class ComponentUtils
{
    private const string ComponentsPath = "res://scenes/components";
    private const string BehaviorPath = ComponentsPath + "/behavior";
    private const string MovementPath = ComponentsPath + "/movement";
    private const string AttackPath = ComponentsPath + "/attack";
    private const string DefensePath = ComponentsPath + "/defense";

    /* Behavior Scenes */
    private static readonly PackedScene PlayerBehaviorScene = LoadScene(BehaviorPath, "player_behavior.tscn");
    private static readonly PackedScene RandomBehaviorScene = LoadScene(BehaviorPath, "random_behavior.tscn");
    private static readonly PackedScene TargetEnemyBehaviorScene = LoadScene(BehaviorPath, "target_enemy_behavior.tscn");
    private static readonly PackedScene FollowALlyBehaviorScene = LoadScene(BehaviorPath, "follow_ally_behavior.tscn");

    /* Movement Scenes */
    private static readonly PackedScene GlideMovementScene = LoadScene(MovementPath, "glide_movement.tscn");

    /* Attack Scenes */
    private static readonly PackedScene ContactAttackScene = LoadScene(AttackPath, "contact_attack.tscn");
    private static readonly PackedScene AreaAttackScene = LoadScene(AttackPath, "area_attack.tscn");
    private static readonly PackedScene MachineGunAttackScene = LoadScene(AttackPath, "machine_gun_attack.tscn");

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

    public enum ComponentCategory
    {
        Behavior,
        Movement,
        Attack,
        Defense
    }

    public enum ComponentType
    {
        /** Behavior Component Types **/
        PlayerBehavior,
        RandomBehavior,
        TargetEnemyBehavior,
        FollowAllyBehavior,

        /** Movement Component Types **/
        GlideMovement,
        // HoverMovement
        // BoostMovement

        /** Attack Component Types **/
        MachineGunAttack,
        ContactAttack,
        AreaAttack,

        /** Defense Component Types **/
        BasicDefense,
        InvincibleDefense,
    }

    private static readonly List<ComponentType> BehaviorComponentTypes = new()
    {
        ComponentType.PlayerBehavior,
        ComponentType.RandomBehavior,
        ComponentType.TargetEnemyBehavior,
        ComponentType.FollowAllyBehavior,
    };

    private static readonly List<ComponentType> MovementComponentTypes = new()
    {
        ComponentType.GlideMovement,
    };

    private static readonly List<ComponentType> AttackComponentTypes = new()
    {
        ComponentType.ContactAttack,
        ComponentType.AreaAttack,
        ComponentType.MachineGunAttack,
    };

    private static readonly List<ComponentType> DefenseComponentTypes = new()
    {
        ComponentType.BasicDefense,
        ComponentType.InvincibleDefense
    };

    public static readonly Dictionary<ComponentCategory, List<ComponentType>> ComponentCategoryToType = new()
    {
        {ComponentCategory.Behavior, BehaviorComponentTypes},
        {ComponentCategory.Movement, MovementComponentTypes},
        {ComponentCategory.Attack, AttackComponentTypes},
        {ComponentCategory.Defense, DefenseComponentTypes},
    };

    private static readonly Dictionary<ComponentType, PackedScene> TypeToScene = new()
    {
        // Behavior Component Types
        { ComponentType.PlayerBehavior, PlayerBehaviorScene },
        { ComponentType.RandomBehavior, RandomBehaviorScene },
        { ComponentType.TargetEnemyBehavior, TargetEnemyBehaviorScene },
        { ComponentType.FollowAllyBehavior, FollowALlyBehaviorScene },

        // Movement Component Types
        { ComponentType.GlideMovement, GlideMovementScene },

        // Attack Component Types
        { ComponentType.ContactAttack, ContactAttackScene },
        { ComponentType.AreaAttack, AreaAttackScene },
        { ComponentType.MachineGunAttack, MachineGunAttackScene },

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
