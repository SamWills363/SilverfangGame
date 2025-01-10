using UnityEngine;

[RequireComponent(typeof(MonoBehaviour))]
[RequireComponent(typeof(Animator))]
public class CharacterCore : MonoBehaviour
{
    [Header("Core Configuration")]
    [Tooltip("Assign the Character Scriptable Object to configure this character.")]
    public CharacterSO characterData;

    [Header("Audio Configuration")]
    [Tooltip("List of sounds this character can play.")]
    public AudioClip[] soundClips;

    // Animator reference
    private Animator animator;

    // Audio source reference
    private AudioSource audioSource;

    // Rigidbody reference for movement
    private Rigidbody rb;

    [Header("Movement Configuration")]
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Vector3 movement;
    private bool isGrounded;
    private float lastMoveX = 0f;
    private float lastMoveZ = 0f;

    // State to determine if the character is in combat
    private bool isInCombat;

    // Character stats
    private int strength;
    private int dexterity;
    private int constitution;
    private int intelligence;
    private int wisdom;
    private int charisma;

    // Health system
    private float currentHP;
    private float maxHP;

    private void Awake()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterCore: No CharacterSO assigned! Assign a CharacterSO to configure this character.", this);
            return;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("CharacterCore: Animator component missing! Ensure an Animator is attached.", this);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("CharacterCore: AudioSource component missing! Ensure an AudioSource is attached.", this);
            return;
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("CharacterCore: Rigidbody component missing! Ensure a Rigidbody is attached.", this);
            return;
        }

        InitializeCharacter();
    }

    private void InitializeCharacter()
    {
        Debug.Log($"Initializing character: {characterData.characterName}");

        // Initialize stats from CharacterSO
        strength = characterData.strength;
        dexterity = characterData.dexterity;
        constitution = characterData.constitution;
        intelligence = characterData.intelligence;
        wisdom = characterData.wisdom;
        charisma = characterData.charisma;

        maxHP = characterData.maxHP;
        currentHP = maxHP;

        Debug.Log($"Stats - STR: {strength}, DEX: {dexterity}, CON: {constitution}, INT: {intelligence}, WIS: {wisdom}, CHA: {charisma}, HP: {currentHP}/{maxHP}");
    }

    public int CalculateSkillCheck(string skill)
    {
        switch (skill.ToLower())
        {
            case "athletics":
                return strength + GetModifier(strength);
            case "acrobatics":
            case "sleight of hand":
            case "stealth":
                return dexterity + GetModifier(dexterity);
            case "arcana":
            case "history":
            case "investigation":
            case "nature":
            case "religion":
                return intelligence + GetModifier(intelligence);
            case "animal handling":
            case "insight":
            case "medicine":
            case "perception":
            case "survival":
                return wisdom + GetModifier(wisdom);
            case "deception":
            case "intimidation":
            case "performance":
            case "persuasion":
                return charisma + GetModifier(charisma);
            default:
                Debug.LogWarning($"Unknown skill: {skill}");
                return 0;
        }
    }

    private int GetModifier(int stat)
    {
        return (stat - 10) / 2;
    }

    public void StatChange(AttributeSO[] stats)
    {
        foreach (var stat in stats)
        {
            switch (stat.type)
            {
                case AttributeSO.AttributeType.Stat:
                    ApplyStatModification(stat);
                    break;
                case AttributeSO.AttributeType.Trait:
                    ApplyTraitModification(stat);
                    break;
                case AttributeSO.AttributeType.Condition:
                    ApplyConditionModification(stat);
                    break;
                default:
                    Debug.LogWarning($"Unknown stat type: {stat.type}");
                    break;
            }
        }
    }

    private void ApplyStatModification(AttributeSO stat)
    {
        switch (stat.itemName.ToLower())
        {
            case "strength":
                strength += Mathf.RoundToInt(stat.value);
                break;
            case "dexterity":
                dexterity += Mathf.RoundToInt(stat.value);
                break;
            case "constitution":
                constitution += Mathf.RoundToInt(stat.value);
                break;
            case "intelligence":
                intelligence += Mathf.RoundToInt(stat.value);
                break;
            case "wisdom":
                wisdom += Mathf.RoundToInt(stat.value);
                break;
            case "charisma":
                charisma += Mathf.RoundToInt(stat.value);
                break;
            default:
                Debug.LogWarning($"Unknown stat name: {stat.itemName}");
                break;
        }
    }

    private void ApplyTraitModification(AttributeSO trait)
    {
        Debug.Log($"Applying trait: {trait.itemName} with value {trait.value}");
        // Implement trait-specific logic here
    }

    private void ApplyConditionModification(AttributeSO condition)
    {
        Debug.Log($"Applying condition: {condition.itemName} with value {condition.value}");
        // Implement condition-specific logic here
    }

    private void Update()
    {
        if (isInCombat)
        {
            // Combat-specific movement logic can be added here in the future
            return;
        }

        // Overworld movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Update last movement direction
        if (movement.magnitude > 0)
        {
            lastMoveX = moveX;
            lastMoveZ = moveZ;
        }

        // Animator Parameters
        animator.SetBool("isWalking", movement.magnitude > 0);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveZ", moveZ);

        // Set Idle direction when movement stops
        if (movement.magnitude == 0)
        {
            animator.SetFloat("idleX", lastMoveX);
            animator.SetFloat("idleZ", lastMoveZ);
        }

        // Jump Input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (isInCombat)
        {
            // Combat-specific physics logic can be added here in the future
            return;
        }

        // Apply Movement
        if (movement.magnitude > 0)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public bool IsInCombat()
    {
        return isInCombat;
    }

    public void SetCombatState(bool state)
    {
        isInCombat = state;
        animator.SetBool("IsInCombat", isInCombat);
        Debug.Log($"Character {characterData.characterName} combat state set to: {isInCombat}");
    }

    public void PlaySound(int index)
    {
        if (soundClips == null || soundClips.Length <= index || index < 0)
        {
            Debug.LogError("CharacterCore: Invalid sound index or soundClips not assigned.");
            return;
        }

        audioSource.PlayOneShot(soundClips[index]);
        Debug.Log($"Character {characterData.characterName} played sound: {soundClips[index].name}");
    }

    // Placeholder methods for manipulating character data (to be implemented later):
    // public void SetHealth(float value) { /* Implementation */ }
    // public void ModifySpeed(float modifier) { /* Implementation */ }
    // public void ResetATB() { /* Implementation */ }
}
