using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionObserver : MonoBehaviour {

    public GameEvent finalTurnEndEvent;
    public GameEvent coreDestroyedEvent;
    public HeroEvent heroDeathEvent;

    private RunTimePhaseData phaseData;

    void Start() {

        phaseData = GetComponent<RunTimePhaseData>();

        heroDeathEvent.RegisterListener(HandleHeroDeathVerification);
        finalTurnEndEvent.RegisterListener(HandleFinalTurnVerification);
        coreDestroyedEvent.RegisterListener(HandleCoreDestroyedVerification);

    }

    private void HandleFinalTurnVerification() {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            Debug.Log("Player não destruiu núcleo a tempo");

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            Debug.Log("Player teve sucesso em defender núcleo");

        }

    }

    private void HandleCoreDestroyedVerification() {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            Debug.Log("Player teve sucesso em destruir o núcleo");

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            Debug.Log("Player fracassou em defender o núcleo");

        }

    }

    private void HandleHeroDeathVerification(RunTimeHeroData heroDead) {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            if (!heroDead.isEnemy) {

                Debug.Log("Player falhou ao comandar o ataque das tropas");

            }

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            if (heroDead.isEnemy) {

                Debug.Log("Player teve sucesso em derrotar o comandante atacante");

            }

        }

    }

}
