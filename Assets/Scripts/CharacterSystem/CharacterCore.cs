using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MonoBehaviour))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

/// <summary>
/// The Character Core Script Component is a glorified "character controller" with public 
/// actions, for use in a MENU environment. It can be adapted for a PC-UI environment
/// if needed to.
/// 
/// An API is exposed with the
/// <c>
/// public class CharacterActions {}
/// </c>
/// class for use with other scripts, items, and events.
/// </summary>

public class CharacterActions
{
    private CharacterCore core;

    public MovementActions Movement { get; private set; }
    public CombatActions Combat { get; private set; }
    public HealthActions Health { get; private set; }
    public ItemActions Items { get; private set; }

    public CharacterActions(CharacterCore core)
    {
        this.core = core;

        Movement = new MovementActions(core);
        Combat = new CombatActions(core);
        Health = new HealthActions(core);
        Items = new ItemActions(core);
    }

    public class MovementActions
    {
        private CharacterCore core;

        public MovementActions(CharacterCore core)
        {
            this.core = core;
        }

        public void ApproachNearestEnemy() => core.ApproachNearestEnemy();

        public void MeleeApproach() => core.MeleeApproach();

        public void RangedApproach() => core.RangedApproach();
        
        public void AerialApproach() => core.AerialApproach();

        public void SetTarget(Transform target) => core.setTarget(target);
    }

    public class CombatActions
    {
        private CharacterCore core;

        public CombatActions(CharacterCore core)
        {
            this.core = core;
        }

        public void SelectAttackTarget(CharacterCore targetCore) => core.SetAttackTarget(targetCore);

        public void BasicMeleeAttack() {
            // Grab all valid targets
            var targets = core.Targeting.GetActiveTargets();

            CharacterCore nearestEnemy = null;
            float minDist = float.MaxValue;
            Vector3 selfPos = core.transform.position;

            // Find the closest Enemy
            foreach (var t in targets) {
                if (t.Tag == CharacterSO.factions.Enemy) {
                    float dist = Vector3.Distance(selfPos, t.Position);
                    if (dist < minDist) {
                        minDist     = dist;
                        nearestEnemy = t.Core;
                    }
                }
            }

            if (nearestEnemy != null) {
                // Use Strength as damage (you can swap in any formula you like)
                float damage = core.Stats.Strength; //Temporary!! CHANGE TO DYNAMIC
                nearestEnemy.TakeDamage(damage);
                Debug.Log($"{core.characterData.characterName} BasicMeleeAttack → " +
                        $"{nearestEnemy.characterData.characterName} took {damage} damage!");
            }
            else {
                Debug.Log($"{core.characterData.characterName} BasicMeleeAttack found no enemies!");
            }
        }

        public void RangedAttack() {
            CharacterCore foe = core.AttackTarget ?? FindNearestEnemy();
            core.ClearAttackTarget();

            if (foe != null) {
                float damage = core.Stats.Strength;
                foe.TakeDamage(damage);
                Debug.Log($"{core.characterData.characterName} → {foe.characterData.characterName} took {damage} ranged dmg!");
            } else {
                Debug.LogWarning($"{core.characterData.characterName} RangedAttack: No target selected or found!");
            }
        }


        // Helper you can reuse for other actions too!
        private CharacterCore FindNearestEnemy() {
            var list = core.Targeting.GetActiveTargets();
            CharacterCore nearest = null;
            float minDist = float.MaxValue;
            Vector3 me = core.transform.position;

            foreach (var t in list) {
                if (t.Tag == CharacterSO.factions.Enemy) {
                    float d = Vector3.Distance(me, t.Position);
                    if (d < minDist) {
                        minDist = d;
                        nearest = t.Core;
                    }
                }
            }
            return nearest;
        }


        public void MeleeDodgeAttack() {

        }

        public void MeleeBlockAttack() {

        }

        public void MeleeStunAttack() {

        }

        public void MeleeKnockbackAttack() {

        }

        public void MeleeImmuneAttack() {

        }

        public void MeleeMultiAttack() {

        }

        public void MeleeJumpAttack() {

        }

        public void BasicRangedAttack() {
            
        }

        public void RangedDodgeAttack() {

        }

        public void RangedBlockAttack() {

        }

        public void RangedMultiAttack() {
            
        }


        public void SpecialAttack()
        {
            // TODO: Implement special attack
            Debug.Log("SpecialAttack() is not yet implemented!");
        }

