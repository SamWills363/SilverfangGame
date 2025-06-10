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
    public void OnStartUpEvent()  => core.RecordFrameData(EncounterSO.FrameType.StartUp);
    public void OnActiveEvent()   => core.RecordFrameData(EncounterSO.FrameType.Active);
    public void OnEndLagEvent()   => core.RecordFrameData(EncounterSO.FrameType.EndLag);

    /// <summary>
    /// Forwards to CharacterActions.Combat.BasicAttack() on the core
    /// </summary>
    public void BasicAttack() {
        core.Actions.Combat.BasicMeleeAttack();
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

    /// <summary>
    /// Call this (e.g. via UI click) to pick that GameObject as your ranged target!
    /// </summary>
    public void SelectAttackTarget(GameObject go) {
        var cc = go.GetComponent<CharacterCore>();
        if (cc != null) core.Actions.Combat.SelectAttackTarget(cc);
    }

    /// <summary>
    /// Animation event–callable: fires your next ranged shot at the chosen target!
    /// </summary>
    public void RangedAttack() => core.Actions.Combat.RangedAttack();
}
