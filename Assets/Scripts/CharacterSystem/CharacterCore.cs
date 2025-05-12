using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonoBehaviour))]
[RequireComponent(typeof(Animator))]
public class CharacterCore : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// 
    /// 
    /// todo: Make documentation for resuse by foreign parties

    /* ----------------------------- Public Section ----------------------------- */
    [Header("Core Configuration")]
    [Tooltip("Assign the Character Scriptable Object to configure this character.")]
    public CharacterSO characterData;

    [Header("Audio Configuration")]
    [Tooltip("These are the sounds that will be played by the character and their animations.")]
    public AudioClip[] soundClips;


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

    /*const actions = {
        attack: {
            approaches: MeleeApproach() => {}
        }
    }*/



    /* ----------------------------- Private Section ---------------------------- */
    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody rb;

    private Vector3 movement;
    private bool isGrounded;
    private float lastMoveX = 0f;
    private float lastMoveZ = 0f;

    private bool isInCombat;
    public bool isDead = false; // Added death state flag

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

        /*
        if (animator == null || audioSource == null || rb == null)
        {
            Debug.LogError("CharacterCore: Missing required components!", this);
            return;
        }
        */
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

    public void setTarget(Transform targetObject) {
        if (targetObject == null) return;
        target = targetObject.position;
    }

    public void MeleeApproach() {

        float meleeTime = 1;
        float meleeStopRadius = 5;

        Debug.Log("setAndRunApproach is being run!");
        StartCoroutine(Approach(meleeTime, target, meleeStopRadius));
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
}