        public void UltimateAttack()
        {
            // TODO: Implement ultimate attack
            Debug.Log("UltimateAttack() is not yet implemented!");
        }

        public void MultiAttack()
        {
            // TODO: Implement combo logic
            Debug.Log("MultiAttack() is not yet implemented!");
        }

    }

    public class HealthActions
    {
        private CharacterCore core;

        public HealthActions(CharacterCore core)
        {
            this.core = core;
        }

        public void TakeDamage(float amount) => core.TakeDamage(amount);

        public void Heal(float amount) => core.Heal(amount);
        
        public float Current => core.Health;
        
        public bool IsDead => core.isDead;
    }

    public class ItemActions
    {
        private CharacterCore core;

        public ItemActions(CharacterCore core)
        {
            this.core = core;
        }

        public void UseItem(string itemID)
        {
            // TODO: Add item usage logic
            Debug.Log($"Using item: {itemID} (not implemented yet!)");
        }

        public void EquipItem(string itemID)
        {
            // TODO: Add equipment logic
            Debug.Log($"Equipping item: {itemID} (not implemented yet!)");
        }

        public void UnequipItem(string itemID)
        {
            // TODO: Add unequip logic
            Debug.Log($"Unequipping item: {itemID} (not implemented yet!)");
        }
    }


}

public class CharacterStats
{
    private CharacterCore core;

    public StatusEffects Status { get; private set; }

    public CharacterStats(CharacterCore core)
    {
        this.core = core;

        Status = new StatusEffects(core);
    }

    public int Strength => core.Strength;
    public int Dexterity => core.Dexterity;
    public int Constitution => core.Constitution;
    public int Intelligence => core.Intelligence;
    public int Wisdom => core.Wisdom;
    public int Charisma => core.Charisma;

    public void ModifyStat(string statName, int delta)
    {
        switch (statName.ToLower())
        {
            case "strength":
                core.SetStrength(core.Strength + delta);
                break;
            case "dexterity":
                core.SetDexterity(core.Dexterity + delta);
                break;
            // add others as needed
            default:
                Debug.LogWarning($"Unknown stat: {statName}");
                break;
        }
    }

    public class StatusEffects
    {
        private CharacterCore core;

        public StatusEffects(CharacterCore core)
        {
            this.core = core;
        }

        public void ApplyEffect(string effect)
        {
            Debug.Log($"Applying status effect: {effect}");
            // TODO: Implement effect system
        }
    }
    
    public CharacterSO.factions Faction => core.characterData.faction;
}

public class CharacterCore : MonoBehaviour
{
    /* ------------------------------- API Setters ------------------------------ */
    public CharacterActions Actions { get; private set; }
    public CharacterStats Stats { get; private set; }
    public TargetingSystem Targeting { get; private set; }
    public CombatManager.CombatActionType CurrentActionType { get; private set; }
    public CharacterCore CurrentTarget { get; private set; }


    public void SetAttackTarget(CharacterCore targetCore) {
        attackTarget = targetCore;
    }
    public CharacterCore AttackTarget => attackTarget;
    public void ClearAttackTarget() => attackTarget = null;
    /* ------------------------ Public Variables Section ------------------------ */
    [Header("Core Configuration")]
    [Tooltip("Assign the Character Scriptable Object to configure this character.")]
    public CharacterSO characterData;

    [Header("Audio Configuration")]
    [Tooltip("These are the sounds that will be played by the character and their animations.")]
    public AudioClip[] soundClips;

    [Header("Animators")]
    [Tooltip("Handles transform-based states (Approach, Idle)")]
    [SerializeField] private Animator unitAnimator;
    [Tooltip("Handles pose states (Approaching, Shooting, Slide, Idle)")]
    [SerializeField] private Animator poseAnimator;

    [Header("Movement Configuration")]
    [Tooltip("These configs alter motion speeds.")]
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Approach Configuration")]
    [Tooltip("The approach speed whenever the Approach() is called.")]
    public Vector3 target = new Vector3(0, 0, 0); // TODO: Change this to a real "target"
    public float arrivalTime = 1;
    public float approachSpeed = 5;
    public float stopRadius = 0;


    public enum approaches {
        Melee,
        Ranged,
        Sky
    }

    // Character Stats - Read Access
    public int Strength => strength;
    public int Dexterity => dexterity;
    public int Constitution => constitution;
    public int Intelligence => intelligence;
    public int Wisdom => wisdom;
    public int Charisma => charisma;

