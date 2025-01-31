using UnityEngine;

[RequireComponent(typeof(MonoBehaviour))]
[RequireComponent(typeof(Animator))]
public class CharacterCore : MonoBehaviour
{
    [Header("Core Configuration")]
    [Tooltip("Assign the Character Scriptable Object to configure this character.")]
    public CharacterSO characterData;

    [Header("Audio Configuration")]
    public AudioClip[] soundClips;

    private Animator animator;
    private AudioSource audioSource;
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

    private bool isInCombat;
    private bool isDead = false; // Added death state flag

    // Character stats
    private int strength, dexterity, constitution, intelligence, wisdom, charisma;

    // Health System
    [Header("Health System")]
    public float maxHealth; // Character's maximum health
    private float currentHealth; // Character's current health

    // **Public Property for Hurtbox Access**
    public float Health => currentHealth;

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

        if (animator == null || audioSource == null || rb == null)
        {
            Debug.LogError("CharacterCore: Missing required components!", this);
            return;
        }

        InitializeCharacter();
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
}
