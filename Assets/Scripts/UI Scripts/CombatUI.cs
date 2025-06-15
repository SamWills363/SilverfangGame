using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CombatUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private Button meleeButton;
    [SerializeField] private Button rangedButton;

    private CharacterCore currentActor;
    private List<CharacterCore> possibleTargets;
    private Action<CombatManager.CombatActionType, CharacterCore> decisionCallback;

    void Awake()
    {
        actionPanel.SetActive(false);
    }

    void OnEnable()
    {
        // don’t run this in edit-mode
        if (!Application.isPlaying) return;
        CombatManager.Instance.OnPlayerDecisionRequested += OnDecisionRequested;
        meleeButton.onClick.AddListener(OnMeleeClicked);
        rangedButton.onClick.AddListener(OnRangedClicked);
    }

    void OnDisable()
    {
        if (!Application.isPlaying || CombatManager.Instance == null) return;
        CombatManager.Instance.OnPlayerDecisionRequested -= OnDecisionRequested;
        meleeButton.onClick.RemoveListener(OnMeleeClicked);
        rangedButton.onClick.RemoveListener(OnRangedClicked);
    }

    // **Capture the callback instead of ignoring it**
    private void OnDecisionRequested(
        CharacterCore actor,
        List<CharacterCore> targets,
        Action<CombatManager.CombatActionType, CharacterCore> callback)
    {
        Debug.Log($"[UI] OnDecisionRequested: actor={actor.name}, targets={targets.Count}");
        currentActor    = actor;
        possibleTargets = targets;
        decisionCallback = callback;

        actionPanel.SetActive(true);
    }

    public void OnMeleeClicked()
    {
        if (currentActor == null || possibleTargets == null || possibleTargets.Count == 0)
            return;

        // **Invoke the callback** so PlayerDecisionPhase sees choiceMade = true
        decisionCallback?.Invoke(
            CombatManager.CombatActionType.BasicMelee,
            possibleTargets[0]
        );

        actionPanel.SetActive(false);
    }

    public void OnRangedClicked()
    {
        Debug.Log($"[UI] OnRangedClicked: currentActor={(currentActor?.name)}, targets={(possibleTargets?.Count)}");
        if (currentActor == null || possibleTargets == null || possibleTargets.Count == 0)
            return;

        decisionCallback?.Invoke(
            CombatManager.CombatActionType.Ranged,
            possibleTargets[0]
        );
        Debug.Log("[UI] decisionCallback invoked");

        actionPanel.SetActive(false);
    }
}
