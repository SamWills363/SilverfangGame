using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CombatStateEvent : UnityEvent<CombatState> { }

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    // ← Add this delegate and event to fire when a player decision is needed
    public delegate void PlayerDecisionHandler(CharacterCore actor, List<CharacterCore> possibleTargets, Action<CombatActionType, CharacterCore> decisionCallback);
    public event PlayerDecisionHandler OnPlayerDecisionRequested;

    [Header("Encounter Recording")]
    [Tooltip("Drag your EncounterSO asset here")]
    [SerializeField] private EncounterSO encounterSO;

    public EncounterSO CurrentEncounterSO => encounterSO;
    public float TurnStartTime        { get; private set; }

    [Header("Phase Change Event")]
    public CombatStateEvent OnStateChanged;

    public CombatState currentState { get; private set; }

    private List<CharacterCore> players     = new();
    private List<CharacterCore> enemies     = new();
    private List<CombatActionRecord> actionQueue = new();
    private int turnCounter = 0;

    public enum CombatActionType { BasicMelee, Ranged, Special, Ultimate }

    private class CombatActionRecord
    {
        public float timestamp;
        public CharacterCore actor;
        public CombatActionType type;
        public CharacterCore target;
        public CombatActionRecord(float ts, CharacterCore actor, CombatActionType t, CharacterCore target)
        {
            timestamp = ts; this.actor = actor; type = t; this.target = target;
        }
    }

    void Awake()
    {
        Instance = this;
        // ← Disambiguate Object: use GameObject.FindObjectsByType instead of Object.FindObjectsByType
        var all = GameObject.FindObjectsByType<CharacterCore>(FindObjectsSortMode.None);
        players = all.Where(c => c.Faction == CharacterSO.factions.Friend).ToList();
        enemies = all.Where(c => c.Faction == CharacterSO.factions.Enemy).ToList();
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(SceneInitiation());

        while (true)
        {
            turnCounter++;

            encounterSO.Initialize(turnCounter, players.Concat(enemies));

            ChangeState(CombatState.EnemyPhase);
            yield return StartCoroutine(EnemyDecisionPhase());

            ChangeState(CombatState.EnemyRevealPhase);
            yield return StartCoroutine(EnemyRevealPhase());

            ChangeState(CombatState.PlayerPhase);
            yield return StartCoroutine(PlayerDecisionPhase());

            ChangeState(CombatState.RecordPhase);
            ChangeState(CombatState.ActionPlayPhase);

            TurnStartTime = Time.time;
            yield return StartCoroutine(PlayActions());

            Debug.Log($"[Encounter] Turn {turnCounter} recorded {encounterSO.Timeline.Count} frames.");

            if (CheckWinLoss()) yield break;
            actionQueue.Clear();
        }
    }

    private void ChangeState(CombatState next)
    {
        currentState = next;
        OnStateChanged?.Invoke(currentState);
        Debug.Log($"→ [CombatManager] State changed to {currentState}");
    }

    private IEnumerator SceneInitiation()
    {
        yield return null;
    }

    private IEnumerator EnemyDecisionPhase()
    {
        foreach (var e in enemies.Where(e => !e.isDead))
        {
            var target = players.Where(p => !p.isDead)
                                .OrderBy(p => Vector3.Distance(e.transform.position, p.transform.position))
                                .FirstOrDefault();
            if (target != null)
                actionQueue.Add(new CombatActionRecord(Time.time, e, CombatActionType.BasicMelee, target));

            yield return null;
        }
    }

    private IEnumerator EnemyRevealPhase()
    {
        foreach (var rec in actionQueue.Where(a => a.actor.Faction == CharacterSO.factions.Enemy))
            Debug.Log($"[Reveal] {rec.actor.characterData.characterName} will {rec.type} → {rec.target.characterData.characterName}");

        // you can hook this up to a UI button instead of a fixed delay
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator PlayerDecisionPhase()
    {
        foreach (var p in players.Where(p => !p.isDead))
        {
            bool choiceMade = false;
            CombatActionType chosenType = CombatActionType.BasicMelee;
            CharacterCore chosenTarget = null;

            // ← This event now exists and can be subscribed by your UI
            OnPlayerDecisionRequested?.Invoke(
                p,
                enemies.Where(e => !e.isDead).ToList(),
                (type, target) =>
                {
                    chosenType   = type;
                    chosenTarget = target;
                    choiceMade   = true;
                }
            );

            while (!choiceMade)
                yield return null;

            actionQueue.Add(new CombatActionRecord(Time.time, p, chosenType, chosenTarget));
            Debug.Log($"[Player] {p.characterData.characterName} → {chosenType} → {chosenTarget.characterData.characterName}");
        }
    }

    private IEnumerator PlayActions()
    {
        // play in the order they were chosen
        var ordered = actionQueue.OrderBy(a => a.timestamp).ToList();

        foreach (var rec in ordered)
        {
            // this will trigger your Animator and fire the Animation Events
            rec.actor.PrepareAction(rec.type, rec.target);

            // find the clip by name and wait its real duration
            var anim      = rec.actor.GetComponent<Animator>();
            var clip      = anim.runtimeAnimatorController
                               .animationClips
                               .First(c => c.name == rec.type.ToString());
            yield return new WaitForSeconds(clip.length);
        }
    }

    private bool CheckWinLoss()
    {
        if (enemies.All(e => e.isDead))
        {
            ChangeState(CombatState.Win);
            return true;
        }
        if (players.All(p => p.isDead))
        {
            ChangeState(CombatState.Lost);
            return true;
        }
        return false;
    }

    // ← keep your OnPlayerDecisionRequested event & CombatState enum below…
}
