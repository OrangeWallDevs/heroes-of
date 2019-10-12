using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimePhaseData : MonoBehaviour {

    public int numPhase;
    public int codPart;
    public string namPhase;
    public int valIniPlayerMoney;
    public int valIniIAMoney;
    public PhaseObjectives idtPhaseType;

    public void SetData(Phase phaseData) {

        numPhase = phaseData.NumPhase;
        codPart = phaseData.CodPart;
        namPhase = phaseData.NamPhase;
        valIniPlayerMoney = phaseData.ValIniIAMoney;
        valIniIAMoney = phaseData.ValIniIAMoney;
        
        switch (phaseData.IdtPhaseType) {

            case ("ATTACK"):
                idtPhaseType = PhaseObjectives.ATTACK;
                break;

            case ("DEFEND"):
                idtPhaseType = PhaseObjectives.DEFEND;
                break;

        }

    }

}
