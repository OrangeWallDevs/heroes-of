using TMPro;
using UnityEngine;

public class ConclusionScreenManager : MonoBehaviour {

    public GameObject conclusionScreen;
    public TextMeshProUGUI screenTitle;

    public GameObject heroSurvivedLabel;
    public GameObject destroyedTowerLabel;
    public GameObject defendedTowersLabel;

    public GameEvent playerWinEvent, playerLooseEvent;

    private RunTimePhaseData phaseData;

    private void Awake() {

        GameObject phaseManager = GameObject.FindGameObjectWithTag("Phase_Manager");
        phaseData = phaseManager.GetComponent<RunTimePhaseData>();

    }

    private void Start() {

        playerWinEvent.RegisterListener(OpenWinScreen);
        playerLooseEvent.RegisterListener(OpenDefeatScreen);

    }

    public void OpenWinScreen() {

        conclusionScreen.SetActive(true);

        AdaptConclusionScreen();

    }

    public void OpenDefeatScreen() {

        conclusionScreen.SetActive(true);
        screenTitle.text = "Derrota";

        AdaptConclusionScreen();

    }

    private void AdaptConclusionScreen() {

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            AttackPhaseConclusionScreen();

        }
        else {

            DefensePhaseConclusionScreen();

        }

    }

    private void AttackPhaseConclusionScreen() {

        heroSurvivedLabel.SetActive(false);
        defendedTowersLabel.SetActive(false);
        destroyedTowerLabel.SetActive(true);

    }

    private void DefensePhaseConclusionScreen() {

        heroSurvivedLabel.SetActive(true);
        defendedTowersLabel.SetActive(true);
        destroyedTowerLabel.SetActive(false);

    }

}
