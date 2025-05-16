using UnityEngine;

/// <summary>
/// Bridges animation events from this GameObject to the parent CharacterCore
/// </summary>
[RequireComponent(typeof(Animator))]
public class CharacterCoreProxy : MonoBehaviour {
    private CharacterCore core;

    private void Awake() {
        // Find the core up in the hierarchy!!
        core = GetComponentInParent<CharacterCore>();
        if (core == null) {
            Debug.LogError("CharacterCoreProxy: No CharacterCore found in parents!", this);
        }
    }

    // Animation Event–callable methods!!

    /// <summary>
    /// Forwards to CharacterActions.Combat.BasicAttack() on the core
    /// </summary>
    public void BasicAttack() {
        core.Actions.Combat.BasicAttack();
    }

    /// <summary>
    /// Animation Event–callable: forwards to CharacterCore’s MeleeApproach
    /// </summary>
    public void MeleeApproach() {
        core.Actions.Movement.MeleeApproach();
    }

    /// <summary>
    /// Animation Event–callable: Approach the nearest enemy without args
    /// </summary>
    public void ApproachNearestEnemy() {
        core.Actions.Movement.ApproachNearestEnemy();
    }
}