    public void SetStrength(int value) => strength = Mathf.Clamp(value, 0, 999);
    public void SetDexterity(int value) => dexterity = Mathf.Clamp(value, 0, 999);
    public void SetConstitution(int value) => constitution = Mathf.Clamp(value, 0, 999);
    public void SetIntelligence(int value) => intelligence = Mathf.Clamp(value, 0, 999);
    public void SetWisdom(int value) => wisdom = Mathf.Clamp(value, 0, 999);
    public void SetCharisma(int value) => charisma = Mathf.Clamp(value, 0, 999);

    [Header("Faction Alignment")]
    [Tooltip("The faction this character belongs to (Friend/Enemy/Neutral)")]
    public CharacterSO.factions Faction => characterData.faction;


    /* ----------------------------- Private Section ---------------------------- */
    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody rb;

    private Vector3 movement;
    private bool isGrounded;
    private float lastMoveX = 0f;
    private float lastMoveZ = 0f;

    private bool isInCombat;
    public bool isDead = false;

    // Character stats
    private int strength, dexterity, constitution, intelligence, wisdom, charisma;

    // Health System
    [Header("Health System")]
    public float maxHealth; // Character's maximum health
    private float currentHealth; // Character's current health

    // Public Property for Hurtbox Access
    public float Health => currentHealth;

    // Tracks the character you explicitly chose to attack
    private CharacterCore attackTarget;
    
    /* --------------------------------- Methods -------------------------------- */
    private void Awake()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterCore: No CharacterSO assigned!", this);
            return;
        }

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        /*
        if (animator == null || audioSource == null || rb == null)
        {
            Debug.LogError("CharacterCore: Missing required components!", this);
            return;
        }
        */
        InitializeCharacter();
        InitializeAPI();
     }

    private void InitializeAPI() {
        Actions = new CharacterActions(this);
        Stats = new CharacterStats(this);
        Targeting = new TargetingSystem(this);
    }

    private void InitializeCharacter()
    {
        Debug.Log($"Initializing character: {characterData.characterName}");

        strength = characterData.strength;
        dexterity = characterData.dexterity;
        constitution = characterData.constitution;
        intelligence = characterData.intelligence;
        wisdom = characterData.wisdom;
        charisma = characterData.charisma;

        maxHealth = characterData.maxHP;
        currentHealth = maxHealth; // Set HP to full at start

        Debug.Log($"Stats - STR: {strength}, DEX: {dexterity}, CON: {constitution}, INT: {intelligence}, WIS: {wisdom}, CHA: {charisma}, HP: {currentHealth}/{maxHealth}");
    }

    void OnValidate()
    {
        // auto-assign if you forget in the Inspector
        if (unitAnimator == null) unitAnimator = GetComponent<Animator>();
        if (poseAnimator == null)
            poseAnimator = GetComponentInChildren<Animator>(true);
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;    
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure HP doesn't go below 0
        Debug.Log($"{characterData.characterName} took {damage} damage! Remaining HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"{characterData.characterName} healed {healAmount}! Current HP: {currentHealth}/{maxHealth}");
    }

    public void OnDeath()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{characterData.characterName} has died!");

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable movement and combat
        isInCombat = false;
        moveSpeed = 0;

        // Disable all colliders
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        // Disable all scripts except this one
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        // Play death sound if available
        if (soundClips.Length > 0)
        {
            audioSource.PlayOneShot(soundClips[0]);
        }

        // Destroy character after delay or trigger respawn logic
        Destroy(gameObject, 3f);
    }

    public void setTarget(Transform targetObject) {
        if (targetObject == null) return;
        target = targetObject.position;
    }

    public void ApproachNearestEnemy() {
            var targets = Targeting.GetActiveTargets();
            CharacterCore closest = null;
            float minDist = float.MaxValue;
            Vector3 selfPos = transform.position;

            foreach (var t in targets) {
                if (t.Tag == CharacterSO.factions.Enemy) {
                    float dist = Vector3.Distance(selfPos, t.Position);
                    if (dist < minDist) {
                        minDist = dist;
                        closest = t.Core;
                    }
                }
            }

        if (closest != null) {
            // Set their position as your approach target
            setTarget(closest.transform);
            MeleeApproach();
        }
        else {
            Debug.LogWarning($"{characterData.characterName} ApproachNearestEnemy: No enemies found!");
        }
    }
    
    public void MeleeApproach() {

        float meleeTime = 1;
        float meleeStopRadius = 5;

        Debug.Log("setAndRunApproach is being run!");
        StartCoroutine(Approach(meleeTime, target, meleeStopRadius));
    }

    public void RangedApproach() {

        float RangedTime = 2;
        float RangedStopRadius = 30;

        Debug.Log("setAndRunApproach is being run!");
        StartCoroutine(Approach(RangedTime, target, RangedStopRadius));
    }

    public void AerialApproach() {

        float AerialTime = 1;
        float AerialStopRadius = 5;

        Debug.Log("setAndRunApproach is being run!");
        StartCoroutine(Approach(AerialTime, target, AerialStopRadius));
    }

    private IEnumerator Approach(float duration, Vector3 targetPosition, float stopRadius)
    {
        if (target == Vector3.zero) {
            Debug.LogWarning("No target selected: Defaulting to target 0, 0, 0.");
        }        

        if (duration <= 0f)
        {
            rb.MovePosition(targetPosition);
            yield break;
        }

        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log(elapsed + " : ms while approaching");
            float t = Mathf.Clamp01(elapsed / duration);
            Vector3 newPos = Vector3.Lerp(startPosition, targetPosition, t);
            rb.MovePosition(newPos);

            // check if unit is within the stop radius.
            if (Vector3.Distance(newPos, targetPosition) <= stopRadius)
            {
                yield break;
            }

            yield return null;
        }

        rb.MovePosition(targetPosition);
    }

    /// <summary>
    /// Called by CombatManager to both enqueue and play the action.
    /// </summary>
    public void PrepareAction(CombatManager.CombatActionType actionType, CharacterCore target)
    {
        CurrentActionType = actionType;
        CurrentTarget     = target;

        // **1) Play motion** on Unit Animator
        switch (actionType)
        {
            case CombatManager.CombatActionType.BasicMelee:
                unitAnimator.Play("Approach");
                poseAnimator.Play("Approaching");
                break;

            case CombatManager.CombatActionType.Ranged:
                // no movement, but swap pose to Shooting
                poseAnimator.Play("Shooting");
                break;

            case CombatManager.CombatActionType.Special:
                // example
                poseAnimator.Play("Slide");
                break;

            default:
                unitAnimator.Play("Idle");
                poseAnimator.Play("Idle");
                break;
        }
    }

    // called by Animation Events
    public void RecordFrameData(EncounterSO.FrameType frameType)
    {
        var cm        = CombatManager.Instance;
        float ts      = Time.time - cm.TurnStartTime;
        cm.CurrentEncounterSO.RecordFrame(
            ts,
            this,
            CurrentActionType,
            CurrentTarget,
            frameType
        );
    }
}

