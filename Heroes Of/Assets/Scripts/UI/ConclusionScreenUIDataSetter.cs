using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConclusionScreenUIDataSetter : MonoBehaviour {

    public TextMeshProUGUI defeatedTroops;
    public TextMeshProUGUI survivedTroops;
    public TextMeshProUGUI towerDestroyed;
    public TextMeshProUGUI towerDefended;
    public TextMeshProUGUI oponentHeroDefeated;
    public TextMeshProUGUI survivedHero;
    public TextMeshProUGUI goldBonus;
    public TextMeshProUGUI totalScore;

    private ScoreRecorder scoreRecorder;
    private RunTimePhaseData phaseData;

    private void Awake() {

        GameObject phaseManager = GameObject.FindGameObjectWithTag("Phase_Manager");
        scoreRecorder = phaseManager.GetComponent<ScoreRecorder>();
        phaseData = phaseManager.GetComponent<RunTimePhaseData>();

    }

    private void OnEnable() {

        defeatedTroops.text = scoreRecorder.TroopsKilledScore.ToString();
        survivedTroops.text = scoreRecorder.TroopsSurvivedScore.ToString();
        oponentHeroDefeated.text = scoreRecorder.HeroKilledScore.ToString();
        goldBonus.text = scoreRecorder.GoldLeftScore.ToString();
        totalScore.text = scoreRecorder.TotalScore.ToString();

        if (phaseData.idtPhaseType == PhaseObjectives.ATTACK) {

            towerDestroyed.text = scoreRecorder.TowersDestroyedScore.ToString();

        }
        else {

            towerDefended.text = scoreRecorder.TowersProtectedScore.ToString();
            survivedHero.text = scoreRecorder.HeroSurvivedScore.ToString();

        }

    }

}
