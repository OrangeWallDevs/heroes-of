using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDetailsPanelController : MonoBehaviour {

    public Sprite idtAttackPhaseImage;
    public Sprite idtDephensePhaseImage;
    public TextMeshProUGUI phaseTitleLabel;

    public TextMeshProUGUI userMaxScoreLabel;

    public Image phaseTypeImage;
    public GameRuntimeData gameRuntimeData;

    void Start() {

    }

    public void ShowPhaseDetails(int phaseIndex) {
        Phase selectedPhase = gameRuntimeData.Phases[phaseIndex - 1];

        if (!(selectedPhase is null)) {
            int userHighestScore = 0;

            if(!(selectedPhase.UserScore is null)) 
                userHighestScore = selectedPhase.UserScore.ValRecordPoints;

            phaseTitleLabel.text = selectedPhase.NamPhase;
            userMaxScoreLabel.text = userHighestScore.ToString();

            if (selectedPhase.IdtPhaseType.ToLower() == "defense") {
                phaseTypeImage.sprite = idtDephensePhaseImage;
            } else {
                phaseTypeImage.sprite = idtAttackPhaseImage;
            }
        }
    }
}