public class TargetingSystem {
    private CharacterCore core;

    public TargetingSystem(CharacterCore core) {
        this.core = core;
    }

    /// <summary>
    /// Holds info about each valid target: its position and faction tag.
    /// </summary>
    public struct TargetInfo {
        public CharacterCore Core;
        public Vector3 Position;
        public CharacterSO.factions Tag;
    }

    /// <summary>
    /// Scans all CharacterCore instances in the scene and tags them Friend/Enemy/Neutral.
    /// Defaults to Neutral if neither friend nor foe.
    /// </summary>
    public List<TargetInfo> GetActiveTargets() {
        var list = new List<TargetInfo>();
        var allCores = Object.FindObjectsByType<CharacterCore>(FindObjectsSortMode.None);
        var myFaction = core.characterData.faction;

        foreach (var other in allCores) {
            if (other == core) continue;  // skip self

            var theirFaction = other.characterData.faction;
            CharacterSO.factions tag;

            // Same non-neutral faction → Friend.
            if (myFaction != CharacterSO.factions.Neutral && theirFaction == myFaction) {
                tag = CharacterSO.factions.Friend;
            }
            // Both non-neutral and different → Enemy.
            else if (myFaction != CharacterSO.factions.Neutral 
                     && theirFaction != myFaction 
                     && theirFaction != CharacterSO.factions.Neutral) {
                tag = CharacterSO.factions.Enemy;
            }
            // Otherwise → Neutral.
            else {
                tag = CharacterSO.factions.Neutral;
            }

            list.Add(new TargetInfo {
                Core     = other,
                Position = other.transform.position,
                Tag      = tag
            });
        }

        return list;
    }
}