using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionObserver : MonoBehaviour {

    public GameEvent finalTurnEndEvent;
    public GameEvent coreDestroyedEvent;
    public HeroEvent heroDeathEvent;

    public GameEvent playerWinEvent, playerLooseEvent;

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
            playerLooseEvent.Raise();

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            Debug.Log("Player teve sucesso em defender núcleo");
            playerWinEvent.Raise();

        }
        PauseGame();

    }

    private void HandleCoreDestroyedVerification() {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            Debug.Log("Player teve sucesso em destruir o núcleo");
            playerWinEvent.Raise();

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            Debug.Log("Player fracassou em defender o núcleo");
            playerLooseEvent.Raise();

        }
        PauseGame();

    }

    private void HandleHeroDeathVerification(RunTimeHeroData heroDead) {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            if (!heroDead.isEnemy) {

                Debug.Log("Player falhou ao comandar o ataque das tropas");
                playerLooseEvent.Raise();

            }

        }
        else if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            if (heroDead.isEnemy) {

                Debug.Log("Player teve sucesso em derrotar o comandante atacante");
                playerWinEvent.Raise();

            }

        }
        PauseGame();

    }

    private void PauseGame() {

        if (Time.timeScale != 0) {

            Time.timeScale = 0;

        }

    }

}
