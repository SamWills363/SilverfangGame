using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterSO", menuName = "Scriptable Objects/EncounterSO")]
public class EncounterSO : ScriptableObject
{
    [Header("Turn Configuration")]
    [Tooltip("Length of this combat turn in seconds.")]
    [SerializeField] private float turnLength = 6f;

    [Tooltip("Turn index.")]
    [SerializeField] private int turnNumber;

    [Header("Combatants (ordered by initiative)")]
    [SerializeField] private List<CharacterCore> combatants = new List<CharacterCore>();

    [Header("Recorded Action Rail (timeline of animation frames)")]
    [SerializeField] private List<AnimFrame> timeline = new List<AnimFrame>();

    [System.Serializable]
    public enum FrameType { StartUp, Active, EndLag }

    [System.Serializable]
    public class AnimFrame
    {
        [Tooltip("Timestamp within the turn (seconds).")]
        public float timestamp;

        [Tooltip("Actor executing this frame.")]
        public CharacterCore actor;

        [Tooltip("Type of action being performed.")]
        public CombatManager.CombatActionType actionType;

        [Tooltip("Target of the action (if any).")]
        public CharacterCore target;

        [Tooltip("Which segment of the animation this frame represents.")]
        public FrameType frameType;
    }

    public float TurnLength     => turnLength;
    public int   TurnNumber     => turnNumber;
    public IReadOnlyList<CharacterCore> Combatants => combatants;
    public IReadOnlyList<AnimFrame>      Timeline    => timeline;

    /// <summary>
    /// Prepares this SO for a new turn: assigns number, orders participants by initiative, and clears old frames.
    /// </summary>
    public void Initialize(int turnNum, IEnumerable<CharacterCore> participants)
    {
        turnNumber   = turnNum;
        combatants   = participants
                       .OrderByDescending(c => c.characterData.speed)
                       .ToList();
        timeline.Clear();
    }

    /// <summary>
    /// Appends a new animation‐frame entry into the timeline.
    /// </summary>
    public void RecordFrame(float timestamp,
                            CharacterCore actor,
                            CombatManager.CombatActionType actionType,
                            CharacterCore target,
                            FrameType frameType)
    {
        timeline.Add(new AnimFrame {
            timestamp  = timestamp,
            actor      = actor,
            actionType = actionType,
            target     = target,
            frameType  = frameType
        });
    }

    /// <summary>
    /// Clears all recorded frames (e.g. at end of turn).
    /// </summary>
    public void ClearTimeline()
    {
        timeline.Clear();
    }
}
